namespace Elite3E.RestServices.Models
{
    public class Credentials
    {
        public string Key { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials(string key, string username, string password)
        {
            this.Key = key;
            this.Username = username;
            this.Password = password;
        }
    }
}
