using api.Dtos.LK_Dtos.LKACSoft_ExecutionDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_ExecutionMapper
    {
        public static LKACSoft_ExecutionDto ToLKACSoft_ExecutionDto(this LKACSoft_Execution LKACSoft_Execution)
        {
            return new LKACSoft_ExecutionDto
            {
                ExecutionID = LKACSoft_Execution.ExecutionID,
                ExecutionName = LKACSoft_Execution.ExecutionName,
                CreatedBy = LKACSoft_Execution.CreatedBy,
                DateCreated = LKACSoft_Execution.DateCreated,
                IsPeriodic = LKACSoft_Execution.IsPeriodic,
                ProcessSchemaStatus = LKACSoft_Execution.ProcessSchemaStatus,
                ProcessSchemaID = LKACSoft_Execution.ProcessSchemaID,
                RelatedToCustomer = LKACSoft_Execution.RelatedToCustomer
            };
        }

        public static LKACSoft_Execution CreateLKACSoft_ExecutionDto(this CreateLKACSoft_ExecutionDto CreateLKACSoft_Execution, string CreatedBy)
        {
            return new LKACSoft_Execution
            {
                //ProcessID = CreateLKACSoft_Process.ProcessID,
                ExecutionName = CreateLKACSoft_Execution.ExecutionName,
                CreatedBy = CreatedBy,
                //DateCreated = CreateLKACSoft_Execution.DateCreated,
                IsPeriodic = CreateLKACSoft_Execution.IsPeriodic,
                ProcessSchemaStatus = CreateLKACSoft_Execution.ProcessSchemaStatus,
                ProcessSchemaID = CreateLKACSoft_Execution.ProcessSchemaID,
                RelatedToCustomer = CreateLKACSoft_Execution.RelatedToCustomer
            };
        }
    }
}
