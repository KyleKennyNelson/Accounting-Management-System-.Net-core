
using Amazon.S3;
using Amazon.S3.Model;
using api.Dtos.LK_Dtos.LKACSoft_JobTaskFileDTO;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/jobtaskfiles")]
    [ApiController]
    [Authorize]
    public class LKACSoft_JobTaskFileController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ILKACSoft_JobTaskFileRepository _jobTaskFileRepo;
        private readonly IConfiguration _config;

        public LKACSoft_JobTaskFileController(IConfiguration config,
            ILKACSoft_JobTaskFileRepository jobTaskFileRepo, IAmazonS3 s3Client)
        {
            _jobTaskFileRepo = jobTaskFileRepo;
            _s3Client = s3Client;
            _config = config;
        }

        // Get all job task files
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllJobTaskFiles()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobTaskFileModels = await _jobTaskFileRepo.GetAllAsync();
            var jobTaskFileDtos = jobTaskFileModels.Select(jtf => jtf.ToLKACSoft_DetailJobTaskFileDto());


            //var jsonResult = JsonSerializer.Serialize(jobTaskFileDtos);

            return Ok(jobTaskFileDtos);
        }

        // Get job task file by Code
        [HttpGet("{jobTaskFileCode}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetJobTaskFileByCode(string jobTaskFileCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobTaskFile = await _jobTaskFileRepo.GetByIdAsync(jobTaskFileCode);
            if (jobTaskFile == null)
                return NotFound(new { message = "JobTaskFile not found" });

            //var jsonResult = JsonSerializer.Serialize(jobTaskFile.ToLKACSoft_DetailJobTaskFileDto());

            return Ok(jobTaskFile.ToLKACSoft_DetailJobTaskFileDto());
        }

        // Create a new JobTaskFile
        [HttpPost]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> CreateJobTaskFile([FromBody] CreateLKACSoft_JobTaskFileDto jobTaskFileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newJobTaskFile = jobTaskFileDto.CreateLKACSoft_JobTaskFileDto();

            var res = await _jobTaskFileRepo.AddAsync(newJobTaskFile);
            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });


            //var jsonResult = JsonSerializer.Serialize(newJobTaskFile.ToLKACSoft_JobTaskFileDto());

            //return Content(jsonResult, "application/json");
            return Ok(newJobTaskFile.ToLKACSoft_JobTaskFileDto());

            //return CreatedAtAction(nameof(GetJobTaskFileByCode), new { id = newJobTaskFile.Code }, newJobTaskFile.ToLKACSoft_JobTaskFileDto());
        }

        // Update an existing JobTaskFile
        [HttpPut("{code}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateJobTaskFile([FromBody] UpdateLKACSoft_JobTaskFileDto jobTaskFileDto, [FromRoute] string code)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateJobTaskFile = jobTaskFileDto.UpdateLKACSoft_JobTaskFileDto(code);

            var res = await _jobTaskFileRepo.UpdateAsync(updateJobTaskFile);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            var updatedJobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(code);

            //var jsonResult = JsonSerializer.Serialize(updatedJobTaskFile.ToLKACSoft_JobTaskFileDto());

            //return Content(jsonResult, "application/json");

            return Ok(updatedJobTaskFile.ToLKACSoft_JobTaskFileDto());
        }

        // Delete a JobTaskFile by Code
        [HttpDelete("{code}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteJobTaskFile([FromRoute]string code)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jobTaskFile = await _jobTaskFileRepo.GetByCodeJTFAsync(code);
            if (jobTaskFile == null)
                return NotFound(new { message = "JobTaskFile not found" });

            var S3Key = jobTaskFile.FileS3Key;

            if (!string.IsNullOrEmpty(S3Key))
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _config["S3Settings:BucketName"],
                    Key = $"images/{S3Key}"
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);

            }

            var isDeleted = await _jobTaskFileRepo.DeleteAsync(code);
            if (!isDeleted)
                return NotFound(new { message = "JobTaskFile not found or already deleted" });

            return NoContent();
        }
    }
}
