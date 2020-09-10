namespace battle_ship.dependencies.model
{
    public class Credentials
    {
        public Credentials()
        {
        }

        public Credentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public bool Equals(Credentials credentials)
        {
            return Email.Equals(credentials.Email) && Password.Equals(credentials.Password);
        }
    }
}