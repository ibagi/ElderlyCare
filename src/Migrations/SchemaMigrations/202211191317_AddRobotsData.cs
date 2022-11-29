using ElderyCare.Data.Models;
using FluentMigrator;

namespace ElderlyCare.Migrations.SchemaMigrations
{
    [Migration(202211191317)]
    public class AddRobotsData : Migration
    {
        public override void Down()
        {
            Execute.Sql("DELETE FROM [dbo].[Robots]");
        }

        public override void Up()
        {
            const int numberOfRobots = 5;
            var opcUrl = "opc.tcp://localhost:4840/";

            for (int i = 0; i < numberOfRobots; i++)
            {
                var robotId = $"Robot0000{i + 1}";

                Insert.IntoTable("Robots").Row(new
                {
                    Id = Guid.NewGuid(), 
                    Name = robotId, 
                    OpcId = robotId, 
                    OpcUrl = opcUrl 
                });
            }
        }
    }
}
