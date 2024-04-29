using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using StackOverflowLite.Application.Utilities;
using StackOverflowLite.Infrastructure.Email;
using StackOverflowLite.Infrastructure.Membership;

namespace StackOverflowLite.EmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;
        private IEmailService _emailService;

        public Worker(ILogger<Worker> logger, IAmazonSQS sqsClient, IEmailService emailService)
        {
            _logger = logger;
            _sqsClient = new AmazonSQSClient();
            _queueUrl = "https://sqs.us-east-1.amazonaws.com/590184136362/StackOverflowLiteQueue"; // SQS queue URL
            _emailService = emailService;
        }

        // Method to read a message from the given queue. It gets one message at a time
        private async Task<ReceiveMessageResponse> GetMessageFromSQS(int waitTime = 0)
        {
            
            var qUrl = "https://sqs.us-east-1.amazonaws.com/590184136362/StackOverflowLiteQueue";
            return await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = qUrl,
                MaxNumberOfMessages = 1,
                WaitTimeSeconds = waitTime
            });
        }

        // Method to delete a message from a queue
        private async Task DeleteMessage(Message message, string qUrl)
        {
            // Console.WriteLine($"\nDeleting message {message.MessageId} from queue...");
            await _sqsClient.DeleteMessageAsync(qUrl, message.ReceiptHandle);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var qUrl = "https://sqs.us-east-1.amazonaws.com/590184136362/StackOverflowLiteQueue";
                try
                {
                    var response = await GetMessageFromSQS(1);
                    if (response.Messages.Count > 0)
                    {
                        var message = response.Messages[0];
                        var body = JsonConvert.DeserializeObject<ApplicationUser>(message.Body);

                        // Send email
                        _emailService.SendSingleEmail(body.FirstName + " " + body.LastName, body.Email, "Confirm your email", message.Body);

                        // Delete message from queue
                        await DeleteMessage(message, qUrl);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message from queue");
                }
               
                await Task.Delay(5000, stoppingToken); // Delay before polling again
            }
        }
    }
}