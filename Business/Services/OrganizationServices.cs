using Auth;
using Data.Models;
using Data.Repositories;

namespace Business.Services;

public class OrganizationServices
{
    private readonly IAuthManager _authManager;
    private readonly OrganizationRepository _organizationRepository;
    
    public OrganizationServices(IAuthManager authManager, OrganizationRepository organizationRepository)
    {
        _authManager = authManager;
        _organizationRepository = organizationRepository;
    }
    
    public bool CreateNewOrganisation(string email, string password, Organization organization)
    {
        if (!_authManager.Register(email, password))
        {
            Console.WriteLine("Failed to register");
            return false;
        }

        User? user = _authManager.GetLoggedInUser(email, password);
        if (user == null) return false;
        
        organization.Owner = user;
        
        return _organizationRepository.Create(organization);
    }
}