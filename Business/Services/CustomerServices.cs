using Data.Models;
using Data.Repositories;

namespace Business;

public class CustomerServices
{
    private CustomerRepository _customerRepository;
    
    public CustomerServices(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public IEnumerable<Customer> GetCustomers(User user)
    {
        return _customerRepository.GetAll(user.OrganizationId);
    }

    public bool CreateCustomer(User user, Customer customer)
    {
        customer.OrganizationId = user.OrganizationId;
        customer.CreatedAt = DateTime.Now;
        customer.CreatedBy = user.Id;
        
        return _customerRepository.Create(customer);
    }

    public bool UpdateCustomer(User user, Customer customer)
    {
        customer.OrganizationId = user.OrganizationId;
        customer.UpdatedAt = DateTime.Now;
        customer.UpdatedBy = user.Id;
        
        return _customerRepository.Update(customer);
    }
}