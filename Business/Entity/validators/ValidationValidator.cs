using Newtonsoft.Json.Linq;

namespace Business.Entity.validators;

public class ValidationValidator : AbstractHandler
{
    private string[] validationTypes = new[]
    {
        "length",
        "required"
    };

    public override JObject? Handle(JObject request)
    {
        foreach (var component in request["components"])
        {
            if(component["validation"] == null)
                continue;
            
            foreach (var validation in component["validation"])
            {
                foreach (var property in validation.Children<JProperty>())
                {
                    string propertyName = property.Name;

                    if (!validationTypes.Contains(propertyName))
                    {
                        Console.Error.WriteLine("Validation type is not valid!");
                        return null;
                    }
                }
            }
        }

        return base.Handle(request);
    }
}