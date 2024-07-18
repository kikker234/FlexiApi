using Newtonsoft.Json.Linq;

namespace Business.Entity;

public abstract class AbstractHandler : IHandler
{
    private IHandler _nextHandler;

    public AbstractHandler SetNext(AbstractHandler handler)
    {
        this._nextHandler = handler;
        
        return handler;
    }
        
    public virtual JObject? Handle(JObject request)
    {
        if (this._nextHandler != null)
        {
            return this._nextHandler.Handle(request);
        }
        else
        {
            return request;
        }
    }
}