using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagementLibrary.Entity;
using TaskManagementLibrary.Enums;

namespace TaskManagementLibrary
{
    public interface ITasks
    {
        Task<List<TaskEntity>> ViewActiveTasksBasedOnSkills(string userGroup, string programmingSkills);
        Task<string> SubmitATask(string taskName, string userName);
        Task CreateTask(TaskEntity task);
        Task<List<TaskEntity>> ViewAllSubmittedTasks();
        Task<TaskEntity> ReviewATask(string taskName);
        Task<string> AcceptOrDelegateTask(string taskName, string reviewStatus, Rating rating);
        Task<List<TaskEntity>> ViewAllActiveTasks();
        Task<string> PickUpATask(string taskName, string userName);
    }
}
