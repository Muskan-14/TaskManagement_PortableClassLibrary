using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementLibrary
{
    public class Tasks : ITasks
    {
        #region Private variables
        private static readonly string filePathOfTasks = @"C:\Tasks.json";

        #endregion

        /// <summary>
        /// This method is used to view all active tasks by the user based on skills
        /// </summary>
        /// <param name="userGroup"></param>
        /// <param name="programmingSkills"></param>
        /// <returns></returns>
        public async virtual Task<List<TaskEntity>> ViewActiveTasksBasedOnSkills(string userGroup, string programmingSkills)
        {
            try
            {
                List<TaskEntity> listOfTasks = new List<TaskEntity>();
                using (StreamReader fileReader = File.OpenText(filePathOfTasks))
                {
                    var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(fileReader.ReadToEnd())
                      ?? new List<TaskEntity>();
                    foreach (var item in allPresentTasks)
                    {
                        if (item.UserGroup.ToString().Equals(userGroup) && item.ProgrammingSkills.ToString().Equals(programmingSkills) &&
                                item.Status.ToString().Equals(StateOfTheTask.Created.ToString()))
                        {
                            listOfTasks.Add(item);
                        }
                    }
                    fileReader.Close();
                }

                return await Task.FromResult(listOfTasks);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SubmitATask(string taskName, string userName)
        {
            try
            {
                bool statusChanged = false;
                if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(taskName))
                {
                    return null;
                }
                else
                {
                    var jsonData = File.ReadAllText((filePathOfTasks));
                    var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                          ?? new List<TaskEntity>();

                    foreach (var item in allPresentTasks)
                    {
                        if (item.Name.Equals(taskName) &&
                            item.DueDate <= DateTime.Now)
                        {
                            break;
                        }
                        else if (item.Name.Equals(taskName) &&
                                !item.Status.ToString().Equals(StateOfTheTask.Accepted.ToString()) &&
                                !item.Status.ToString().Equals(StateOfTheTask.InternalBacklog.ToString()) &&
                                item.CurrentWorkingUser.Equals(userName) &&
                                item.DueDate > DateTime.Now)
                        {
                            item.Status = StateOfTheTask.Submitted;
                            statusChanged = true;
                            break;
                        }
                    }

                    jsonData = JsonConvert.SerializeObject(allPresentTasks, Formatting.Indented);
                    File.WriteAllText(filePathOfTasks, jsonData);
                    if (statusChanged)
                        return await Task.FromResult("Task has been submitted.");
                    else
                        return await Task.FromResult("Task could not be submitted. Please contact administrator");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to create a new task by the administrator
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Task CreateTask(TaskEntity task)
        {
            try
            {
                if (!File.Exists(filePathOfTasks))
                {
                    File.Create(filePathOfTasks).Close();
                }
                var jsonData = File.ReadAllText((filePathOfTasks));
                var listOfTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                      ?? new List<TaskEntity>();
                listOfTasks.Add(task);
                jsonData = JsonConvert.SerializeObject(listOfTasks, Formatting.Indented);
                File.WriteAllText(filePathOfTasks, jsonData);
                return Task.CompletedTask;
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
        public async Task<List<TaskEntity>> ViewAllSubmittedTasks()
        {
            try
            {
                List<TaskEntity> listOfTasks = new List<TaskEntity>();
                using (StreamReader fileReader = new StreamReader(filePathOfTasks))
                {
                    var jsonData = fileReader.ReadToEnd();
                    var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                      ?? new List<TaskEntity>();
                    foreach (var item in allPresentTasks)
                    {
                        if (item.Status.ToString().Equals(StateOfTheTask.Submitted.ToString()))
                        {
                            listOfTasks.Add(item);
                        }
                    }
                    fileReader.Close();
                    return await Task.FromResult(listOfTasks);
                }
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
        public async Task<TaskEntity> ReviewATask(string taskName)
        {
            try
            {
                TaskEntity task = null;
                using (StreamReader fileReader = new StreamReader(filePathOfTasks))
                {
                    var jsonData = fileReader.ReadToEnd();
                    var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                      ?? new List<TaskEntity>();
                    foreach (var item in allPresentTasks)
                    {
                        if (item.Status.ToString().Equals(StateOfTheTask.Submitted.ToString()) && item.Name.Equals(taskName))
                        {
                            task = item;
                            break;
                        }
                    }
                    fileReader.Close();
                    return await Task.FromResult(task);
                }
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
        public async Task<string> AcceptOrDelegateTask(string taskName, string reviewStatus, Rating rating)
        {
            try
            {
                if (string.IsNullOrEmpty(taskName) || string.IsNullOrEmpty(reviewStatus))
                {
                    return null;
                }
                else
                {
                    bool reviewDone = false;
                    var jsonData = File.ReadAllText((filePathOfTasks));
                    var listOfTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                          ?? new List<TaskEntity>();
                    foreach (var item in listOfTasks)
                    {
                        //If the submitted task is accepted
                        if (item.Name.Equals(taskName) && item.Status.ToString().Equals(StateOfTheTask.Submitted.ToString()) && reviewStatus.Equals(ReviewStatus.Accept.ToString()))
                        {
                            item.Status = StateOfTheTask.Accepted;
                            item.Rating = rating;

                            #region Provide compensation to user

                            await Administrator.ProvideCompensation(item.Name, item.CurrentWorkingUser, item.Rating);
                            #endregion
                            reviewDone = true;
                            break;
                        }
                        // If submitted task is to be delegated to the lower group in the hierarchy
                        else if (item.Name.Equals(taskName) && item.Status.ToString().Equals(StateOfTheTask.Submitted.ToString()) && reviewStatus.Equals(ReviewStatus.Delegate.ToString()))
                        {
                            if (item.UserGroup.ToString().Equals(UserGroup.ProUser.ToString()))
                            {
                                item.UserGroup = UserGroup.PublicUsers;
                                item.CurrentWorkingUser = null;
                                item.Status = StateOfTheTask.Delegated;
                                reviewDone = true;
                                break;
                            }
                            //If the submitted task is rejected even after public users's submission
                            else
                            {
                                item.Status = StateOfTheTask.InternalBacklog;
                                reviewDone = true;
                                break;
                            }
                        }
                    }
                    jsonData = JsonConvert.SerializeObject(listOfTasks, Formatting.Indented);
                    File.WriteAllText(filePathOfTasks, jsonData);
                    if (reviewDone)
                    {
                        return await Task.FromResult("Task reviewed");
                    }
                    else
                    {
                        return await Task.FromResult("Task could not be reviewed");
                    }
                }

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
        public async Task<List<TaskEntity>> ViewAllActiveTasks()
        {
            try
            {
                List<TaskEntity> listOfTasks = new List<TaskEntity>();
                using (StreamReader fileReader = new StreamReader(filePathOfTasks))
                {
                    var jsonData = fileReader.ReadToEnd();
                    var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                      ?? new List<TaskEntity>();
                    foreach (var item in allPresentTasks)
                    {
                        if (!item.Status.ToString().Equals(StateOfTheTask.Accepted.ToString()))
                        {
                            listOfTasks.Add(item);
                        }
                    }
                    fileReader.Close();
                    return await Task.FromResult(listOfTasks);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method  is used by the user to pick up a task
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual async Task<string> PickUpATask(string taskName, string userName)
        {
            try
            {
                bool statusChanged = false;
                if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(taskName))
                {
                    return null;
                }
                else
                {
                    var jsonData = File.ReadAllText((filePathOfTasks));
                    var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                          ?? new List<TaskEntity>();

                    foreach (var item in allPresentTasks)
                    {
                        if (item.Name.Equals(taskName) &&
                                item.Status.ToString().Equals(StateOfTheTask.Created.ToString()) &&
                                item.DueDate > DateTime.Now)
                        {
                            item.CurrentWorkingUser = userName;
                            item.Status = StateOfTheTask.Picked;
                            statusChanged = true;
                            break;
                        }
                    }

                    jsonData = JsonConvert.SerializeObject(allPresentTasks, Formatting.Indented);
                    File.WriteAllText(filePathOfTasks, jsonData);
                    if (statusChanged)
                        return await Task.FromResult("Task has been assigned to you.");
                    else
                        return await Task.FromResult("Task could not be assigned to you. Please contact administrator");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Method which can be called by a cron job daily to push delayed tasks either back to open or to Internal Backlog
        /// <summary>
        /// This method is used to push delayed tasks to Internal Backlog
        /// </summary>
        /// <returns></returns>
        public async Task<string> BackLogAllDueTasks()
        {
            try
            {
                bool statusChanged = false;
                var jsonData = File.ReadAllText((filePathOfTasks));
                var listOfTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
                      ?? new List<TaskEntity>();
                foreach (var item in listOfTasks)
                {
                    if (item.DueDate < DateTime.Now &&
                        item.Status.ToString() != StateOfTheTask.Accepted.ToString() &&
                        (item.UserGroup.ToString().Equals(UserGroup.ProUser.ToString())))
                    {
                        item.Status = StateOfTheTask.Delegated;
                        statusChanged = true;
                    }
                    else if (item.DueDate < DateTime.Now &&
                        item.Status.ToString() != StateOfTheTask.Accepted.ToString())
                    {
                        item.Status = StateOfTheTask.InternalBacklog;
                        statusChanged = true;
                    }
                }

                jsonData = JsonConvert.SerializeObject(listOfTasks, Formatting.Indented);
                File.WriteAllText(filePathOfTasks, jsonData);
                if (statusChanged)
                {
                    return await Task.FromResult("Tasks have been moved to Internal Backlog.");
                }
                else
                    return await Task.FromResult("No tasks to move to Internal Backlog.");



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }


}
