using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_JobTaskFile_repository : ILKACSoft_JobTaskFileRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_JobTaskFile_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(LKACSoft_JobTaskFile, string)> AddAsync(LKACSoft_JobTaskFile jobTaskFile)
        {
            var fileName = new SqlParameter("@FileName", jobTaskFile.FileName ?? (object)DBNull.Value);
            //var fileType = new SqlParameter("@FileType", jobTaskFile.FileType ?? (object)DBNull.Value);
            //var FileS3Key = new SqlParameter("@FileS3Key", jobTaskFile.FileS3Key ?? (object)DBNull.Value);
            var accountantID = new SqlParameter("@AccountantID", jobTaskFile.AccountantID ?? (object)DBNull.Value);
            var accountingStatus = new SqlParameter("@AccountingStatus", jobTaskFile.AccountingStatus ?? (object)DBNull.Value);
            var accountantReceivedAt = new SqlParameter("@AccountantReceivedAt", jobTaskFile.AccountantReceivedAt ?? (object)DBNull.Value);
            var readyToBeReturnedAt = new SqlParameter("@ReadyToBeReturnedAt", jobTaskFile.ReadyToBeReturnedAt ?? (object)DBNull.Value);
            var archivingStatus = new SqlParameter("@ArchivingStatus", jobTaskFile.ArchivingStatus ?? (object)DBNull.Value);
            var physicalLocation = new SqlParameter("@PhysicalLocation", jobTaskFile.PhysicalLocation ?? (object)DBNull.Value);
            var archivedDate = new SqlParameter("@ArchivedDate", jobTaskFile.ArchivedDate ?? (object)DBNull.Value);
            var returnedDate = new SqlParameter("@ReturnedDate", jobTaskFile.ReturnedDate ?? (object)DBNull.Value);
            var createdBy = new SqlParameter("@CreatedBy", jobTaskFile.CreatedBy ?? (object)DBNull.Value);
            var relatedToExecution = new SqlParameter("@RelatedToExecution", jobTaskFile.RelatedToExecution ?? (object)DBNull.Value);
            var documentType = new SqlParameter("@DocumentType", jobTaskFile.DocumentType ?? (object)DBNull.Value);
            //var customerCode = new SqlParameter("@CustomerCode", jobTaskFile.CustomerCode ?? (object)DBNull.Value);

            var newCode = new SqlParameter("@NewCode", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_JobTaskFile
                        @FileName,
                        @AccountantID, @AccountingStatus, @AccountantReceivedAt, 
                        @ReadyToBeReturnedAt, @ArchivingStatus, @PhysicalLocation,
                        @ArchivedDate, @ReturnedDate, @CreatedBy, @RelatedToExecution,
                        @DocumentType, @NewCode OUTPUT, @ResponseMessage OUTPUT
                """,
                fileName, accountantID, accountingStatus,
                accountantReceivedAt, readyToBeReturnedAt, archivingStatus, physicalLocation,
                archivedDate, returnedDate, createdBy, relatedToExecution, documentType,
                newCode, responseMessage
            );

            jobTaskFile.Code = newCode.Value.ToString();

            return (jobTaskFile, responseMessage.Value.ToString());
        }

        public async Task<string> AddFileAsync(LKACSoft_JobTaskFile jobTaskFile)
        {
            var code = new SqlParameter("@Code", jobTaskFile.Code);
            var fileName = new SqlParameter("@FileName", string.IsNullOrWhiteSpace(jobTaskFile.FileName) ? (object)DBNull.Value : jobTaskFile.FileName);
            var fileType = new SqlParameter("@FileType", string.IsNullOrWhiteSpace(jobTaskFile.FileType) ? (object)DBNull.Value : jobTaskFile.FileType);
            var FileS3Key = new SqlParameter("@FileS3Key", string.IsNullOrWhiteSpace(jobTaskFile.FileS3Key) ? (object)DBNull.Value : jobTaskFile.FileS3Key);


            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_JobTaskFile_File
                        @Code, @FileName, @FileType, @FileS3Key,
                        @ResponseMessage OUTPUT
                """,
                code, fileName, fileType, FileS3Key, responseMessage
            );

            return responseMessage.Value.ToString();
        }

        public async Task<bool> DeleteAsync(string jobTaskFileCode)
        {
            var jobTaskFileCodeParam = new SqlParameter("@Code", jobTaskFileCode);
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC DBO.sp_Delete_LKACSoft_JobTaskFile @Code", jobTaskFileCodeParam);
            return rowsAffected > 0;
        }

        public async Task<List<V_DetailJobTaskFiles>> GetAllAsync()
        {
            var jobTaskFileList = await _context.V_DetailJobTaskFiles
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_DetailJobTaskFile")
                                .ToListAsync();
            return jobTaskFileList;
        }

        public async Task<LKACSoft_JobTaskFile?> GetByCodeJTFAsync(string jobTaskFileCode)
        {
            var jobTaskFileCodeParam = new SqlParameter("@code", jobTaskFileCode);

            var jobTaskFile = (await _context.LKACSoft_JobTaskFile
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_JobTaskFile @code", jobTaskFileCodeParam)
                .ToListAsync())
                .FirstOrDefault();

            return jobTaskFile;
        }

        public async Task<V_DetailJobTaskFiles?> GetByIdAsync(string jobTaskFileCode)
        {
            var jobTaskFileCodeParam = new SqlParameter("@code", jobTaskFileCode);

            var jobTaskFile = (await _context.V_DetailJobTaskFiles
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_DetailJobTaskFile @code", jobTaskFileCodeParam)
                .ToListAsync())
                .FirstOrDefault();

            return jobTaskFile;
        }

        public async Task<(LKACSoft_JobTaskFile, string)> UpdateAsync(LKACSoft_JobTaskFile jobTaskFile)
        {
            var code = new SqlParameter("@Code", jobTaskFile.Code);
            //var fileName = new SqlParameter("@FileName", string.IsNullOrWhiteSpace(jobTaskFile.FileName) ? (object)DBNull.Value : jobTaskFile.FileName);
            //var fileType = new SqlParameter("@FileType", string.IsNullOrWhiteSpace(jobTaskFile.FileType) ? (object)DBNull.Value : jobTaskFile.FileType);
            //var FileS3Key = new SqlParameter("@FileS3Key", string.IsNullOrWhiteSpace(jobTaskFile.FileS3Key) ? (object)DBNull.Value : jobTaskFile.FileS3Key);
            //var createdAt = new SqlParameter("@CreatedAt", jobTaskFile.CreatedAt ?? (object)DBNull.Value);
            var accountantID = new SqlParameter("@AccountantID", string.IsNullOrWhiteSpace(jobTaskFile.AccountantID) ? (object)DBNull.Value : jobTaskFile.AccountantID);
            var accountingStatus = new SqlParameter("@AccountingStatus", jobTaskFile.AccountingStatus ?? jobTaskFile.AccountingStatus);
            var accountantReceivedAt = new SqlParameter("@AccountantReceivedAt", jobTaskFile.AccountantReceivedAt ?? (object)DBNull.Value);
            var readyToBeReturnedAt = new SqlParameter("@ReadyToBeReturnedAt", jobTaskFile.ReadyToBeReturnedAt ?? (object)DBNull.Value);
            var archivingStatus = new SqlParameter("@ArchivingStatus", jobTaskFile.ArchivingStatus ?? jobTaskFile.ArchivingStatus);
            var physicalLocation = new SqlParameter("@PhysicalLocation", string.IsNullOrWhiteSpace(jobTaskFile.PhysicalLocation) ? (object)DBNull.Value : jobTaskFile.PhysicalLocation);
            var archivedDate = new SqlParameter("@ArchivedDate", jobTaskFile.ArchivedDate ?? (object)DBNull.Value);
            var returnedDate = new SqlParameter("@ReturnedDate", jobTaskFile.ReturnedDate ?? (object)DBNull.Value);
            var createdBy = new SqlParameter("@CreatedBy", string.IsNullOrWhiteSpace(jobTaskFile.CreatedBy) ? (object)DBNull.Value : jobTaskFile.CreatedBy);
            var relatedToExecution = new SqlParameter("@RelatedToExecution", string.IsNullOrWhiteSpace(jobTaskFile.RelatedToExecution) ? (object)DBNull.Value : jobTaskFile.RelatedToExecution);
            var documentType = new SqlParameter("@DocumentType", string.IsNullOrWhiteSpace(jobTaskFile.DocumentType) ? (object)DBNull.Value : jobTaskFile.DocumentType);
            //var customerCode = new SqlParameter("@CustomerCode", jobTaskFile.CustomerCode ?? (object)DBNull.Value);

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Update_LKACSoft_JobTaskFile
                        @Code, @AccountantID, @AccountingStatus, 
                        @AccountantReceivedAt, @ReadyToBeReturnedAt, @ArchivingStatus, @PhysicalLocation,
                        @ArchivedDate, @ReturnedDate, @CreatedBy, @RelatedToExecution, @DocumentType,
                        @ResponseMessage OUTPUT
                """,
                code, accountantID, accountingStatus, accountantReceivedAt, 
                readyToBeReturnedAt, archivingStatus, physicalLocation,
                archivedDate, returnedDate, createdBy, relatedToExecution, documentType,
                responseMessage
            );

            return (jobTaskFile, responseMessage.Value.ToString());
        }

        public async Task<string> UpdateFileAsync(LKACSoft_JobTaskFile jobTaskFile)
        {
            var code = new SqlParameter("@Code", jobTaskFile.Code);
            var fileName = new SqlParameter("@FileName", string.IsNullOrWhiteSpace(jobTaskFile.FileName) ? (object)DBNull.Value : jobTaskFile.FileName);
            var fileType = new SqlParameter("@FileType", string.IsNullOrWhiteSpace(jobTaskFile.FileType) ? (object)DBNull.Value : jobTaskFile.FileType);
            var FileS3Key = new SqlParameter("@FileS3Key", string.IsNullOrWhiteSpace(jobTaskFile.FileS3Key) ? (object)DBNull.Value : jobTaskFile.FileS3Key);
            

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Update_LKACSoft_JobTaskFile_File
                        @Code, @FileName, @FileType, @FileS3Key, 
                        @ResponseMessage OUTPUT
                """,
                code, fileName, fileType, FileS3Key, responseMessage
            );

            return responseMessage.Value.ToString();
        }
    }
}
