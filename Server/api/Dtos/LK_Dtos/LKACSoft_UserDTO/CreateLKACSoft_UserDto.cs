namespace api.Dtos.LK_Dtos.LKACSoft_UserDTO
{
    public class CreateLKACSoft_UserDto
    {
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public required string District { get; set; }
        public required DateTime Dob { get; set; }
        public bool? IsQuitJob { get; set; } = false;
        public DateTime? DateCreate { get; set; } = DateTime.Now;
        public string? Team { get; set; }
    }
}
