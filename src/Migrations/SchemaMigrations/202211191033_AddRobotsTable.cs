using ElderyCare.Data.Models;
using FluentMigrator;

namespace ElderlyCare.Migrations.SchemaMigrations
{
    [Migration(202211191033)]
    public class AddRobotsTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Robots");
        }

        public override void Up()
        {
            Create.Table("Robots")
                .WithColumn(nameof(Robot.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(Robot.Name)).AsString()
                .WithColumn(nameof(Robot.OpcUrl)).AsString()
                .WithColumn(nameof(Robot.OpcId)).AsString()
                .WithColumn(nameof(Robot.Created)).AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn(nameof(Robot.Updated)).AsDateTime().Nullable()
                .WithColumn(nameof(Robot.Deleted)).AsBoolean().WithDefaultValue(false);
        }
    }
}
