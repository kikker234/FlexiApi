using Data.Models;
using Data.Models.components;

namespace Data.Repositories;

public interface IComponentDataRepository
{
    IList<ComponentObject> ReadAll(Instance instance, string type);
    bool Create(ComponentObject componentObject);
    ComponentObject? ReadById(int id);
    bool Update(ComponentObject componentObject);
}