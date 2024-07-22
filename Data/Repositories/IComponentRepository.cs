using Data.Models.components;

namespace Data.Repositories;

public interface IComponentRepository : ICrudRepository<Component>
{
    public Component? ReadByType(string type, int instanceId);
}