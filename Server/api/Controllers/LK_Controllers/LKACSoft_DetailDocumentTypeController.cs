
using Amazon.Runtime.Documents;
using api.Dtos.LK_Dtos.LKACSoft_DetailDocumentTypesDTO;
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/detaildocumentTypes")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DetailDocumentTypeController : ControllerBase
    {
        private readonly ILKACSoft_DetailDocumentTypeRepository _detaildocumentTypeRepo;

        public LKACSoft_DetailDocumentTypeController(ILKACSoft_DetailDocumentTypeRepository detaildocumentTypeRepo)
        {
            _detaildocumentTypeRepo = detaildocumentTypeRepo;
        }

        // Get all documenttypes
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDetailDocumentTypes([FromQuery] QueryObject_DetailCustomer queryByCustomer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detaildocumentTypeModels = await _detaildocumentTypeRepo.GetAllAsync(queryByCustomer);
            var detaildocumentTypeDtos = detaildocumentTypeModels.Select(dt => dt.ToLKACSoft_DetailDocumentTypeDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detaildocumentTypeDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detaildocumentTypeDtos);
        }

        // Get all customers without documentTypes
        [HttpGet("GetCustomersWithoutDocumentType/{documentTypeID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllCustomersWithoutDocumentType([FromRoute] string documentTypeID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerModels = await _detaildocumentTypeRepo
                                                    .GetAllCustomerWithOutDocumentTypeAsync(documentTypeID);
            var customerDtos = customerModels.Select(c => c.ToLKACSoft_CustomerDto());

            return Ok(customerDtos);
        }

        // Get DocumentType by ID
        [HttpGet("{customerCode}/{documentTypeID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailDocumentTypeById(
            [FromRoute] string customerCode, [FromRoute]string documentTypeID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detaildocumentType = await _detaildocumentTypeRepo.GetByIdForCreateAndUpdateAsync(
                                                    customerCode, documentTypeID);
            if (detaildocumentType == null)
                return NotFound(new { message = "DetailDocumentType not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detaildocumentType.ToLKACSoft_DetailDocumentTypeDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detaildocumentType.ToLKACSoft_DetailDocumentTypeDto());
        }

        // Delete a documentType by ID
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteDocumentType(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isDeleted = await _detaildocumentTypeRepo.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(new { message = "DocumentType not found or already deleted" });

            return NoContent();
        }

        // Delete a CustomerDocumentType by customerCode, DocumentTypeID
        [HttpDelete("{customerCode}/{documentTypeID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteCustomerDocumentType(
            [FromRoute]string customerCode, [FromRoute] string documentTypeID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isDeleted = await _detaildocumentTypeRepo.DeleteCustomerDocumentTypeAsync(
                customerCode, documentTypeID);
            if (!isDeleted)
                return NotFound(new { message = "CustomerDocumentType not found or already deleted" });

            return NoContent();
        }

        // Update an existing CustomerDocumentType by customerCode, DocumentTypeID
        [HttpPut("{customerCode}/{documentTypeID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateCustomerDocumentType(
            [FromBody] UpdateLKACSoft_DetailDocumentTypeDto detailDocumentTypeDto, 
            [FromRoute] string customerCode, [FromRoute] string documentTypeID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateDetailDocumentType = detailDocumentTypeDto.UpdateLKACSoft_DetailDocumentTypeDto(
                                                                    customerCode, documentTypeID);

            var res = await _detaildocumentTypeRepo.UpdateCustomerDocumentTypeAsync(updateDetailDocumentType);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            var updatedDetailDocumentType = await _detaildocumentTypeRepo.GetByIdForCreateAndUpdateAsync(customerCode, documentTypeID);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(updatedDetailDocumentType.ToLKACSoft_DetailDocumentTypeDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(updatedDetailDocumentType.ToLKACSoft_DetailDocumentTypeDto());
        }

        // Insert a new CustomerDocumentType by customerCode, DocumentTypeID
        [HttpPost("{customerCode}/{documentTypeID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> CreateCustomerDocumentType(
            [FromBody] CreateLKACSoft_DetailDocumentTypeDto detailDocumentTypeDto,
            [FromRoute] string customerCode, [FromRoute] string documentTypeID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createDetailDocumentType = detailDocumentTypeDto.CreateLKACSoft_DetailDocumentTypeDto(
                                                                    customerCode, documentTypeID);

            var res = await _detaildocumentTypeRepo.AddCustomerDocumentTypeAsync(createDetailDocumentType);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            var createdDetailDocumentType = await _detaildocumentTypeRepo.GetByIdForCreateAndUpdateAsync(customerCode, documentTypeID);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(createdDetailDocumentType.ToLKACSoft_DetailDocumentTypeDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            //return CreatedAtAction(nameof(GetDetailDocumentTypeById),
            //    new { customerCode = createdDetailDocumentType.Code, documentTypeID = createdDetailDocumentType.DocumentTypeID },
            //    createdDetailDocumentType.ToLKACSoft_DetailDocumentTypeDto());

            return Ok(createdDetailDocumentType.ToLKACSoft_DetailDocumentTypeDto());
        }

    }
}
