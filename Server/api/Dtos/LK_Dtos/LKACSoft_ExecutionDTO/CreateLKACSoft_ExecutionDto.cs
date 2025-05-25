namespace api.Dtos.LK_Dtos.LKACSoft_ExecutionDTO
{
    public class CreateLKACSoft_ExecutionDto
    {
        //public string ExecutionID { get; set; }
        public string? ExecutionName { get; set; }
        //public DateTime? DateCreated { get; set; } = DateTime.Now;
        public bool? IsPeriodic { get; set; } = false;
        public string? ProcessSchemaStatus { get; set; }
        public string? ProcessSchemaID { get; set; }
        public string? RelatedToCustomer { get; set; }
    }
}
