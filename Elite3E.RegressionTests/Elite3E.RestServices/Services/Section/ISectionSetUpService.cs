using Elite3E.RestServices.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Services.Section
{
    public interface ISectionSetUpService
    {
        Task<IRestResponse> AddSectionAsync(string sessionId, string processItemId, ApiSectionEntity ApiSectionEntity);
        Task<IRestResponse> GetGLSectionSearchGLTypeValueAsync(string sessionId, string processItemId, ApiSectionEntity ApiSectionEntity);

    }
}
