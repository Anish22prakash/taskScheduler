namespace TaskSchedulerAPI.Common
{
    public static class Enums
    {
        public enum Roles
        {
            Admin = 1,
            User = 2,
        }

        public enum TaskStatus
        {
            Pending = 1,
            Completed = 2,
            Canceled = 3,
        }

        public enum TaskPriority
        {
            High = 1,
            mid  = 2,
            low  = 3,
        }
    }
}
