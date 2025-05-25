namespace api.Dtos.LK_Dtos.LKACSoft_DepartmentDTO
{
    public class LKACSoft_DepartmentDto
    {
        public required string Code { get; set; }
        public string? Name { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? Closed { get; set; }
    }
}
