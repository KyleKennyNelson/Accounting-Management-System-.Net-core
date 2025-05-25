namespace api.Dtos.APIAuthentication
{
    public class UserApiRoleDto
    {
        public string RoleName { get; set; }
        public List<UserApiPermissionDto>? Permissions { get; set; }
    }
}
