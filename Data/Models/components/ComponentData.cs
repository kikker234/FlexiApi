namespace Data.Models.components;

public class ComponentData
{
    public int Id { get; set; }
    
    public int ComponentFieldId { get; set; }
    public ComponentField ComponentField { get; set; }
}