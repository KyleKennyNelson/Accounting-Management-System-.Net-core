namespace api.Dtos.LK_Dtos.LKACSoft_UserDTO
{
    public class LKACSoft_DetailUserKPIDto
    {
        public string ID { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public DateTime? Dob { get; set; }
        public bool? IsQuitJob { get; set; } = false;
        public DateTime? DateCreate { get; set; }
        public string? Team { get; set; }
    }
}
