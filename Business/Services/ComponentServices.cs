using Data.Models;
using Data.Models.components;
using Data.Repositories;

namespace Business.Services;

public class ComponentServices
{
    private readonly ComponentRepository _componentRepository;

    public ComponentServices(ComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    public bool CreateComponent(Component component)
    {
        return _componentRepository.Create(component);
    }
    
    public IEnumerable<Component> GetComponents(User user, string type, Dictionary<string, string> body)
    {
        IList<Component> components = _componentRepository.GetByType(type, user.OrganizationId).ToList();
        
        if (!components.Any())
            return components;
        
        if (body.Count > 0)
        {
            //components = components.Where(c => c.CustomComponentFields.Any(f => body.ContainsKey(f.Key) && f.Value == body[f.Key])).ToList();
        }
        
        return components.Where(c => HasAccessToComponent(user, c));
    }

    private bool HasAccessToComponent(User user, Component component)
    {
        return user.OrganizationId == component.OrganizationId;
    }
}