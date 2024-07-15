namespace Data.Models.components;

public class ComponentField
{
    public int Id { get; set; }
    public String Key { get; set; }
    
    public int ComponentId { get; set; }
    public Component Component { get; set; }
}