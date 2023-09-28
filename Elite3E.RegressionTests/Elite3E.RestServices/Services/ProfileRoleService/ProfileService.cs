using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;


namespace Elite3E.RestServices.Services.ProfileRoleService
{
    public class ProfileService : IProfileService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> GetBaseUserRoles(string sessionID, string processItemID, string rowKey)
        {
            var body = new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/NxBaseUser/childStates/NxUser_RoleChild/sortAttributes",
                        Value = new ValueClass
                        {
                            RoleBaseUserName = new Role()
                            {
                                Id = "Role.BaseUserName",
                                Direction = 1,
                                Order=0
                            },
                            UserBaseName = new BaseUser()
                            {
                                Id = "User.BaseUserName",
                                Direction = 1,
                                Order = 1
                            }
                        }
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objectStates/NxBaseUser/childStates/NxUser_RoleChild/clearUISort",
                        Value = true
                    },
                        new()
                    {
                        Op = "replace",
                        Path = "/objectStates/NxBaseUser/childStates/NxUser_RoleChild/clearUIGroup",
                        Value = false
                    }
                },
                Path = "/objects/NxBaseUser/rows/" + rowKey + "/childObjects/NxUser_RoleChild",
                Resort = true,
                StartRow = 0,
                FilterText = "",
                CacheBlockSize=100
            };
            var requestBody = JsonConvert.SerializeObject(body, JsonHelper.Settings);
            return await ProcessDataService.AddDataAsync(sessionID, processItemID, requestBody);
        }

        public async Task<IRestResponse> SelectTheProfileAsync(string sessionId, string processItemId, ApiProfileEntity profile)
        {
            var body = JsonConvert.SerializeObject(new AddBatchModel()
            {
                Path = "/objects/NxBaseUser/rows/-",
                ItemIDs = new List<System.Guid>(){
                    {new(profile.ProfileRowKey) }
                }
            }, JsonHelper.Settings);
            var urlExtension = "data/batchadd";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }
    }
}
