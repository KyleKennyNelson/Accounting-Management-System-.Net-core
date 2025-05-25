using api.Dtos.LK_Dtos.LKACSoft_TaskStatusDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_TaskStatusMapper
    {
        public static LKACSoft_TaskTypeDto ToLKACSoft_TaskStatusDto(this LKACSoft_TaskStatus LKACSoft_TaskStatus)
        {
            return new LKACSoft_TaskTypeDto
            {
                TaskStatusID = LKACSoft_TaskStatus.TaskStatusID,
                TaskStatusName = LKACSoft_TaskStatus.TaskStatusName,
                DesignatedColor = LKACSoft_TaskStatus.DesignatedColor
            };
        }

    }
}
