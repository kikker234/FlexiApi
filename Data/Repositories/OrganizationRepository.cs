using Data.Models;

namespace Data.Repositories;

public class OrganizationRepository : ICrudRepository<Organization>
{
    private FlexiContext _context;

    public OrganizationRepository(FlexiContext context)
    {
        _context = context;
    }

    public bool Create(Organization t)
    {
        try
        {
            _context.Organizations.Add(t);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Organization? Read(int id)
    {
        return _context.Organizations.Find(id);
    }

    public IEnumerable<Organization> ReadAll()
    {
        return _context.Organizations.Where(o => o.DeletedAt == null);
    }

    public bool Update(Organization t)
    {
        try
        {
            _context.Organizations.Update(t);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(Organization organization)
    {
        try
        {
            _context.Organizations.Remove(organization);
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
            var organization = _context.Organizations.Find(id);

            if (organization == null)
            {
                return false;
            }

            organization.DeletedAt = DateTime.Now;
            return Update(organization);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}