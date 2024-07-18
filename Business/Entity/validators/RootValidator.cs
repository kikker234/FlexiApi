using Newtonsoft.Json.Linq;

namespace Business.Entity.validators;

public class RootValidator : AbstractHandler
{
    public override JObject? Handle(JObject request)
    {
        if(request["components"] != null)
        {
            return base.Handle(request);
        }

        return null;
    }
}