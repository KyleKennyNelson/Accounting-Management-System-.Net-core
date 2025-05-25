using api.Dtos.LK_Dtos.LKACSoft_DetailExecutionsDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailExecutionMapper
    {
        public static LKACSoft_DetailExecutionDto ToLKACSoft_DetailExecutionDto(this V_DetailExecutions V_DetailExecutions)
        {
            var res = new LKACSoft_DetailExecutionDto
            {
                Execution = new ExecutionDto
                {
                    ExecutionID = V_DetailExecutions.ExecutionID,
                    ExecutionName = V_DetailExecutions.ExecutionName,
                    CreatedBy = V_DetailExecutions.CreatedByForExecution,
                    DateCreated = V_DetailExecutions.DateCreated,
                    IsPeriodic = V_DetailExecutions.IsPeriodic
                },
                TaskCount = V_DetailExecutions.TaskCount,
            };

            if (V_DetailExecutions.FieldName != null && V_DetailExecutions.FieldValue != null)
            {
                res.Execution.ExecutionAttributes = new Dictionary<string, object>
                {
                    { V_DetailExecutions.FieldName, V_DetailExecutions.FieldValue }
                };
            }

            if (V_DetailExecutions.CreatedByID != null)
            {
                res.CreatedBy = new LKACSoft_User
                {
                    ID = V_DetailExecutions.CreatedByID,
                    Username = V_DetailExecutions.CreatedByUsername,
                    Firstname = V_DetailExecutions.CreatedByFirstname,
                    LastName = V_DetailExecutions.CreatedByLastname,
                    Avatar = V_DetailExecutions.CreatedByAvatar,
                    Address = V_DetailExecutions.CreatedByAddress,
                    District = V_DetailExecutions.CreatedByDistrict,
                    Dob = V_DetailExecutions.CreatedByDob,
                    IsQuitJob = V_DetailExecutions.CreatedByIsQuitJob,
                    DateCreate = V_DetailExecutions.CreatedByDateCreate,
                    Team = V_DetailExecutions.CreatedByTeam
                };
            }

            if (V_DetailExecutions.ProcessSchemaStatusID != null)
            {
                res.ProcessSchemaStatus = new LKACSoft_ProcessSchemaStatus
                {
                    ProcessSchemaStatusID = V_DetailExecutions.ProcessSchemaStatusID,
                    OrderIndex = V_DetailExecutions.OrderIndex,
                    CreatedAt = V_DetailExecutions.processSchemaStatusCreatedAt
                };
            }

            // ── Process status ────────────────────────────────────────────
            if (V_DetailExecutions.ProcessStatusID != null)
            {
                res.ProcessStatus = new LKACSoft_ProcessStatus
                {
                    ProcessStatusID = V_DetailExecutions.ProcessStatusID,
                    StatusName = V_DetailExecutions.StatusName,
                    DesignatedColor = V_DetailExecutions.DesignatedColor
                };
            }

            // ── Process schema (definition) ───────────────────────────────
            if (V_DetailExecutions.ProcessSchemaID != null)
            {
                res.ProcessSchemaID = new LKACSoft_ProcessSchema
                {
                    ProcessSchemaID = V_DetailExecutions.ProcessSchemaID,
                    Name = V_DetailExecutions.ProcessSchemaName,
                    Description = V_DetailExecutions.ProcessSchemaDescription,
                    CreatedAt = V_DetailExecutions.CreatedAt,
                    UpdatedAt = V_DetailExecutions.UpdatedAt
                };
            }

            // ── Related customer ──────────────────────────────────────────
            if (!string.IsNullOrWhiteSpace(V_DetailExecutions.Code))
            {
                res.RelatedToCustomer = new LKACSoft_Customer
                {
                    Code = V_DetailExecutions.Code,
                    Name = V_DetailExecutions.Name,
                    ShortName = V_DetailExecutions.ShortName,
                    Address = V_DetailExecutions.Address,
                    LogoS3Key = V_DetailExecutions.LogoS3Key,
                    FilterLocation = V_DetailExecutions.FilterLocation,
                    GetDocsDate = V_DetailExecutions.GetDocsDate,
                    DateCreate = V_DetailExecutions.DateCreate,
                    Suspended = V_DetailExecutions.Suspended,
                    SuspendedTo = V_DetailExecutions.SuspendedTo,
                    Dissolved = V_DetailExecutions.Dissolved,
                    DissolvedDate = V_DetailExecutions.DissolvedDate,
                    MainAccountant = V_DetailExecutions.MainAccountant,
                    CreatedBy = V_DetailExecutions.CreatedBy,
                    AssignedToCustomerSupport = V_DetailExecutions.AssignedToCustomerSupport,
                    ResponsibleAccountantTeam = V_DetailExecutions.ResponsibleAccountantTeam,
                    LKACSoft_DepartmentCode = V_DetailExecutions.LKACSoft_DepartmentCode,
                    ContractExpiry = V_DetailExecutions.ContractExpiry,
                    ContractSignedDate = V_DetailExecutions.ContractSignedDate
                };
            }

            return res;
        }
    }
}
