using System.ComponentModel.DataAnnotations;

namespace api.Service.S3_Objects
{
    public class CompleteMultipartUpload
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string UploadId { get; set; }

        [Required]
        public List<PartETagInfo> Parts { get; set; }
    }
}
