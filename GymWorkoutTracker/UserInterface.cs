using GymWorkoutTracker.Data;
using GymWorkoutTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GymWorkoutTracker
{

    //this is no longer up to date
    public class UserInterface
    {
        public WorkoutContext context;
        public UserInterface()
        {
        }

        public void Start(WorkoutContext ctx)
        {
            this.context = ctx;
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
                        AddUser();
                        break;

                    case "2":
                        ListAllUsers();
                        break;

                    case "3":
                        AddExercise();
                        break;

                    case "4":
                        ListAllExercises();
                        break;
                    case "5":
                        AddResult();
                        break;

                    case "6":
                        ListAllResults();
                        break;

                    case "7":
                        ListSelectedResults();
                        break;

                    default:
                        Console.WriteLine($"Please, select the action from the list. Your input '{action}' is not a valid command.");
                        break;
                }

                Console.Write("\nSelect action: ");
            }
        }


        public void AddUser()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            if (context.CreateUser(name))
            {
                Console.WriteLine($"New user '{name}' succesfully added to database.");
            }
            else
            {
                Console.WriteLine($"Could not add new user! Username '{name}' already exists. \n" +
                        $"Try again with different name\n");
            }
        }
        public void ListAllUsers()
        {
            var userNames = context.GetAllUsers();
            //riittääkö että palauttaa pelkän nimen vai tarviiko palauttaa koko user johonkin muuhun?
            if (userNames == null)
            {
                Console.WriteLine("No stored user profiles.");
            }
            else
            {
                foreach (var name in userNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
        public void AddExercise()
        {
            Console.Write("Name of the exercise: ");
            string name = Console.ReadLine();

            if (context.CreateExercise(name))
            {
                Console.WriteLine($"New exercise '{name}' succesfully added to database.");
            }
            else
            {
                Console.WriteLine($"Could not add new exercise! Exercise '{name}' already exists. \n" +
                        $"Try again with different name\n");
            }
        }
        public void ListAllExercises()
        {
            var exerciseNames = context.GetAllExercises();
            //riittääkö että palauttaa pelkän nimen vai tarviiko palauttaa koko user johonkin muuhun?
            if (exerciseNames == null)
            {
                Console.WriteLine("No stored exercises.");
            }
            else
            {
                foreach (var name in exerciseNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
        public void AddResult()
        {

            Console.Write("Name of the user: ");
            string name = Console.ReadLine();
            var user = context.GetUser(name);

            if (user == null)
            {
                Console.WriteLine($"Could not found user with name {name}");
            }
            else
            {
                int repeats = 0;
                int weight = 0;
                Console.Write("Name of the exercise: ");
                string exerciseName = Console.ReadLine();
                var exercise = context.GetExercise(exerciseName);

                if (exercise == null)
                {
                    Console.WriteLine($"Could not found exercise {exerciseName}");
                }
                else
                {
                    //TODO paluu nappi jos ei haluakaan laittaa tuloksia. ts. keskeytä toiminto
                    //TODO Default-pvm "tämä päivä" mutta käyttäjä voi valita myös aiemman päivämäärän käyttöliittymän avulla
                    while (true)
                    {
                        Console.Write("How many repeats: ");
                        string repInput = Console.ReadLine();

                        if (int.TryParse(repInput, out repeats))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"'{repInput} is not a valid number. Please add numeric value for repeats");
                        }
                    }
                    while (true)
                    {
                        Console.Write("Weight amount: ");
                        string weightInput = Console.ReadLine();

                        if (int.TryParse(weightInput, out weight))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"'{weightInput} is not a valid number. Please add numeric value for weight");
                        }
                    }

                    var dateNow = DateTime.Now.Date;

                    if (context.AddResult(user, exercise, repeats, weight, dateNow))
                    {
                        Console.WriteLine("New result added!");
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong and the result could not be added. Please, try again");
                    }
                }
            }
        }
        public void ListAllResults()
        {
            var allResults = context.GetAllResults();
            if (allResults == null)
            {
                Console.WriteLine("No stored workout results.");
            }
            else
            {
                Console.WriteLine("Results:");
                Console.WriteLine("Date \t\t User \t\t Exercise \t\t result");
                foreach (var result in allResults)
                {
                    Console.WriteLine($"{result.Date.ToShortDateString()} \t {result.User?.UserName} \t\t {result.Exercise?.ExerciseName} \t\t {result.Repeats} x {result.Weight}");
                }
            }
        }
        //TODO Tietyn lajin & käyttäjän Maksimi-tulosten haku kuvaajaan (käyttäjän kehityskaari)
        //TODO lisää mahdollisuus valita useita käyttäjiä tai lajeja
        public void ListSelectedResults()
        {
            Console.WriteLine("\nGive username (leave the field blank to get the results for all users");
            string user = Console.ReadLine();
            Console.WriteLine("Give name of the exercise: \n(leave the field blank to get results for all exercises): ");
            string exercise = Console.ReadLine();
            Console.WriteLine("How many records: \n(leave this as blank if you want all records to be shown): ");
            string quantity = Console.ReadLine();
            int amount = 100;
            if (quantity == "")
            {
                //ei muutosta
            }
            else if (!Int32.TryParse(quantity, out amount))
            {
                Console.WriteLine($"Invalid user input, {quantity} is not a number. Showing max 100 records.");
            }
            var selectedResults = context.GetSelectedResults(user, exercise, amount);
            if (selectedResults == null)
            {
                Console.WriteLine("No stored workout results with given parameters.");
            }
            else
            {
                Console.WriteLine("Results:");
                Console.WriteLine("Date \t\t User \t\t Exercise \t\t result");
                foreach (var result in selectedResults)
                {
                    Console.WriteLine($"{result.Date.ToShortDateString()} \t {result.User?.UserName} \t\t {result.Exercise?.ExerciseName} \t\t {result.Repeats} x {result.Weight}");
                }
            }
        }
    }
}