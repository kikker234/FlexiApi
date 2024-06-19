using Data.Models;
using Data.Repositories;

namespace Business.Services;

public class OrganizationServices
{
    private readonly OrganizationRepository _organizationRepository;
    
    public OrganizationServices(OrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
    
    public IEnumerable<Organization> GetAll()
    {
        return _organizationRepository.ReadAll();
    }

    public bool Create(Organization organization)
    {
        if(GetAll().Any(o => o.Name == organization.Name))
            return false;
        
        return _organizationRepository.Create(organization);
    }

    public bool Delete(int id)
    {
        if(id <= 0)
            return false;
        
        return _organizationRepository.Delete(id);
    }
}