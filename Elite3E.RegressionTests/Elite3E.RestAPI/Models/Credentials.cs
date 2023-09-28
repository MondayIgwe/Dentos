namespace Elite3E.RestAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    class Credentials
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public Credentials(string name, string username, string password)
        {
            this.name = name;
            this.username = username;
            this.password = password;
        }


    }
}
