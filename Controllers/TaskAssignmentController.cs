using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSchedulerAPI.DtoModels;
using TaskSchedulerAPI.Model;
using TaskSchedulerAPI.Service.TaskAssignment;
using TaskSchedulerAPI.Service.User;

namespace TaskSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentController(ITaskAssignmentService assignmentService) : ControllerBase
    {
        [HttpPost , Route("RegisterTask")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterTask(RegisterTaskDto taskDto)
        {
            if (ModelState.IsValid)
            {
                var data = await assignmentService.CreateTaskAssignmentAsync(taskDto);
                if (data != null)
                {
                    return Ok(new { success = true, statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { success = false, statusCode = 400, error = "Failed to register Task" });
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                     .Select(e => e.ErrorMessage)
                                     .ToList();

            return BadRequest(new { success = false, statusCode = 400, errors = errors });
        }

        [HttpGet, Route("GetAllTask")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllTask(int userId)
        {
            if (userId > 0)
            {
                var data = await assignmentService.GetTaskAssignmentByUserID(userId);
                if (data != null && data.Count>0)
                {
                    return Ok(new { success = true, statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { success = false, statusCode = 400, error = $"Failed to retrived Task by userId {userId}" });
                }
            }
            
            return BadRequest(new { success = false, statusCode = 400, errors = $"userId not valid {userId}" });
        }

        [HttpGet, Route("GetTaskByTaskId")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetTaskByTaskId(int taskId)
        {
            if (taskId > 0)
            {
                var data = await assignmentService.GetTaskAssignmentAsync(taskId);
                if (data != null)
                {
                    return Ok(new { success = true, statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { success = false, statusCode = 400, error = $"Failed to retrived Task by taskId : {taskId}" });
                }
            }
            return BadRequest(new { success = false, statusCode = 400, errors = $"userId not valid {taskId}" });
        }


        [HttpPatch, Route("UpdateByTaskId")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateByTaskId(int taskId , int statusId)
        {
            if (taskId > 0 && statusId > 0)
            {
                var data = await assignmentService.UpdateTaskStatus(taskId , statusId);
                if (data.Contains("successfully"))
                {
                    return Ok(new { success = true, statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { success = false, statusCode = 400, error = data });
                }
            }
            return BadRequest(new { success = false, statusCode = 400, errors = $"taskId/statusId not valid ,taskid {taskId}" });
        }
    }
}
