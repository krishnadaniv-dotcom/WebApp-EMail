using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp_EMail.Models;
using WebApp_EMail.Services;

namespace WebApp_EMail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateTokenController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public ValidateTokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("validate")]
        [Authorize]
        public async Task<IActionResult> ValidateToken([FromBody] TokenRequest tokenRequest)
        {
            if (string.IsNullOrEmpty(tokenRequest.Token))
            {
                return BadRequest("Token is required.");
            }

            try
            {
                var claimsPrincipal = await _tokenService.ValidateToken(tokenRequest.Token);

                return Ok(new { Message = "Token is valid", Claims = "Valid" }); //claimsPrincipal.Claims
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = "Token validation failed", Error = ex.Message });
            }
        }
    }
}
