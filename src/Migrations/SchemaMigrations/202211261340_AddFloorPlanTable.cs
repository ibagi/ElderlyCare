using ElderyCare.Data.Models;
using FluentMigrator;

namespace ElderlyCare.Migrations.SchemaMigrations
{
    [Migration(202211261340)]
    public class AddFloorPlanTable : Migration
    {
        public override void Down()
        {
            Delete.Table("FloorPlans");
        }

        public override void Up()
        {
            Create.Table("FloorPlans")
                .WithColumn(nameof(FloorPlan.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(FloorPlan.Name)).AsString()
                .WithColumn(nameof(FloorPlan.SvgUrl)).AsString()
                .WithColumn(nameof(FloorPlan.IsDefault)).AsBoolean()
                .WithColumn(nameof(FloorPlan.Created)).AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn(nameof(FloorPlan.Updated)).AsDateTime().Nullable()
                .WithColumn(nameof(FloorPlan.Deleted)).AsBoolean().WithDefaultValue(false);
        }
    }
}
