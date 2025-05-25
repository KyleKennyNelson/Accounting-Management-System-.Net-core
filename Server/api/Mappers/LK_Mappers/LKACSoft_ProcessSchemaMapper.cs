using api.Dtos.LK_Dtos.LKACSoft_ProcessSchemaDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_ProcessSchemaMapper
    {
        public static LKACSoft_ProcessSchemaStatusDto ToLKACSoft_ProcessSchemaDto(this LKACSoft_ProcessSchema LKACSoft_ProcesssSchema)
        {
            return new LKACSoft_ProcessSchemaStatusDto
            {
                ProcessSchemaID = LKACSoft_ProcesssSchema.ProcessSchemaID,
                Name = LKACSoft_ProcesssSchema.Name,
                Description = LKACSoft_ProcesssSchema.Description,
                CreatedAt = LKACSoft_ProcesssSchema.CreatedAt,
                UpdatedAt = LKACSoft_ProcesssSchema.UpdatedAt,
            };
        }
    }
}
