using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementLibrary.Enums;

namespace TaskManagementLibrary.Entity
{
    public class UserEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public UserGroup UserGroup { get; set; }
        public ProgrammingSkills ProgrammingSkills { get; set; }
        public int Compensation { get; set; }
    }
}
