using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DetailDocumentType_repository : ILKACSoft_DetailDocumentTypeRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DetailDocumentType_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(V_DetailDocumentTypes, string)> AddCustomerDocumentTypeAsync(V_DetailDocumentTypes detailDocumentType)
        {
            var customerCode = new SqlParameter("@CustomerCode", detailDocumentType.Code);

            var documentTypeId = new SqlParameter("@DocumentTypeID", detailDocumentType.DocumentTypeID);

            var documentReceivingMechanism = new SqlParameter("@DocumentReceivingMechanism", string.IsNullOrWhiteSpace(detailDocumentType.DocumentReceivingMechanism)
                ? (object)DBNull.Value : detailDocumentType.DocumentReceivingMechanism);

            var avgAmount = new SqlParameter("@AvgAmount", detailDocumentType.AvgAmount ?? (object)DBNull.Value);

            var registeredAmount = new SqlParameter("@RegisteredAmount", detailDocumentType.RegisteredAmount ?? (object)DBNull.Value);

            //var currentTotalAmount = new SqlParameter("@CurrentTotalAmount", detailDocumentType.CurrentTotalAmount ?? (object)DBNull.Value);

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_DetailDocumentType
                        @CustomerCode, @DocumentTypeID,
                        @DocumentReceivingMechanism, @AvgAmount, @RegisteredAmount,
                        @ResponseMessage OUTPUT
                """,
                customerCode, documentTypeId, documentReceivingMechanism,
                avgAmount, registeredAmount,
                responseMessage
            );

            return (detailDocumentType, responseMessage.Value.ToString());
        }

        public async Task<bool> DeleteAsync(string documentTypeID)
        {
            var documentTypeIDParam = new SqlParameter("@DocumentTypeID", documentTypeID);
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC DBO.sp_Delete_LKACSoft_DocumentType @DocumentTypeID", documentTypeIDParam);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteCustomerDocumentTypeAsync(string customerCode, string documentTypeID)
        {
            var documentTypeIDParam = new SqlParameter("@DocumentTypeID", documentTypeID);

            var customerCodeParam = new SqlParameter("@CustomerCode", customerCode);
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC DBO.sp_Delete_LKACSoft_DetailDocumentType @DocumentTypeID, @CustomerCode", 
                documentTypeIDParam, customerCodeParam);

            return rowsAffected > 0;
        }

        public async Task<List<V_DetailDocumentTypes>> GetAllAsync(QueryObject_DetailCustomer query)
        {
            var detaildocumentTypeList =  await _context.V_DetailDocumentTypes
                                .FromSqlRaw("EXEC DBO.sp_GetAll_V_DetailDocumentType")
                                .AsQueryable()
                                .ToListAsync();

            if (!string.IsNullOrEmpty(query.CustomerCode))
            {
                detaildocumentTypeList = detaildocumentTypeList.Where(de => de.Code.Contains(query.CustomerCode)).ToList();
            }

            return detaildocumentTypeList;
        }

        public async Task<List<LKACSoft_Customer>> GetAllCustomerWithOutDocumentTypeAsync(string documentTypeID)
        {
            var documentTypeIDParam = new SqlParameter("@documentTypeID", documentTypeID);

            var customerList = await _context.LKACSoft_Customer
                            .FromSqlRaw("EXEC DBO.sp_GetExcept_V_CustomerWithOutDetailDocumentType @DocumentTypeID",
                                documentTypeIDParam)
                            .ToListAsync();

            return customerList;
        }

        public async Task<V_DetailDocumentTypes?> GetByIdForCreateAndUpdateAsync(string customerCode, string documentTypeID)
        {
            var customerCodeParam = new SqlParameter("@CustomerCode", customerCode);
            var documentTypeIDParam = new SqlParameter("@DocumentTypeID", documentTypeID);

            var detaildocumentType = (await _context.V_DetailDocumentTypes
                            .FromSqlRaw("EXEC DBO.sp_GetByID_V_DetailDocumentType_CustomerCode_DocumentTypeID @CustomerCode, @DocumentTypeID",
                                customerCodeParam, documentTypeIDParam)
                            .ToListAsync())
                            .FirstOrDefault();

            return detaildocumentType;
        }

        public async Task<(V_DetailDocumentTypes, string)> UpdateCustomerDocumentTypeAsync(V_DetailDocumentTypes detailDocumentType)
        {
            var customerCode = new SqlParameter("@CustomerCode", detailDocumentType.Code);

            var documentTypeId = new SqlParameter("@DocumentTypeID", detailDocumentType.DocumentTypeID);

            var documentReceivingMechanism = new SqlParameter("@DocumentReceivingMechanism", string.IsNullOrWhiteSpace(detailDocumentType.DocumentReceivingMechanism)
                ? (object)DBNull.Value : detailDocumentType.DocumentReceivingMechanism);

            var avgAmount = new SqlParameter("@AvgAmount", detailDocumentType.AvgAmount ?? (object)DBNull.Value);

            var registeredAmount = new SqlParameter("@RegisteredAmount", detailDocumentType.RegisteredAmount ?? (object)DBNull.Value);

            //var currentTotalAmount = new SqlParameter("@CurrentTotalAmount", detailDocumentType.CurrentTotalAmount ?? (object)DBNull.Value);

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Update_LKACSoft_DetailDocumentType
                        @CustomerCode, @DocumentTypeID,
                        @DocumentReceivingMechanism, @AvgAmount, @RegisteredAmount,
                        @ResponseMessage OUTPUT
                """,
                customerCode, documentTypeId, documentReceivingMechanism,
                avgAmount, registeredAmount,
                responseMessage
            );

            return (detailDocumentType, responseMessage.Value.ToString());
        }
    }
}
