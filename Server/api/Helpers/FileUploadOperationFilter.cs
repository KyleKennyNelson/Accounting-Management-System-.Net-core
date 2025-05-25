using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Identify parameters of type IFormFile
            var fileParams = context.MethodInfo.GetParameters()
                               .Where(p => p.ParameterType == typeof(Microsoft.AspNetCore.Http.IFormFile));

            foreach (var param in fileParams)
            {
                // Remove the automatically generated parameter
                var parameterToRemove = operation.Parameters.FirstOrDefault(p => p.Name == param.Name);
                if (parameterToRemove != null)
                {
                    operation.Parameters.Remove(parameterToRemove);
                }
            }

            // Define the file upload parameter in the request body schema
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            },
                            Required = new HashSet<string> { "file" }
                        }
                    }
                }
            };
        }
    }
}

