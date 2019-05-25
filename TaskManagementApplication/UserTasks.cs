using System;
using System.Collections.Generic;
using TaskManagementLibrary;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementApplication
{
    public class UserTasks
    {
        public async static void UserChoices(int choice)
        {
            try
            {
                switch (choice)
                {
                    #region View all active tasks against skills
                    case 1:
                        Console.WriteLine("Enter your UserGroup(ProUser or PublicUsers) and Programming Skills (Angular or CSharp) separated by a space");
                        var userDetails = Console.ReadLine().Split(' ');
                        if (!string.IsNullOrWhiteSpace(userDetails.ToString()))
                        {
                            var userGroupOfTheUser = (UserGroup)Enum.Parse(typeof(UserGroup), userDetails[0]);
                            var programmingSkills = (ProgrammingSkills)Enum.Parse(typeof(ProgrammingSkills), userDetails[1]);
                            var userInfo = new User();
                            var listOfTasks = await userInfo.ViewActiveTasksBasedOnSkills(userGroupOfTheUser.ToString(), programmingSkills.ToString());
                            if (listOfTasks != null && listOfTasks.Count > 0)
                            {
                                foreach (var task in listOfTasks)
                                {
                                    Console.WriteLine("Task name :{0}, Task Description : {1}, Due Date : {2}", task.Name, task.Description, task.DueDate);
                                }
                            }
                            else if(listOfTasks.Count == 0)
                            {
                                Console.WriteLine("No tasks to view");
                            }
                            else
                            {
                                Console.WriteLine("Tasks could not be loaded");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Please provide valid details");
                        }

                        break;
                    #endregion

                    #region Pick a task
                    case 2:
                        Console.WriteLine("Please enter the task name and your userName separated by a space");
                        var details = Console.ReadLine().Split(' ');
                        var result = String.Empty;
                        if (!string.IsNullOrWhiteSpace(details.ToString()))
                        {
                            User user = new User();
                            var userGroup = user.GetTypeOfUserFromUserName(details[1]);
                            if (userGroup.Result.Equals(UserGroup.ProUser.ToString()))
                            {
                                var userInfo = new TaskManagementLibrary.ProUsers();
                                result = await userInfo.PickUpATask(details[0], details[1]);
                            }
                            else if (userGroup.Result.Equals(UserGroup.PublicUsers.ToString()))
                            {
                                var userInfo = new TaskManagementLibrary.PublicUsers();
                                result = await userInfo.PickUpATask(details[0], details[1]);
                            }
                            if (result != null)
                                Console.WriteLine(result);
                            else
                            {
                                Console.WriteLine("Please enter valid details");
                            }
                        }
                        break;
                    #endregion

                    #region Submit a task
                    case 3:
                        Console.WriteLine("Please enter the task name and your userName separated by a space");
                        var detailsOfTask = Console.ReadLine().Split(' ');
                        if (!string.IsNullOrWhiteSpace(detailsOfTask.ToString()))
                        {
                            var userInfo = new User();
                            var taskSubmitted = await userInfo.SubmitATask(detailsOfTask[0], detailsOfTask[1]);
                            if (taskSubmitted != null)
                                Console.WriteLine(taskSubmitted);
                            else
                            {
                                Console.WriteLine("Please enter valid details");
                            }
                        }
                        break;
                    #endregion

                    #region View total compensation
                    case 4:
                        Console.WriteLine("Please enter your username");
                        var userName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(userName))
                        {
                            var userInfo = new User();
                            var compensation = await userInfo.ViewTotalCompensation(userName);
                            if (compensation >= 0)
                                Console.WriteLine("Total compensation received till date : {0}",compensation);
                            else
                            {
                                Console.WriteLine("Please enter valid details");
                            }
                        }
                        break;
                        #endregion

                }
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }


    }



}



