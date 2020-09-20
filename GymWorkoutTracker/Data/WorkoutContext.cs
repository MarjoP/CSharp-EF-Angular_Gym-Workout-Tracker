using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymWorkoutTracker.Models;

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


        public User GetUser(string name)
        {
            var user = this.Users.Where(user => user.UserName == name).FirstOrDefault<User>();
            return user;
        }
        public Boolean CreateUser(String name)
        {
            if (GetUser(name) == null)
            {
                Users.Add(new User() { UserName = name });
                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetAllUsers()
        {
            if (!Users.Any())
            {
                return null;
            }
            else
            {
                return Users.Select(s => s.UserName).ToList();
            }
        }
        public Exercise GetExercise(string name)
        {
            var exercise = this.Exercises.Where(exer => exer.ExerciseName == name).FirstOrDefault<Exercise>();
            return exercise;
        }
        public Boolean CreateExercise(String name)
        {
            if (GetExercise(name) == null)
            {
                Exercises.Add(new Exercise() { ExerciseName = name });
                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> GetAllExercises()
        {
            if (!Exercises.Any())
            {
                return null;
            }
            else
            {
                return Exercises.Select(s => s.ExerciseName).ToList();
            }
        }
        public Boolean AddResult(User user, Exercise exer, int reps, int weight, DateTime date)
        {
            try
            {
                Results.Add(new Result()
                {
                    User = user,
                    Exercise = exer,
                    Repeats = reps,
                    Weight = weight,
                    Date = date
                });
                SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void HelperMethod1()
        {
            foreach (var User in Users)
            {
            // do nothing
            }
        }
        public void HelperMethod2()
        {
            foreach (var Exercise in Exercises)
            {
                // do nothing
            }
        }

        //TODO Eroon helpermetodeista?
        public List<Result> GetAllResults()
        {
            HelperMethod1();
            HelperMethod2();
            if (!Results.Any())
            {
                return null;
            }
            else
            {
                return Results.ToList();
            }
        }

        public List<Result> GetSelectedResults(string name, string exer, int qty)
        {
            HelperMethod1();
            HelperMethod2();
            var sortedRecord = new List<Result>();
            if (name == "")
            {
                sortedRecord = Results.OrderByDescending(s => s.Date).ToList();
            }
            else
            {
                sortedRecord = Results.Where(s => s.User.UserName == name).ToList();
            }
            if (exer != "")
            {
                sortedRecord = sortedRecord.Where(x => x.Exercise.ExerciseName == exer).TakeLast(qty).ToList();
            }

            else
            {
                sortedRecord = sortedRecord.TakeLast(qty).ToList();
            }
            return sortedRecord;
       
        }
    }
}