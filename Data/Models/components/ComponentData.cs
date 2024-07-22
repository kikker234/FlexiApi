using System.Text.Json.Serialization;

namespace Data.Models.components;

public class ComponentData
{
    public int Id { get; set; }

    public string value { get; set; }
    
    public int ComponentFieldId { get; set; }
    public ComponentField ComponentField { get; set; }
    
    public int ComponentObjectId { get; set; }
    [JsonIgnore]
    public ComponentObject ComponentObject { get; set; }
}