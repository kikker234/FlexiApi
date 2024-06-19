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
    
    public bool CreateInstance(Instance instance)
    {
        instance.Key = GenerateInstanceKey();
        
        return _instanceRepository.Create(instance);   
    }

    private string GenerateInstanceKey()
    {
        return Guid.NewGuid().ToString();
    }
}