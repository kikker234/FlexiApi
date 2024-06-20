namespace Data.Models;

public class Organization
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int InstanceId { get; set; }
    public Instance Instance { get; set; }
    public DateTime? DeletedAt { get; set; }
}