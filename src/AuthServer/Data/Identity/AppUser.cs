using Microsoft.AspNetCore.Identity;

namespace AuthServer.Data.Identity {
  public class AppUser : IdentityUser {
    public string Name { get; set; }
  }
}