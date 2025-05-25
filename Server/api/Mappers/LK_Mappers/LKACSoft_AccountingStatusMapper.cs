using api.Dtos.LK_Dtos.LKACSoft_AccountingStatusDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_AccountingStatusMapper
    {
        public static LKACSoft_AccountingStatusDto ToLKACSoft_AccountingStatusDto(this LKACSoft_AccountingStatus LKACSoft_AccountingStatus)
        {
            return new LKACSoft_AccountingStatusDto
            {
                ID = LKACSoft_AccountingStatus.ID,
                Name = LKACSoft_AccountingStatus.Name
            };
        }
    }
}
