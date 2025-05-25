using api.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace api.MiddleWare
{
    public class ParseTokenMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;

        public ParseTokenMiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _secretKey = configuration["Jwt:SecretKey"];
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring(7);

                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_secretKey);

                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = context.RequestServices
                            .GetRequiredService<IConfiguration>()["Jwt:Issuer"],
                        ValidAudience = context.RequestServices
                            .GetRequiredService<IConfiguration>()["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                    // Attach the user principal to the HttpContext
                    context.User = principal;
                }
                catch (Exception ex)
                {
                    // Token validation failed, log or handle the exception as needed
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid Token");
                    return;
                }
            }

            // Proceed to the next middleware
            await _next(context);
        }
    }
}
