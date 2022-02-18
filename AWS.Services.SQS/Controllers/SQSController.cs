using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Services.SQS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SQSController : ControllerBase
    {
        private readonly IAmazonSQS _amazonSQS;

        public SQSController(IAmazonSQS amazonSQS)
        {
            _amazonSQS = amazonSQS;
        }

        [HttpGet]
        public async Task<ActionResult> AddToQueue()
        {
            SendMessageRequest request = new SendMessageRequest
            {
                QueueUrl = "https://sqs.sa-east-1.amazonaws.com/787152564072/UdemyProjectQueue",
                MessageBody = $"Test at {DateTime.Now.ToLongTimeString()}"
            };

            SendMessageResponse result = await _amazonSQS.SendMessageAsync(request);

            return Ok("Hi!");
        }
    }
}
