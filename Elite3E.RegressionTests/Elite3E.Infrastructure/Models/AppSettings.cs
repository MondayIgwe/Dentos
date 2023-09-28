using System;
using System.Collections.Generic;
using Elite3E.Infrastructure.Enums;

namespace Elite3E.Infrastructure.Models
{
    public class AppSettings
    {
        /// <summary>
        /// The BaseUrl for calling to Elite 3E e.g. https://dfin91tewa01.dentons.global/TE_3E_GD_FT/web/ui
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// A username to insert into the BaseUrl to allow logging on with a specfic user where the URL format is: 
        ///   https://<username>:<password>@dfin91tewa01.dentons.global/TE_3E_GD_FT/web/ui
        /// </summary>
        public string BaseUrlUsername { get; set; }

        /// <summary>
        /// A password to insert into the BaseUrl to allow logging on with a specfic user where the URL format is: 
        ///   https://<username>:<password>@dfin91tewa01.dentons.global/TE_3E_GD_FT/web/ui
        /// </summary>
        public string BaseUrlPassword { get; set; }

        public string ConnectionString { get; set; }

        public string ScreenShotFilePath { get; set; }

        public string EntityUrl { get; set; }

        public List<UserDetail> UserDetails { get; set; }

        public string RemoteUrl { get; set; }

        public string AdminUser1 { get; set; }

        public string FiscalInvoicePrefix { get; set; }

        public string SuspenseGlType { get; set; }

        public string ApiUserName { get; set; }

        public string ApiUserPassword { get; set; }

        public string BaseApiUrl { get; set; }

        public Uri ApiBaseUrl { get; set; }

        public Regions Region { get; set; }

        public int? DefaultTimeout { get; set; }
    }
}

