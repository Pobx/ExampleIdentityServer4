using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models {

  public class LoginRequestViewModel {

    [Required]
    [EmailAddress]
    [Display (Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength (100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
    [DataType (DataType.Password)]
    [Display (Name = "Password")]
    public string Password { get; set; }
  }
}