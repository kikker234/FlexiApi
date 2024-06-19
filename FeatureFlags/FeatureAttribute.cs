namespace FeatureFlags;

public class FeatureAttribute : Attribute
{
    public string FeatureName { get; set; }
    
    public FeatureAttribute(string featureName)
    {
        FeatureName = featureName;
    }
}