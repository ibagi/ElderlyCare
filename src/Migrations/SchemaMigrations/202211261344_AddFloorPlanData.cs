using ElderyCare.Data.Models;
using FluentMigrator;

namespace ElderlyCare.Migrations.SchemaMigrations
{
    [Migration(202211261344)]
    public class AddFloorPlanData : Migration
    {
        public override void Down()
        {
            Execute.Sql("DELETE FROM [dbo].[FloorPlans]");
        }

        public override void Up()
        {
            var defaultFloorPlan = new FloorPlan
            {
                Name = "Default Floor Plan",
                IsDefault = true,
                SvgUrl = @"/assets/default-floorplan.svg"
            };

            Insert.IntoTable("FloorPlans").Row(defaultFloorPlan);
        }
    }
}
