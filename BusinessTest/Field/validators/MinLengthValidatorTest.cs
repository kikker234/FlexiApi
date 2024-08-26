using Business.Field.validators;
using Data.Models.components;
using JetBrains.Annotations;

namespace BusinessTest.Field.validators;

[TestClass]
[TestSubject(typeof(MinLengthValidator))]
public class MinLengthValidatorTest
{
    private MinLengthValidator _validator;
    
    [TestInitialize]
    public void TestInitialize()
    {
        ComponentValidation compValidator = new ComponentValidation
        {
            ValidationValue = "5",
            ValidationType = "min-length"
        };
        _validator = new MinLengthValidator(compValidator);
    }
    
    [TestMethod]
    public void ValidateField_InputMeetsLengthRequirement_ReturnsTrue()
    {
        // Arrange
        string value = "12345";

        // Act
        bool result = _validator.ValidateField(value);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidateField_InputDoesNotMeetLengthRequirement_ReturnsFalse()
    {
        // Arrange
        string value = "123";

        // Act
        bool result = _validator.ValidateField(value);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidateField_ValidationValueIsNotANumber_ReturnsFalse()
    {
        // Arrange
        string value = "abcd";

        // Act
        bool result = _validator.ValidateField(value);

        // Assert
        Assert.IsFalse(result);
    }
}