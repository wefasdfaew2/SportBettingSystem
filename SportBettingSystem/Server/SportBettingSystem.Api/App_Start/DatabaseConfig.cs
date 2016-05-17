namespace SportBettingSystem.Api.App_Start
{
    using System.Data.Entity;

    using Data;
    using Data.Helpers;
    using Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportBettingSystemDbContext, Configuration>());
            SportBettingSystemDbContext.Create().Database.Initialize(true);
        }

        public static void Populate()
        {
            DatabaseHelper.GetInstance.Populate();
        }
    }
}