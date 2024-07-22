using Business.Entity;
using Business.Entity.validators;
using Data.Models.components;
using Newtonsoft.Json.Linq;

namespace Business.Field;

public class AbstractFieldValidator : IFieldValidator
{
    private IFieldValidator? _nextHandler;
    protected ComponentValidation validator;
    
    public AbstractFieldValidator(ComponentValidation validator)
    {
        this.validator = validator;
    }

    public AbstractFieldValidator SetNext(AbstractFieldValidator handler)
    {
        this._nextHandler = handler;

        return handler;
    }

    public virtual bool ValidateField(string value)
    {
        if (this._nextHandler != null)
        {
            return this._nextHandler.ValidateField(value);
        }

        return true;
    }
}