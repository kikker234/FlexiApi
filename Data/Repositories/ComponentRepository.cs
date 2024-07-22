using Data.Models.components;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ComponentRepository : IComponentRepository
{
    private readonly DbContext _context;
    
    public ComponentRepository(DbContext context)
    {
        _context = context;
    }
    
    public bool Create(Component component)
    {
        try
        {
            _context.Add(component);
            return _context.SaveChanges() > 0;
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Component? Read(int id)
    {
        try
        {
            Component? component = _context.Find<Component>(id);
            
            return component;
        } 
        catch
        {
            return null;
        }
    }

    public IEnumerable<Component> ReadAll()
    {
        try
        {
            return _context.Set<Component>().ToList();
        }
        catch
        {
            return new List<Component>();
        }
    }
    

    public bool Delete(Component component)
    {
        return Delete(component.Id);
    }

    public bool Delete(int id)
    {
        try
        {
            Component? component = Read(id);
            return _context.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }


    public bool Update(Component existingComponent)
    {
        try 
        {
            _context.Update(existingComponent);
            return _context.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public Component? ReadByType(string type, int instanceId)
    {
        try
        {
            return _context.Set<Component>()
                .Include(c => c.Fields)
                .FirstOrDefault(c => c.Name == type && c.InstanceId == instanceId);
        }
        catch
        {
            return null;
        }
    }
}