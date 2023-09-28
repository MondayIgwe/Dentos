using System.Collections.Generic;

namespace Elite3E.RestAPI.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class Role
    {
        public string role { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public string category { get; set; }
        public List<Role> Role { get; set; }
    }

    public class Root
    {
        public List<User> Users { get; set; }
    }

}