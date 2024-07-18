using System.Runtime.InteropServices.JavaScript;
using Business.Entity.validators;
using Newtonsoft.Json.Linq;

namespace BusinessTest.Entity.Validators;

[TestClass]
public class ComponentValidatorTests
{
    private ComponentValidator _componentValidatorTests;

    [TestInitialize]
    public void TestInitialize()
    {
        _componentValidatorTests = new ComponentValidator();
    }

    [TestMethod]
    public void Handle_ValidInput_Request()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Component 1",
                    ["type"] = "String"
                },
                new JObject
                {
                    ["name"] = "Component 2",
                    ["type"] = "String"
                }
            }
        };

        JObject? result = _componentValidatorTests.Handle(json);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Handle_NoName_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["type"] = "String"
                }
            }
        };

        JObject? result = _componentValidatorTests.Handle(json);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Handle_EmptyName_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "",
                    ["type"] = "String"
                }
            }
        };

        JObject? result = _componentValidatorTests.Handle(json);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Handle_NullName_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = null,
                    ["type"] = "String"
                }
            }
        };

        JObject? result = _componentValidatorTests.Handle(json);

        Assert.IsNull(result);
    }
}