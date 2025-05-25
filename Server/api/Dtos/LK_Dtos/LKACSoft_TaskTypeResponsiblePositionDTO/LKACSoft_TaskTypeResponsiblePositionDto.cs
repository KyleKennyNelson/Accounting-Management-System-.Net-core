namespace api.Dtos.LK_Dtos.LKACSoft_TaskTypeResponsiblePositionDTO
{
    public class LKACSoft_TaskTypeResponsiblePositionDto
    {
        public required string TaskStatusID { get; set; }
        public required string TaskTypeID { get; set; }
        public required string RoleID { get; set; }
        public bool? CanExitStatus { get; set; }
        public bool? CanEnterStatus { get; set; }
    }
}
