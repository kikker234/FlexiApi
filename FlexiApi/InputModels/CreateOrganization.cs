namespace FlexiApi.InputModels;

public class CreateOrganization
{
    public String Email { get; set; }
    public String Password { get; set; }
    public String OrganizationName { get; set; }
}