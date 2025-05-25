using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_CustomerDTO
{
    public class LKACSoft_CustomerDto
    {
        public string Code { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Address { get; set; }
        public string? LogoS3Key { get; set; }
        public string? FilterLocation { get; set; }
        public int? GetDocsDate { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool? Suspended { get; set; }
        public DateTime? SuspendedTo { get; set; }
        public bool? Dissolved { get; set; }
        public DateTime? DissolvedDate { get; set; }
        public string? MainAccountant { get; set; }
        public string? CreatedBy { get; set; }
        public string? AssignedToCustomerSupport { get; set; }
        public string? ResponsibleAccountantTeam { get; set; }
        public string? LKACSoft_DepartmentCode { get; set; }
        public DateTime? ContractExpiry { get; set; }
        public DateTime? ContractSignedDate { get; set; }
    }
}
