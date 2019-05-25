using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagementLibrary.Entity;

namespace TaskManagementLibrary
{
    public interface IUser
    {
        Task<List<TaskEntity>> ViewActiveTasksBasedOnSkills(string userGroup, string programmingSkills);
        Task<string> SubmitATask(string taskName, string userName);
        Task<string> GetTypeOfUserFromUserName(string userName);
        Task<string> PickUpATask(string taskName, string userName);
        Task<int> ViewTotalCompensation(string userName);

    }
}
