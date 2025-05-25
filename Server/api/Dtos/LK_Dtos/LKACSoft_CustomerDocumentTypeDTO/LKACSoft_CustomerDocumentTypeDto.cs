using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_CustomerDocumentTypeDTO
{
    public class LKACSoft_CustomerDocumentTypeDto
    {
        public required string CustomerCode { get; set; }
        public required string DocumentTypeID { get; set; }
        public string? DocumentReceivingMechanism { get; set; }
        public int? AvgAmount { get; set; }
        public int? RegisteredAmount { get; set; }
        public int? CurrentTotalAmount { get; set; }
    }
}
