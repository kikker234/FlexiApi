using Data.Models;

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

    public Customer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Customer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public bool Update(Customer t)
    {
        try
        {
            Console.WriteLine("Updating customer with id: " + t.Id + "...");
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
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Customer> GetAll(int userOrganizationId)
    {
        return _context.Customers.Where(c => c.OrganizationId == userOrganizationId).ToList();
    }
}