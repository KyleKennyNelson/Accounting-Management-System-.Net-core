using api.Dtos.LK_Dtos.LKACSoft_TaskTypeDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_TaskTypeMapper
    {
        public static LKACSoft_TaskTypeResponsiblePositionDto ToLKACSoft_TaskTypeDto(this LKACSoft_TaskType LKACSoft_TaskType)
        {
            return new LKACSoft_TaskTypeResponsiblePositionDto
            {
                TaskTypeID = LKACSoft_TaskType.TaskTypeID,
                TaskTypeName = LKACSoft_TaskType.TaskTypeName,
                Description = LKACSoft_TaskType.Description,
                TaskTypeDesignatedColor = LKACSoft_TaskType.TaskTypeDesignatedColor
            };
        }
    }
}
