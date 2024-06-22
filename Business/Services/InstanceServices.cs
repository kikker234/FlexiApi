using Data.Models;
using Data.Repositories;

namespace Business;

public class InstanceServices
{
    private readonly InstanceRepository _instanceRepository;
    
    public InstanceServices(InstanceRepository instanceRepository)
    {
        _instanceRepository = instanceRepository;
    }
    
    public string CreateInstance(Instance instance)
    {
        instance.Key = GenerateInstanceKey();
        
        _instanceRepository.Create(instance);   
        return Guid.NewGuid().ToString();
    }

    public string GenerateInstanceKey()
    {
        return Guid.NewGuid().ToString();
    }
}