using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace AWS.Services.SNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNSController : ControllerBase
    {
        private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;

        public SNSController(IAmazonSimpleNotificationService amazonSimpleNotificationService)
        {
            _amazonSimpleNotificationService = amazonSimpleNotificationService;
        }

        [HttpGet]
        public async Task<ActionResult> SendMessage()
        {
            var request = new PublishRequest
            {
                Message = $"Test at {DateTime.Now.ToLongTimeString()}",
                TopicArn = "arn:aws:sns:sa-east-1:787152564072:UdemyProjectTopic"
            };

            var response = _amazonSimpleNotificationService.PublishAsync(request);

            return Ok("Message");
        }

    }
}
