using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailProcessSchemaStatusesDTO
{
    public class LKACSoft_DetailProcessSchemaStatusDto
    {
        public required LKACSoft_ProcessSchemaStatus ProcessSchemaStatus { get; set; }
        public LKACSoft_ProcessSchema? ProcessSchema { get; set; }
        public LKACSoft_ProcessStatus? ProcessStatus { get; set; }

    }
}
