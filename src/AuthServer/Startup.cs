using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AuthServer.Data.Identity;
using IdentityServer4.EntityFramework.DbContexts;
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

    public IConfiguration Configuration { get; }
    private AppDbSettings appDbSettings { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {
      services.AddControllersWithViews ();

      services.Configure<AppDbSettings> (Configuration.GetSection ("ConnectionString"));

      appDbSettings = Configuration.GetSection ("AuthServer").Get<AppDbSettings> ();

      services.AddDbContext<AppIdentityDbContext> (options => options.UseSqlServer (appDbSettings.ConnectionString));

      services.AddIdentity<AppUser, IdentityRole> ()
        .AddEntityFrameworkStores<AppIdentityDbContext> ()
        .AddDefaultTokenProviders ();
  
      services.AddIdentityServer ()
        .AddOperationalStore (options => {

          options.ConfigureDbContext = builder => builder.UseSqlServer (appDbSettings.ConnectionString);
          options.EnableTokenCleanup = true;
          // options.TokenCleanupInterval = 30;
        })
        .AddInMemoryIdentityResources (Config.GetIdentityResources ())
        .AddInMemoryApiResources (Config.GetApiResources ())
        .AddInMemoryClients (Config.GetClients ())
        .AddAspNetIdentity<AppUser> ()
        .AddDeveloperSigningCredential ();
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
      app.UseHttpsRedirection ();
      app.UseStaticFiles ();
      app.UseIdentityServer ();

      app.UseRouting ();

      app.UseAuthorization ();

      app.UseEndpoints (endpoints => {
        endpoints.MapControllerRoute (
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}