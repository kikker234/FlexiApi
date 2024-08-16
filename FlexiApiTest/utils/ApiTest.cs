using Microsoft.Playwright;

namespace FlexiApiTest.utils;

public class ApiTest : PageTest
{
    protected async Task<IAPIRequestContext> GetContext()
    {
        return await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "http://localhost:5161",
        });
    } 
}