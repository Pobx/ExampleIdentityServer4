using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Data.Identity {
  public class AppIdentityDbContext : IdentityDbContext<AppUser> {
    public AppIdentityDbContext (DbContextOptions<AppIdentityDbContext> options) : base (options) {

    }

    // protected override void OnModelCreating (ModelBuilder builder) {
    //   base.OnModelCreating (builder);
    //   builder.Entity<IdentityRole> ().HasData (new IdentityRole { Name = Constants.Roles.Consumer, NormalizedName = Constants.Roles.Consumer.ToUpper () });
    // }

  }
}