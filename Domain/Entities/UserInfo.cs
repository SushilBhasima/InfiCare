using Microsoft.AspNetCore.Identity;

namespace InfiCare.Domain.Entities;

public class UserInfo : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
