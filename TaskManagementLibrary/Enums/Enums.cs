using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementLibrary.Enums
{
    public enum StateOfTheTask
    {
        Created = 1,
        Picked = 2,
        Submitted = 3,
        Accepted = 4,
        Delegated = 5,
        InternalBacklog = 6

    }

    public enum UserGroup
    {
        Administrator = 1,
        ProUser = 2,
        PublicUsers = 3
    }

    public enum ProgrammingSkills
    {
        All,
        Angular,
        CSharp
    }

    public enum ReviewStatus
    {
        Accept,
        Delegate
    }

    public enum Rating
    {
        Bad = 1,
        RoomForImprovement = 2,
        Average = 3,
        Good = 4,
        Excellent = 5

    }
}
