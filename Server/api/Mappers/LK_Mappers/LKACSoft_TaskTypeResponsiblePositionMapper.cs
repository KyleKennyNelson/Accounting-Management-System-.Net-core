using api.Dtos.LK_Dtos.LKACSoft_TaskTypeResponsiblePositionDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_TaskTypeResponsiblePositionMapper
    {
        public static LKACSoft_TaskTypeResponsiblePositionDto ToLKACSoft_TaskTypeResponsiblePositionDto(this LKACSoft_TaskTypeResponsiblePosition LKACSoft_TaskTypeResponsiblePosition)
        {
            return new LKACSoft_TaskTypeResponsiblePositionDto
            {
                TaskStatusID = LKACSoft_TaskTypeResponsiblePosition.TaskStatusID,
                TaskTypeID = LKACSoft_TaskTypeResponsiblePosition.TaskTypeID,
                RoleID = LKACSoft_TaskTypeResponsiblePosition.RoleID,
                CanExitStatus = LKACSoft_TaskTypeResponsiblePosition.CanExitStatus,
                CanEnterStatus = LKACSoft_TaskTypeResponsiblePosition.CanEnterStatus,
            };
        }
    }
}
