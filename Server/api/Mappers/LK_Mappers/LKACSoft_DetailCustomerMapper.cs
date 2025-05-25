using api.Dtos.LK_Dtos.LKACSoft_DetailCustomersDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailCustomerMapper
    {
        public static LKACSoft_CustomerDto ToLKACSoft_DetailCustomerDto(this V_DetailCustomers V_DetailCustomers)
        {
            var res = new LKACSoft_CustomerDto
            {
                Customer = new LKACSoft_Customer
                {
                    Code = V_DetailCustomers.Code,
                    Name = V_DetailCustomers.Name,
                    ShortName = V_DetailCustomers.ShortName,
                    Address = V_DetailCustomers.Address,
                    LogoS3Key = V_DetailCustomers.LogoS3Key,
                    FilterLocation = V_DetailCustomers.FilterLocation,
                    GetDocsDate = V_DetailCustomers.GetDocsDate,
                    DateCreate = V_DetailCustomers.DateCreate,
                    Suspended = V_DetailCustomers.Suspended,
                    SuspendedTo = V_DetailCustomers.SuspendedTo,
                    Dissolved = V_DetailCustomers.Dissolved,
                    DissolvedDate = V_DetailCustomers.DissolvedDate,
                    MainAccountant = V_DetailCustomers.MainAccountant,
                    CreatedBy = V_DetailCustomers.CreatedBy,
                    AssignedToCustomerSupport = V_DetailCustomers.AssignedToCustomerSupport,
                    ResponsibleAccountantTeam = V_DetailCustomers.ResponsibleAccountantTeam,
                    LKACSoft_DepartmentCode = V_DetailCustomers.LKACSoft_DepartmentCode,
                    ContractExpiry = V_DetailCustomers.ContractExpiry,
                    ContractSignedDate = V_DetailCustomers.ContractSignedDate
                }
            };


            if (V_DetailCustomers.MainAccountantID != null)
            {
                res.MainAccountant = new LKACSoft_User
                {
                    ID = V_DetailCustomers.MainAccountantID,
                    Username = V_DetailCustomers.MainAccountantUsername,
                    Firstname = V_DetailCustomers.MainAccountantFirstname,
                    LastName = V_DetailCustomers.MainAccountantLastname,
                    Avatar = V_DetailCustomers.MainAccountantAvatar,
                    Address = V_DetailCustomers.MainAccountantAddress,
                    District = V_DetailCustomers.MainAccountantDistrict,
                    Dob = V_DetailCustomers.MainAccountantDob,
                    IsQuitJob = V_DetailCustomers.MainAccountantIsQuitJob,
                    DateCreate = V_DetailCustomers.MainAccountantDateCreate,
                    Team = V_DetailCustomers.MainAccountantTeam
                };
            }

            if (V_DetailCustomers.CreatedByID != null)
            {
                res.CreateBy = new LKACSoft_User
                {
                    ID = V_DetailCustomers.CreatedByID,
                    Username = V_DetailCustomers.CreatedByUsername,
                    Firstname = V_DetailCustomers.CreatedByFirstname,
                    LastName = V_DetailCustomers.CreatedByLastname,
                    Avatar = V_DetailCustomers.CreatedByAvatar,
                    Address = V_DetailCustomers.CreatedByAddress,
                    District = V_DetailCustomers.CreatedByDistrict,
                    Dob = V_DetailCustomers.CreatedByDob,
                    IsQuitJob = V_DetailCustomers.CreatedByIsQuitJob,
                    DateCreate = V_DetailCustomers.CreatedByDateCreate,
                    Team = V_DetailCustomers.CreatedByTeam
                };
            }

            if (V_DetailCustomers.AssignedSupportID != null)
            {
                res.AssignedToCustomerSupport = new LKACSoft_User
                {
                    ID = V_DetailCustomers.AssignedSupportID,
                    Username = V_DetailCustomers.AssignedSupportUsername,
                    Firstname = V_DetailCustomers.AssignedSupportFirstname,
                    LastName = V_DetailCustomers.AssignedSupportLastname,
                    Avatar = V_DetailCustomers.AssignedSupportAvatar,
                    Address = V_DetailCustomers.AssignedSupportAddress,
                    District = V_DetailCustomers.AssignedSupportDistrict,
                    Dob = V_DetailCustomers.AssignedSupportDob,
                    IsQuitJob = V_DetailCustomers.AssignedSupportIsQuitJob,
                    DateCreate = V_DetailCustomers.AssignedSupportDateCreate,
                    Team = V_DetailCustomers.AssignedSupportTeam
                };
            }

            if (V_DetailCustomers.AccountantTeamID != null)
            {
                res.ResponsibleAccountantTeam = new LKACSoft_AccountantTeam
                {
                    TeamID = V_DetailCustomers.AccountantTeamID,
                    TeamName = V_DetailCustomers.AccountantTeamName,
                    TeamLeader = V_DetailCustomers.AccountantTeamLeader
                };
            }

            if (V_DetailCustomers.DepartmentCode != null)
            {
                res.DepartmentCode = new LKACSoft_Department
                {
                    Code = V_DetailCustomers.DepartmentCode,
                    Name = V_DetailCustomers.DepartmentName,
                    DisplayOrder = V_DetailCustomers.DepartmentDisplayOrder,
                    Closed = V_DetailCustomers.DepartmentClosed
                };
            }

            return res;
        }
    }
}
