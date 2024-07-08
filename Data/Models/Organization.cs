namespace Data.Models;

public class Organization
{
    public int Id { get; set; }
    public String Name { get; set; }
    
    public string OwnerId { get; set; }
    public User Owner { get; set; }
    
    public int Size { get; set; }
    public Plan? Plan { get; set; }
    
    public int InstanceId { get; set; }
    public Instance Instance { get; set; }
}