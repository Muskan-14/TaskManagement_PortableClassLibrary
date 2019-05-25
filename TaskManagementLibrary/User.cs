using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementLibrary
{
    public class User : IUser
    {
        #region Private variables
        private static readonly string filePathOfTasks = @"C:\Tasks.json";
        private static readonly string filePathOfUsers = @"C:\Users.json";

        #endregion

        /// <summary>
        /// This method is used to get the usergroup from userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> GetTypeOfUserFromUserName(string userName)
        {
            try
            {
                List<UserEntity> listOfUsers = new List<UserEntity>();
                string typeOfUser = String.Empty;
                bool foundUser = false;
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return null;
                }
                else
                {
                    using (StreamReader file = File.OpenText(filePathOfUsers))
                    {
                        listOfUsers = JsonConvert.DeserializeObject<List<UserEntity>>(file.ReadToEnd());
                        file.Close();
                    }
                    foreach (var user in listOfUsers)
                    {
                        if (user.UserName.Equals(userName))
                        {
                            typeOfUser = user.UserGroup.ToString();
                            foundUser = true;
                            break;
                        }
                    }
                    if (foundUser)
                    {
                        return await Task.FromResult(typeOfUser);
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to view all active tasks by the user based on skills
        /// </summary>
        /// <param name="userGroup"></param>
        /// <param name="programmingSkills"></param>
        /// <returns></returns>
        public async Task<List<TaskEntity>> ViewActiveTasksBasedOnSkills(string userGroup, string programmingSkills)
        {
            try
            {
                if (string.IsNullOrEmpty(userGroup) || string.IsNullOrEmpty(programmingSkills))
                {
                    return null;
                }
                if (userGroup.Equals(UserGroup.ProUser.ToString()))
                {
                    Tasks tasks = new Tasks();
                    return await tasks.ViewActiveTasksBasedOnSkills(userGroup, programmingSkills);
                }
                else
                {
                    PublicUsers publicUsers = new PublicUsers();
                    return await publicUsers.ViewActiveTasksBasedOnSkills(userGroup, programmingSkills);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to by the user to submit a task
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> SubmitATask(string taskName, string userName)
        {
            try
            {
                Tasks task = new Tasks();
                return await task.SubmitATask(taskName, userName);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> PickUpATask(string taskName, string userName)
        {
            try
            {
                var userGroup = GetTypeOfUserFromUserName(userName);
                if (userGroup.Equals(UserGroup.ProUser))
                {
                    Tasks tasks = new Tasks();
                    return await tasks.PickUpATask(taskName, userName);
                }
                else
                {
                    PublicUsers publicUsers = new PublicUsers();
                    return await publicUsers.PickUpATask(taskName, userName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ViewTotalCompensation(string userName)
        {
            try
            {
                int compensation = 0;
                using (StreamReader file = File.OpenText(filePathOfUsers))
                {
                    var listOfUsers = JsonConvert.DeserializeObject<List<UserEntity>>(file.ReadToEnd());
                    foreach (var user in listOfUsers)
                    {
                        if (user.UserName.Equals(userName))
                        {
                            compensation = user.Compensation;
                            break;
                        }
                    }
                    file.Close();
                }
                    return await Task.FromResult(compensation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

