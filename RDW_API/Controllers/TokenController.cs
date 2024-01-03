using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RDW_API.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RDW_API.Controllers
{
    /// <summary>
    /// Controller to Generate JWT Bearer Token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(IOptions<JwtOptions> configuration) : ControllerBase
    {
        private readonly JwtOptions _configuration = configuration.Value;

        /// <summary>
        /// Generate an Authentication JWT Bearer Token.
        /// </summary>
        /// <param name="secretKey">The secret key used to sign the token</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request - Secret key is required</response>
        /// <response code="403">Forbidden - Invalid secret key</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public IActionResult GenerateToken([FromQuery] string? secretKey)
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                return new ObjectResult(new { message = "Secret key is required." }) // Can replace this with actual error model if needed for better logging on requests
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (!IsSecretKeyValid(secretKey))
            {
                return new ObjectResult(new { message = "Secret key invalid." }) // Can replace this with actual error model if needed for better logging on requests
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            var issuer = _configuration.Issuer;
            var audience = _configuration.Audience;
            var expirationInMinutes = Convert.ToInt32(_configuration.ExpirationInMinutes);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user-id"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationInMinutes),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        private bool IsSecretKeyValid(string providedKey)
        {
            // Replace this with your logic to check if the provided key matches the expected key
            var expectedKey = _configuration.Key; // or load from configuration
            return string.Equals(providedKey, expectedKey, StringComparison.Ordinal);
        }
    }
}
