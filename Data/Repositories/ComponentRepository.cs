using Data.Models.components;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ComponentRepository : IComponentRepository
{
    private readonly FlexiContext _context;
    
    public ComponentRepository(FlexiContext context)
    {
        _context = context;
    }

    public IEnumerable<Component> GetComponents()
    {
        return _context.Components
            .Include(component => component.Fields)
            .ThenInclude(field => field.Validations);
    } 
    
    public IEnumerable<Component> GetByType(string type, int organizationId)
    {
        return new List<Component>();
    }
    
    public bool Create(Component component)
    {
        try
        {
            _context.Components.Add(component);
            return _context.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(Component component)
    {
        try
        {
            _context.Components.Remove(component);
            return _context.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public void Update(Component existingComponent)
    {
        try 
        {
            _context.Components.Update(existingComponent);
            _context.SaveChanges();
        }
        catch
        {
        }
    }
}