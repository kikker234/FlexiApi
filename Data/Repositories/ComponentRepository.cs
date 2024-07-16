using Data.Models.components;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ComponentRepository
{
    private readonly FlexiContext _context;
    
    public ComponentRepository(FlexiContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Component> GetByType(string type, int organizationId)
    {
        return _context.Components
            .Where(c => c.Type == type && c.OrganizationId == organizationId)
            .Include(c => c.CustomComponentFields);
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
}