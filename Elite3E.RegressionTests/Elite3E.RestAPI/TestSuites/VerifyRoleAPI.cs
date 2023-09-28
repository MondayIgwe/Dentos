using Newtonsoft.Json;
using NUnit.Framework;
using Elite3E.RestAPI.Services;
using Elite3E.RestAPI.General;
using Elite3E.RestAPI.Models;
using System.Collections.Generic;
using System.IO;

namespace Elite3E.RestAPI.TestSuites
{
    [TestFixture]
    public class ProfileRoleVerificationSuite
    {
        private Common common = new();
        private NxBaseUserService nxBaseUser = new();


        [SetUp]
        public void init()
        {
            common = new Common();
            nxBaseUser = new NxBaseUserService();
        }


        [Test]
        public void VerifyRole()
        {
            List<string> InvalidRoles = new List<string>();
            //Use json data temporarily
            Root baseUser = JsonConvert.DeserializeObject<Root>(File.ReadAllText("Data/Profile.json"));
            foreach (var User in baseUser.Users)
            {
                string sessionID = common.GetSession();
                string processItemID = nxBaseUser.GetNxBaseUserProcessItemID(common.BASE_API_URL, sessionID);
                string rowKey = nxBaseUser.SearchNxBaseUser(common.BASE_API_URL, sessionID, processItemID,
                    User.name);
                nxBaseUser.SelectBaseUserAdd(common.BASE_API_URL, sessionID, processItemID, rowKey);
                List<string> rolesFrom3E = nxBaseUser.GetBaseUserRoles(common.BASE_API_URL, sessionID, processItemID,
                    rowKey, User.name);

                var role3e = rolesFrom3E;
                foreach (var rolexcel in User.Role)
                {
                    if (!(role3e.Contains(rolexcel.role)))
                    {
                        InvalidRoles.Add("Role: " + rolexcel.role + " not Assigned in " + User.name);
                        //Assert.Fail("Role: " + rolexcel.role + " not Assigned in "+User.name);
                    }
                }

                //Assert.AreEqual(rolesFrom3E.Count(), rolesFromExcel.Count(), "Total number of Assigned roles differ between 3E and Excel Shet");
            }
            string i = InvalidRoles.ToString();

        }
        public List <string> GetRoles(string profile)
        {
                string sessionID = common.GetSession();
                string processItemID = nxBaseUser.GetNxBaseUserProcessItemID(common.BASE_API_URL, sessionID);
                string rowKey = nxBaseUser.SearchNxBaseUser(common.BASE_API_URL, sessionID, processItemID,
                    profile);
                nxBaseUser.SelectBaseUserAdd(common.BASE_API_URL, sessionID, processItemID, rowKey);
                List<string> rolesFrom3E = nxBaseUser.GetBaseUserRoles(common.BASE_API_URL, sessionID, processItemID,
                    rowKey, profile);

            return rolesFrom3E;
        }

        [TearDown]
        public void After()
        {
            //Cancel process, to prevent it from getting locked

        }
    }
}