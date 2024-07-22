namespace Data.Models.components;

public class ComponentObject
{
    public int Id { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    public string? UpdatedById { get; set; }
    public User? UpdatedBy { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string? DeletedById { get; set; }
    public User? DeletedBy { get; set; }
    
    public int ComponentId { get; set; }
    public Component Component { get; set; }
    
    public IList<ComponentData> Data { get; set; }
}