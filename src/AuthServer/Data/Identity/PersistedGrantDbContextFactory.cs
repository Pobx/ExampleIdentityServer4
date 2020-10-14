using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AuthServer.Data.Identity {
  public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext> {

    private const string connectionString ="Server=localhost,1433;Database=AuthServer;User Id=sa;Password=P@ssword1234;";
    public PersistedGrantDbContext CreateDbContext (string[] args) {
      var configuration = new ConfigurationBuilder();
      var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext> ();
      optionsBuilder.UseSqlServer (connectionString,
        sql => sql.MigrationsAssembly (typeof (PersistedGrantDbContextFactory).GetTypeInfo ().Assembly.GetName ().Name));
      return new PersistedGrantDbContext (optionsBuilder.Options, new OperationalStoreOptions ());
    }
  }
}