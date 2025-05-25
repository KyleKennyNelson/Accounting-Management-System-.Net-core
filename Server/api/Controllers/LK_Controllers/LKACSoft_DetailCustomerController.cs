
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Helpers;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/detailcustomers")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DetailCustomerController : ControllerBase
    {
        private readonly ILKACSoft_DetailCustomerRepository _detailcustomerRepo;

        public LKACSoft_DetailCustomerController(ILKACSoft_DetailCustomerRepository detailcustomerRepo)
        {
            _detailcustomerRepo = detailcustomerRepo;
        }

        // Get all tasks
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDetailCustomers([FromQuery] QueryObject_DetailCustomer queryByMainAccountantUserID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailcustomerModels = await _detailcustomerRepo.GetAllAsync(queryByMainAccountantUserID);
            var detailcustomerDtos = detailcustomerModels.Select(dc => dc.ToLKACSoft_DetailCustomerDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detailcustomerDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detailcustomerDtos);
        }

        // Get customer by Code
        [HttpGet("{customerCode}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailCustomerById(string customerCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailcustomer = await _detailcustomerRepo.GetByIdAsync(customerCode);
            if (detailcustomer == null)
                return NotFound(new { message = "DetailCustomer not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detailcustomer.ToLKACSoft_DetailCustomerDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detailcustomer.ToLKACSoft_DetailCustomerDto());
        }

    }
}
