using System.Text.Json.Nodes;

namespace Data.Models.components;

public class ComponentValidation
{
    public int Id { get; set; }
    
    public string ValidationType { get; set; }
    public string ValidationValue { get; set; }
    
    public int ComponentFieldId { get; set; }
    public ComponentField ComponentField { get; set; }
}