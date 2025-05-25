
using api.Helpers;
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DetailDocumentTypeRepository
    {
        Task<List<V_DetailDocumentTypes>> GetAllAsync(QueryObject_DetailCustomer query);
        Task<List<LKACSoft_Customer>> GetAllCustomerWithOutDocumentTypeAsync(string documentTypeID);
        Task<V_DetailDocumentTypes?> GetByIdForCreateAndUpdateAsync(string customerCode, string documentTypeID);
        Task<bool> DeleteAsync(string documentTypeID);
        Task<bool> DeleteCustomerDocumentTypeAsync(string customerCode, string documentTypeID);
        Task<(V_DetailDocumentTypes, string)> UpdateCustomerDocumentTypeAsync(V_DetailDocumentTypes detailDocumentType);
        Task<(V_DetailDocumentTypes, string)> AddCustomerDocumentTypeAsync(V_DetailDocumentTypes detailDocumentType);
    }
}
