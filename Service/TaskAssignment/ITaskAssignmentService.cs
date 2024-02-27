using TaskSchedulerAPI.DtoModels;
using TaskSchedulerAPI.Model;

namespace TaskSchedulerAPI.Service.TaskAssignment
{
    public interface ITaskAssignmentService
    {
        public Task<int?> CreateTaskAssignmentAsync(RegisterTaskDto taskDto);
        public Task<Model.TaskAssignment?> GetTaskAssignmentAsync(int taskId);
        public Task<IList<Model.TaskAssignment>> GetTaskAssignmentByUserID(int userId);
        public Task<string> UpdateTaskStatus(int TaskId , int  StatusId);
    }
}
