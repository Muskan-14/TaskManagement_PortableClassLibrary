# TaskManagement_PortableClassLibrary
This is a Task Management Portable Class Library created on ASP.NET Core. There are 3 types of users considered here, namely - Administrator, Pro-Users and Public Users who follow a hierarchy as per their order.

Functionality:
Administrator will create the task and assign to a user group. The task will be available only to the
assigned user group to pick until the due date.
Within the due date, if the task is not completed and accepted, it will be assigned to the next user
group in the hierarchy.
If the task not complete by both public and Pro users, it will move to internal backlog.
If the task completed by either public users or Pro users, administrator will review, accept and provide
the compensation.
Once the task is accepted it will be closed and won’t be visible to any users.
