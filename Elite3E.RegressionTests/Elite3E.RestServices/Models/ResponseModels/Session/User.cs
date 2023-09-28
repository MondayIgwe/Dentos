using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.Session
{
    public class User
    {
        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("mapping")] public string Mapping { get; set; }

        [JsonProperty("initials")] public string Initials { get; set; }

        [JsonProperty("helpRootUri")] public Uri HelpRootUri { get; set; }

        [JsonProperty("canProxy")] public bool CanProxy { get; set; }

        [JsonProperty("uiMessageDisplayDuration")]
        public long UiMessageDisplayDuration { get; set; }

        [JsonProperty("uiQuickFindShowDialog")]
        public bool UiQuickFindShowDialog { get; set; }

        [JsonProperty("isDeveloper")] public bool IsDeveloper { get; set; }

        [JsonProperty("isWorkflowAdmin")] public bool IsWorkflowAdmin { get; set; }

        [JsonProperty("canEditCompanyNameLogo")]
        public bool CanEditCompanyNameLogo { get; set; }

        [JsonProperty("canEditGlobalModels")] public bool CanEditGlobalModels { get; set; }

        [JsonProperty("uiReadOnlyTheme")] public string UiReadOnlyTheme { get; set; }

        [JsonProperty("uiCompactView")] public bool UiCompactView { get; set; }

        [JsonProperty("uiEnableGridPerformanceMode")]
        public bool UiEnableGridPerformanceMode { get; set; }
    }
}
