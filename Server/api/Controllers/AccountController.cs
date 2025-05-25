using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using api.Interfaces;
using api.Identity;
using api.Dtos.LK_Dtos.LKACSoft_UserDTO;
using api.Mappers.LK_Mappers;
using api.Interfaces.I_LKRepo;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILKACSoft_UserRepository _userRepo;

        //private readonly IUser_Roles_repository _user_Roles_Repository;

        private readonly ITokenService _tokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILKACSoft_UserRepository userRepo,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _userRepo = userRepo;

            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid login request." });

            // Find the user by username
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return Unauthorized(new { Message = "Invalid username." });

            // Check the password
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { Message = "Invalid username or password." });

            // Get user roles
            //var roles = await _user_Roles_Repository.getRolesFromUser(user.UserName);
            var roles = await _userManager.GetRolesAsync(user);

            // Generate the token
            var token = await _tokenService.CreateToken(user.Id, roles);

            // Return the token to the frontend
            return Ok(new { Token = token });
        }

        // Create a new user
        [HttpPost("register")]
        //[Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> RegistUser([FromBody] TestCreateLKACSoft_UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = userDto.TestCreateLKACSoft_UserDto();

            var res = await _userRepo.AddAsync(newUser);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(newExecution.ToLKACSoft_ExecutionDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(newUser.ToLKACSoft_UserDto());
        }
    }

    // Login request model
    public class LoginModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}