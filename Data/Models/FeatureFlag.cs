namespace Data.Models;

public class FeatureFlag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EnabledAt { get; set; }
    
    
    public int InstanceId { get; set; }
    public Instance Instance { get; set; }
}
