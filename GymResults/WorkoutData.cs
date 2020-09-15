using System;

namespace GymResults
{
    public class WorkoutData
    {
        public int Id
        {
            get; set;
        }
        public User User
        {
            get; set;
        }
        public Exercise Exercise
        {
            get; set;
        }
        public DateTime Date
        {
            get; set;
        }
        public Result Result
        {
            get; set;
        }
    }

}
