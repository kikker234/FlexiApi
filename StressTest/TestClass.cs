using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Integration.Utils;

public abstract class TestClass
{
    private static string? key = null;
    private static HttpClient? _client;

    public HttpClient GetClient()
    {
        if (_client == null)
        {
            Console.WriteLine("new client created");
            _client = new WebApplicationFactory<Program>().CreateClient();
            _client.DefaultRequestHeaders.Add("Instance", "79abd14c-63d9-4d31-a68c-fbc91280ad1a");
        }

        if(key != null && !_client.DefaultRequestHeaders.Contains("Authorization"))
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + key);
        
        return _client;
    }

    public void RemoveInstanceHeader()
    {
        _client.DefaultRequestHeaders.Remove("Instance");
    }
    
    public void AddInstanceHeader()
    {
        _client.DefaultRequestHeaders.Add("Instance", "79abd14c-63d9-4d31-a68c-fbc91280ad1a");
    }

    public HttpResponseMessage GetResponse(string url)
    {
        return GetClient().GetAsync(url).Result;
    }

    public HttpResponseMessage PostResponse(string url, object body)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
        return GetClient().PostAsync(url, content).Result;
    }

    private const int MaxLoginAttempts = 3;
    private int loginAttempts = 0;

    public void Login()
    {
        try
        {
            var url = "/api/auth?email=jusraaijmakers@gmail.com&password=Hoezo999!";
            var response = GetResponse(url);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                if (loginAttempts >= MaxLoginAttempts)
                {
                    Console.WriteLine("Max login attempts reached. Aborting.");
                    return;
                }

                Console.WriteLine("Login failed, attempting to register...");
                Register();
                Console.WriteLine("Registration successful, attempting to login again...");

                loginAttempts++;
                Login();
                return;
            }

            string jsonReponse = response.Content.ReadAsStringAsync().Result;

            var token = JsonNode.Parse(jsonReponse)["data"].ToString();
            key = token;

            // Reset login attempts after successful login
            loginAttempts = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during login: " + ex.Message);
        }
    }

    public void Logout()
    {
        key = null;

        if (_client != null)
        {
            _client.DefaultRequestHeaders.Remove("Authorization");
        }
    }

    public void Register()
    {
        try
        {
            var url = "/api/organization";
            var body = new
            {
                email = "jusraaijmakers@gmail.com",
                password = "Hoezo999!",
                repeatPassword = "Hoezo999!",
                organizationName = "Test Organization",
                employees = 3,
                plan = 1,
            };

            var response = PostResponse(url, body);
            
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Failed to register");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during registration: " + ex.Message);
        }
    }
}