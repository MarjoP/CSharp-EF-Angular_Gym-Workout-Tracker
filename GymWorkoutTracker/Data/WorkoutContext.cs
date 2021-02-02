using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymWorkoutTracker.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GymWorkoutTracker.Data
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions<WorkoutContext> options)
            : base(options)
        {
        }

        public DbSet<GymWorkoutTracker.Models.User> Users {
            get; set;
        }
        public DbSet<GymWorkoutTracker.Models.Result> Results {
            get; set;
        }
        public DbSet<GymWorkoutTracker.Models.Exercise> Exercises {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany<Result>(e => e.Results)
                 .WithOne(a => a.User)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>().HasOne<User>(b => b.User)
                .WithMany(d => d.Results)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Exercise>().HasMany<Result>(g => g.Results)
                .WithOne(h => h.Exercise)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public async Task<Boolean> CreateUser(String name)
        {
            if (!String.IsNullOrEmpty(name) &&  Users.Where(user => user.UserName == name).FirstOrDefault<User>() == null)
            {
                Users.Add(new User() { UserName = name });
                await SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> CreateExercise(String name)
        {
            if (!String.IsNullOrEmpty(name) && Exercises.Where(exer => exer.ExerciseName == name).FirstOrDefault<Exercise>() == null)
            {
                Exercises.Add(new Exercise() { ExerciseName = name });
                await SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}