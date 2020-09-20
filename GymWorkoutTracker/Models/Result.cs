using System;

namespace GymWorkoutTracker.Models
{
    public class Result
    {
        public int Id {
            get; set;
        }
        public int MyProperty {
            get; set;
        }
        public User User {
            get; set;
        }
        public Exercise Exercise {
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
