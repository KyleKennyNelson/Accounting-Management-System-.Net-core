using api.Dtos.LK_Dtos.LKACSoft_JobTaskFileDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_JobTaskFileMapper
    {
        public static LKACSoft_JobTaskFileDto ToLKACSoft_JobTaskFileDto(this LKACSoft_JobTaskFile LKACSoft_JobTaskFile)
        {
            return new LKACSoft_JobTaskFileDto
            {
                Code = LKACSoft_JobTaskFile.Code,
                FileName = LKACSoft_JobTaskFile.FileName,
                FileType = LKACSoft_JobTaskFile.FileType,
                CreatedAt = LKACSoft_JobTaskFile.CreatedAt,
                AccountantID = LKACSoft_JobTaskFile.AccountantID,
                AccountingStatus = LKACSoft_JobTaskFile.AccountingStatus,
                AccountantReceivedAt = LKACSoft_JobTaskFile.AccountantReceivedAt,
                ReadyToBeReturnedAt = LKACSoft_JobTaskFile.ReadyToBeReturnedAt,
                ArchivingStatus = LKACSoft_JobTaskFile.ArchivingStatus,
                PhysicalLocation = LKACSoft_JobTaskFile.PhysicalLocation,
                ArchivedDate = LKACSoft_JobTaskFile.ArchivedDate,
                ReturnedDate = LKACSoft_JobTaskFile.ReturnedDate,
                CreatedBy = LKACSoft_JobTaskFile.CreatedBy,
                RelatedToExecution = LKACSoft_JobTaskFile.RelatedToExecution,
                DocumentType = LKACSoft_JobTaskFile.DocumentType,
                CustomerCode = LKACSoft_JobTaskFile.CustomerCode
            };
        }

        public static LKACSoft_JobTaskFile CreateLKACSoft_JobTaskFileDto(this CreateLKACSoft_JobTaskFileDto CreateLKACSoft_JobTaskFile)
        {
            return new LKACSoft_JobTaskFile
            {
                FileName = CreateLKACSoft_JobTaskFile.FileName,
                //FileType = CreateLKACSoft_JobTaskFile.FileType,
                //FileS3Key = CreateLKACSoft_JobTaskFile.FileS3Key,
                CreatedAt = CreateLKACSoft_JobTaskFile.CreatedAt,
                AccountantID = CreateLKACSoft_JobTaskFile.AccountantID,
                AccountingStatus = CreateLKACSoft_JobTaskFile.AccountingStatus,
                AccountantReceivedAt = CreateLKACSoft_JobTaskFile.AccountantReceivedAt,
                ReadyToBeReturnedAt = CreateLKACSoft_JobTaskFile.ReadyToBeReturnedAt,
                ArchivingStatus = CreateLKACSoft_JobTaskFile.ArchivingStatus,
                PhysicalLocation = CreateLKACSoft_JobTaskFile.PhysicalLocation,
                ArchivedDate = CreateLKACSoft_JobTaskFile.ArchivedDate,
                ReturnedDate = CreateLKACSoft_JobTaskFile.ReturnedDate,
                CreatedBy = CreateLKACSoft_JobTaskFile.CreatedBy,
                RelatedToExecution = CreateLKACSoft_JobTaskFile.RelatedToExecution,
                DocumentType = CreateLKACSoft_JobTaskFile.DocumentType,
            };
        }

        public static LKACSoft_JobTaskFile UpdateLKACSoft_JobTaskFileDto(this UpdateLKACSoft_JobTaskFileDto UpdateLKACSoft_JobTaskFile, string Code)
        {
            return new LKACSoft_JobTaskFile
            {
                Code = Code,
                CreatedAt = UpdateLKACSoft_JobTaskFile.CreatedAt,
                AccountantID = UpdateLKACSoft_JobTaskFile.AccountantID,
                AccountingStatus = UpdateLKACSoft_JobTaskFile.AccountingStatus,
                AccountantReceivedAt = UpdateLKACSoft_JobTaskFile.AccountantReceivedAt,
                ReadyToBeReturnedAt = UpdateLKACSoft_JobTaskFile.ReadyToBeReturnedAt,
                ArchivingStatus = UpdateLKACSoft_JobTaskFile.ArchivingStatus,
                PhysicalLocation = UpdateLKACSoft_JobTaskFile.PhysicalLocation,
                ArchivedDate = UpdateLKACSoft_JobTaskFile.ArchivedDate,
                ReturnedDate = UpdateLKACSoft_JobTaskFile.ReturnedDate,
                CreatedBy = UpdateLKACSoft_JobTaskFile.CreatedBy,
                RelatedToExecution = UpdateLKACSoft_JobTaskFile.RelatedToExecution,
                DocumentType = UpdateLKACSoft_JobTaskFile.DocumentType,
            };
        }

        public static LKACSoft_JobTaskFile UpdateLKACSoft_JobTaskFile_FileDto(this UpdateLKACSoft_JobTaskFile_FileDto UpdateLKACSoft_JobTaskFile_File, string Code)
        {
            return new LKACSoft_JobTaskFile
            {
                Code = Code,
                FileName = UpdateLKACSoft_JobTaskFile_File.FileName,
                FileType = UpdateLKACSoft_JobTaskFile_File.FileType,
                FileS3Key = UpdateLKACSoft_JobTaskFile_File.FileS3Key
            };
        }
    }
}
