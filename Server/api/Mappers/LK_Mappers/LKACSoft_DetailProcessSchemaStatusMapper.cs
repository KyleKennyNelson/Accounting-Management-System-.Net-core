using api.Dtos.LK_Dtos.LKACSoft_DetailProcessSchemaStatusesDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailProcessSchemaStautsMapper
    {
        public static LKACSoft_DetailProcessSchemaStatusDto ToLKACSoft_DetailProcessSchemaStatusDto(this V_DetailProcessSchemaStatuses V_DetailProcessSchemaStatuses)
        {
            var res = new LKACSoft_DetailProcessSchemaStatusDto
            {
                ProcessSchemaStatus = new LKACSoft_ProcessSchemaStatus
                {
                    ProcessSchemaStatusID = V_DetailProcessSchemaStatuses.ProcessSchemaStatusID,
                    OrderIndex = V_DetailProcessSchemaStatuses.OrderIndex,
                    CreatedAt = V_DetailProcessSchemaStatuses.CreatedAt,
                },
            };

            if (V_DetailProcessSchemaStatuses.ProcessSchemaID != null)
            {
                res.ProcessSchema = new LKACSoft_ProcessSchema
                {
                    ProcessSchemaID = V_DetailProcessSchemaStatuses.ProcessSchemaID,
                    Name = V_DetailProcessSchemaStatuses.ProcessSchemaName,
                    Description = V_DetailProcessSchemaStatuses.ProcessSchemaDescription,
                    CreatedAt = V_DetailProcessSchemaStatuses.ProcessSchemaCreatedAt,
                    UpdatedAt = V_DetailProcessSchemaStatuses.ProcessSchemaUpdatedAt,
                };
            }

            if (V_DetailProcessSchemaStatuses.ProcessStatusID != null)
            {
                res.ProcessStatus = new LKACSoft_ProcessStatus
                {
                    ProcessStatusID = V_DetailProcessSchemaStatuses.ProcessStatusID,
                    StatusName = V_DetailProcessSchemaStatuses.StatusName,
                    DesignatedColor = V_DetailProcessSchemaStatuses.DesignatedColor,
                };
            }

            return res;
        }
    }
}
