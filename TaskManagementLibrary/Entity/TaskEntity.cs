using System;
using TaskManagementLibrary.Enums;

namespace TaskManagementLibrary.Entity
{
    public class TaskEntity
    {
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public UserGroup UserGroup { get; set; }
        public StateOfTheTask Status { get; set; }
        public string CurrentWorkingUser { get; set; }
        public ProgrammingSkills ProgrammingSkills { get; set; }
        public Rating Rating { get; set; }
        
       



    }
}
