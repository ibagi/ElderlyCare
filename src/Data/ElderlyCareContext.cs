using ElderyCare.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElderyCare.Data
{
    public class ElderlyCareContext : DbContext
    {
        public DbSet<Robot> Robots { get; set; }
        public DbSet<RobotState> RobotStates { get; set; }
        public DbSet<FloorPlan> FloorPlans { get; set; }

        public ElderlyCareContext(DbContextOptions options) :
            base(options)
        {

        }

        internal void EnsureItsMigratedAndSeeded()
        {
            Database.EnsureCreated();

            var alreadySeeded = FloorPlans.Any();

            if (!alreadySeeded)
            {
                SeedWithInitialData();
            }
        }

        private void SeedWithInitialData()
        {
            const int numberOfRobots = 5;
            var opcUrl = "opc.tcp://opcua_server:4840/";

            for (int i = 0; i < numberOfRobots; i++)
            {
                var robotId = $"Robot0000{i + 1}";

                Robots.Add(new Robot
                {
                    Name = robotId,
                    OpcId = robotId,
                    OpcUrl = opcUrl
                });
            }

            var defaultFloorPlan = new FloorPlan
            {
                Name = "Default Floor Plan",
                IsDefault = true,
                SvgUrl = @"/assets/default-floorplan.svg"
            };

            FloorPlans.Add(defaultFloorPlan);
            SaveChanges();
        }
    }
}