using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace StackOverflowLite.EmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public Worker(ILogger<Worker> logger, IAmazonSQS sqsClient)
        {
            _logger = logger;
            _sqsClient = sqsClient;
            _queueUrl = "https://sqs.us-east-1.amazonaws.com/590184136362/StackOverflowLiteQueue"; // SQS queue URL

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Poll messages from SQS queue
                var receiveMessageRequest = new ReceiveMessageRequest
                {
                    QueueUrl = _queueUrl,
                    MaxNumberOfMessages = 10 // Adjust as needed
                };
                var response = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

                if (response.Messages.Any())
                {
                    foreach (var message in response.Messages)
                    {
                        // Process email verification message
                        var messageBody = JsonConvert.DeserializeObject<EmailVerificationMessage>(message.Body);
                        // Send email confirmation using the email service
                        // Example: await _emailService.SendConfirmationEmailAsync(messageBody.Email, messageBody.ConfirmationLink);

                        // Delete processed message from SQS queue
                        var deleteMessageRequest = new DeleteMessageRequest
                        {
                            QueueUrl = _queueUrl,
                            ReceiptHandle = message.ReceiptHandle
                        };
                        await _sqsClient.DeleteMessageAsync(deleteMessageRequest, stoppingToken);
                    }
                }

                await Task.Delay(5000, stoppingToken); // Delay before polling again
            }
        }
    }
}