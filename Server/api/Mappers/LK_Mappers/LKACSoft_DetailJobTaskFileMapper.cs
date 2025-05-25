using api.Dtos.LK_Dtos.LKACSoft_DetailJobTaskFilesDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailJobTaskFileMapper
    {
        public static LKACSoft_DetailJobTaskFileDto ToLKACSoft_DetailJobTaskFileDto(this V_DetailJobTaskFiles V_DetailJobTaskFiles)
        {
            var res = new LKACSoft_DetailJobTaskFileDto
            {
                JobTaskFile = new LKACSoft_JobTaskFile
                {
                    Code = V_DetailJobTaskFiles.JobTaskFileCode,
                    FileName = V_DetailJobTaskFiles.FileName,
                    FileType = V_DetailJobTaskFiles.FileType,
                    CreatedAt = V_DetailJobTaskFiles.CreatedAt,
                    AccountantReceivedAt = V_DetailJobTaskFiles.AccountantReceivedAt,
                    ReadyToBeReturnedAt = V_DetailJobTaskFiles.ReadyToBeReturnedAt,
                    PhysicalLocation = V_DetailJobTaskFiles.PhysicalLocation,
                    ArchivedDate = V_DetailJobTaskFiles.ArchivedDate,
                    ReturnedDate = V_DetailJobTaskFiles.ReturnedDate
                }
            };

            if (V_DetailJobTaskFiles.AccountingStatusID != null)
            {
                res.AccountingStatus = new LKACSoft_AccountingStatus
                {
                    ID = (int)V_DetailJobTaskFiles.AccountingStatusID,
                    Name = V_DetailJobTaskFiles.AccountingStatusName,
                };
            }

            if (V_DetailJobTaskFiles.ArchivingStatusID != null)
            {
                res.ArchivingStatus = new LKACSoft_ArchivingStatus
                {
                    ID = (int)V_DetailJobTaskFiles.ArchivingStatusID,
                    Name = V_DetailJobTaskFiles.ArchivingStatusName,
                };
            }

            if (V_DetailJobTaskFiles.accountantID != null)
            {
                res.Accountant = new LKACSoft_User
                {
                    ID = V_DetailJobTaskFiles.accountantID,
                    Username = V_DetailJobTaskFiles.accountantUsername,
                    Firstname = V_DetailJobTaskFiles.accountantFirstname,
                    LastName = V_DetailJobTaskFiles.accountantLastname,
                    Avatar = V_DetailJobTaskFiles.accountantAvatar,
                    Address = V_DetailJobTaskFiles.accountantAddress,
                    District = V_DetailJobTaskFiles.accountantDistrict,
                    Dob = V_DetailJobTaskFiles.accountantDob,
                    IsQuitJob = V_DetailJobTaskFiles.accountantIsQuitJob,
                    DateCreate = V_DetailJobTaskFiles.accountantDateCreate,
                    Team = V_DetailJobTaskFiles.accountantTeam
                };
            }

            if (V_DetailJobTaskFiles.CreatedByID != null)
            {
                res.CreatedBy = new LKACSoft_User
                {
                    ID = V_DetailJobTaskFiles.CreatedByID,
                    Username = V_DetailJobTaskFiles.CreatedByUsername,
                    Firstname = V_DetailJobTaskFiles.CreatedByFirstname,
                    LastName = V_DetailJobTaskFiles.CreatedByLastname,
                    Avatar = V_DetailJobTaskFiles.CreatedByAvatar,
                    Address = V_DetailJobTaskFiles.CreatedByAddress,
                    District = V_DetailJobTaskFiles.CreatedByDistrict,
                    Dob = V_DetailJobTaskFiles.CreatedByDob,
                    IsQuitJob = V_DetailJobTaskFiles.CreatedByIsQuitJob,
                    DateCreate = V_DetailJobTaskFiles.CreatedByDateCreate,
                    Team = V_DetailJobTaskFiles.CreatedByTeam
                };
            }

            if (V_DetailJobTaskFiles.ExecutionID != null)
            {
                res.RelatedToExecution = new LKACSoft_Execution
                {
                    ExecutionID = V_DetailJobTaskFiles.ExecutionID,
                    ExecutionName = V_DetailJobTaskFiles.ExecutionName,
                    CreatedBy = V_DetailJobTaskFiles.CreatedByForExecution,
                    DateCreated = V_DetailJobTaskFiles.DateCreated,
                    IsPeriodic = V_DetailJobTaskFiles.IsPeriodic
                };
            }

            if (V_DetailJobTaskFiles.DocumentTypeID != null)
            {
                res.CustomerDocumentType = new LKACSoft_CustomerDocumentType
                {
                    DocumentTypeID = V_DetailJobTaskFiles.DocumentTypeID,
                    DocumentReceivingMechanism = V_DetailJobTaskFiles.DocumentReceivingMechanism,
                    AvgAmount = V_DetailJobTaskFiles.AvgAmount,
                    RegisteredAmount = V_DetailJobTaskFiles.RegisteredAmount
                };
            }

            if (V_DetailJobTaskFiles.DocumentTypeName != null)
            {
                res.DocumentType = new LKACSoft_DocumentType
                {
                    DocumentTypeName = V_DetailJobTaskFiles.DocumentTypeName,
                };
            }

            if (V_DetailJobTaskFiles.Code != null)
            {
                res.Customer = new LKACSoft_Customer
                {
                    Code = V_DetailJobTaskFiles.Code,
                    Name = V_DetailJobTaskFiles.Name,
                    ShortName = V_DetailJobTaskFiles.ShortName,
                    Address = V_DetailJobTaskFiles.Address,
                    LogoS3Key = V_DetailJobTaskFiles.LogoS3Key,
                    FilterLocation = V_DetailJobTaskFiles.FilterLocation,
                    GetDocsDate = V_DetailJobTaskFiles.GetDocsDate,
                    DateCreate = V_DetailJobTaskFiles.DateCreate,
                    Suspended = V_DetailJobTaskFiles.Suspended,
                    SuspendedTo = V_DetailJobTaskFiles.SuspendedTo,
                    Dissolved = V_DetailJobTaskFiles.Dissolved,
                    DissolvedDate = V_DetailJobTaskFiles.DissolvedDate,
                    MainAccountant = V_DetailJobTaskFiles.MainAccountant,
                    CreatedBy = V_DetailJobTaskFiles.CreatedBy,
                    AssignedToCustomerSupport = V_DetailJobTaskFiles.AssignedToCustomerSupport,
                    ResponsibleAccountantTeam = V_DetailJobTaskFiles.ResponsibleAccountantTeam,
                    LKACSoft_DepartmentCode = V_DetailJobTaskFiles.LKACSoft_DepartmentCode,
                    ContractExpiry = V_DetailJobTaskFiles.ContractExpiry,
                    ContractSignedDate = V_DetailJobTaskFiles.ContractSignedDate
                };
            }

            return res;
        }
    }
}
