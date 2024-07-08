using System.ComponentModel.DataAnnotations;

namespace FlexiApi.InputModels;

public class CreateOrganization
{
    public String Email { get; set; }
    public String Password { get; set; }
    public String RepeatPassword { get; set; }
    public String OrganizationName { get; set; }
    public int Employees { get; set; }
    public int Plan { get; set; }
    
    public override string ToString()
    {
        return $"Email: {Email}, Password: {Password}, RepeatPassword: {RepeatPassword}, OrganizationName: {OrganizationName}, Employees: {Employees}, Plan: {Plan}";
    }
}