
using Amazon.Runtime.Documents;
using api.Dtos.LK_Dtos.LKACSoft_DocumentTypeDTO;
using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/documentTypes")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DocumentTypeController : ControllerBase
    {
        private readonly ILKACSoft_DocumentTypeRepository _documentTypeRepo;

        public LKACSoft_DocumentTypeController(ILKACSoft_DocumentTypeRepository documentTypeRepo)
        {
            _documentTypeRepo = documentTypeRepo;
        }

        // Get all documenttypes
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDocumentTypes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var documentTypeModels = await _documentTypeRepo.GetAllAsync();
            var documentTypeDtos = documentTypeModels.Select(dt => dt.ToLKACSoft_DocumentTypeDto());


            return Ok(documentTypeDtos);
        }

        // Get DocumentType by ID
        [HttpGet("{documentTypeID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDocumentTypeById([FromRoute]string documentTypeID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var documentType = await _documentTypeRepo.GetByIdAsync(documentTypeID);
            if (documentType == null)
                return NotFound(new { message = "DocumentType not found" });


            return Ok(documentType.ToLKACSoft_DocumentTypeDto());
        }
    }
}
