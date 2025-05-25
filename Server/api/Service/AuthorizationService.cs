using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using api.Dtos.APIPermission;
using api.Interfaces;
using api.Models;
using api.Models.AuthModels;
using api.Repository;
using Microsoft.AspNetCore.Authorization;


namespace api.Service
{
    public class ApiPermissionRequirement : IAuthorizationRequirement
    {
        // You can put extra info here if needed, but empty is fine for now
    }
    public class AuthorizationService : IAuthorizationHandler
    {
        private readonly IApiPermissionRole _apiPermRoleRepo;

        public AuthorizationService(IApiPermissionRole apiPermRoleRepo)
        {
            _apiPermRoleRepo = apiPermRoleRepo;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.OfType<ApiPermissionRequirement>().ToList();

            if (!pendingRequirements.Any())
                return; // No relevant requirement, exit

            var httpContext = (context.Resource as Microsoft.AspNetCore.Http.HttpContext);
            if (httpContext == null)
                return;

            var request = httpContext.Request;

            var apiPerm = new APIPermissionDto()
            {
                APIName = request.Path,
                PermissionName = request.Method,
            };

            var apiPermRolesList = await _apiPermRoleRepo.getRolesFromAPIPerm(apiPerm);

            if (apiPermRolesList == null || !apiPermRolesList.Any())
                return;

            var allowedRoles = apiPermRolesList.Select(r => r.Role).ToList();
            var userRoles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value);

            if (userRoles.Any(role => allowedRoles.Contains(role)))
            {
                foreach (var requirement in pendingRequirements)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
