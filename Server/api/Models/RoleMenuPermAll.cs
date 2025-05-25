
namespace api.Models
{
    public class RoleMenuPermAll
    {
        public required string RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? UserCount { get; set; }
        public required int MenuId { get; set; }
        public string? MenuName { get; set; }
        public required int PermId { get; set; }
        public string? PermName { get; set; }
    }
}
