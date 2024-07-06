
using Auth.Attributes;
using Business;
using Data.Models;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerServices _customerServices;

    public CustomerController(CustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    [HttpGet]
    [Authorize]
    [Route("/api/customers")]
    public IActionResult GetCustomers()
    {
        try
        {
            return Ok(ApiResponse<IEnumerable<Customer>>.Success(_customerServices.GetCustomers()));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<IEnumerable<Customer>>.Error("Could not load customers!"));
        }
    }
}