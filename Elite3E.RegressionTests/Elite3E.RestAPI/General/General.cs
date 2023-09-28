using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework;
using Elite3E.RestAPI.Models;
using Elite3E.Infrastructure.Configuration;

namespace Elite3E.RestAPI.General
{
    /// <summary>
    /// Contains Get Session Methods for Authenticating web services
    /// Includes most shared methods across services
    /// </summary>
    public class Common
    {
        public string BASE_API_URL = ApplicationConfigurationBuilder.Instance.ApiBaseUrl.ToString();
        //https://dfin91tewa01.dentons.global/TE_3E_GD_FT/web/api/v1/

        public static NtlmAuthenticator GetAuthenticator()
        {
            string username = ApplicationConfigurationBuilder.Instance.ApiUserName;
            string password = ApplicationConfigurationBuilder.Instance.ApiUserPassword;
            Credentials credentials = new Credentials("dentons/"+ username, username, password);
            return new NtlmAuthenticator(credentials.username, credentials.password);
        }

        public string GetSession()
        {
            var client = new RestClient(BASE_API_URL + "session");
            client.Timeout = -1;
            client.Authenticator = GetAuthenticator();
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("Authorization", AuthenticateHeader());
            IRestResponse response = client.Execute(request);
            Assert.IsNotNull(response.StatusDescription, "3E - SessionID - Service Unavailable");
            Assert.That(!response.StatusDescription.ToLower().Contains("service unavailable"), "3E - SessionID - Service Unavailable");
            Assert.That(!response.StatusDescription.ToLower().Contains("proxy authentication required"), "3E - SessionID - Check Proxy.");
            Assert.That(!response.StatusDescription.ToLower().Contains("unauthorized"), "3E - SessionID - Unauthorized Status returned from API.");
            Assert.That(response.StatusDescription.Equals("Created"), "3E - SessionID - Error Creating API Session.");

            EliteSession session = null;
            try
            {
                session = JsonConvert.DeserializeObject<EliteSession>(response.Content);
            }
            catch (Exception)
            {
                Assert.That(false, "3E - SessionID - Error Creating API Session");
            }

            return session.Id.ToString();
        }

        private static string AuthenticateHeader()
        {
            return Constants.ProfileConstants.AuthenticationHeader;
        }

        /// <summary>
        /// Takes in a response from a service
        /// then finds the specified tag/param value 
        /// </summary>
        /// <param name="soapResponse"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetTagValue2(String soapResponse, String tag)
        {
            try
            {
                soapResponse = soapResponse.ToLower();
                tag = tag.ToLower();
                String s1 = soapResponse.Substring(soapResponse.IndexOf("\"" + tag + "\""));
                String s2 = s1.Replace("\"" + tag + "\"", "");
                String s4 = s2.Replace(":\"", "");
                String s3 = s4.Substring(0, s4.IndexOf("\""));
                return s3;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }

        }



    }



}