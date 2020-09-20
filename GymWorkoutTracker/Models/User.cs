using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymWorkoutTracker.Models
{
    public class User
    {
        public int UserId {
            get; set;
        }
        public string UserName {
            get; set;
        }
        public ICollection<Result> Results {
            get; set;
        }
    }
}
