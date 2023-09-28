using NUnit.Framework;
using RestSharp;
using Elite3E.RestAPI.General;
using Newtonsoft.Json;
using System.Collections.Generic;
using Elite3E.RestAPI.Models.Response;

namespace Elite3E.RestAPI.Services
{
    
    public class NxBaseUserService
    {


        public NxBaseUserService()
        {

        }

        /// <summary>
        /// Gets the item ID of the specified Process
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public string GetNxBaseUserProcessItemID(string endpoint, string sessionID)
        {
            var client = new RestClient(endpoint + "process/NxBaseUser");
            IRestResponse response = new RestResponse();
            client.Timeout = -1;
            client.Authenticator = Common.GetAuthenticator();
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-3E-SessionId", sessionID);
            request.AddHeader("Accept", "application/json, text/plain, */*");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Content-Type", "application/json");
            response = client.Execute(request);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK, "GetNxBaseUserProcessItemID did not respond with OK, returned status - " + response.StatusCode);

            string processid = Common.GetTagValue2(response.Content, "processItemId");
            Assert.NotNull(processid, "No process Id Found");
            return processid;
        }

        /// <summary>
        /// Returns the rowKey of the provided BaseUser
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="sessionID"></param>
        /// <param name="processItemID"></param>
        /// <param name="baseUser"></param>
        /// <returns></returns>
        public string SearchNxBaseUser(string endpoint, string sessionID, string processItemID, string baseUser)
        {
            string body = "{\"toprows\":100,\"text\":\"" + baseUser + "\",\"processItemId\":\"" + processItemID + "\",\"queryResultAttributes\":[{\"id\":\"BaseUserName\",\"captionId\":\"45ed408a-035f-4670-8c97-bb52836ef9ad\",\"caption\":\"User / Group Name\",\"dataType\":15},{\"id\":\"IsActive\",\"captionId\":\"0da9c70f-e4ec-446b-90e7-a649ba5c9d6e\",\"caption\":\"Active\",\"dataType\":1},{\"id\":\"ArchetypeCode\",\"captionId\":\"762a75ae-1599-4b19-96ef-5d5e4fd5ef88\",\"caption\":\"User / Role\",\"dataType\":15}],\"sortAttributes\":[{\"captionId\":\"90f699d3-a20c-4e03-829a-d2a7e589dfd8\",\"caption\":\"Name\",\"id\":\"BaseUserName\",\"sortDirection\":1}]}";
            var client = new RestClient(endpoint + "find/worklist/quick");
            IRestResponse response = new RestResponse();
            client.Timeout = -1;
            client.Authenticator = Common.GetAuthenticator();
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-3E-SessionId", sessionID);
            request.AddHeader("X-3E-ProcessItemId", processItemID);
            request.AddHeader("Accept", "application/json, text/plain, */*");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            response = client.Execute(request);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK, "SearchNxBaseUser did not respond with OK, returned status - " + response.StatusCode);
            //Assert.That(response.Content.Contains("\"rowCount\":1"));
            string rowKey = Common.GetTagValue2(response.Content, "rowKey");
            Assert.NotNull(rowKey, "No Base User found for: " + baseUser);
            return rowKey;
        }

        /// <summary>
        /// Selects the Base User Provided on the previous method
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="sessionID"></param>
        /// <param name="processItemID"></param>
        /// <param name="key"></param>
        public void SelectBaseUserAdd(string endpoint, string sessionID, string processItemID, string key)
        {
            string body = "{\"path\":\"/objects/NxBaseUser/rows/-\",\"itemIDs\":[\"" + key + "\"]}";
            var client = new RestClient(endpoint + "data/batchadd");
            IRestResponse response = new RestResponse();
            client.Timeout = -1;
            client.Authenticator = Common.GetAuthenticator();
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-3E-SessionId", sessionID);
            request.AddHeader("X-3E-ProcessItemId", processItemID);
            request.AddHeader("Accept", "application/json, text/plain, */*");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            response = client.Execute(request);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK, "SelectBaseUserAdd did not respond with OK, returned status - " + response.StatusCode);
        }


        /// <summary>
        /// Returns a list of Base User assigned Roles
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="sessionID"></param>
        /// <param name="processItemID"></param>
        /// <param name="key"></param>
        public List<string> GetBaseUserRoles(string endpoint, string sessionID, string processItemID, string key, string baseUser)
        {
            string body = "{\"changes\":[{\"op\":\"replace\",\"path\":\"/objectStates/NxBaseUser/childStates/NxUser_RoleChild/sortAttributes\",\"value\":{\"Role.BaseUserName\":{\"id\":\"Role.BaseUserName\",\"direction\":1,\"order\":0},\"User.BaseUserName\":{\"id\":\"User.BaseUserName\",\"direction\":1,\"order\":1}}},{\"op\":\"replace\",\"path\":\"/objectStates/NxBaseUser/childStates/NxUser_RoleChild/clearUISort\",\"value\":true}],\"endRow\":26,\"path\":\"/objects/NxBaseUser/rows/" + key + "/childObjects/NxUser_RoleChild\",\"startRow\":0,\"cacheBlockSize\":50}";
            var client = new RestClient(endpoint + "data");
            IRestResponse response = new RestResponse();
            client.Timeout = -1;
            client.Authenticator = Common.GetAuthenticator();
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-3E-SessionId", sessionID);
            request.AddHeader("X-3E-ProcessItemId", processItemID);
            request.AddHeader("Accept", "application/json, text/plain, */*");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            response = client.Execute(request);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK, "GetBaseUserRoles did not respond with OK, returned status - " + response.StatusCode);
            Root templObj = JsonConvert.DeserializeObject<Root>(response.Content);
            int length = templObj.data.objects.NxUser_RoleChild.Rows.Count;
            List<string> roles = new List<string>();

            foreach (var item in templObj.data.objects.NxUser_RoleChild.Rows)
            {
                roles.Add(item.Value.Attributes.RoleID.aliasValue);
            }

            Assert.NotNull(roles, "No Roles found for this base User: " + baseUser);
            return roles;
        }

    
    }
}
