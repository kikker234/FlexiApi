using System.Collections;
using Business.Services;
using Data.Models.components;
using Data.Repositories;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Newtonsoft.Json.Linq;

namespace BusinessTest.Services;

[TestClass]
[TestSubject(typeof(EntityServices))]
public class EntityServicesTest
{
    private EntityServices _entityServices;
    private Mock<IComponentRepository> _componentRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _componentRepository = new Mock<IComponentRepository>();
        _entityServices = new EntityServices(_componentRepository.Object);

        List<Component> comps = new List<Component>();
        comps.Add(new Component()
        {
            Name = "Egg",
        });

        _componentRepository.Setup(c => c.GetComponents()).Returns(comps);
    }

    [TestMethod]
    public void Import_ValidJson_True()
    {
        string json =
            "{\n  \"components\": [\n    {\n      \"name\": \"person\",\n      \"fields\": [\n        {\n          \"name\": \"name\",\n          \"type\": \"string\",\n          \"validation\": [\n            {\n              \"required\": true\n            },\n            {\n              \"length\": 5\n            }\n          ]\n        },\n        {\n          \"name\": \"age\",\n          \"type\": \"number\",\n          \"default\": 0\n        }\n      ]\n    }\n  ]\n}";

        JObject jsonObject = JObject.Parse(json);

        bool result = _entityServices.Import(jsonObject);

        _componentRepository.Verify(c => c.Create(It.IsAny<Component>()), Times.Once);
        _componentRepository.Verify(c => c.Delete(It.IsAny<Component>()), Times.Once);
        _componentRepository.Verify(c => c.Update(It.IsAny<Component>()), Times.Never);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Import_CreateNewTables_True()
    {
        _componentRepository.Setup(c => c.GetComponents()).Returns(new List<Component>());

        string json =
            "{\n  \"components\": [\n    {\n      \"name\": \"person\",\n      \"fields\": [\n        {\n          \"name\": \"name\",\n          \"type\": \"string\",\n          \"validation\": [\n            {\n              \"required\": true\n            },\n            {\n              \"length\": 5\n            }\n          ]\n        },\n        {\n          \"name\": \"age\",\n          \"type\": \"number\",\n          \"default\": 0\n        }\n      ]\n    }\n  ]\n}";

        JObject jsonObject = JObject.Parse(json);
        bool result = _entityServices.Import(jsonObject);

        _componentRepository.Verify(c => c.Create(It.IsAny<Component>()), Times.Once);
        _componentRepository.Verify(c => c.Delete(It.IsAny<Component>()), Times.Never);
        _componentRepository.Verify(c => c.Update(It.IsAny<Component>()), Times.Never);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Import_DeleteAll_True()
    {
        string json =
            "{\n  \"components\": []\n}";

        JObject jsonObject = JObject.Parse(json);
        bool result = _entityServices.Import(jsonObject);

        _componentRepository.Verify(c => c.Create(It.IsAny<Component>()), Times.Never);
        _componentRepository.Verify(c => c.Delete(It.IsAny<Component>()), Times.Once);
        _componentRepository.Verify(c => c.Update(It.IsAny<Component>()), Times.Never);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Import_UpdateFields_True()
    {
        string json =
            "{\n  \"components\": [\n    {\n      \"name\": \"Egg\",\n      \"fields\": [\n        {\n          \"name\": \"name\",\n          \"type\": \"string\",\n          \"validation\": [\n            {\n              \"required\": true\n            },\n            {\n              \"length\": 5\n            }\n          ]\n        },\n        {\n          \"name\": \"age\",\n          \"type\": \"number\",\n          \"default\": 0\n        }\n      ]\n    }\n  ]\n}";

        JObject jsonObject = JObject.Parse(json);

        bool result = _entityServices.Import(jsonObject);

        _componentRepository.Verify(c => c.Create(It.IsAny<Component>()), Times.Never);
        _componentRepository.Verify(c => c.Delete(It.IsAny<Component>()), Times.Never);
        _componentRepository.Verify(c => c.Update(It.IsAny<Component>()), Times.Once);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Import_InvalidJson_True()
    {
        string json =
            "{\n  \"components\": [\n    {\n      \"name\": \"person\",\n      \"fields\": []\n    }\n  ]\n}";

        JObject jsonObject = JObject.Parse(json);

        _componentRepository.Setup(c => c.GetComponents()).Returns(new List<Component>());
        
        Assert.ThrowsException<Exception>(() => _entityServices.Import(jsonObject));
    }
}