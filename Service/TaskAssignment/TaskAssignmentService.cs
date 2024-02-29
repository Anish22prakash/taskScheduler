using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskSchedulerAPI.Common;
using TaskSchedulerAPI.Data;
using TaskSchedulerAPI.DtoModels;
using TaskSchedulerAPI.LoggerService;
using TaskSchedulerAPI.Model;
using TaskSchedulerAPI.Service.User;

namespace TaskSchedulerAPI.Service.TaskAssignment
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TaskAssignmentService(ApplicationDbContext dbContext , ILoggerManager logger , IMapper mapper)
        {
            _context = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int?> CreateTaskAssignmentAsync(RegisterTaskDto taskDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(taskDto.TaskTitle))
                {
                    _logger.LogWarning($"provide valid task title");
                    return null;
                }
                Model.User? exUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == taskDto.UserId);
                if(exUser == null)
                {
                    _logger.LogWarning($"provide valid userId");
                    return null;
                }

                var newTask = _mapper.Map<Model.TaskAssignment>(taskDto);
                newTask.CreatedDate = DateTime.Now;
                newTask.UpdatedDate = DateTime.Now;
                newTask.User = exUser;

               await _context.TaskAssignments.AddAsync(newTask);
               await _context.SaveChangesAsync();
                _logger.LogInfo($"task data is successfully saved ,with taskId{newTask.TaskId}");
                return newTask.TaskId;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"method name : ,CreateTaskAssignmentAsync , {taskDto.ToString()}");
                throw;
            }
        }

        public async Task<string> DeleteTaskAssignmentAsync(int taskId)
        {
            try
            {
               Model.TaskAssignment? exTask = await _context.TaskAssignments.FirstOrDefaultAsync(u => u.TaskId == taskId);
                if(exTask == null)
                {
                    _logger.LogInfo($"task assigment is not found by given taskId : {taskId}");
                    return null;
                }
                _context.TaskAssignments.Remove(exTask);
              await  _context.SaveChangesAsync();
                _logger.LogInfo($"task is successfully deleted , taskId: {taskId}");
                return "task is successfully deleted";
            }catch (Exception ex)
            {
                _logger.LogError($"method name : DeleteTaskAssignmentAsync , taskId :{taskId}");
                throw;
            }
        }

        public async Task<Model.TaskAssignment?> GetTaskAssignmentAsync(int taskId)
        {
            try
            {
                var exTask = await _context.TaskAssignments.FirstOrDefaultAsync(u => u.TaskId == taskId);
                if (exTask == null)
                {
                    _logger.LogWarning($"no task found by taskId : {taskId}");
                    return null;
                }

                _logger.LogInfo($"task data is successfully retrived ,with userId{exTask.TaskId}");
                return exTask;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"method name : GetTaskAssignmentAsync ,taskId : {taskId}");
                throw;
            }
        }

        public async Task<IList<Model.TaskAssignment>> GetTaskAssignmentByUserID(int userId)
        {
            try
            {
                var exUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (exUser == null)
                {
                    _logger.LogWarning($"no task found by userId : {userId}");
                    return null;
                }
                var taskAssignments = await _context.TaskAssignments
                            .Where(t => t.UserId == userId)
                            .ToListAsync();

                _logger.LogInfo($"list of task data is successfully retrived ,with userId {userId}");
                return taskAssignments;
               
            }
            catch (Exception ex)
            {
                this._logger.LogError($"method name :, GetTaskAssignmentByUserID ,userId :{userId}");
                throw;
            }
        }

        public async Task<string> UpdateTaskStatus(int TaskId, int StatusId)
        {
            try
            {
                var exTask = await _context.TaskAssignments.FirstOrDefaultAsync(u => u.TaskId == TaskId);
                if (exTask == null)
                {
                    _logger.LogWarning($"no task found by TaskId : {TaskId}");
                    return $"Invalid taskId {TaskId}";
                }
                
                if (!Enum.IsDefined(typeof(TaskSchedulerAPI.Common.Enums.TaskStatus), StatusId))
                {
                    _logger.LogWarning($"Invalid task status: {StatusId}");
                    return $"Invalid task status {StatusId}";
                }

                exTask.TaskStatus = Convert.ToInt32((TaskSchedulerAPI.Common.Enums.TaskStatus)StatusId);
                _context.TaskAssignments.Update(exTask);
                await _context.SaveChangesAsync();

                _logger.LogInfo($"task data is successfully update ,with taskId {TaskId}");
                return "successfully task status is updated";

            }
            catch (Exception ex)
            {
                this._logger.LogError($"method name : UpdateTaskStatus ,taskId : {TaskId}");
                throw;
            }
        }
    }
}
