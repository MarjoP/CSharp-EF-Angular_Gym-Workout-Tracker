using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Security.Cryptography;

namespace GymResults
{

    //TODO Muuta käyttöliittymä-puoli ja tietokantakäsittely erilleen?
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
        public static void HelperMethod1(GymContext context)
        {
            foreach (var User in context.Users)
            {
            //do nothing
            }
        }
        public static void HelperMethod2(GymContext context)
        {
            foreach (var Exercise in context.Exercises)
            {
                //do nothing
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
                    Console.WriteLine("New result added!");
                }
            }
        }
        //Listaa kaikki tulokset

        public static void ListResults(GymContext context)
        {
            HelperMethod1(context);
            HelperMethod2(context);

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
                      Console.WriteLine($"{result.Date.ToShortDateString()} \t {result.User?.UserName} \t\t {result.Exercise?.ExerciseName} \t\t {result.Result.Repeats} x {result.Result.Weight}");
               
                }
            }
        }

        //TODO Tietyn lajin & käyttäjän Maksimi-tulosten haku kuvaajaan (käyttäjän kehityskaari)
        //TODO lisää mahdollisuus valita useita käyttäjiä tai lajeja
        public static void GetSelectedResults(GymContext context)
        {
            HelperMethod1(context);
            HelperMethod2(context);
            var sortedRecord = new List<WorkoutData>();
            NameBeginning:
            Console.WriteLine("\nGive username (leave the field blank to get the results for all users");
            string user = Console.ReadLine();
            if (user == "")
            {
                sortedRecord = context.WorkoutDatas.ToList();
            }
            else if 
            (GetUser(context, user) == null)
            {
                Console.WriteLine($"Could not found user with name {user}");
                goto NameBeginning;
            }
            else
            {
                sortedRecord = context.WorkoutDatas.Where(s => s.User.UserName == user).ToList();
            }

            Console.WriteLine("Give name of the exercise: \n(leave the field blank to get results for all exercises): ");
            string exercise = Console.ReadLine(); 

            if (exercise == "")
            {
                //ei muutosta
            } 
            else if (GetExercise(context, exercise) == null)
            {
                Console.WriteLine($"Could not found exercise with name {exercise}");
            }
            else
            {
                sortedRecord = sortedRecord.Where(s => s.Exercise.ExerciseName == exercise).ToList();
            }

            Console.WriteLine("How many records: \n(leave this as blank if you want all records to be shown): ");
            string quantity = Console.ReadLine(); 
            int amount;
            if (quantity == "")
            {
             //ei muutosta
            }
            else if (!Int32.TryParse(quantity, out amount))
            {
                Console.WriteLine($"Invalid user input, {quantity} is not a number");
            }
            else
            {
                sortedRecord = sortedRecord.TakeLast<WorkoutData>(amount).ToList();
            }

            if (sortedRecord == null)
            {
                Console.WriteLine("No records found");
            }
            else
            {
                Console.WriteLine("Results:");
                Console.WriteLine("Date \t\t User \t\t Exercise \t\t result");
                foreach (var result in sortedRecord)
                {
                    Console.WriteLine($"{result.Date.ToShortDateString()} \t {result.User?.UserName} \t\t {result.Exercise?.ExerciseName} \t\t {result.Result.Repeats} x {result.Result.Weight}");
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
                "3: Add new exercise \t\t4: List exercises \n5: Add result \t\t\t6: List all results \n" +
                "7: List results based on selection: user/exercise/latest/max-results (this selection is under work...)");
            while (true)
            {
                string action = Console.ReadLine();

                if (action == "0")
                    break;

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

                    case "7":
                        GetSelectedResults(context);
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





