using Elite3E.Infrastructure.Configuration;

namespace Elite3E.Infrastructure.Helper
{
    /// <summary>
    /// Helper methods for constructing URLs for the Elite 3E application.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Injects the username/password into the Elite 3E URL so that a different user can be specified.
        /// Generates a URL in the format: https://<username>:<password>@dfin91tewa01.dentons.global/TE_3E_GD_FT/web/ui
        /// </summary>
        /// <returns>The base URL from appsettings with the username/password injected if they have been specified.</returns>
        public static string GetBaseUrl()
        {
            var applicationConfigurationBuilder = ApplicationConfigurationBuilder.Instance;

            var url = applicationConfigurationBuilder.BaseUrl;
            var username = applicationConfigurationBuilder.BaseUrlUsername;
            var password = applicationConfigurationBuilder.BaseUrlPassword;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                // Inject the username and password into the URL i.e. in the format https://<username>:<password>@dfin91tewa01.dentons.global/TE_3E_GD_FT/web/ui.
                var urlPrefix = "https://";
                var urlPrefixWithUsernamePassword = $"{urlPrefix}{username}:{password}@";
                url = url.Replace(urlPrefix, urlPrefixWithUsernamePassword);
            }

            return url;
        }
    }
}
