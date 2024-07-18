using Business.Entity.validators;
using Newtonsoft.Json.Linq;

namespace BusinessTest.Entity.Validators;

[TestClass]
public class FieldValidatorTest
{
    private FieldValidator _fieldValidator;
    
    [TestInitialize]
    public void Initialize()
    {
        _fieldValidator = new FieldValidator();
    }

    [TestMethod]
    public void Handle_Valid_Request()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Person",
                    ["type"] = "String",
                    ["fields"] = new JArray
                    {
                        new JObject
                        {
                            ["name"] = "Name",
                            ["type"] = "String"
                        },
                        new JObject
                        {
                            ["name"] = "Age",
                            ["type"] = "number"
                        }
                    }
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Handle_NoFields_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Person",
                    ["type"] = "String"
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void Handle_InvalidFieldType_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Person",
                    ["type"] = "String",
                    ["fields"] = new JArray
                    {
                        new JObject
                        {
                            ["name"] = "Name",
                            ["type"] = "String"
                        },
                        new JObject
                        {
                            ["name"] = "Age",
                            ["type"] = "Banana"
                        }
                    }
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void Handle_MissingName_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Person",
                    ["type"] = "String",
                    ["fields"] = new JArray
                    {
                        new JObject
                        {
                            ["type"] = "String"
                        },
                        new JObject
                        {
                            ["name"] = "Age",
                            ["type"] = "number"
                        }
                    }
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void Handle_MissingType_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Person",
                    ["type"] = "String",
                    ["fields"] = new JArray
                    {
                        new JObject
                        {
                            ["name"] = "Name"
                        },
                        new JObject
                        {
                            ["name"] = "Age",
                            ["type"] = "number"
                        }
                    }
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
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
                    ["name"] = "Person",
                    ["type"] = "String",
                    ["fields"] = new JArray
                    {
                        new JObject
                        {
                            ["name"] = "",
                            ["type"] = "String"
                        },
                        new JObject
                        {
                            ["name"] = "Age",
                            ["type"] = "number"
                        }
                    }
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void Handle_EmptyType_Null()
    {
        JObject json = new JObject
        {
            ["components"] = new JArray
            {
                new JObject
                {
                    ["name"] = "Person",
                    ["type"] = "String",
                    ["fields"] = new JArray
                    {
                        new JObject
                        {
                            ["name"] = "Name",
                            ["type"] = ""
                        },
                        new JObject
                        {
                            ["name"] = "Age",
                            ["type"] = "number"
                        }
                    }
                }
            }
        };
        
        JObject? result = _fieldValidator.Handle(json);
        
        Assert.IsNull(result);
    }
}