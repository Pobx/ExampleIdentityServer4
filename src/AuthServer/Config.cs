using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthServer {
  // public class Config {

  //   public static IEnumerable<IdentityResource> GetIdentityResources () {
  //     return new List<IdentityResource> {
  //       new IdentityResources.OpenId (),
  //       new IdentityResources.Email (),
  //       new IdentityResources.Profile (),
  //     };
  //   }

  //   public static IEnumerable<ApiResource> GetApiResources () {
  //     return new List<ApiResource> {
  //       new ApiResource ("resourceapi", "Resource API") {
  //         Scopes = new List<string> () {
  //           "api.read"
  //           }
  //           }
  //     };
  //   }

  //   public static IEnumerable<Client> GetClients () {
  //     return new [] {
  //       new Client {
  //         RequireConsent = false,
  //           ClientId = "angular_spa",
  //           ClientName = "Angular SPA",
  //           AllowedGrantTypes = GrantTypes.Implicit,
  //           AllowedScopes = { "openid", "profile", "email", "api.read" },
  //           RedirectUris = { "http://localhost:4200/auth-callback" },
  //           PostLogoutRedirectUris = { "http://localhost:4200/" },
  //           AllowedCorsOrigins = { "http://localhost:4200" },
  //           AllowAccessTokensViaBrowser = true,
  //           AccessTokenLifetime = 3600
  //           }
  //     };
  //   }

  // }

  public static class Config {

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope> {
      new ApiScope ("api1", "My Api")
    };

    public static IEnumerable<ApiResource> ApiResources =>
      new List<ApiResource> {
        new ApiResource ("resourceapi", "Resource API") {
        Scopes = new List<string> () {
        "api1"
        }
        }
      };

    public static IEnumerable<Client> Clients => new List<Client> {
      new Client {
      ClientId = "client",
      // ไม่ต้อง Interactive กับ user. ใช้ clientid/secret สำหรับ authentication น่าจะเหมาะกับ machine to machine
      AllowedGrantTypes = GrantTypes.ClientCredentials,
      // Secret สำหรับ Authentication
      ClientSecrets = {
      new Secret ("secret".Sha256 ())
      },
      // Scopes ที่ client สามารถเข้าถึงได้
      AllowedScopes = { "api1" }

      },

      new Client {
      RequireConsent = false,
      ClientId = "angular_spa",
      ClientName = "Angular SPA",
      AllowedGrantTypes = GrantTypes.Implicit,
      AllowedScopes = { "openid", "profile", "email", "api1" },
      RedirectUris = { "http://localhost:4200/auth-callback" },
      PostLogoutRedirectUris = { "http://localhost:4200/" },
      AllowedCorsOrigins = { "http://localhost:4200" },
      AllowAccessTokensViaBrowser = true,
      AccessTokenLifetime = 3600
      }
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
      new List<IdentityResource> {
        new IdentityResources.OpenId (),
        new IdentityResources.Profile (),
        new IdentityResources.Email()
      };

  }

}