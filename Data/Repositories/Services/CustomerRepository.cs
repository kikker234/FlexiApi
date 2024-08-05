using Data.Models;
using Data.Utils;

namespace Data.Repositories;

public class CustomerRepository : ICrudRepository<Customer>
{
    private FlexiContext _context;
    
    public CustomerRepository(FlexiContext context)
    {
        _context = context;
    }
    
    public bool Create(Customer t)
    {
        try
        {
            _context.Customers.Add(t);
            return _context.SaveChanges() > 0;
        } 
        catch (Exception e)
        {
            return false;
        }
    }

    public Optional<Customer> Read(int id)
    {
        return Optional<Customer>.Of(_context.Customers.Find(id));
    }

    public IEnumerable<Customer> ReadAll()
    {
        return _context.Customers;
    }

    public bool Update(Customer t)
    {
        try
        {
            _context.Customers.Update(t);
            return _context.SaveChanges() > 0;
        } 
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Delete(Customer t)
    {
        try
        {
            _context.Customers.Remove(t);
            return _context.SaveChanges() > 0;
        } 
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            return _context.SaveChanges() > 0;
        } 
        catch (Exception e)
        {
            return false;
        }
    }

    public IEnumerable<Customer> GetAll(int userOrganizationId)
    {
        return _context.Customers.Where(c => c.OrganizationId == userOrganizationId).ToList();
    }
}