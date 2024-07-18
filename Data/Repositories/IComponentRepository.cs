using Data.Models.components;

namespace Data.Repositories;

public interface IComponentRepository
{
    IEnumerable<Component> GetComponents();
    IEnumerable<Component> GetByType(string type, int organizationId);
    bool Create(Component component);
    bool Delete(Component component);
    void Update(Component existingComponent);
}