using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using api.Interfaces;
using api.Models;
using api.Models.AuthModels;
using api.Repository;
using Microsoft.IdentityModel.Tokens;


namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        //public TokenService(IConfiguration config, RequestDelegate next) 
        //{
        //    this._config = config;
        //    this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        //    this._next = next;
        //}

        public TokenService(IConfiguration config)
        {
            this._config = config;
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public async Task<string> CreateToken(string UserID, IList<string> roles)
        {
            // Create claims list
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, UserID) // Standard claim type for username
            };

            // Add each role as a separate claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString())); // ClaimTypes.Role is the standard type for roles
            }


            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        //    if (authHeader != null && authHeader.StartsWith("Bearer "))
        //    {
        //        var token = authHeader.Substring("Bearer ".Length).Trim();

        //        try
        //        {
        //            // Parse the token without relying on HttpContext.User
        //            var handler = new JwtSecurityTokenHandler();
        //            var jwtToken = handler.ReadJwtToken(token);

        //            // Validate the token manually
        //            var key = Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]);
        //            var validationParams = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidIssuer = _config["JWT:Issuer"],
        //                ValidateAudience = true,
        //                ValidAudience = _config["JWT:Audience"],
        //                IssuerSigningKey = new SymmetricSecurityKey(key)
        //            };
        //            handler.ValidateToken(token, validationParams, out _);

        //            // Extract UserName claim
        //            var userNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        //            if (!string.IsNullOrEmpty(userNameClaim))
        //            {
        //                // Deserialize UserName from JSON if needed
        //                var userName = JsonSerializer.Deserialize<string>(userNameClaim);

        //                // Store UserName in HttpContext.Items for downstream usage
        //                context.Items["UserName"] = userName;
        //            }
        //        }
        //        catch
        //        {
        //            // Log or handle token parsing errors
        //        }
        //    }

        //    await _next(context);
        //}
    }
}
