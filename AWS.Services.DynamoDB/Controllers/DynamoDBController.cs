using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using AWS.Services.DynamoDB.Models;
using AWS.Services.DynamoDB.Data.Services.Interfaces;

namespace AWS.Services.DynamoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamoDBController : ControllerBase
    {
        private readonly IDynamoDBService _dynamoDBService;

        public DynamoDBController(IDynamoDBService dynamoDBService)
        {
            _dynamoDBService = dynamoDBService;
        }

        [HttpGet("getallmessages")]
        public async Task<List<MessageModelDTO>> GetAllMessages()
        {
            var allDocs = await _dynamoDBService.GetAllMessages();

            return allDocs;
        }

        [HttpGet("insertmessages")]
        public void InsertMessages()
        {
            _dynamoDBService.InsertMessages();
        }

        [HttpGet("getusermessages/{user}")]
        public async Task<ActionResult> GetUserMessages(string user)
        {
            var messages = await _dynamoDBService.GetUserMessages(user);
            
            return Ok(messages);    
        }

        [HttpGet("getthreadmessages/{thread}")]
        public async Task<ActionResult> GetThreadMessages(string thread)
        {
            var messages = await _dynamoDBService.GetThreadMessages(thread);

            return Ok(messages);
        }

        [HttpGet("getuserthreadmessages")]
        public async Task<ActionResult> GetUserTHreadMessages(string user, string thread)
        {
            var userThreadMessages = await _dynamoDBService.GetUserTHreadMessages(user, thread);

            return Ok(userThreadMessages);
        }

    }
}
