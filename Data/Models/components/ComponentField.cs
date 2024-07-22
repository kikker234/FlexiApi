using System.Text.Json.Serialization;

namespace Data.Models.components;

public class ComponentField
{
    public int Id { get; set; }
    public String Key { get; set; }
    public String Type { get; set; }

    public IList<ComponentValidation> Validations { get; set; } = new List<ComponentValidation>();
    
    public int ComponentId { get; set; }
    
    public Component Component { get; set; }
}