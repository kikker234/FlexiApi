using Data.Models;

namespace Data.Repositories;

public class OrganizationRepository : ICrudRepository<Organization>
{
    private readonly FlexiContext _context;
    
    public OrganizationRepository(FlexiContext context)
    {
        _context = context;
    }
    
    public bool Create(Organization t)
    {
        try
        {
            _context.Organisations.Add(t);
            return _context.SaveChanges() > 0;
        } catch
        {
            return false;
        }
    }

    public Organization? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Organization> ReadAll()
    {
        throw new NotImplementedException();
    }

    public bool Update(Organization t)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Organization t)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}