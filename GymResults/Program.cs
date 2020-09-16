using System;
using System.ComponentModel.Design;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Security.Cryptography;

namespace GymResults
{
    class Program
    {
        //Uuden käyttäjän lisääminen
        public static void AddUser(GymContext context)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            if (GetUser(context, name) == null)
            {
                context.Users.Add(new User() { UserName = name });
                context.SaveChanges();
                Console.WriteLine($"New user '{name}' succesfully added to database.");
            }
            else
            {
                Console.WriteLine($"Could not add new user! Username {name} already exists. \n" +
                    $"Try again with different name\n");
            }
        }
        //Käyttäjien listaus
        public static void ListUsers(GymContext context)
        {
            if (!context.Users.Any())
            {
                Console.WriteLine("No stored user profiles.");
            }
            else
            {

                Console.WriteLine("Stored user profiles:");
                foreach (var User in context.Users)
                {
                    Console.WriteLine(User.UserName);
                }
            }
        }

        //Hae tietty käyttäjä, palauttaa null jos käyttäjää ei löydy
        public static User GetUser(GymContext context, string name)
        {
            var tyyppi = context.Users.Where(user => user.UserName == name).FirstOrDefault<User>();
            return tyyppi;
        }

        //uuden lajin lisääminen
        public static void AddExercise(GymContext context)
        {
            Console.Write("Name of exercise: ");
            string name = Console.ReadLine();
            if (GetExercise(context, name) == null)
            {
                context.Exercises.Add(new Exercise() { ExerciseName = name });
                context.SaveChanges();
                Console.WriteLine($"New exercise '{name}' succesfully added to database");
            }
            else
            {
                Console.WriteLine($"Could not add new exercise! Name {name} already exists. \n" +
                    $"Try again with different name");
            }
        }

        public static void ListExercises(GymContext context)
        {
            if (!context.Exercises.Any())
            {
                Console.WriteLine("No stored exercises");
            }
            else
            {
                Console.WriteLine("Existing exercises:");
                foreach (var Exercise in context.Exercises)
                {
                    Console.WriteLine(Exercise.ExerciseName);
                }
            }
        }

        //Hae tietty harjoitus, palauttaa null jos harjoitusta ei löydy
        public static Exercise GetExercise(GymContext context, string name)
        {
            var exer = context.Exercises.Where(ex => ex.ExerciseName == name).FirstOrDefault<Exercise>();
            return exer;
        }
        
        public static void AddResult(GymContext context)
        {
            Console.Write("UserName: ");
            string name = Console.ReadLine();
            if (GetUser(context, name) == null)
            {
                Console.WriteLine($"Could not found user with name {name}");
            }
            else
            {
                Console.Write("Exercise: ");
                string exer = Console.ReadLine();
                if (GetExercise(context, exer) == null)
                {
                    Console.WriteLine($"Could not found exercise {exer}");
                }
                else
                {
                    //TODO toiminta tapauksessa jossa käyttäjän syöte ei ole numero
                    Console.Write("How many repeats: ");
                    int repeats = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Weight amount: ");
                    int weight = Convert.ToInt32(Console.ReadLine());

                    Result result = new Result()
                    {
                        Repeats = repeats,
                        Weight = weight
                    };
                    //TODO Default-pvm "tämä päivä" mutta käyttäjä voi valita myös aiemman päivämäärän käyttöliittymän avulla
                    DateTime dateToday = DateTime.Now;

                    WorkoutData trainingData = new WorkoutData()
                    {
                        User = GetUser(context, name),
                        Exercise = GetExercise(context, exer),
                        Date = dateToday.Date,
                        Result = result
                    };

                    context.WorkoutDatas.Add(trainingData);
                    context.SaveChanges();
                }
            }
        }
        //Listaa kaikki tulokset
        //TODO tulosten haku käyttäjän, lajin, max-saavutusten jne. perusteella
        //Tietyn lajin & käyttäjän Maksimi-tulosten haku kuvaajaan (käyttäjän kehityskaari)
        public static void ListResults(GymContext context)
        {
            if (!context.WorkoutDatas.Any())
            {
                Console.WriteLine("No stored workout results.");
            }
            else
            {
                Console.WriteLine("Results:");
                Console.WriteLine("Date \t\t User \t\t Exercise \t\t result");
                foreach (var result in context.WorkoutDatas)
                {
                    Console.WriteLine($"{result.Date.ToShortDateString()} \t\t {result.User.UserName} \t\t {result.Exercise.ExerciseName} \t\t {result.Result.Repeats} x {result.Result.Weight}");
                }
            }
        }
        static void Main(string[] args)
        {

            using (var context = new GymContext())
            {
                Console.WriteLine("This is the beginning of a beautiful and functional app\n");
                Console.WriteLine("Select action\n");
                Console.WriteLine("0: Quit \n1: Add new user \t\t2: List users \n" +
                    "3: Add new exercise \t\t4: List exercises \n5: Add result \t\t\t6: List results");
                while (true)
                {
                    string action = Console.ReadLine();

                    if (action == "0")
                    {
                        break;
                    }

                    switch (action)
                    {
                        case "1":
                            AddUser(context);
                            break;

                        case "2":
                            ListUsers(context);
                            break;

                        case "3":
                            AddExercise(context);
                            break;

                        case "4":
                            ListExercises(context);
                            break;

                        case "5":
                            AddResult(context);
                            break;

                        case "6":
                            ListResults(context);
                            break;

                        default:
                            Console.WriteLine($"Please, select the action from the list. Your input '{action}' is not a valid command.");
                            break;
                    }
                    Console.Write("\nSelect action: ");
                }

            }
        }



    }
}





