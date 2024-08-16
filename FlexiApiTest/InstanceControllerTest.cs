using System.Text.Json;
using Data.Models;
using FlexiApiTest.utils;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;

namespace FlexiApiTest;

[TestClass]
public class InstanceControllerTest : ApiTest
{
    private string? _instanceKey;
    
    [TestMethod]
    public async Task CreateInstance_ValidInput_ReturnsOkAndValidInstanceKey()
    {
        var context = await GetContext();

        var instance = new Instance
        {
            Name = "Test",
            OwnerEmail = "test@mail.com",
            Description = "Test instance"
        };
        
        var result = await context.PostAsync("/api/instance", new()
        {
            DataObject = instance
        });
        
        var json = await result.JsonAsync();
        json.Should().NotBeNull("Response JSON should not be null");

        json.Value.GetProperty("isSuccess").GetBoolean().Should().BeTrue("The response should indicate success");
        
        _instanceKey = json.Value.GetProperty("data").GetString();
        _instanceKey.Should().NotBeNullOrWhiteSpace("Instance key should be a non-empty string");
        
        _instanceKey.Should().MatchRegex(@"^[\da-fA-F]{8}-([\da-fA-F]{4}-){3}[\da-fA-F]{12}$", "Instance key should be a valid GUID");
        
        result.Ok.Should().BeTrue("The HTTP status should indicate success");
    }

    [TestMethod]
    public async Task RegenerateInstanceKey_NonExistingInstance_ReturnsBadRequest()
    {
        var context = await GetContext();

        var result = await context.PatchAsync("/api/instance?instanceKey=nonexistingkey");
        result.Ok.Should().BeFalse("The HTTP status should indicate failure for non-existing instance");
    }

    [TestMethod]
    public async Task RegenerateInstanceKey_ExistingInstance_ReturnsNewKey()
    {
        var context = await GetContext();

        var instance = new Instance
        {
            Name = "Test",
            OwnerEmail = "peter@demail.com",
            Description = "Test instance"
        };
        
        var result = await context.PostAsync("/api/instance", new()
        {
            DataObject = instance
        });
        
        result.Ok.Should().BeTrue("The HTTP status should indicate success when creating an instance");
        
        var json = await result.JsonAsync();
        var instanceKey = json.Value.GetProperty("data").GetString();
        
        result = await context.PatchAsync($"/api/instance?instanceKey={instanceKey}");
        json = await result.JsonAsync();
        var newInstanceKey = json.Value.GetProperty("data").GetString();
        
        result.Ok.Should().BeTrue("The HTTP status should indicate success when regenerating instance key");
        newInstanceKey.Should().NotBe(instanceKey, "A new instance key should be generated");
        
        _instanceKey = newInstanceKey;
    }
    
    [TestMethod]
    public async Task DeleteInstance_NonExistingInstance_ReturnsBadRequest()
    {
        var context = await GetContext();

        var result = await context.DeleteAsync("/api/instance?instanceKey=nonexistingkey");
        result.Ok.Should().BeFalse("The HTTP status should indicate failure for non-existing instance deletion");
    }
    
    [TestMethod]
    public async Task DeleteInstance_ExistingInstance_ReturnsOk()
    {
        var context = await GetContext();

        var instance = new Instance
        {
            Name = "Test",
            OwnerEmail = "pater@gmail.com",
            Description = "Test instance"
        };
        
        var result = await context.PostAsync("/api/instance", new()
        {
            DataObject = instance
        });
        
        result.Ok.Should().BeTrue("The HTTP status should indicate success when creating an instance");
        
        var json = await result.JsonAsync();
        var instanceKey = json.Value.GetProperty("data").GetString();
        
        result = await context.DeleteAsync($"/api/instance?instanceKey={instanceKey}");
        result.Ok.Should().BeTrue("The HTTP status should indicate success when deleting an existing instance");
    }
    
    [TestCleanup]
    public async Task Cleanup()
    {
        if (string.IsNullOrWhiteSpace(_instanceKey)) return;
        
        var context = await GetContext();
        var result = await context.DeleteAsync($"/api/instance?instanceKey={_instanceKey}");
        result.Ok.Should().BeTrue("Cleanup should successfully delete the instance");
    }
}
