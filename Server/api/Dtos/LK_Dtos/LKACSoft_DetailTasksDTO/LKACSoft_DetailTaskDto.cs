using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailTasksDTO
{
    public class LKACSoft_DetailTaskDto
    {
        public required LKACSoft_Task Task { get; set; }
        public LKACSoft_User? AssignedTo { get; set; }
        public LKACSoft_User? AssignedBy { get; set; }
        public LKACSoft_Execution? Execution { get; set; }
        public LKACSoft_TaskStatus? TaskStatus { get; set; }
        public LKACSoft_Priority? Priority { get; set; }
        public LKACSoft_TaskType? TaskType { get; set; }
    }
}
