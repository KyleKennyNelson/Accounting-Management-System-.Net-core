
using api.Dtos.APIPermission;
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Helpers;
using api.Identity;
using api.Interfaces;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace api.Controllers.LK_Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class LKACSoft_TaskController : ControllerBase
    {
        private readonly ILKACSoft_TaskRepository _taskRepo;

        public LKACSoft_TaskController(ILKACSoft_TaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [HttpGet("GetAmountOfTaskStatuses/{Year}/{Quarter}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAmountOfTaskStatuses([FromRoute] int Year, [FromRoute] int Quarter, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var amountOfTaskStatues = await _taskRepo.GetAmountOfTaskStatusPerYearAsync(Quarter, Year, queryIndex);

            if (amountOfTaskStatues == null)
                return NotFound(new { message = "No data found" });

            return Ok(amountOfTaskStatues.ToLKACSoft_AmountTaskStatusJsonDto());
        }

        [HttpGet("GetAmountOfRetriedTasks/{Year}/{Quarter}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAmountOfRetriedTasks([FromRoute] int Year, [FromRoute] int Quarter, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var amountOfRetriedTasks = await _taskRepo.GetAmountOfTasksPerYearAsync(Quarter, Year, queryIndex);

            if (amountOfRetriedTasks == null)
                return NotFound(new { message = "No data found" });

            return Ok(amountOfRetriedTasks.ToLKACSoft_AmountRetriedTaskJsonDto());
        }

        [HttpGet("GetTaskVisualization/{Year}/{Quarter}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetTaskVisualization([FromRoute] int Year, [FromRoute] int Quarter, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskVisualization = await _taskRepo.GetTaskVisualizationAsync(Quarter, Year, queryIndex);

            if (taskVisualization == null)
                return NotFound(new { message = "No data found" });

            return Ok(taskVisualization.ToLKACSoft_TaskVisualizationJsonDto());
        }

        [HttpGet("GetTaskAverageCompletionTimePerQuarter/{Year}/{Quarter}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetTaskAverageCompletionTimePerQuarter([FromRoute] int Year, [FromRoute] int Quarter, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskAverageCompletionTimePerQuarter = await _taskRepo.GetTaskAverageCompletionTimePerQuarterAsync(Quarter, Year, queryIndex);

            if (taskAverageCompletionTimePerQuarter == null)
                return NotFound(new { message = "No data found" });

            return Ok(taskAverageCompletionTimePerQuarter.ToLKACSoft_TaskAverageCompletionJsonDto());
        }

        // Fix for CS4014: Await the asynchronous call to ensure proper execution order.
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllTasks()
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (User.IsInRole("TRUONGPHONGKT"))
            {
                var isManager = true;
                var isLeader = false;

                var taskModels = await _taskRepo.GetAllAsync(userId, isManager, isLeader);
                var taskDtos = taskModels.Select(t => t.ToLKACSoft_TaskDto());

                return Ok(taskDtos);
            }

            else if (User.IsInRole("TRUONGNHOMKT"))
            {
                var isManager = false;
                var isLeader = true;

                var taskModels = await _taskRepo.GetAllAsync(userId, isManager, isLeader);
                var taskDtos = taskModels.Select(t => t.ToLKACSoft_TaskDto());

                return Ok(taskDtos);
            }
            else
            {
                var isManager = false;
                var isLeader = false;

                var taskModels = await _taskRepo.GetAllAsync(userId, isManager, isLeader);
                var taskDtos = taskModels.Select(t => t.ToLKACSoft_TaskDto());

                return Ok(taskDtos);
            }
        }

        // Get task by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetTaskById([FromRoute]string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (User.IsInRole("TRUONGPHONGKT"))
            {
                var isManager = true;
                var isLeader = false;

                var task = await _taskRepo.GetByIdAsync(userId, id, isManager, isLeader);
                if (task == null)
                    return NotFound(new { message = "Task not found" });

                return Ok(task.ToLKACSoft_TaskDto());
            }

            else if (User.IsInRole("TRUONGNHOMKT"))
            {
                var isManager = false;
                var isLeader = true;

                var task = await _taskRepo.GetByIdAsync(userId, id, isManager, isLeader);
                if (task == null)
                    return NotFound(new { message = "Task not found" });

                return Ok(task.ToLKACSoft_TaskDto());
            }

            else
            {
                var isManager = false;
                var isLeader = false;

                var task = await _taskRepo.GetByIdAsync(userId, id, isManager, isLeader);
                if (task == null)
                    return NotFound(new { message = "Task not found" });

                return Ok(task.ToLKACSoft_TaskDto());
            }
        }

        // Create a new task
        [HttpPost]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> CreateTask([FromBody] CreateLKACSoft_TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var isLeader = false;

            if (!User.IsInRole("TRUONGPHONGKT"))
                isLeader = true;

            var newTask = taskDto.CreateLKACSoft_TaskDto();

            var res = await _taskRepo.AddAsync(newTask, isLeader, userId);
            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.TaskID }, newTask.ToLKACSoft_TaskDto());
        }

        // Update an existing task
        [HttpPut("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateLKACSoft_TaskDto taskDto, [FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var isLeader = false;
            var isManager = false;

            if (!User.IsInRole("TRUONGPHONGKT"))
                isLeader = true;
            else
                isManager = true;

            var updateTask = taskDto.UpdateLKACSoft_TaskDto(id);

            var res = await _taskRepo.UpdateAsync(updateTask, isLeader, userId);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            var updatedTask = await _taskRepo.GetByIdAsync(null, id, isManager, isLeader);

            if (updatedTask == null) // Ensure updatedTask is not null before calling ToLKACSoft_TaskDto
                return NotFound(new { message = "Task not found after update" });

            return Ok(updatedTask.ToLKACSoft_TaskDto());
        }

        // Update the taskStatus for Task
        [HttpPut("UpdateTaskStatus/{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateTask_TaskStatus([FromBody] UpdateLKACSoft_Task_TaskStatusDto task_taskStatusDto, [FromRoute]string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var isLeader = false;
            var isManager = false;

            if (User.IsInRole("TRUONGPHONGKT"))
                isManager = true;
            else if (User.IsInRole("TRUONGNHOMKT"))
                isLeader = true;

            var updateTask = task_taskStatusDto.UpdateLKACSoft_Task_TaskStatusDto(id);

            var res = await _taskRepo.UpdateTaskStatusAsync(updateTask, userId, isManager);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            var updatedTask = await _taskRepo.GetByIdAsync(userId, id, isManager, isLeader);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(updatedTask.ToLKACSoft_TaskDto(), jsonOptions);

            //return Content(jsonResult, "application/json");


            return Ok(updatedTask.ToLKACSoft_TaskDto());
        }

        //// Update the taskStatus for Task, only Manager allowed
        //[HttpPut("UpdateTaskStatus/ForManager/{id}")]
        //[Authorize(Policy = "ApiPermissionPolicy")]
        //public async Task<IActionResult> UpdateTask_TaskStatusForManager([FromBody] UpdateLKACSoft_Task_TaskStatusDto task_taskStatusDto, [FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // Extract user ID from claims

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (userId == null)
        //    {
        //        return Unauthorized("User ID not found in token.");
        //    }

        //    var isLeader = false;
        //    var isManager = false;

        //    if (User.IsInRole("TRUONGPHONGKT"))
        //        isManager = true;
        //    else if (User.IsInRole("TRUONGNHOMKT"))
        //        isLeader = true;

        //    var updateTask = task_taskStatusDto.UpdateLKACSoft_Task_TaskStatusDto(id);

        //    var res = await _taskRepo.UpdateTaskStatusAsync(updateTask, isManager);

        //    if (res.Item2 != null && res.Item2 != "")
        //        return BadRequest(new { message = res.Item2 });

        //    var updatedTask = await _taskRepo.GetByIdAsync(userId, id, isManager, isLeader);

        //    // Serialize the object manually using JsonSerializer
        //    //var jsonOptions = new JsonSerializerOptions
        //    //{
        //    //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
        //    //    WriteIndented = true // Optional: Pretty-print JSON
        //    //};
        //    //var jsonResult = JsonSerializer.Serialize(updatedTask.ToLKACSoft_TaskDto(), jsonOptions);

        //    //return Content(jsonResult, "application/json");


        //    return Ok(updatedTask.ToLKACSoft_TaskDto());
        //}

        // Delete a task by ID
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteTask([FromRoute]string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var isLeader = false;

            if (!User.IsInRole("TRUONGPHONGKT"))
                isLeader = true;

            var isDeleted = await _taskRepo.DeleteAsync(id, isLeader, userId);
            if (!isDeleted)
                return NotFound(new { message = "Task not found or already deleted" });

            return NoContent();
        }
    }
}
