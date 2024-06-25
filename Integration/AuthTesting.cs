using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using FlexiApi;
using Newtonsoft.Json;

namespace Integration;

[TestClass]
public class AuthTesting
{
    private static WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var url = "/api/Auth/Register"; // vervang door jouw API endpoint
        var body = new StringContent(
            JsonConvert.SerializeObject(new
            {
                Email = "admin@admin.com",
                Password = "password"
            }), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(url, body);

        // Assert
        Console.WriteLine(response);
        response.EnsureSuccessStatusCode(); 
        
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _factory.Dispose();
    }
}