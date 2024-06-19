using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class InstanceRepository : ICrudRepository<Instance>
{
    private FlexiContext _context;

    public InstanceRepository(FlexiContext context)
    {
        _context = context;
    }

    public bool Create(Instance t)
    {
        try
        {
            _context.Instances.Add(t);
            return _context.SaveChanges() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Instance? Read(int id)
    {
        return _context.Instances.Find(id);
    }

    public IEnumerable<Instance> ReadAll()
    {
        return _context.Instances;
    }

    public bool Update(Instance t)
    {
        try
        {
            _context.Instances.Update(t);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(Instance t)
    {
        try
        {
            _context.Instances.Remove(t);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            Instance? instance = Read(id);
            if (instance != null)
            {
                _context.Instances.Remove(instance);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}