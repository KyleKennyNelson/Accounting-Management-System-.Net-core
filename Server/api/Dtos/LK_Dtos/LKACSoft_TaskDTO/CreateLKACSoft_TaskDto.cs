namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{
    public class CreateLKACSoft_TaskDto
    {
        //public DateTime? DateAssigned { get; set; } = DateTime.Now;
        public DateTime? TaskDeadline { get; set; }
        public string? AssignedTo { get; set; }
        public string? Title { get; set; }
        //public string Detail { get; set; }
        public string? TaskStatusID { get; set; }
        //public DateTime? DateAccepted { get; set; } = DateTime.Now;
        //public DateTime? DateCompleted { get; set; } = DateTime.Now;
        //public string ReviewedBy { get; set; }
        //public string ReviewNote { get; set; }
        //public DateTime? DateReview { get; set; } = DateTime.Now;
        //public bool? IsRetried { get; set; } = false;
        public string? RelatedToExecution { get; set; }
        public string? TaskType { get; set; }

        //public int? DesignatedNumberOfDocument { get; set; } = 0;
        //public int? NumberOfCompletedDocument { get; set; } = 0;
        public string? Priority { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
