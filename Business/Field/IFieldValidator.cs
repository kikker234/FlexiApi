using Data.Models.components;

namespace Business.Field;

public interface IFieldValidator
{
    public bool ValidateField(string value);
}