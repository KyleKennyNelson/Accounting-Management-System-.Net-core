using api.Dtos.LK_Dtos.LKACSoft_ProcessStatusDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_ProcessStatusMapper
    {
        public static LKACSoft_ProcessStatusDto ToLKACSoft_ProcessStatusDto(this LKACSoft_ProcessStatus LKACSoft_ProcesssStatus)
        {
            return new LKACSoft_ProcessStatusDto
            {
                ProcessStatusID = LKACSoft_ProcesssStatus.ProcessStatusID,
                StatusName = LKACSoft_ProcesssStatus.StatusName,
                DesignatedColor = LKACSoft_ProcesssStatus.DesignatedColor
            };
        }
    }
}
