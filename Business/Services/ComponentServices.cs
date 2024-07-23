using System.Collections;
using Business.Field;
using Business.Field.factory;
using BusinessTest.Exceptions;
using Data.Models;
using Data.Models.components;
using Data.Repositories;

namespace Business.Services;

public class ComponentServices
{
    private readonly IComponentDataRepository _componentDataRepository;
    private readonly IComponentRepository _componentRepository;

    public ComponentServices(IComponentRepository componentRepository, IComponentDataRepository componentDataRepository)
    {
        _componentRepository = componentRepository;
        _componentDataRepository = componentDataRepository;
    }

    public bool Create(User user, Instance instance, string type, Dictionary<string, string> data)
    {
        Component? component = _componentRepository.ReadByType(type, instance.Id);

        if (component == null)
            throw new ComponentNotFoundException(type);
        
        IsValidComponentObject(data, component);

        ComponentObject componentObject = new ComponentObject
        {
            ComponentId = component.Id,
            CreatedById = user.Id,
            CreatedAt = DateTime.Now,
            UpdatedById = user.Id,
            UpdatedAt = DateTime.Now,
            Data = new List<ComponentData>()
        };

        foreach (KeyValuePair<string, string> entry in data)
        {
            ComponentField? field = component.Fields.FirstOrDefault(f => f.Key == entry.Key)
                                    ?? throw new Exception("Field not found: " + entry.Key);

            ComponentData componentData = new ComponentData
            {
                ComponentFieldId = field.Id,
                value = entry.Value,
            };

            componentObject.Data.Add(componentData);
        }

        return _componentDataRepository.Create(componentObject);
    }

    public IEnumerable<ComponentObject> GetComponents(string type, Instance instance)
    {
        return _componentDataRepository.ReadAll(instance, type);
    }

    public bool Update(User user, Dictionary<string, string> data, int id)
    {
        ComponentObject? componentObject = _componentDataRepository.ReadById(id);
        if (componentObject == null)
            throw new ComponentNotFoundException(id);

        foreach (KeyValuePair<string, string> entry in data)
        {
            ComponentField? field = componentObject.Component.Fields.FirstOrDefault(f => f.Key == entry.Key);

            if (field == null)
                throw new Exception("Field not found: " + entry.Key);

            if (!IsValidField(field, data[field.Key]))
                throw new Exception("Invalid data for field: " + field.Key);

            ComponentData? componentData = componentObject.Data.FirstOrDefault(d => d.ComponentFieldId == field.Id);

            if (componentData == null)
                throw new Exception("Data not found for field: " + entry.Key);

            componentData.value = entry.Value;
        }

        componentObject.UpdatedAt = DateTime.Now;
        componentObject.UpdatedById = user.Id;

        return _componentDataRepository.Update(componentObject);
    }

    public bool Delete(User user, int id)
    {
        ComponentObject? componentObject = _componentDataRepository.ReadById(id);

        if (componentObject == null)
            throw new Exception("Component not found");

        componentObject.DeletedAt = DateTime.Now;
        componentObject.DeletedById = user.Id;

        return _componentDataRepository.Update(componentObject);
    }

    public ComponentObject? GetComponent(int id)
    {
        return _componentDataRepository.ReadById(id);
    }


    private bool IsValidComponentObject(Dictionary<string, string> data, Component component)
    {
        foreach (ComponentField field in component.Fields)
        {
            if(!IsValidField(field, data[field.Key]))
                throw new InvalidFieldDataException(field.Key);
        }

        return true;
    }

    private bool IsValidField(ComponentField field, string value)
    {
        IFieldValidator validator = new ValidationFactory().CreateValidator(field);

        if (!validator.ValidateField(value))
            return false;

        return true;
    }
}