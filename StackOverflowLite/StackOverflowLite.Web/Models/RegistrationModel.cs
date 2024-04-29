using Amazon.SQS.Model;
using Amazon.SQS;
using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using StackOverflowLite.Application.Utilities;
using StackOverflowLite.Infrastructure.Membership;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace StackOverflowLite.Web.Models
{
    public class RegistrationModel
    {
        private ILifetimeScope _scope;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;
        private IEmailService _emailService;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        public string? ReturnUrl { get; set; }

        public RegistrationModel() { }
        public RegistrationModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAmazonSQS sqsClient, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sqsClient = sqsClient;
            _queueUrl = "https://sqs.us-east-1.amazonaws.com/590184136362/StackOverflowLiteQueue"; // SQS queue URL
            _emailService = emailService;
        }
        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _userManager = _scope.Resolve<UserManager<ApplicationUser>>();
            _signInManager = _scope.Resolve<SignInManager<ApplicationUser>>();
            _emailService = _scope.Resolve<IEmailService>();
        }
        private async Task SendMessageToSQS(IAmazonSQS sqsClient, string qUrl, string messageBody)
        {
            sqsClient = new AmazonSQSClient();
            qUrl = "https://sqs.us-east-1.amazonaws.com/590184136362/StackOverflowLiteQueue";
            SendMessageResponse responseSendMsg = await sqsClient.SendMessageAsync(qUrl, messageBody);
        }

        internal async Task<(IEnumerable<IdentityError>? errors, string? redirectLocation)> RegisterAsync(string urlPrefix)
        {
            ReturnUrl ??= urlPrefix;

            var user = new ApplicationUser { UserName = Email, Email = Email, FirstName = "", LastName = "" };
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = $"{urlPrefix}/Account/ConfirmEmail?userId={user.Id}&code={code}&returnUrl={ReturnUrl}";
                var mesageBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                await SendMessageToSQS(_sqsClient, _queueUrl, mesageBody);

                _emailService.SendSingleEmail(FirstName + " " + LastName, Email, "Confirm your email", mesageBody);

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    var confirmationPageLink = $"RegisterConfirmation?email={Email}&returnUrl={ReturnUrl}";
                    return (null, confirmationPageLink);
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return (null, ReturnUrl);
                }
            }
            else
            {
                return (result.Errors, null);
            }
        }
    }
}
