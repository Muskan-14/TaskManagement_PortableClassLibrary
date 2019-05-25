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
    public class ProUsers : Tasks
    {
        #region Private variables
        private static readonly string filePathOfTasks = @"C:\Tasks.json";

        #endregion

        /// <summary>
        /// This method  is used by the user to pick up a task
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        
        //public async override Task<string> PickUpATask(string taskName, string userName)
        //{
        //    try
        //    {
        //        bool statusChanged = false;
        //        if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(taskName))
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            var jsonData = File.ReadAllText((filePathOfTasks));
        //            var allPresentTasks = JsonConvert.DeserializeObject<List<TaskEntity>>(jsonData)
        //                  ?? new List<TaskEntity>();

        //            foreach (var item in allPresentTasks)
        //            {
        //                if (item.Name.Equals(taskName) &&
        //                        item.Status.ToString().Equals(StateOfTheTask.Created.ToString()) &&
        //                        item.DueDate < DateTime.Now)
        //                {
        //                    item.CurrentWorkingUser = userName;
        //                    item.Status = StateOfTheTask.Picked;
        //                    statusChanged = true;
        //                    break;
        //                }
        //            }

        //            jsonData = JsonConvert.SerializeObject(allPresentTasks, Formatting.Indented);
        //            File.WriteAllText(filePathOfTasks, jsonData);
        //            if (statusChanged)
        //                return await Task.FromResult("Task has been assigned to you.");
        //            else
        //                return await Task.FromResult("Task could not be assigned to you. Please contact administrator");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

    }
}
