using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Elite3E.RestServices.Services.OpenProcess
{
        public interface IOpenProcessService
        {
            Task<IRestResponse> GetParameterDataAsync(string sessionId, string id, string value);
            Task<IRestResponse> SearchUserDataAsync(string sessionId, string requestId, string userName, string contextId);
            Task<IRestResponse> PostReportDataAsync(string sessionId, string processValue, string userValue, string userName, string id);

            Task<IRestResponse> PostOpenProcessCancelActionAsync(string sessionId, string actionData, string processValue,
                string userValue, string userName, string id);
        }
    
}
