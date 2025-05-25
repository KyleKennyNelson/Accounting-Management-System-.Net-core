namespace api.Models.AuthModels
{
    public class UserApiRolePermission
    {
        public string UserName { get; set; }
        public string API { get; set; }
        public List<Role>? Roles { get; set; }
    }

    public class Role
    {
        public string RoleName {  get; set; }
        public List<string>? Permissions { get; set; }
    }
}
