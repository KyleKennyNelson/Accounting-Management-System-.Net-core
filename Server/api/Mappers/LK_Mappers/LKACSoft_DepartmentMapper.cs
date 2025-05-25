using api.Dtos.LK_Dtos.LKACSoft_DepartmentDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DepartmentMapper
    {
        public static LKACSoft_DepartmentDto ToLKACSoft_DepartmentDto(this LKACSoft_Department LKACSoft_Department)
        {
            return new LKACSoft_DepartmentDto
            {
                Code = LKACSoft_Department.Code,
                Name = LKACSoft_Department.Name,
                DisplayOrder = LKACSoft_Department.DisplayOrder,
                Closed = LKACSoft_Department.Closed,
            };
        }
    }
}
