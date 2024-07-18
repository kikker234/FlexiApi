using Business.Entity;
using Business.Entity.adapter;
using Business.Entity.validators;
using Data.Models.components;
using Data.Repositories;
using Newtonsoft.Json.Linq;

namespace Business.Services;

public class EntityServices
{
    private readonly IComponentRepository _componentRepository;
    
    public EntityServices(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }
    
    public bool Import(JObject json)
    {
        AbstractHandler jsonNullValidator = new RootValidator();
        jsonNullValidator
            .SetNext(new ComponentValidator())
            .SetNext(new FieldValidator())
            .SetNext(new ValidationValidator());

        if (jsonNullValidator.Handle(json) == null) throw new Exception("Entity map is not valid!");

        // convert to Component;
        JsonComponentAdapter adapter = new JsonComponentAdapter();
        IEnumerable<Component> inputComponents = adapter.Convert(json).ToList();
        IEnumerable<Component> existingComponents = _componentRepository.GetComponents().ToList();
        
        // Process components
        AddNewComponents(inputComponents, existingComponents);
        DeleteOldComponents(inputComponents, existingComponents);
        UpdateExistingComponents(inputComponents, existingComponents);

        return true;
    }

    private void AddNewComponents(IEnumerable<Component> inputComponents, IEnumerable<Component> existingComponents)
    {
        var existingComponentNames = existingComponents.Select(c => c.Name).ToHashSet();

        foreach (var inputComponent in inputComponents)
        {
            if (!existingComponentNames.Contains(inputComponent.Name))
            {
                _componentRepository.Create(inputComponent);
            }
        }
    }

    private void DeleteOldComponents(IEnumerable<Component> inputComponents, IEnumerable<Component> existingComponents)
    {
        var inputComponentNames = inputComponents.Select(c => c.Name).ToHashSet();

        foreach (var existingComponent in existingComponents)
        {
            if (!inputComponentNames.Contains(existingComponent.Name))
            {
                _componentRepository.Delete(existingComponent);
            }
        }
    }

    private void UpdateExistingComponents(IEnumerable<Component> inputComponents, IEnumerable<Component> existingComponents)
    {
        var inputComponentDict = inputComponents.ToDictionary(c => c.Name);

        foreach (var existingComponent in existingComponents)
        {
            if (inputComponentDict.TryGetValue(existingComponent.Name, out var inputComponent))
            {
                existingComponent.Fields = inputComponent.Fields;
                _componentRepository.Update(existingComponent);
            }
        }
    }
}