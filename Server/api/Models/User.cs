namespace api.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneVerified { get; set; }
        public List<Roles> Roles { get; set; }
        public bool Locked { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class Roles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
