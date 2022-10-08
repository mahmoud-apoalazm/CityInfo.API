namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "admin@mycompany.com";
        private string _mailfrom = "noreply@mycompany.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailfrom} to {_mailTo} , " +
                $"with {nameof(LocalMailService)}."
                );
            Console.WriteLine($"subject: {subject}");
            Console.WriteLine($"message: {message}");

        }
    }
}
