using Auth;
using Auth.Attributes;
using Business;
using Data.Models;
using FlexiApi.Attributes;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerServices _customerServices;
    private readonly IAuthManager _authManager;

    public CustomerController(CustomerServices customerServices, IAuthManager authManager)
    {
        _customerServices = customerServices;
        _authManager = authManager;
    }

    [HttpGet]
    [Authorize]
    [Route("/api/customers")]
    public IActionResult GetCustomers()
    {
        try
        {
            User? user = _authManager.GetLoggedInUser(HttpContext);
            
            if(user == null) 
                throw new Exception("Could not load customers!");
            
            return Ok(ApiResponse<IEnumerable<Customer>>.Success(_customerServices.GetCustomers(user)));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<IEnumerable<Customer>>.Error("Could not load customers!"));
        }
    }
    
    [HttpPost]
    [Authorize]
    [Validation(typeof(Customer))]
    [Route("/api/customers")]
    public IActionResult AddCustomer([FromBody] Customer customer)
    {
        try
        {
            User? user = _authManager.GetLoggedInUser(HttpContext);
            
            if(user == null || !_customerServices.CreateCustomer(user, customer)) 
                throw new Exception("Could not add customer!");
                
            return Ok(ApiResponse<Customer>.Success(customer));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<Customer>.Error("Could not add customer!"));
        }
    }
    
    [HttpPut]
    [Authorize]
    [Validation(typeof(Customer))]
    [Route("/api/customers")]
    public IActionResult UpdateCustomer([FromBody] Customer customer)
    {
       
        try
        {
            User? user = _authManager.GetLoggedInUser(HttpContext);
            
            if(user == null || !_customerServices.UpdateCustomer(user, customer)) 
                throw new Exception("Could not update customer!");
                
            return Ok(ApiResponse<Customer>.Success(customer));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<Customer>.Error("Could not update customer!"));
        }
    }
}