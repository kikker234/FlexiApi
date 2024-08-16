using Auth;
using Data.Models;
using Data.Repositories;
using FluentResults;

namespace Business.Services;

public class OrganizationServices
{
    private readonly IAuthManager _authManager;
    private readonly OrganizationRepository _organizationRepository;
    private readonly InstanceRepository _instanceRepository;
    
    public OrganizationServices(IAuthManager authManager, OrganizationRepository organizationRepository, InstanceRepository instanceRepository)
    {
        _authManager = authManager;
        _organizationRepository = organizationRepository;
        _instanceRepository = instanceRepository;
    }
    
    public Result CreateNewOrganisation(string email, string password, Organization organization, string instanceKey)
    {
        Instance? instance = _instanceRepository.GetByKey(instanceKey);
        if (instance == null) return Result.Fail("Instance not found");
        
        organization.InstanceId = instance.Id;
        _organizationRepository.Create(organization);
        
        if (!_authManager.Register(email, password, organization))
        {
            return Result.Fail("Failed to register user");
        }

        User? user = _authManager.GetLoggedInUser(email, password);
        return user != null ? Result.Ok() : Result.Fail("Failed to get user");
    }
}