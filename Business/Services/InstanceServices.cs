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
        return instance.Key;
    }

    public string GenerateInstanceKey()
    {
        return Guid.NewGuid().ToString();
    }

    public Instance GetInstance(string instanceKey)
    {
        Instance instance = _instanceRepository.GetByKey(instanceKey);
        
        if(instance == null) throw new Exception("Instance not found");
        
        return instance;
    }
    
    public Instance GetInstance(string instanceKey, string email)
    {
        Instance instance = _instanceRepository.GetByKey(instanceKey);
        
        if(instance == null) throw new Exception("Instance not found");
        if(instance.OwnerEmail != email) throw new Exception("Instance not found");
        
        return instance;
    }


    public bool UpdateInstance(Instance instance)
    {
        return _instanceRepository.Update(instance);
    }

    public string RegenerateInstanceKey(string instanceKey)
    {
        Instance? instance = _instanceRepository.GetByKey(instanceKey);
        
        if(instance == null) throw new Exception("Instance not found");
        
        instance.Key = GenerateInstanceKey();
        _instanceRepository.Update(instance);
        
        return instance.Key;
    }

    public bool DeleteInstance(Instance instance)
    {
        return _instanceRepository.Delete(instance);
    }
}