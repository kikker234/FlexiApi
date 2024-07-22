using Data.Models;
using Data.Models.components;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ComponentDataRepository : IComponentDataRepository
{
    private readonly FlexiContext _context;

    public ComponentDataRepository(FlexiContext context)
    {
        _context = context;
    }

    public IList<ComponentObject> ReadAll(Instance instance, string type)
    {
        return _context.ComponentObjects
            .Include(obj => obj.Data)
            .ThenInclude(data => data.ComponentField)
            .ToList();
    }

    public bool Create(ComponentObject componentObject)
    {
        try
        {
            _context.ComponentObjects.Add(componentObject);
            return _context.SaveChanges() > 0;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to create component object", e);
        }
    }

    public ComponentObject? ReadById(int id)
    {
        return _context.ComponentObjects
            .Include(obj => obj.Data)
            .ThenInclude(data => data.ComponentField)
            .Include(obj => obj.Component)
            .ThenInclude(component => component.Fields)
            .FirstOrDefault(obj => obj.Id == id);
    }

    public bool Update(ComponentObject componentObject)
    {
        try
        {
            _context.ComponentObjects.Update(componentObject);
            return _context.SaveChanges() > 0;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to update component object", e);
        }
    }
}