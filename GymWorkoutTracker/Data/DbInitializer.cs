using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GymWorkoutTracker.Data
{
    public class DbInitializer
    {

        public static void Initialize(WorkoutContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

        }
    }

}
