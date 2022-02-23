using Amazon.SQS;
using Amazon.SQS.Model;

namespace AWS.Services.ElasticContainerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAmazonSQS _amazonSQS;
        private string _queueUrl = "https://sqs.sa-east-1.amazonaws.com/787152564072/ECSQueue";

        public Worker(ILogger<Worker> logger, IAmazonSQS amazonSQS)
        {
            _logger = logger;
            _amazonSQS = amazonSQS;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _queueUrl,
                    WaitTimeSeconds = 10,
                    MaxNumberOfMessages = 10
                };

                var messages = await _amazonSQS.ReceiveMessageAsync(request);

                if(messages.Messages.Any())
                {
                    foreach (var message in messages.Messages)
                    {
                        _logger.LogInformation(message.Body);
                        await _amazonSQS.DeleteMessageAsync(_queueUrl, message.ReceiptHandle);
                    }
                }

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}