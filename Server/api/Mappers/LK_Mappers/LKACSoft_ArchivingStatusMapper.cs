using api.Dtos.LK_Dtos.LKACSoft_ArchivingStatusDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_ArchivingStatusMapper
    {
        public static LKACSoft_ArchivingStatusDto ToLKACSoft_ArchivingStatusDto(this LKACSoft_ArchivingStatus LKACSoft_ArchivingStatus)
        {
            return new LKACSoft_ArchivingStatusDto
            {
                ID = LKACSoft_ArchivingStatus.ID,
                Name = LKACSoft_ArchivingStatus.Name
            };
        }
    }
}
