using Newtonsoft.Json.Linq;

namespace Business.Entity;

public interface IHandler
{
    JObject? Handle(JObject request);
}