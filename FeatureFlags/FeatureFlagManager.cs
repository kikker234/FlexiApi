using Data.Models;
using Data.Repositories;
using Microsoft.FeatureManagement;

namespace FeatureFlags;

public class FeatureFlagManager
{
    
    private FeatureFlagRepository _featureFlagRepository;
    private InstanceRepository _instanceRepository;
    
    public FeatureFlagManager(FeatureFlagRepository featureFlagRepository, InstanceRepository instanceRepository)
    {
        _featureFlagRepository = featureFlagRepository;
        _instanceRepository = instanceRepository;
    }
    
    public bool HasCustomFeatures(Instance instance)
    {
        return _featureFlagRepository.ReadByInstance(instance).Any();
    }
    
    public bool CreateFeature(int instanceId, string featureName)
    {
        Instance? instance = _instanceRepository.Read(instanceId);

        if (instance == null) return false;
        
        FeatureFlag featureFlag = new FeatureFlag
        {
            Name = featureName,
            InstanceId = instanceId,
            CreatedAt = DateTime.Now
        };
        
        return _featureFlagRepository.Create(featureFlag);
    }
    
    public bool Enable(int featureFlagId)
    {
        FeatureFlag? featureFlag = _featureFlagRepository.Read(featureFlagId);
        
        if (featureFlag == null) return false;
        
        featureFlag.EnabledAt = DateTime.Now;
        
        return _featureFlagRepository.Update(featureFlag);
    }

    public bool HasFeature(string featureAttributeFeatureName, int instanceId)
    {
        Instance? instance = _instanceRepository.Read(instanceId);
        
        if (instance == null) return false;
        
        return _featureFlagRepository.ReadByInstance(instance).Any(ff => ff.Name == featureAttributeFeatureName && ff.EnabledAt != null);
    }
}