using Microsoft.AspNetCore.Mvc;
using WebApp_EMail.Models;
using WebApp_EMail.Services;

namespace WebApp_EMail.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class SendEmailController : ControllerBase
        {
            private readonly TokenService _tokenService;
            private readonly IEmailService _emailService;

            public SendEmailController(TokenService tokenService, IEmailService emailService)
            {
                _tokenService = tokenService;
                _emailService = emailService;
            }

            [HttpPost("sendemail")]
            public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
            {
                if (string.IsNullOrEmpty(request.Token))
                {
                    return BadRequest("Token is required.");
                }

                try
                {
                    var claimsPrincipal = await _tokenService.ValidateToken(request.Token);

                    if (claimsPrincipal == null)
                    {
                        return Unauthorized(new { Message = "Invalid token." });
                    }
                    var emailSent = await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);

                    if (emailSent)
                    {
                        return Ok(new { Message = "Email sent successfully." });
                    }

                    return StatusCode(500, new { Message = "Failed to send email." });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { Message = "Token validation failed", Error = ex.Message });
                }
            }
        }
}
