# ExampleIdentityServer4
## Code สำหรับจัดการเรื่อง Migrations
### dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
### dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
### dotnet ef migrations add InitialAppUser -c AppIdentityDbContext -o Migrations/IdentityServer/AppIdentityDb
### dotnet ef database update -c PersistedGrantDbContext
### dotnet ef database update -c ConfigurationDbContext
### dotnet ef database update -c AppIdentityDbContext

# Credit as below
### Link: https://github.com/IdentityModel/oidc-client-js/wiki
### Link: https://identityserver4.readthedocs.io/en/latest/
### Link: https://fullstackmark.com/post/21/user-authentication-and-identity-with-angular-aspnet-core-and-identityserver