using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Services.SimpleEmailNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SESController : ControllerBase
    {
        private IAmazonSimpleEmailService _amazonSimpleEmailService;
        private string _fromAddress = "eegito85@gmail.com";
        private string _toAddress = "eegito85@gmail.com";
        private string _subject = "SES";
        private string _body = "<h1>It worked</h1> <p>So happy!!!</p>";

        public SESController(IAmazonSimpleEmailService amazonSimpleEmailService)
        {
            _amazonSimpleEmailService = amazonSimpleEmailService;
        }

        [HttpGet]
        public async Task<ActionResult> SendEmail()
        {
            SendEmailRequest sendEmailRequest = new SendEmailRequest()
            {
                Destination = new Destination() { ToAddresses = new List<string>() { _toAddress } },
                Message = new Message()
                {
                    Body = new Body()
                    {
                        Html = new Content()
                        {
                            Charset = "UTF-8",
                            Data = _body
                        }
                    },
                    Subject = new Content()
                    {
                        Charset = "UTF-8",
                        Data = _subject
                    }
                },
                Source = _fromAddress
            };

            var sendEmailResult = await _amazonSimpleEmailService.SendEmailAsync(sendEmailRequest);

            if(sendEmailResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return BadRequest("Something went wrong with the request");
            }

            return Ok("Email was sent succesfully");
        }

    }
}
