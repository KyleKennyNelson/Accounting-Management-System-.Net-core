
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Helpers;
using LKACSoftModel;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_TaskRepository
    {
        Task<LKACSoft_AmountTaskStatusDto?> GetAmountOfTaskStatusPerYearAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex);
        Task<LKACSoft_AmountRetriedTaskDto?> GetAmountOfTasksPerYearAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex);
        Task<LKACSoft_TaskVisualizationDto?> GetTaskVisualizationAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex);
        Task<LKACSoft_TaskAverageCompletionTimePerQuarterDto?> GetTaskAverageCompletionTimePerQuarterAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex);
        Task<List<LKACSoft_Task>> GetAllAsync(string userID, bool isManager, bool isLeader);
        Task<LKACSoft_Task?> GetByIdAsync(string userID, string taskID, bool isManager, bool isLeader);
        Task<LKACSoft_Task?> GetByLastestCreatedIdAsync();
        Task<(LKACSoft_Task?, string?)> AddAsync(LKACSoft_Task task, bool isLeader, string userID);
        Task<(LKACSoft_Task?, string?)> UpdateAsync(LKACSoft_Task task, bool isLeader, string userID);
        Task<(LKACSoft_Task?, string?)> UpdateTaskStatusAsync(LKACSoft_Task task, string userID, bool IsManager);
        Task<bool> DeleteAsync(string taskID, bool isLeader, string userID);
    }
}
