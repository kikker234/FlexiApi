using Data.Models.components;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public class User : IdentityUser
{
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}