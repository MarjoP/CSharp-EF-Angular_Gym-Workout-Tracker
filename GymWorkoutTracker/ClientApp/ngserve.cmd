using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymWorkoutTracker.ClientApp
{
    public class ngserve
    {
    @echo ** Angular Live Development Server is listening on localhost:%~2, open your browser on http://localhost:%~2/ **
ng serve %1 %~2
    }
}
