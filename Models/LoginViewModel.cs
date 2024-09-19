using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfiCare.Models;

public class LoginViewModel
{
    //[Required]
    //[EmailAddress]
    //public required string Email { get; set; }

    [Display(Name = "UserName")]
    public required string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DisplayName("Remember me?")]
    public bool RememberMe { get; set; }
}
