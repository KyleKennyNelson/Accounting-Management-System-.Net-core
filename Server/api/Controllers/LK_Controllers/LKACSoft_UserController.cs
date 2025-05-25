
using Amazon.S3.Model;
using Amazon.S3;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using api.Dtos.LK_Dtos.LKACSoft_UserDTO;
using api.Dtos.LK_Dtos.LKACSoft_ExecutionDTO;
using System.Security.Claims;

namespace api.Controllers.LK_Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class LKACSoft_UserController : ControllerBase
    {
        private readonly ILKACSoft_UserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IAmazonS3 _s3Client;

        public LKACSoft_UserController(ILKACSoft_UserRepository userRepo, IConfiguration config, IAmazonS3 s3Client)
        {
            _userRepo = userRepo;
            _config = config;
            _s3Client = s3Client;
        }

        // Get all users
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModels = await _userRepo.GetAllAsync();
            var userDtos = userModels.Select(u => u.ToLKACSoft_UserDto());

            return Ok(userDtos);
        }

        // Get user by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user.ToLKACSoft_UserDto());
        }

        // Get users by TeamID
        [HttpGet("GetUserByTeamID")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetUsersOfTeam()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var CurrUser = await _userRepo.GetByIdAsync(userId);

            if (CurrUser == null)
                return NotFound(new { message = "Current user not found" });

            var userModels = await _userRepo.GetAllAsync();
            var userDtos = userModels.Where(u => u.Team == CurrUser.Team).Select(u => u.ToLKACSoft_UserDto());

            return Ok(userDtos);
        }


        // Get users by TeamID
        [HttpGet("GetUserByTeamID/{teamid}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetUserByTeamId([FromRoute]string teamid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModels = await _userRepo.GetAllAsync();
            var userDtos = userModels.Where(u => u.Team == teamid).Select(u => u.ToLKACSoft_UserDto());

            return Ok(userDtos);
        }

        // Upload avatar
        [HttpPost("Avatar/{UserID}")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UploadAvatar([FromRoute] string UserID, [FromForm] FileUploadModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fix for CS8602: Check if model.File is null before accessing its properties
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded");

            using var stream = model.File.OpenReadStream();
            var key = Guid.NewGuid().ToString();

            var user_AvatarDto = new UpdateLKACSoft_User_AvatarDto
            {
                Avatar = key
            };

            var AddUserAvatar = user_AvatarDto.UpdateLKACSoft_User_AvatarDto(UserID);

            var res = await _userRepo.AddAvatarAsync(AddUserAvatar);

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

            var updatedAvatar = await _userRepo.GetByIdAsync(UserID);

            await _s3Client.PutObjectAsync(putRequest);

            return Ok(updatedAvatar);
        }

        // Update avatar
        [HttpPut("Avatar/{UserID}")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateAvatar([FromRoute] string UserID, [FromForm] FileUploadModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepo.GetByIdAsync(UserID);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var S3Key = user.Avatar;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "Avatar not found in S3" });

            // Fix for CS8602: Check if model.File is null before accessing its properties
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No avatar updated");

            using var stream = model.File.OpenReadStream();
            var NewKey = Guid.NewGuid().ToString();

            var user_AvatarDto = new UpdateLKACSoft_User_AvatarDto
            {
                Avatar = NewKey
            };

            var updateUser = user_AvatarDto.UpdateLKACSoft_User_AvatarDto(UserID);

            var res = await _userRepo.UpdateAvatarAsync(updateUser);

            if (res != null && res != "")
                return BadRequest(new { message = res });

            //Delete the old avatar from S3

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
                Key = $"images/{NewKey}",
                InputStream = stream,
                ContentType = model.File.ContentType,
                Metadata = { ["file-name"] = model.File.FileName }
            };

            var updatedAvatar = await _userRepo.GetByIdAsync(UserID);

            await _s3Client.PutObjectAsync(putRequest);

            return Ok(updatedAvatar);
        }

        // Delete avatar
        [HttpDelete("Avatar/{UserID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteAvatar([FromRoute] string UserID)
        {
            var user = await _userRepo.GetByIdAsync(UserID);
            if (user == null)
                return NotFound(new { message = "user not found" });

            var S3Key = user.Avatar;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "Avatar not found in S3" });

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _config["S3Settings:BucketName"],
                Key = $"images/{S3Key}"
            };

            var user_AvatarDto = new UpdateLKACSoft_User_AvatarDto();

            var updateUser = user_AvatarDto.UpdateLKACSoft_User_AvatarDto(UserID);

            var res = await _userRepo.UpdateAvatarAsync(updateUser);

            if (res != null && res != "")
                return BadRequest(new { message = res });

            await _s3Client.DeleteObjectAsync(deleteRequest);

            return Ok($"User's avatar deleted successfully");
        }

        // Generate a pre-signed URL for GET
        [HttpGet("Avatar/{UserID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetPreSignedUrl([FromRoute] string UserID)
        {
            var user = await _userRepo.GetByIdAsync(UserID);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var S3Key = user.Avatar;

            if (string.IsNullOrEmpty(S3Key))
                return NotFound(new { message = "Avatar not found in S3" });
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

                return Ok(new { UserID, url = preSignedUrl });
            }
            catch (AmazonS3Exception ex)
            {
                return BadRequest($"S3 error generating pre-signed URL: {ex.Message}");
            }
        }
    }
}
