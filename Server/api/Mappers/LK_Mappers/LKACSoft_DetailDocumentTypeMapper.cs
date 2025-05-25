using api.Dtos.LK_Dtos.LKACSoft_CustomerDocumentTypeDTO;
using api.Dtos.LK_Dtos.LKACSoft_DetailDocumentTypesDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailDocumentTypeMapper
    {
        public static LKACSoft_DocumentTypeDto ToLKACSoft_DetailDocumentTypeDto(this V_DetailDocumentTypes V_DetailDocumentTypes)
        {
            var res = new LKACSoft_DocumentTypeDto
            {
                CustomerDocumentType = new LKACSoft_CustomerDocumentTypeDto
                {
                    DocumentTypeID = V_DetailDocumentTypes.DocumentTypeID,
                    CustomerCode = V_DetailDocumentTypes.Code,
                    DocumentReceivingMechanism = V_DetailDocumentTypes.DocumentReceivingMechanism,
                    AvgAmount = V_DetailDocumentTypes.AvgAmount,
                    RegisteredAmount = V_DetailDocumentTypes.RegisteredAmount,
                    CurrentTotalAmount = V_DetailDocumentTypes.CurrentTotalAmount,
                },
                DocumentType = new LKACSoft_DocumentType
                {
                    DocumentTypeID = V_DetailDocumentTypes.DocumentTypeID,
                    DocumentTypeName = V_DetailDocumentTypes.DocumentTypeName
                },
                ArchivedAmount = V_DetailDocumentTypes.ArchivedAmount,
                LendAmount = V_DetailDocumentTypes.LendAmount,
                LostAmount = V_DetailDocumentTypes.LostAmount
            };

            //if (V_DetailDocumentTypes.DocumentTypeID != null)
            //{
            //    res.DocumentType = new LKACSoft_DocumentType
            //    {
            //        DocumentTypeID = V_DetailDocumentTypes.DocumentTypeID,
            //        DocumentTypeName = V_DetailDocumentTypes.DocumentTypeName
            //    };
            //}

            if (V_DetailDocumentTypes.Code != null)
            {
                res.RelatedToCustomer = new LKACSoft_Customer
                {
                    Code = V_DetailDocumentTypes.Code,
                    Name = V_DetailDocumentTypes.Name,
                    ShortName = V_DetailDocumentTypes.ShortName,
                    Address = V_DetailDocumentTypes.Address,
                    LogoS3Key = V_DetailDocumentTypes.LogoS3Key,
                    FilterLocation = V_DetailDocumentTypes.FilterLocation,
                    GetDocsDate = V_DetailDocumentTypes.GetDocsDate,
                    DateCreate = V_DetailDocumentTypes.DateCreate,
                    Suspended = V_DetailDocumentTypes.Suspended,
                    SuspendedTo = V_DetailDocumentTypes.SuspendedTo,
                    Dissolved = V_DetailDocumentTypes.Dissolved,
                    DissolvedDate = V_DetailDocumentTypes.DissolvedDate,
                    MainAccountant = V_DetailDocumentTypes.MainAccountant,
                    CreatedBy = V_DetailDocumentTypes.CreatedBy,
                    AssignedToCustomerSupport = V_DetailDocumentTypes.AssignedToCustomerSupport,
                    ResponsibleAccountantTeam = V_DetailDocumentTypes.ResponsibleAccountantTeam,
                    LKACSoft_DepartmentCode = V_DetailDocumentTypes.LKACSoft_DepartmentCode,
                    ContractExpiry = V_DetailDocumentTypes.ContractExpiry,
                    ContractSignedDate = V_DetailDocumentTypes.ContractSignedDate
                };
            }

            return res;
        }

        public static V_DetailDocumentTypes UpdateLKACSoft_DetailDocumentTypeDto(
            this UpdateLKACSoft_DetailDocumentTypeDto UpdateLKACSoft_DetailDocumentTypeDto, 
            string CustomerCode, string DocumentTypeID)
        {
            return new V_DetailDocumentTypes
            {
                Code = CustomerCode,
                DocumentTypeID = DocumentTypeID,
                DocumentReceivingMechanism = UpdateLKACSoft_DetailDocumentTypeDto.DocumentReceivingMechanism,
                AvgAmount = UpdateLKACSoft_DetailDocumentTypeDto.AvgAmount,
                RegisteredAmount = UpdateLKACSoft_DetailDocumentTypeDto.RegisteredAmount,
            };
        }

        public static V_DetailDocumentTypes CreateLKACSoft_DetailDocumentTypeDto(
            this CreateLKACSoft_DetailDocumentTypeDto CreateLKACSoft_DetailDocumentTypeDto,
            string CustomerCode, string DocumentTypeID)
        {
            return new V_DetailDocumentTypes
            {
                Code = CustomerCode,
                DocumentTypeID = DocumentTypeID,
                DocumentReceivingMechanism = CreateLKACSoft_DetailDocumentTypeDto.DocumentReceivingMechanism,
                AvgAmount = CreateLKACSoft_DetailDocumentTypeDto.AvgAmount,
                RegisteredAmount = CreateLKACSoft_DetailDocumentTypeDto.RegisteredAmount,
            };
        }
    }
}
