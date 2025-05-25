using api.Dtos.APIPermission;
using api.Models;
using api.Models.AuthModels;

namespace api.Interfaces
{
    public interface IApiPermissionRole
    {
        public Task<List<V_ApiPermissionRole>> getRolesFromAPIPerm(APIPermissionDto apiperm);
    }
}
