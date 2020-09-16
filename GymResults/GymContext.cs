using System.Data.Entity;
using System;

namespace GymResults
{
    public class GymContext : DbContext
    {
        public GymContext() : base()
        {
           // Database.SetInitializer<GymContext>(new CreateDatabaseIfNotExists<GymContext>());
            Database.SetInitializer<GymContext>(new DropCreateDatabaseIfModelChanges<GymContext>());
           // Database.SetInitializer<GymContext>(new DropCreateDatabaseAlways<GymContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutData> WorkoutDatas {get; set; }
    }

}
