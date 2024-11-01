using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;


namespace WonderPlane.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly WonderPlane.Server.Services.IEmailSender _emailSender;

        public EmailController(WonderPlane.Server.Services.IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("sendemail")]
        public async Task<ActionResult> SendEmailAsync(EmailDto emailDto)
        {
            try
            {
                await _emailSender.SendEmailAsync(emailDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}