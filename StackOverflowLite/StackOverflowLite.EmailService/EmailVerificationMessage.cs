namespace StackOverflowLite.EmailService
{
    public class EmailVerificationMessage
    {
        public string Email { get; set; }
        public string ConfirmationLink { get; set; }
    }
}