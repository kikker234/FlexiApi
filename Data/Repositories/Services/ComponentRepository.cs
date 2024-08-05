using Data.Models;
using Data.Models.components;
using Data.Utils;
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

    public Optional<Component> Read(int id)
    {
        try
        {
            Component? component = _context.Find<Component>(id);
            
            return Optional<Component>.Of(component);
        } 
        catch
        {
            return Optional<Component>.Empty();
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
        try
        {
            _context.Remove(component);
            return _context.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            Optional<Component> component = Read(id);
            
            if (component.IsEmpty())
                return false;

            return Delete(component.GetValue());
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