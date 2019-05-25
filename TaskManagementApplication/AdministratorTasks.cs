using System;
using System.Threading.Tasks;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementApplication
{
    public class AdministratorTasks
    {
        public async static void AdministratorChoices(int choice)
        {
            try
            {
                switch (choice)
                {
                    #region Create a task
                    case 1:
                        Console.WriteLine("Enter the name of the task");
                        var nameOfThetask = Console.ReadLine();

                        Console.WriteLine("Enter the description");
                        var descriptnOfTheTask = Console.ReadLine();

                        Console.WriteLine("Enter the due date in mm/dd/yyyy format");
                        var dueDateOfTheTask = Convert.ToDateTime(Console.ReadLine());
                        if (dueDateOfTheTask < DateTime.Now)
                        {
                            Console.WriteLine("Please provide a valid date");
                            break;
                        }

                        Console.WriteLine("Enter the UserGroup : ProUser or PublicUsers");
                        var userGroupOfTheTask = (UserGroup)Enum.Parse(typeof(UserGroup), Console.ReadLine());

                        Console.WriteLine("Enter the Programming Skills : Angular or CSharp");
                        var programmingSkills = (ProgrammingSkills)Enum.Parse(typeof(ProgrammingSkills), Console.ReadLine());

                        TaskEntity tasks = new TaskEntity
                        {
                            TaskId = Guid.NewGuid(),
                            Name = nameOfThetask,
                            Description = descriptnOfTheTask,
                            DueDate = dueDateOfTheTask,
                            UserGroup = userGroupOfTheTask,
                            ProgrammingSkills = programmingSkills,
                            Status = StateOfTheTask.Created
                        };
                        await TaskManagementLibrary.Administrator.CreateTask(tasks);
                        if (Task.CompletedTask.IsCompletedSuccessfully)
                            Console.WriteLine("Task Created");

                        break;
                    #endregion

                    #region View all Submitted Tasks
                    case 2:

                        var listOfTasks = await TaskManagementLibrary.Administrator.ViewAllSubmittedTasks();
                        if (listOfTasks != null && listOfTasks.Count > 0)
                        {
                            foreach (var task in listOfTasks)
                            {
                                Console.WriteLine("Task name :{0}, Task Description : {1} Due Date : {2},Task Status :{3}, Submitted by:{4}", task.Name, task.Description, task.DueDate, task.Status.ToString(), task.CurrentWorkingUser);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tasks could not be loaded");
                        }
                        break;
                    #endregion

                    #region Review a task
                    case 3:
                        Console.WriteLine("Enter the task name of the task you wish to review");
                        var output = await TaskManagementLibrary.Administrator.ReviewATask(Console.ReadLine());
                        if (output != null)
                            Console.WriteLine("Task name :{0}, Task Description : {1} Due Date : {2},Task Status :{3}", output.Name, output.Description, output.DueDate, output.Status.ToString(), output.CurrentWorkingUser);
                        else
                        {
                            Console.WriteLine("Task could not be loaded");
                        }
                        break;
                    #endregion

                    #region Accept or Delegate a Task
                    case 4:
                        Console.WriteLine("Enter the task name of the task and the review status(Accept or Delegate) and rating you wish to give(Between 1 to 5),all separated by a space");
                        var taskDetails = Console.ReadLine().Split(' ');
                        var reviewStatus = (ReviewStatus)Enum.Parse(typeof(ReviewStatus), taskDetails[1]);
                        var rating = (Rating)Enum.Parse(typeof(Rating), taskDetails[2]);

                        var result = await TaskManagementLibrary.Administrator.AcceptOrDelegateTask(taskDetails[0], reviewStatus.ToString(), rating);
                        if (result != null)
                            Console.WriteLine(result);
                        else
                        {
                            Console.WriteLine("Please enter valid details");
                        }
                        break;
                    #endregion

                    #region View all Active Tasks
                    case 5:

                        var activeTasks = await TaskManagementLibrary.Administrator.ViewAllActiveTasks();
                        if (activeTasks != null && activeTasks.Count > 0)
                        {
                            foreach (var task in activeTasks)
                            {
                                Console.WriteLine("Task name :{0}, Task Description : {1} Due Date : {2},Task Status :{3}", task.Name, task.Description, task.DueDate, task.Status.ToString(), task.CurrentWorkingUser);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tasks could not be loaded");
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

