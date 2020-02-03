dotnet ef migrations add InitialDbMigration -c DatabaseContext -o Application/Database/Migrations/Database
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Application/Database/Migrations/ConfigurationDb
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Application/Database/Migrations/PersistedGrantDb
