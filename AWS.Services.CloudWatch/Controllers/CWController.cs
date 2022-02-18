using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWS.Services.CloudWatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CWController : ControllerBase
    {
        private readonly ILogger<CWController> _logger;

        public CWController(ILogger<CWController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> LogTest()
        {
            _logger.LogInformation("This is an information logger");
            _logger.LogWarning("This is a warning logger");

            string criticalMessage = "This is a critical message";
            _logger.LogCritical("Critical Message: {0}", criticalMessage);

            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error ocurred");
            }

            return Ok("All ok!");
        }


    }
}
