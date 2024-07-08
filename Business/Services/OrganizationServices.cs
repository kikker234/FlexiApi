using Auth;
using Data.Models;
using Data.Repositories;

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
    
    public bool CreateNewOrganisation(string email, string password, Organization organization, string instanceKey)
    {
        if (!_authManager.Register(email, password))
        {
            Console.WriteLine("Failed to register");
            return false;
        }

        User? user = _authManager.GetLoggedInUser(email, password);
        if (user == null) return false;
        
        organization.Owner = user;
        organization.InstanceId = _instanceRepository.GetByKey(instanceKey).Id;
        
        return _organizationRepository.Create(organization);
    }
}