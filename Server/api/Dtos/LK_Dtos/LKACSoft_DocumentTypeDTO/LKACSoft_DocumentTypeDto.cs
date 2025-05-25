using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DocumentTypeDTO
{
    public class LKACSoft_CustomerDocumentTypeDto
    {
        public required LKACSoft_DocumentType DocumentType { get; set; }

        public int? ArchivedAmount { get; set; } = 0;

        public int? LendAmount { get; set; } = 0;

        public int? LostAmount { get; set; } = 0;
    }
}
