# ExampleIdentityServer4
## Code สำหรับจัดการเรื่อง Migrations
### dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
### dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
### dotnet ef migrations add InitialAppUser -c AppIdentityDbContext -o Migrations/IdentityServer/AppIdentityDb
### dotnet ef database update -c AppIdentityDbContext