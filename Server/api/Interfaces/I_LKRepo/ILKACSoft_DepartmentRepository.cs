
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DepartmentRepository
    {
        Task<List<LKACSoft_Department>> GetAllAsync();
        Task<LKACSoft_Department?> GetByIdAsync(string departmentCode);
    }
}
