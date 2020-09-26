using System;

namespace GymWorkoutTracker.Models
{
    public class SimpleResult
    {
        public int Id {
            get; set;
        }
      
        public string UserName {
            get; set;
        }
        public string ExerciseName {
            get; set;
        }
        public DateTime Date {
            get; set;
        }
        public int Repeats {
            get; set;
        }
        public int Weight {
            get; set;
        }
    }

}
