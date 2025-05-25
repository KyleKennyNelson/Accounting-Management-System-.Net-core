namespace api.Dtos.LK_Dtos.LKACSoft_ProcessSchemaDTO
{
    public class LKACSoft_ProcessSchemaStatusDto
    {
        public required string ProcessSchemaID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
