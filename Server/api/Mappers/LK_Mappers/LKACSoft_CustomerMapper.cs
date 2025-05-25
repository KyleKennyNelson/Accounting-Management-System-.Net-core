using api.Dtos.LK_Dtos.LKACSoft_CustomerDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_CustomerMapper
    {
        public static LKACSoft_CustomerDto ToLKACSoft_CustomerDto(this LKACSoft_Customer LKACSoft_Customer)
        {
            return new LKACSoft_CustomerDto
            {
                Code = LKACSoft_Customer.Code,
                Name = LKACSoft_Customer.Name,
                ShortName = LKACSoft_Customer.ShortName,
                Address = LKACSoft_Customer.Address,
                LogoS3Key = LKACSoft_Customer.LogoS3Key,
                FilterLocation = LKACSoft_Customer.FilterLocation,
                GetDocsDate = LKACSoft_Customer.GetDocsDate,
                DateCreate = LKACSoft_Customer.DateCreate,
                Suspended = LKACSoft_Customer.Suspended,
                SuspendedTo = LKACSoft_Customer.SuspendedTo,
                Dissolved = LKACSoft_Customer.Dissolved,
                DissolvedDate = LKACSoft_Customer.DissolvedDate,
                MainAccountant = LKACSoft_Customer.MainAccountant,
                CreatedBy = LKACSoft_Customer.CreatedBy,
                AssignedToCustomerSupport = LKACSoft_Customer.AssignedToCustomerSupport,
                ResponsibleAccountantTeam = LKACSoft_Customer.ResponsibleAccountantTeam,
                LKACSoft_DepartmentCode = LKACSoft_Customer.LKACSoft_DepartmentCode,
                ContractExpiry = LKACSoft_Customer.ContractExpiry,
                ContractSignedDate = LKACSoft_Customer.ContractSignedDate
            };
        }
    }
}
