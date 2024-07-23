namespace BusinessTest.Exceptions;

public class InvalidFieldDataException : Exception
{
    public string FieldKey { get; }
    public InvalidFieldDataException(string fieldKey) 
        : base($"Invalid data for field: {fieldKey}")
    {
        FieldKey = fieldKey;
    }
}