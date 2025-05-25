using api.Dtos.LK_Dtos.LKACSoft_PriorityDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_PriorityMapper
    {
        public static LKACSoft_PriorityDto ToLKACSoft_PriorityDto(this LKACSoft_Priority LKACSoft_Priority)
        {
            return new LKACSoft_PriorityDto
            {
                PriorityID = LKACSoft_Priority.PriorityID,
                PriorityName = LKACSoft_Priority.PriorityName,
                DesignatedColor = LKACSoft_Priority.DesignatedColor
            };
        }
    }
}
