using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AuthServer.Data.Identity;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthServer {
  public class Startup {
    public Startup (IConfiguration configuration) {
      Configuration = configuration;
    }

    public Startup (IConfiguration configuration, AppDbSettings appDbSettings) {
      this.Configuration = configuration;
      this.appDbSettings = appDbSettings;

    }
    public IConfiguration Configuration { get; }
    private AppDbSettings appDbSettings { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {
      services.AddControllersWithViews ();

      // services.Configure<AppDbSettings> (Configuration.GetSection ("ConnectionString"));

      appDbSettings = Configuration.GetSection ("AuthServer").Get<AppDbSettings> ();
      var migrationsAssembly = typeof (Startup).GetTypeInfo ().Assembly.GetName ().Name;

      services.AddDbContext<AppIdentityDbContext> (options => options.UseSqlServer (appDbSettings.ConnectionString));

      services.AddIdentity<AppUser, IdentityRole> (options => {
          options.Password.RequireDigit = false;
          options.Password.RequireLowercase = false;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireUppercase = false;
          // options.Password.RequiredUniqueChars = false;
          options.Password.RequiredLength = 4;
        })
        .AddEntityFrameworkStores<AppIdentityDbContext> ()
        .AddDefaultTokenProviders ();

      services.AddIdentityServer ()
        // .AddOperationalStore (options => {

        //   options.ConfigureDbContext = builder => builder.UseSqlServer (appDbSettings.ConnectionString);
        //   options.EnableTokenCleanup = true;
        //   options.TokenCleanupInterval = 30;
        // })
        .AddInMemoryIdentityResources (Config.IdentityResources)
        .AddInMemoryApiResources (Config.ApiResources)
        .AddInMemoryClients (Config.Clients)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddAspNetIdentity<AppUser> ()
      .AddDeveloperSigningCredential ();

      // services.AddIdentity<AppUser, IdentityRole> (options => {
      //     options.Password.RequireDigit = false;
      //     options.Password.RequireLowercase = false;
      //     options.Password.RequireNonAlphanumeric = false;
      //     options.Password.RequireUppercase = false;
      //     // options.Password.RequiredUniqueChars = false;
      //     options.Password.RequiredLength = 4;
      //   })
      //   .AddEntityFrameworkStores<AppIdentityDbContext> ()
      //   .AddDefaultTokenProviders ();

      // services.AddIdentityServer (options => {
      //     // options.UserInteraction.LoginUrl = "http://localhost:4200/login";
      //   })
      //   .AddAspNetIdentity<AppUser> ()
      //   .AddConfigurationStore (options => {
      //     options.ConfigureDbContext = b => b.UseSqlServer (appDbSettings.ConnectionString, sql => sql.MigrationsAssembly (migrationsAssembly));
      //   })
      //   .AddOperationalStore (options => {
      //     options.ConfigureDbContext = b => b.UseSqlServer (appDbSettings.ConnectionString, sql => sql.MigrationsAssembly (migrationsAssembly));
      //   })
      //   .AddDeveloperSigningCredential ();

      services.AddCors (options => options.AddPolicy ("AllowAll", p => p.AllowAnyOrigin ()
        .AllowAnyMethod ()
        .AllowAnyHeader ()));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
      } else {
        app.UseExceptionHandler ("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts ();
      }

      // InitializeDatabase (app);

      // app.UseHttpsRedirection ();
      app.UseStaticFiles ();
      app.UseCors ("AllowAll");
      app.UseIdentityServer ();

      app.UseRouting ();

      app.UseAuthorization ();

      app.UseEndpoints (endpoints => {
        endpoints.MapControllerRoute (
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }

    private void InitializeDatabase (IApplicationBuilder app) {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory> ().CreateScope ()) {
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext> ().Database.Migrate ();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext> ();
        context.Database.Migrate ();
        if (!context.Clients.Any ()) {
          foreach (var client in Config.Clients) {
            context.Clients.Add (client.ToEntity ());
          }
          context.SaveChanges ();
        }

        if (!context.IdentityResources.Any ()) {
          foreach (var resource in Config.IdentityResources) {
            context.IdentityResources.Add (resource.ToEntity ());
          }
          context.SaveChanges ();
        }

        if (!context.ApiScopes.Any ()) {
          foreach (var resource in Config.ApiScopes) {
            context.ApiScopes.Add (resource.ToEntity ());
          }
          context.SaveChanges ();
        }

        if (!context.ApiResources.Any ()) {
          foreach (var resource in Config.ApiResources) {
            context.ApiResources.Add (resource.ToEntity ());
          }
          context.SaveChanges ();
        }

      }
    }

  }
}