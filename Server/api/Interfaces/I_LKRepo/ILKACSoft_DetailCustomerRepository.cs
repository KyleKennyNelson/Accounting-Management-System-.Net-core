
using api.Helpers;
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DetailCustomerRepository
    {
        Task<List<V_DetailCustomers>> GetAllAsync(QueryObject_DetailCustomer query);
        Task<V_DetailCustomers?> GetByIdAsync(string customerCode);
    }
}
