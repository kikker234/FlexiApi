using Business.Entity;
using Business.Entity.adapter;
using Business.Entity.validators;
using Data.Models;
using Data.Models.components;
using Data.Repositories;
using FluentResults;
using Newtonsoft.Json.Linq;

namespace Business.Services;

public class EntityServices
{
    private readonly IComponentRepository _componentRepository;

    public EntityServices(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    public Result Import(JObject json, Instance instance)
    {
        AbstractHandler jsonNullValidator = new RootValidator();
        jsonNullValidator
            .SetNext(new ComponentValidator())
            .SetNext(new FieldValidator())
            .SetNext(new ValidationValidator());

        if (jsonNullValidator.Handle(json) == null)
            return Result.Fail("Entity map is not valid");

        // convert to Component;
        JsonComponentAdapter adapter = new JsonComponentAdapter();
        IEnumerable<Component> inputComponents = adapter.Convert(json).ToList();
        IEnumerable<Component> existingComponents = _componentRepository.ReadAll().ToList();
        
        Result addResult = AddNewComponents(inputComponents, existingComponents, instance); 
        Result deleteResult = DeleteOldComponents(inputComponents, existingComponents);
        Result updateResult = UpdateExistingComponents(inputComponents, existingComponents);

        return Result.Merge(addResult, deleteResult, updateResult);
    }

    private Result AddNewComponents(IEnumerable<Component> inputComponents, IEnumerable<Component> existingComponents, Instance instance)
    {
        var existingComponentNames = existingComponents.Select(c => c.Name).ToHashSet();

        bool success = true;
        Component? failureComponent = null;
        foreach (var inputComponent in inputComponents)
        {
            if (!existingComponentNames.Contains(inputComponent.Name))
            {
                inputComponent.InstanceId = instance.Id;
                bool currentSuccess = _componentRepository.Create(inputComponent);
                
                if (!currentSuccess)
                {
                    success = false;
                    failureComponent = inputComponent;
                    break;
                }
            }
        }
        
        return Result.OkIf(success, "Failed to add component: " + failureComponent?.Name);
    }

    private Result DeleteOldComponents(IEnumerable<Component> inputComponents, IEnumerable<Component> existingComponents)
    {
        var inputComponentNames = inputComponents.Select(c => c.Name).ToHashSet();

        bool success = true;
        Component? failureComponent = null;
        foreach (var existingComponent in existingComponents)
        {
            if (!inputComponentNames.Contains(existingComponent.Name))
            {
                bool currentSuccess = _componentRepository.Delete(existingComponent);
                
                if (!currentSuccess)
                {
                    success = false;
                    failureComponent = existingComponent;
                    break;
                }
            }
        }
        
        return Result.OkIf(success, "Failed to delete component: " + failureComponent?.Name);
    }

    private Result UpdateExistingComponents(IEnumerable<Component> inputComponents,
        IEnumerable<Component> existingComponents)
    {
        var inputComponentDict = inputComponents.ToDictionary(c => c.Name);

        bool success = true;
        Component? failureComponent = null;
        foreach (var existingComponent in existingComponents)
        {
            if (inputComponentDict.TryGetValue(existingComponent.Name, out var inputComponent))
            {
                existingComponent.Fields = inputComponent.Fields;
                bool currentSuccess = _componentRepository.Update(existingComponent);

                if (!currentSuccess)
                {
                    success = false;
                    failureComponent = existingComponent;
                    break;
                }
            }
        }

        return Result.OkIf(success, "Failed to update component: " + failureComponent?.Name);
    }
}