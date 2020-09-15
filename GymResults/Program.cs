using System;
using System.ComponentModel.Design;
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
            }
            else
            {
                Console.WriteLine($"Could not add new user! Username {name} already exists. \n" +
                    $"Try again with different name");
            }
        }
        //Käyttäjien listaus
        public static void ListUsers(GymContext context)
        {
            foreach (var User in context.Users)
            {
                Console.WriteLine(User.UserName);
            }
        }
        //Hae tietty käyttäjä, palauttaa null jos käyttäjää ei löydy
        public static User GetUser(GymContext context, string name)
        {
            var tyyppi =context.Users.Where(user => user.UserName == name).FirstOrDefault<User>();
            return tyyppi;
        }


        static void Main(string[] args)
        {
            
            using (var context = new GymContext())
            {
            Console.WriteLine("This is the beginning of a beautiful app\n");
            Console.WriteLine("Select action");
            Console.WriteLine("0: Quit \n1: Add new user \n2: List users \n" +
                "3:Get user by name \n4:List users \n5:List exercises \n6: ...");
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
                        User mara = GetUser(context, "Marjo");
                        Console.WriteLine("Mara: " +mara.UserName);
                          break;
                  }
                    Console.WriteLine("Select action");
                }

            }
        }
          


     }
}
       
    


