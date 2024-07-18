using Newtonsoft.Json.Linq;

namespace Business.Entity.validators;

public class ComponentValidator : AbstractHandler
{
    public override JObject? Handle(JObject request)
    {
        foreach (var component in request["components"])
        {
            string name = component["name"]?.ToString();
            
            if (string.IsNullOrEmpty(name))
            {
                Console.Error.WriteLine("Component name or type is missing!");
                return null;
            }
        }

        return base.Handle(request);
    }
}