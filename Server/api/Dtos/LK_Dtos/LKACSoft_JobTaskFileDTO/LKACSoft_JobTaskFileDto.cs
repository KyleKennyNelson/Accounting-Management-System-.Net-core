namespace api.Dtos.LK_Dtos.LKACSoft_JobTaskFileDTO
{
    public class LKACSoft_JobTaskFileDto
    {
        public string Code { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? AccountantID { get; set; }
        public int? AccountingStatus { get; set; }
        public DateTime? AccountantReceivedAt { get; set; }
        public DateTime? ReadyToBeReturnedAt { get; set; }
        public int? ArchivingStatus { get; set; }
        public string? PhysicalLocation { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? RelatedToExecution { get; set; }
        public string? DocumentType { get; set; }
        public string? CustomerCode { get; set; }
    }
}
