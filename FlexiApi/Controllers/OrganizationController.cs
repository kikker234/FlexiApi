using Business.Services;
using Data.Models;
using FlexiApi.Attributes;
using FlexiApi.InputModels;
using FlexiApi.Resources;
using FlexiApi.Utils;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FlexiApi.Controllers;

[Route("api/[controller]")]
public class OrganizationController : FlexiController
{
    private OrganizationServices _organizationServices;
    private IStringLocalizer<ValidationMessages> _localizer;
    private readonly Serilog.ILogger _logger;

    public OrganizationController(OrganizationServices organizationServices,
        IStringLocalizer<ValidationMessages> localizer,
        Serilog.ILogger logger)
    {
        _organizationServices = organizationServices;
        _localizer = localizer;
        _logger = logger;
    }

    [HttpPost]
    [Validation(typeof(CreateOrganization))]
    public IActionResult CreateOrganization([FromBody] CreateOrganization? data)
    {
        _logger.Information("Creating new organization with email: {email} and organization name: {organizationName}", data.Email, data.OrganizationName);
        
        if (data == null)
        {
            _logger.Warning("Invalid data provided for creating organization");
            return BadRequest(ApiResponse<string>.Error(_localizer["InvalidData"]));
        }

        String email = data.Email;
        String password = data.Password;
        String organizationName = data.OrganizationName;

        Organization organization = new Organization
        {
            Name = organizationName
        };

        String? instanceKey = Request.Headers["Instance"];
        if (Request.Headers["Instance"].Count == 0 || instanceKey == null || instanceKey.Length == 0)
        {
            _logger.Warning("Instance key missing for creating organization");
            return BadRequest(ApiResponse<string>.Error(_localizer["InstanceKeyMissing"]));
        }

        Result result = _organizationServices.CreateNewOrganisation(email, password, organization, instanceKey);
        _logger.Information("Organization created successfully with email: {email} and organization name: {organizationName}", email, organizationName);
        return HandleResult(result);
    }
}