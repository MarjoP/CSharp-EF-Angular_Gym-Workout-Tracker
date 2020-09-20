using System.Collections.Generic;

namespace GymWorkoutTracker.Models
{
    public class Exercise
    {
        public int ExerciseId {
            get; set;
        }
        public string ExerciseName {
            get; set;
        }
        public ICollection<Result> Results {
            get; set;
        }
    }

}
