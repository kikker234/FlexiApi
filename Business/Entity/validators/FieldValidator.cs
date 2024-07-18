using Newtonsoft.Json.Linq;

namespace Business.Entity.validators;

public class FieldValidator : AbstractHandler
{
    private string[] _validTypes = {"string", "number"};
    
    public override JObject? Handle(JObject request)
    {
        foreach (var component in request["components"])
        {
            if (component["fields"] == null || !component["fields"].HasValues) return null;

            foreach (var field in component["fields"])
            {
                var name = field["name"];
                var type = field["type"];

                if (name == null || type == null)
                {
                    Console.Error.WriteLine("Field name or type is missing!");
                    return null;
                }
                
                string nameString = name.ToString();
                string typeString = type.ToString();

                if (string.IsNullOrEmpty(nameString) || string.IsNullOrEmpty(typeString))
                {
                    Console.Error.WriteLine("Field name or type is null or empty!");
                    return null;
                }

                if (!_validTypes.Contains(typeString.ToLower()))
                {
                    Console.Error.WriteLine("Field type is not valid!");
                    return null;
                }
            }
        }

        return base.Handle(request);
    }
}