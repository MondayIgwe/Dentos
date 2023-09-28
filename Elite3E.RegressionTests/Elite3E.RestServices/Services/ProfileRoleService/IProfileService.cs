using Elite3E.RestServices.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Services.ProfileRoleService
{
    public interface IProfileService
    {
        Task<IRestResponse> SelectTheProfileAsync(string sessionId, string processItemId, ApiProfileEntity profile);
        Task<IRestResponse> GetBaseUserRoles(string sessionID, string processItemID, string rowKey);

    }
}
