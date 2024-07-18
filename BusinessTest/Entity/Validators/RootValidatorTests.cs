using Business.Entity.validators;
using Newtonsoft.Json.Linq;

namespace BusinessTest.Entity.Validators;

[TestClass]
public class RootValidatorTests
{

    private readonly RootValidator _rootValidator;
    
    public RootValidatorTests()
    {
        _rootValidator = new RootValidator();
    }
    
    [TestMethod]
    public void Handle_Valid_Request()
    {
        JObject json = JObject.Parse("{ \"components\": [ { \"name\": \"component1\" } ] }");
        
        JObject? result = _rootValidator.Handle(json);
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public void Handle_Empty_Null()
    {
        JObject json = JObject.Parse("{ \"components\": [] }");
        
        JObject? result = _rootValidator.Handle(json);
        
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void Handle_NullComponents_Null()
    {
        JObject json = JObject.Parse("{ \"components\": null }");
        
        JObject? result = _rootValidator.Handle(json);
        
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void Handle_NoComponents_Null()
    {
        JObject json = JObject.Parse("{ }");
        
        JObject? result = _rootValidator.Handle(json);
        
        Assert.IsNull(result);
    }
}