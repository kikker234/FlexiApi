using System.Net;
using Integration.Utils;
using Microsoft.AspNetCore.Http;

namespace Integration.Entity;

[TestClass]
public class EntityMapTests : TestClass
{
    [TestInitialize]
    public void Setup()
    {
        Login();
    }

    [TestMethod]
    [TestCategory("EntityMap")]
    public void Import_NotLoggedIn_Unauthorized()
    {
        Logout();
        HttpResponseMessage message = PostResponse("/api/entity/import", "{}");

        Assert.AreEqual(HttpStatusCode.Unauthorized, message.StatusCode);
    }

    [TestMethod]
    [TestCategory("EntityMap")]
    public void Import_NoInstanceHeader_BadRequest()
    {
        RemoveInstanceHeader();
        HttpResponseMessage message = PostResponse("/api/entity/import", "{}");

        Assert.AreEqual(HttpStatusCode.BadRequest, message.StatusCode);
    }

    [TestMethod]
    [TestCategory("EntityMap")]
    public void Import_Valid_Ok()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources",
            "valid-entity-map.json");
        Assert.IsTrue(File.Exists(filePath), $"File not found: {filePath}");
        string validJson = File.ReadAllText(filePath);

        HttpResponseMessage message = PostResponse("/api/entity/import", validJson);

        Assert.AreEqual(HttpStatusCode.OK, message.StatusCode);
    }
    
    [TestMethod]
    [TestCategory("EntityMap")]
    public void Import_InvalidJson_BadRequest()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources",
            "invalid-entity-map.json");
        Assert.IsTrue(File.Exists(filePath), $"File not found: {filePath}");
        string invalidJson = File.ReadAllText(filePath);
    
        HttpResponseMessage message = PostResponse("/api/entity/import", invalidJson);
    
        Assert.AreEqual(HttpStatusCode.BadRequest, message.StatusCode);
    }

    [TestCleanup]
    public void CleanUp()
    {
        AddInstanceHeader();
    }
}