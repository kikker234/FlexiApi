using Business.Services;
using BusinessTest.Exceptions;
using Data.Models;
using Data.Models.components;
using Data.Repositories;
using JetBrains.Annotations;
using Moq;

namespace BusinessTest.Services;

[TestClass]
[TestSubject(typeof(ComponentServices))]
public class ComponentServicesTest
{
    private ComponentServices _componentServices;

    private Mock<IComponentRepository> _componentRepository;
    private Mock<IComponentDataRepository> _componentDataRepository;
    
    private Component _testComponent;
    private Dictionary<string, string> _testData;

    [TestInitialize]
    public void Setup()
    {
        _componentRepository = new Mock<IComponentRepository>();
        _componentDataRepository = new Mock<IComponentDataRepository>();

        _componentServices = new ComponentServices(
            _componentRepository.Object,
            _componentDataRepository.Object
        );
        
        _testComponent = new Component
        {
            Id = 1,
            Name = "person",
            Fields = new List<ComponentField>
            {
                new ComponentField
                {
                    Id = 1,
                    Key = "name",
                    Type = "string",
                    ComponentId = 1
                },
                new ComponentField
                {
                    Id = 2,
                    Key = "age",
                    Type = "number",
                    ComponentId = 1
                },
                new ComponentField()
                {
                    Id = 3,
                    Key = "email",
                    Type = "string",
                    ComponentId = 1,
                    Validations = new List<ComponentValidation>()
                    {
                        new ComponentValidation
                        {
                            Id = 1,
                            ValidationType = "Required",
                            ValidationValue = "true",
                            ComponentFieldId = 3
                        },
                    },
                }
            },
        };
        
        _testData = new Dictionary<string, string>
        {
            {"name", "Peter"},
            {"age", "34"},
            {"email", "jusraaijmakers@gmail.com"},
        };
    }

    [TestMethod]
    public void Create_HappyFlow_True()
    {
        // Arrange
        _componentRepository.Setup(x => x.ReadByType(It.IsAny<string>(), It.IsAny<int>())).Returns(_testComponent);
        _componentDataRepository.Setup(x => x.Create(It.IsAny<ComponentObject>())).Returns(true);

        User user = new User();
        Instance instance = new Instance();
        user.Organization = new Organization();
        user.Organization.Instance = instance;
        
        // Act
        bool result = _componentServices.Create(user, instance, "person", _testData);
        
        // Assert
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void Create_ComponentNotFound_Exception()
    {
        // Arrange
        _componentRepository.Setup(x => x.ReadByType(It.IsAny<string>(), It.IsAny<int>())).Returns((Component?) null);

        User user = new User();
        Instance instance = new Instance();
        user.Organization = new Organization();
        user.Organization.Instance = instance;
        
        // Act
        Assert.ThrowsException<ComponentNotFoundException>(() => _componentServices.Create(user, instance, "person", _testData));
    }
    
    [TestMethod]
    public void Create_ValidationFailed_Exception()
    {
        // Arrange
        _componentRepository.Setup(x => x.ReadByType(It.IsAny<string>(), It.IsAny<int>())).Returns(_testComponent);

        User user = new User();
        Instance instance = new Instance();
        user.Organization = new Organization();
        user.Organization.Instance = instance;
        
        _testData["email"] = "";
        
        // Act
        Assert.ThrowsException<InvalidFieldDataException>(() => _componentServices.Create(user, instance, "person", _testData));
    }
    
    [TestMethod]
    public void Create_FieldNotFound_Exception()
    {
        // Arrange
        _componentRepository.Setup(x => x.ReadByType(It.IsAny<string>(), It.IsAny<int>())).Returns(_testComponent);

        User user = new User();
        Instance instance = new Instance();
        user.Organization = new Organization();
        user.Organization.Instance = instance;
        
        _testData["unknown"] = "value";
        
        // Act
        Assert.ThrowsException<Exception>(() => _componentServices.Create(user, instance, "person", _testData));
    }
    
    [TestMethod]
    public void GetComponents_HappyFlow_List()
    {
        // Arrange
        _componentDataRepository.Setup(x => x.ReadAll(It.IsAny<Instance>(), It.IsAny<string>())).Returns(new List<ComponentObject>());

        Instance instance = new Instance();
        
        // Act
        IEnumerable<ComponentObject> result = _componentServices.GetComponents("person", instance);
        
        // Assert
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public void GetComponents_EmptyList_EmptyList()
    {
        // Arrange
        _componentDataRepository.Setup(x => x.ReadAll(It.IsAny<Instance>(), It.IsAny<string>())).Returns(new List<ComponentObject>());

        Instance instance = new Instance();
        
        // Act
        IEnumerable<ComponentObject> result = _componentServices.GetComponents("person", instance);
        
        // Assert
        Assert.IsFalse(result.Any());
    }
    
    [TestMethod]
    public void GetComponents_NullInstance_EmptyList()
    {
        // Arrange
        _componentDataRepository.Setup(x => x.ReadAll(It.IsAny<Instance>(), It.IsAny<string>())).Returns(new List<ComponentObject>());

        // Act
        IEnumerable<ComponentObject> result = _componentServices.GetComponents("person", null);
        
        // Assert
        Assert.IsFalse(result.Any());
    }
}