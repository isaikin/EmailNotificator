
namespace Epam.EmailNotificator
{
    public class Sender
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public Sender(string email, string password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }
    }
}
