using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskManagementLibrary;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementApplication
{
    public class Program
    {
      

        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter your username:");
            string userName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Please provide a valid UserName");
            }
            else
            {
                User user = new User();
                var typeOfUser = user.GetTypeOfUserFromUserName(userName).GetAwaiter().GetResult();
                if (string.IsNullOrWhiteSpace(typeOfUser))
                {
                    Console.WriteLine("Please provide a valid UserName");
                }

                else if (typeOfUser.ToString().Contains(Enum.GetName(typeof(UserGroup), UserGroup.Administrator)))
                {
                    Console.WriteLine("Enter 1 to Create a Task");
                    Console.WriteLine("Enter 2 to View all submitted tasks");
                    Console.WriteLine("Enter 3 to Review a Task");
                    Console.WriteLine("Enter 4 to Accept or Delegate a task and provide rating to it");
                    Console.WriteLine("Enter 5 to View all active tasks");

                    var adminChoice = Convert.ToInt32(Console.ReadLine());
                    AdministratorTasks.AdministratorChoices(adminChoice);
                }

                else
                {
                    Console.WriteLine("Enter 1 to view all task based on your skills");
                    Console.WriteLine("Enter 2 to pick up a task");
                    Console.WriteLine("Enter 3 to Submit a task");
                    Console.WriteLine("Enter 4 to View total compensation received till date");

                    var userChoice = Convert.ToInt32(Console.ReadLine());
                    UserTasks.UserChoices(userChoice);
                }
            }
            
            Console.ReadLine();
        }

       
    }
}
