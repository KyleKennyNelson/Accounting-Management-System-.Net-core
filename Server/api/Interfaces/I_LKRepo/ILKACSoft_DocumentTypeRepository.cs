
using api.Helpers;
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DocumentTypeRepository
    {
        Task<List<V_DocumentTypeDtos>> GetAllAsync();
        Task<V_DocumentTypeDtos?> GetByIdAsync(string documentTypeID);
    }
}
