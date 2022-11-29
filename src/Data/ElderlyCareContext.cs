using ElderyCare.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ElderyCare.Data
{
    public class ElderlyCareContext : DbContext
    {
        public DbSet<Robot> Robots { get; set; }
        public DbSet<RobotState> RobotStates { get; set; }
        public DbSet<FloorPlan> FloorPlans { get; set; }

        public ElderlyCareContext(DbContextOptions options):
            base(options)
        {

        }
    }
}