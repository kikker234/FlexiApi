using System.Text.Json.Serialization;

namespace Data.Models.components;

public class Component
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int InstanceId { get; set; }

    [JsonIgnore] public Instance Instance { get; set; }
    
    public IList<ComponentField> Fields { get; set; }

    public string Get(string fieldName)
    {
        ComponentField? field = Fields.FirstOrDefault(f => f.Key == fieldName);
        
        if(field == null) return "";
        
        return "";
    }
}