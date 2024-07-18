using Data.Models.components;
using Newtonsoft.Json.Linq;

namespace Business.Entity.adapter;

public class JsonComponentAdapter : IAdapter<JObject, IEnumerable<Component>>
{
    public IEnumerable<Component> Convert(JObject json)
    {
        IList<Component> components = new List<Component>();
        
        foreach (var jsonComponent in json["components"])
        {
            Component component = new Component
            {
                Name = jsonComponent["name"].ToString(),
                Fields = new List<ComponentField>(),
            };
            
            foreach (var field in jsonComponent["fields"])
            {
                ComponentField compField = new ComponentField();
                compField.Key = field["name"].ToString();
                compField.Type = field["type"].ToString();
                
                if (field["validation"] == null) continue;
                
                foreach (var validation in field["validation"])
                {
                    foreach (var property in validation.Children<JProperty>())
                    {
                        string propertyName = property.Name;
                        ComponentValidation compValidation = new ComponentValidation();
                        compValidation.ValidationType = propertyName;
                        compValidation.ValidationValue = validation.First.First.ToString();

                        compField.Validations.Add(compValidation);
                    }
                }
                
                component.Fields.Add(compField);
            }
            
            components.Add(component);
        }
        
        return components;
    }
}