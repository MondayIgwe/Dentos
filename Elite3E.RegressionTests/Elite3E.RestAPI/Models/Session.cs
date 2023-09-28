using System;

namespace Elite3E.RestAPI.Models
{

    /// <summary>
    /// 
    /// </summary>
    public partial class EliteSession
    {
        public Guid Id { get; set; }
        public string Language { get; set; }
        public bool ProcessAllowGlobalReplace { get; set; }
    }

}