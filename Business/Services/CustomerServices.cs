using Data.Models;

namespace Business;

public class CustomerServices
{
    public IEnumerable<Customer> GetCustomers()
    {
        List<Customer> customers = new()
        {
            new Customer
            {
                Name = "John Doe",
            },
            new Customer
            {
                Name = "Jane Doe",
            },
            new Customer
            {
                Name = "Tiffie Toffie",
            },
        };
        
        return customers;
    }
}