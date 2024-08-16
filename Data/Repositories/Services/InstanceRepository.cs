using Data.Models;
using Data.Utils;
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

    public Optional<Instance> Read(int id)
    {
        return Optional<Instance>.Of(_context.Instances.Find(id));
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
            Optional<Instance> instance = Read(id);
            if (instance.IsEmpty())
                return false;
            
            _context.Instances.Remove(instance.GetValue());
            return _context.SaveChanges() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Instance? GetByKey(string instanceKey)
    {
        return _context.Instances.FirstOrDefault(i => i.Key == instanceKey);
    }
}