using api.Dtos.APIPermission;
using api.Identity;
using api.Interfaces;
using api.Models;
using api.Models.AuthModels;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repository
{
    public class ApiPermissionRole_repository : IApiPermissionRole
    {
        private readonly ApplicationDBContext _context;
        public ApiPermissionRole_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<V_ApiPermissionRole>> getRolesFromAPIPerm(APIPermissionDto apiperm)
        {
            var ApiNameParam = new SqlParameter("@ApiName", apiperm.APIName);

            var PermNameParam = new SqlParameter("@PermissionName", apiperm.PermissionName);

            var APIPermRoles = await _context.V_ApiPermissionRole
                .FromSqlRaw("EXEC DBO.sp_Get_Roles_InApiPermission_By_API_Perm @ApiName, @PermissionName", ApiNameParam, PermNameParam)
                .ToListAsync();
            return APIPermRoles;
        }
    }
}
