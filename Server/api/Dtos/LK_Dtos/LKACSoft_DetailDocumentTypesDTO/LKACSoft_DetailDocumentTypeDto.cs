using api.Dtos.LK_Dtos.LKACSoft_CustomerDocumentTypeDTO;
using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailDocumentTypesDTO
{
    public class LKACSoft_DocumentTypeDto
    {
        public required LKACSoft_CustomerDocumentTypeDto CustomerDocumentType { get; set; }
        public required LKACSoft_DocumentType DocumentType { get; set; }

        public LKACSoft_Customer? RelatedToCustomer { get; set; }

        public int? ArchivedAmount { get; set; } = 0;

        public int? LendAmount { get; set; } = 0;

        public int? LostAmount { get; set; } = 0;
    }
}
