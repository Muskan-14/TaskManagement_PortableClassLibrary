using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementLibrary
{
    public class Administrator
    {
        #region Private variabless
        private static readonly string filePathOfUsers = @"C:\Users.json";
        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to create a new task by the administrator
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async static Task CreateTask(TaskEntity task)
        {
            try
            {
                Tasks createTask = new Tasks();
                await createTask.CreateTask(task);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to view all submitted tasks by the administrator which are not accepted
        /// </summary>
        /// <returns></returns>
        public async static Task<List<TaskEntity>> ViewAllSubmittedTasks()
        {
            try
            {
                Tasks tasks = new Tasks();
                return await tasks.ViewAllSubmittedTasks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to view the details of a given task
        /// </summary>
        /// <returns> the task details</returns>
        public async static Task<TaskEntity> ReviewATask(string taskName)
        {
            try
            {
                Tasks tasks = new Tasks();
                return await tasks.ReviewATask(taskName);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to accept or delegate the submitted tasks by the administrator
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="reviewStatus"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        public async static Task<string> AcceptOrDelegateTask(string taskName, string reviewStatus, Rating rating)
        {
            try
            {
                Tasks tasks = new Tasks();
                return await tasks.AcceptOrDelegateTask(taskName, reviewStatus, rating);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to view all active tasks by the administrator which are not accepted
        /// </summary>
        /// <returns></returns>
        public async static Task<List<TaskEntity>> ViewAllActiveTasks()
        {
            try
            {
                Tasks tasks = new Tasks();
                return await tasks.ViewAllActiveTasks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task ProvideCompensation(string taskName, string currentWorkingUser, Rating rating)
        {
            using (StreamReader file = File.OpenText(filePathOfUsers))
            {
                var listOfUsers = JsonConvert.DeserializeObject<List<UserEntity>>(file.ReadToEnd());
                foreach (var user in listOfUsers)
                {
                    if (user.UserName.Equals(currentWorkingUser))
                    {
                       if(rating == Rating.Bad || rating == Rating.RoomForImprovement)
                        {
                            user.Compensation += 1000;
                        }
                       else if(rating == Rating.Average)
                        {
                            user.Compensation += 3000;
                        }
                        else if (rating == Rating.Good)
                        {
                            user.Compensation += 4000;
                        }
                        else
                        {
                            user.Compensation += 5000;
                        }
                        break;
                    }
                }

               var jsonData = JsonConvert.SerializeObject(listOfUsers, Formatting.Indented);
               File.WriteAllText(filePathOfUsers, jsonData);
               file.Close();
            }
            
        }

        #endregion


    }
}
