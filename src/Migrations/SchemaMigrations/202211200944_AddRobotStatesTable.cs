using ElderyCare.Data.Models;
using FluentMigrator;

namespace ElderlyCare.Migrations.SchemaMigrations
{
    [Migration(202211200944)]
    public class AddRobotStatesTable : Migration
    {
        public override void Down()
        {
            Delete.Table("RobotStates");
        }

        public override void Up()
        {
            Create.Table("RobotStates")
                .WithColumn(nameof(RobotState.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(RobotState.RobotId)).AsGuid().ForeignKey()
                .WithColumn(nameof(RobotState.RobotName)).AsString()
                .WithColumn(nameof(RobotState.State)).AsString()
                .WithColumn(nameof(RobotState.Created)).AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn(nameof(RobotState.Updated)).AsDateTime().Nullable()
                .WithColumn(nameof(RobotState.Deleted)).AsBoolean().WithDefaultValue(false);

            Create.ForeignKey("FK_RobotStates_Robots")
                .FromTable("RobotStates").ForeignColumn("RobotId")
                .ToTable("Robots").PrimaryColumn("Id");
        }
    }
}
