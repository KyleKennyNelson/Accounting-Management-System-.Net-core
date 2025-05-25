using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailDocumentTypesDTO
{
    public class CreateLKACSoft_DetailDocumentTypeDto
    {
        //public required string? CustomerCode { get; set; }
        //public required string? DocumentTypeID { get; set; }
        public string? DocumentReceivingMechanism { get; set; }
        public int? AvgAmount { get; set; }
        public int? RegisteredAmount { get; set; }
    }
}
