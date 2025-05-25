using api.Dtos.LK_Dtos.LKACSoft_NotificationDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_NotificationMapper
    {
        public static LKACSoft_NotificationDto ToLKACSoft_NotificationDto(this LKACSoft_Notification LKACSoft_Notification)
        {
            return new LKACSoft_NotificationDto
            {
                NotificationID = LKACSoft_Notification.NotificationID,
                Detail = LKACSoft_Notification.Detail,
                DateCreated = LKACSoft_Notification.DateCreated
            };
        }
    }
}
