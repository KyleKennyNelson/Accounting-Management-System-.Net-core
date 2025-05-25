using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailCustomersDTO
{
    public class LKACSoft_CustomerDto
    {
        public required LKACSoft_Customer Customer { get; set; }
        public LKACSoft_User? MainAccountant { get; set; }
        public LKACSoft_User? CreateBy { get; set; }
        public LKACSoft_User? AssignedToCustomerSupport { get; set; }
        public LKACSoft_AccountantTeam? ResponsibleAccountantTeam { get; set; }
        public LKACSoft_Department? DepartmentCode { get; set; }
    }
}
