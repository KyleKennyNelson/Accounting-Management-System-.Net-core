using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailJobTaskFilesDTO
{
    public class LKACSoft_DetailJobTaskFileDto
    {
        public required LKACSoft_JobTaskFile JobTaskFile { get; set; }

        public LKACSoft_AccountingStatus? AccountingStatus { get; set; }

        public LKACSoft_ArchivingStatus? ArchivingStatus { get; set; }

        public LKACSoft_User? Accountant { get; set; }

        public LKACSoft_User? CreatedBy { get; set; }

        public LKACSoft_Execution? RelatedToExecution { get; set; }

        public LKACSoft_CustomerDocumentType? CustomerDocumentType { get; set; }

        public LKACSoft_DocumentType? DocumentType { get; set; }

        public LKACSoft_Customer? Customer { get; set; }
    }
}
