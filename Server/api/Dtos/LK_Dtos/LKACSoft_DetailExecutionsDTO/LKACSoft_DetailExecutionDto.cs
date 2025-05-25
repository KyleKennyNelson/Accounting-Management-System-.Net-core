using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailExecutionsDTO
{
    public class ExecutionDto : LKACSoft_Execution
    {
        public Dictionary<string, object>? ExecutionAttributes { get; set; }
    }

    public class LKACSoft_DetailExecutionDto
    {
        public required ExecutionDto Execution { get; set; }

        public LKACSoft_User? CreatedBy { get; set; }

        public LKACSoft_ProcessSchemaStatus? ProcessSchemaStatus { get; set; }

        public LKACSoft_ProcessStatus? ProcessStatus { get; set; }

        public LKACSoft_ProcessSchema? ProcessSchemaID { get; set; }

        public LKACSoft_Customer? RelatedToCustomer { get; set; }
        public int? TaskCount { get; set; } = 0;
    }
}
