namespace GringottsBank.Application.Models.Token
{
    public class CreateTokenCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}