using System.Text.Json.Nodes;
using FlexiApi.Utils;
using FlexiApiTest.utils;
using FluentAssertions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FlexiApiTest;

[TestClass]
public class AuthTest : ApiTest
{
    [TestMethod]
    public async Task Valid_InvalidToken_FalseData()
    {
        var context = await GetContext();

        var result = await context.GetAsync("/api/auth/valid?token=ABCD");

        var json = await result.JsonAsync();
        ApiResponse<Boolean> response = JsonSerializer.Deserialize<ApiResponse<Boolean>>(json.ToString());
        
        response.Data.Should().BeFalse("because the token is invalid");
        result.Ok.Should().Be(true, "because the request should be successful");
        result.Status.Should().Be(200, "because the status code should be 200");
    }
}