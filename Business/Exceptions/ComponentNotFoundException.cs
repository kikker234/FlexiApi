namespace BusinessTest.Exceptions;

public class ComponentNotFoundException : Exception
{
    public int? ComponentId { get; }
    public string? Type { get;  }
    
    public ComponentNotFoundException(string type)
        : base($"Component {type} not found!")
    {
        Type = type;
    }
    
    public ComponentNotFoundException(int id)
        : base($"Component with ID {id} not found")
    {
        ComponentId = id;
    }
}