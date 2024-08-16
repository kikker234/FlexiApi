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

    public OrganizationController(OrganizationServices organizationServices,
        IStringLocalizer<ValidationMessages> localizer)
    {
        _organizationServices = organizationServices;
        _localizer = localizer;
    }

    [HttpPost]
    [Validation(typeof(CreateOrganization))]
    public IActionResult CreateOrganization([FromBody] CreateOrganization? data)
    {
        if (data == null)
        {
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
            return BadRequest(ApiResponse<string>.Error(_localizer["InstanceKeyMissing"]));

        Result result = _organizationServices.CreateNewOrganisation(email, password, organization, instanceKey);
        return HandleResult(result);
    }
}