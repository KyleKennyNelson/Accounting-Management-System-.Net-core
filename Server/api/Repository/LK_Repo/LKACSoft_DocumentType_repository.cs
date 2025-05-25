using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DocumentType_repository : ILKACSoft_DocumentTypeRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DocumentType_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<V_DocumentTypeDtos>> GetAllAsync()
        {
            var documentTypeDtoList =  await _context.V_DocumentTypeDtos
                                .FromSqlRaw("EXEC DBO.sp_GetAll_V_DocumentTypeDto")
                                .ToListAsync();

            return documentTypeDtoList;
        }

        public async Task<V_DocumentTypeDtos?> GetByIdAsync(string documentTypeID)
        {
            var documentTypeIDParam = new SqlParameter("@DocumentTypeID", documentTypeID);

            var documentTypeDto = (await _context.V_DocumentTypeDtos
                            .FromSqlRaw("EXEC DBO.sp_GetByID_V_DocumentTypeDto @DocumentTypeID",
                                documentTypeIDParam)
                            .ToListAsync())
                            .FirstOrDefault();

            return documentTypeDto;
        }
    }
}
