using api.Dtos.LK_Dtos.LKACSoft_DocumentTypeDTO;
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DocumentTypeMapper
    {
        public static LKACSoft_CustomerDocumentTypeDto ToLKACSoft_DocumentTypeDto(this V_DocumentTypeDtos V_DocumentTypeDtos)
        {
            return new LKACSoft_CustomerDocumentTypeDto
            {
                DocumentType = new LKACSoft_DocumentType
                {
                    DocumentTypeID = V_DocumentTypeDtos.DocumentTypeID,
                    DocumentTypeName = V_DocumentTypeDtos.DocumentTypeName,
                },
                ArchivedAmount = V_DocumentTypeDtos.ArchivedAmount,
                LendAmount = V_DocumentTypeDtos.LendAmount,
                LostAmount = V_DocumentTypeDtos.LostAmount,
            };
        }
    }
}
