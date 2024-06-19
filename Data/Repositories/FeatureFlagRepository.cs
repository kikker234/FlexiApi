using Data.Models;

namespace Data.Repositories;

public class FeatureFlagRepository : ICrudRepository<FeatureFlag>
{
    private readonly FlexiContext _context;
    
    public FeatureFlagRepository(FlexiContext context)
    {
        _context = context;
    }
    
    public bool Create(FeatureFlag t)
    {
        try
        {
            _context.FeatureFlags.Add(t);
            _context.SaveChanges();
            return true;
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public FeatureFlag? Read(int id)
    {
        return _context.FeatureFlags.Find(id);
    }

    public IEnumerable<FeatureFlag> ReadAll()
    {
        return _context.FeatureFlags.ToList();
    }

    public IEnumerable<FeatureFlag> ReadByInstance(Instance instance)
    {
        return _context.FeatureFlags.Where(f => f.InstanceId == instance.Id).ToList();
    }

    public bool Update(FeatureFlag t)
    {
        try
        {
            _context.FeatureFlags.Update(t);
            _context.SaveChanges();
            return true;
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(FeatureFlag featureFlag)
    {
        try
        {
            _context.FeatureFlags.Remove(featureFlag);
            _context.SaveChanges();
            return true;
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            FeatureFlag? flag = Read(id);

            if (flag == null) return false;
            
            _context.FeatureFlags.Remove(flag);
            _context.SaveChanges();
            return true;
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}