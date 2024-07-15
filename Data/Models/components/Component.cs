using System.Text.Json.Serialization;

namespace Data.Models.components;

public class Component
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public string Type { get; set; }
    
    [JsonIgnore]
    public int OrganizationId { get; set; }
    [JsonIgnore]
    public Organization Organization { get; set; }
    
    public IList<ComponentField> CustomComponentFields { get; set; }
}