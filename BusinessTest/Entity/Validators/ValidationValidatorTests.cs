using Business.Entity.validators;
using Newtonsoft.Json.Linq;

namespace BusinessTest.Entity.Validators;

[TestClass]
public class ValidationValidatorTests
{
    private ValidationValidator _validationValidator;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _validationValidator = new ValidationValidator();
    }

    [TestMethod]
    public void Handle_Valid_Request()
    {
        string jsonString =
            "{\n  \"components\": [\n    {\n      \"name\": \"floepie\",\n      \"type\": \"string\",\n      \"validation\": [\n        {\n          \"required\": true\n        },\n        {\n          \"length\": 5\n        }\n      ]\n    }\n  ]\n}";
        JObject request = JObject.Parse(jsonString);
        
        JObject? result = _validationValidator.Handle(request);
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public void Handle_ValidationNonexisting_Null()
    {
        string jsonString =
            "{\n  \"components\": [\n    {\n      \"name\": \"floepie\",\n      \"type\": \"string\",\n      \"validation\": [\n        {\n          \"required\": true\n        },\n        {\n          \"floenk\": 5\n        }\n      ]\n    }\n  ]\n}";
        JObject request = JObject.Parse(jsonString);
        
        JObject? result = _validationValidator.Handle(request);
        
        Assert.IsNull(result);
    }

    [TestMethod]
    public void Handle_NoValidationRules_Request()
    {
        String jsonString =
            "{\n  \"components\": [\n    {\n      \"name\": \"floepie\",\n      \"type\": \"string\"\n    }\n  ]\n}";
        
        JObject request = JObject.Parse(jsonString);
        
        JObject? result = _validationValidator.Handle(request);
        
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public void Handle_NoRules_Request()
    {
        String jsonString =
            "{\n  \"components\": [\n    {\n      \"name\": \"floepie\",\n      \"type\": \"string\",\n      \"validation\": []\n    }\n  ]\n}";
        
        JObject request = JObject.Parse(jsonString);
        
        JObject? result = _validationValidator.Handle(request);
        
        Assert.IsNotNull(result);
    }
}