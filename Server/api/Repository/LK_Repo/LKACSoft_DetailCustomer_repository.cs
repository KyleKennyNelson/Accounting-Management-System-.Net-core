using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DetailCustomer_repository : ILKACSoft_DetailCustomerRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DetailCustomer_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<List<V_DetailCustomers>> GetAllAsync(QueryObject_DetailCustomer query)
        {
            var detailcustomerList =  await _context.V_DetailCustomers
                                .FromSqlRaw("EXEC DBO.sp_GetAll_V_DetailCustomer")
                                .AsQueryable()
                                .ToListAsync();

            //if (!string.IsNullOrEmpty(query.CustomerCode))
            //{
            //    detailcustomerList = detailcustomerList.Where(de => de.Code.Contains(query.CustomerCode)).ToList();
            //}

            if (!string.IsNullOrEmpty(query.MainAccountantUserID))
            {
                detailcustomerList = detailcustomerList.Where(de => de.MainAccountantID.Contains(query.MainAccountantUserID)).ToList();
            }

            return detailcustomerList;
        }

        public async Task<V_DetailCustomers> GetByIdAsync(string customerCode)
        {
            var customerCodeParam = new SqlParameter("@customerCode", customerCode);

            var detailcustomer = (await _context.V_DetailCustomers
                .FromSqlRaw("EXEC DBO.sp_GetByID_V_DetailCustomer @customerCode", customerCodeParam)
                .ToListAsync())
                .FirstOrDefault();

            return detailcustomer;
        }
    }
}
