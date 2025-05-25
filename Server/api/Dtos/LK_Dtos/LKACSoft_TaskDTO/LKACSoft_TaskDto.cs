namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{
    public class LKACSoft_TaskDto
    {
        public string TaskID { get; set; }
        public DateTime? DateAssigned { get; set; }
        public DateTime? TaskDeadline { get; set; }
        public string? AssignedTo { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public string? TaskStatusID { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string? ReviewedBy { get; set; }
        public string? ReviewNote { get; set; }
        public DateTime? DateReview { get; set; }
        public bool? IsRetried { get; set; } = false;
        public string? RelatedToExecution { get; set; }
        public string? TaskType { get; set; }
        public int? DesignatedNumberOfDocument { get; set; } = 0;
        public int? NumberOfCompletedDocument { get; set; } = 0;
        public string? Priority { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
