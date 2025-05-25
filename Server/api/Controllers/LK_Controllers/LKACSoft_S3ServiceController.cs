
using Amazon.S3;
using Amazon.S3.Model;
using api.Dtos.LK_Dtos.LKACSoft_JobTaskFileDTO;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Service.S3_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Controllers.LK_Controllers
{
    public class FileUploadModel
    {
        [Required]
        public IFormFile? File { get; set; }
    }
    [Route("api/s3service")]
    [ApiController]
    [Authorize]
    public class LKACSoft_S3ServiceController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAmazonS3 _s3Client;
        private readonly ILKACSoft_JobTaskFileRepository _jobTaskFileRepo;

        public LKACSoft_S3ServiceController(IConfiguration config, IAmazonS3 s3Client,
            ILKACSoft_JobTaskFileRepository jobTaskFileRepo)
        {
            this._config = config;
            this._s3Client = s3Client;
            _jobTaskFileRepo = jobTaskFileRepo;
        }

        // Upload a file
        [HttpPost("{JobTaskFileCode}")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UploadFile([FromRoute] string JobTaskFileCode, [FromForm] FileUploadModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fix for CS8602: Check if model.File is null before accessing its properties
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded");

            using var stream = model.File.OpenReadStream();
            var key = Guid.NewGuid().ToString();

            var jobTaskFile_FileDto = new UpdateLKACSoft_JobTaskFile_FileDto
            {
                FileName = model.File.FileName,
                FileType = model.File.ContentType,
                FileS3Key = key
            };

            var updateJobTaskFile = jobTaskFile_FileDto.UpdateLKACSoft_JobTaskFile_FileDto(JobTaskFileCode);

            var res = await _jobTaskFileRepo.AddFileAsync(updateJobTaskFile);

            if (res != null && res != "")
                return BadRequest(new { message = res });

            var putRequest = new PutObjectRequest
            {
                BucketName = _config["S3Settings:BucketName"],
                Key = $"images/{key}",
                InputStream = stream,
                ContentType = model.File.ContentType,
                Metadata = { ["file-name"] = model.File.FileName }
            };

            var updatedJobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);

            await _s3Client.PutObjectAsync(putRequest);

            return Ok(updatedJobTaskFile);
        }

        // Update file
        [HttpPut("{JobTaskFileCode}")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateFile([FromRoute] string JobTaskFileCode, [FromForm] FileUploadModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);
            if (jobTaskFile == null)
                return NotFound(new { message = "JobTaskFile not found" });

            var S3Key = jobTaskFile.FileS3Key;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "File not found in S3" });

            // Fix for CS8602: Check if model.File is null before accessing its properties
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file updated");

            using var stream = model.File.OpenReadStream();
            var key = Guid.NewGuid().ToString();

            var jobTaskFile_FileDto = new UpdateLKACSoft_JobTaskFile_FileDto
            {
                FileName = model.File.FileName,
                FileType = model.File.ContentType,
                FileS3Key = key
            };

            var updateJobTaskFile = jobTaskFile_FileDto.UpdateLKACSoft_JobTaskFile_FileDto(JobTaskFileCode);

            var res = await _jobTaskFileRepo.UpdateFileAsync(updateJobTaskFile);

            if (res != null && res != "")
                return BadRequest(new { message = res });

            //Delete the old file from S3

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _config["S3Settings:BucketName"],
                Key = $"images/{S3Key}"
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);

            // Upload the new file to S3

            var putRequest = new PutObjectRequest
            {
                BucketName = _config["S3Settings:BucketName"],
                Key = $"images/{key}",
                InputStream = stream,
                ContentType = model.File.ContentType,
                Metadata = { ["file-name"] = model.File.FileName }
            };

            var updatedJobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);

            await _s3Client.PutObjectAsync(putRequest);

            return Ok(updatedJobTaskFile);
        }

        // Get a File
        [HttpGet("DowloadFile/{JobTaskFileCode}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DowloadFile([FromRoute] string JobTaskFileCode)
        {
            var jobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);
            if (jobTaskFile == null)
                return NotFound(new { message = "JobTaskFile not found" });

            var S3Key = jobTaskFile.FileS3Key;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "File not found in S3" });

            var getRequest = new GetObjectRequest
            {
                BucketName = _config["S3Settings:BucketName"],
                Key = $"images/{S3Key}"
            };

            var response = await _s3Client.GetObjectAsync(getRequest);

            return File(response.ResponseStream, response.Headers.ContentType, response.Metadata["file-name"]);

        }

        // Delete a file
        [HttpDelete("{JobTaskFileCode}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteFile([FromRoute] string JobTaskFileCode)
        {
            var jobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);
            if (jobTaskFile == null)
                return NotFound(new { message = "JobTaskFile not found" });

            var S3Key = jobTaskFile.FileS3Key;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "File not found in S3" });

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _config["S3Settings:BucketName"],
                Key = $"images/{S3Key}"
            };

            var jobTaskFile_FileDto = new UpdateLKACSoft_JobTaskFile_FileDto();

            var updateJobTaskFile = jobTaskFile_FileDto.UpdateLKACSoft_JobTaskFile_FileDto(JobTaskFileCode);

            var res = await _jobTaskFileRepo.UpdateFileAsync(updateJobTaskFile);

            if (res != null && res != "")
                return BadRequest(new { message = res });

            await _s3Client.DeleteObjectAsync(deleteRequest);

            return Ok($"File {jobTaskFile.FileName} deleted successfully");
        }

        // Generate a pre-signed URL for GET
        [HttpGet("{JobTaskFileCode}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetPreSignedUrl([FromRoute] string JobTaskFileCode)
        {
            var jobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);
            if (jobTaskFile == null)
                return NotFound(new { message = "JobTaskFile not found" });

            var S3Key = jobTaskFile.FileS3Key;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "File not found in S3" });
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _config["S3Settings:BucketName"],
                    Key = $"images/{S3Key}",
                    Verb = HttpVerb.GET,
                    Expires = DateTime.UtcNow.AddMinutes(3)
                };

                string preSignedUrl = await _s3Client.GetPreSignedURLAsync(request);

                return Ok(new { JobTaskFileCode, url = preSignedUrl });
            }
            catch (AmazonS3Exception ex)
            {
                return BadRequest($"S3 error generating pre-signed URL: {ex.Message}");
            }
        }

        //// Start a multipart upload
        //[HttpPost("start-multipart/{JobTaskFileCode}")]
        //public async Task<IActionResult> StartMultipartUpload([FromRoute] string JobTaskFileCode,
        //    string fileName, string contentType)
        //    //[FromForm] FileUploadModel model)
        //{
        //    try
        //    {
        //        var key = Guid.NewGuid().ToString();
        //        var request = new InitiateMultipartUploadRequest
        //        {
        //            BucketName = _config["S3Settings:BucketName"],
        //            Key = $"images/{key}",
        //            ContentType = /*model.File.ContentType,*/ contentType,
        //            Metadata = { ["file-name"] = /*model.File.FileName }*/ fileName }
        //        };

        //        var jobTaskFile_FileDto = new UpdateLKACSoft_JobTaskFile_FileDto
        //        {
        //            FileName = /*model.File.FileName,*/ fileName,
        //            FileType = /*model.File.ContentType,*/ contentType,
        //            FileS3Key = key
        //        };

        //        var updateJobTaskFile = jobTaskFile_FileDto.UpdateLKACSoft_JobTaskFile_FileDto(JobTaskFileCode);

        //        var res = await _jobTaskFileRepo.UpdateAsync(updateJobTaskFile);

        //        if (res.Item2 != null && res.Item2 != "")
        //            return BadRequest(new { message = res.Item2 });

        //        var updatedJobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(JobTaskFileCode);

        //        var response = await _s3Client.InitiateMultipartUploadAsync(request);

        //        return Ok(new { key, uploadId = response.UploadId });
        //    }
        //    catch (AmazonS3Exception ex)
        //    {
        //        return BadRequest($"S3 error starting multipart upload: {ex.Message}");
        //    }
        //}

        //// Generate a pre-signed URL for a multipart upload part
        //[HttpPost("Presigned-Multipart/{key}")]
        //[Consumes("multipart/form-data")]
        //public IActionResult GeneratePreSignedUrlForPart([FromRoute]string key, string uploadId, int partNumber)
        //{
        //    try
        //    {
        //        var request = new GetPreSignedUrlRequest
        //        {
        //            BucketName = _config["S3Settings:BucketName"],
        //            Key = $"images/{key}",
        //            Verb = HttpVerb.PUT,
        //            Expires = DateTime.UtcNow.AddMinutes(15),
        //            UploadId = uploadId,
        //            PartNumber = partNumber
        //        };

        //        string preSignedUrl = _s3Client.GetPreSignedURL(request);

        //        return Ok(new { key, url = preSignedUrl });
        //    }
        //    catch (AmazonS3Exception ex)
        //    {
        //        return BadRequest($"S3 error generating pre-signed URL for part: {ex.Message}");
        //    }
        //}

        //// Complete a multipart upload
        //[HttpPost("complete-multipart/{key}")]
        //public async Task<IActionResult> CompleteMultipartUpload([FromRoute]string key, [FromBody] CompleteMultipartUpload complete)
        //{
        //    try
        //    {
        //        var request = new CompleteMultipartUploadRequest
        //        {
        //            BucketName = _config["S3Settings:BucketName"],
        //            Key = $"images/{key}",
        //            UploadId = complete.UploadId,
        //            PartETags = complete.Parts.Select(p => new PartETag(p.PartNumber, p.ETag)).ToList()
        //        };

        //        var response = await _s3Client.CompleteMultipartUploadAsync(request);

        //        return Ok(new { key, location = response.Location });
        //    }
        //    catch (AmazonS3Exception ex)
        //    {
        //        return BadRequest($"S3 error completing multipart upload: {ex.Message}");
        //    }
        //}
    }
}
