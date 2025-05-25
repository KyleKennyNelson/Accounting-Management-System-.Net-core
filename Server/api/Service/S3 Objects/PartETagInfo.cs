using System.ComponentModel.DataAnnotations;

namespace api.Service.S3_Objects
{
    public class PartETagInfo
    {
        [Required]
        public int PartNumber { get; set; }

        [Required]
        public string ETag { get; set; }
    }
}
