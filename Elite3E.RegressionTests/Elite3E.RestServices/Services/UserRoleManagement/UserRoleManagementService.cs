using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.ModelHelper;

namespace Elite3E.RestServices.Services.UserRoleManagement
{
    public class UserRoleManagementService : IUserRoleManagementService
    {
        public ILookUpService LookUpService = new LookUpService();
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddUserAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/attributes/BaseUserName/value",
                        Value = userRoleManagementEntity.UserName
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/attributes/DataRole/value",
                        Value = userRoleManagementEntity.DataRoleValue
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/DefaultUnit/value",
                        Value = userRoleManagementEntity.DefaultOperatingUnitValue
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/NetworkAlias/value",
                        Value = userRoleManagementEntity.NetworkAlias
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/EmailAddr/value",
                        Value = userRoleManagementEntity.EmailAddress
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/CanProxy/value",
                        Value = userRoleManagementEntity.CanProxy
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/CanEditDashboard/value",
                        Value = userRoleManagementEntity.CanEditDashboard
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/IsAllowTimeEntry/value",
                        Value = userRoleManagementEntity.IsAllowTimeEntry
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/CanEditGlobalModel/value",
                        Value = userRoleManagementEntity.CanEditGlobalModel
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/Language/value",
                        Value = userRoleManagementEntity.LanguageValue
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxBaseUser/rows/" +  userRoleManagementEntity.UserRoleManagementId + "/attributes/Dashboard/value",
                        Value = userRoleManagementEntity.DashboardValue,
                        Alias = userRoleManagementEntity.DashboardAlias,
                        Id = "Dashboard"
                    }
                }
              
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddUserRolesAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity)
        {
            List<Changes> changeList = new List<Changes>();
            foreach(var userRole in userRoleManagementEntity.UserRole)
            {
                Changes change = new Changes()
                {
                    Op = "replace",
                    Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/childObjects/NxUser_RoleChild/rows/" + userRole.UserRoleManagementChildId + "/attributes/RoleID/value",
                    Value = userRole.UserRoleValue
                };
                changeList.Add(change);
            }

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = changeList

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetDataRoleAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxSecurityDataRoleMap",
                Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/attributes/DataRole/aliasValue",
                Text = userRoleManagementEntity.DataRoleAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetDashboardAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxFWKAppObject",
                Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/attributes/Dashboard/aliasValue",
                Text = userRoleManagementEntity.DashboardAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetUserRoleValueAsync(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity,string userRoleID,string userRole)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxRole",
                Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/childObjects/NxUser_RoleChild/rows/" + userRoleID + "/attributes/RoleID/aliasValue",
                Text = userRole,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetUserRoleAdvancedSearchList(string sessionId, string processItemId, UserRoleManagementEntity userRoleManagementEntity,string userRoleID, string userRole)
        { 
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "NxRole",
                Path = "/objects/NxBaseUser/rows/" + userRoleManagementEntity.UserRoleManagementId + "/childObjects/NxUser_RoleChild/rows/" + userRoleID+ "/attributes/RoleID/aliasValue",
                Select = new Select()
                {
                    Where = new Where()
                    {
                        Operator = "And",
                        Predicates = new List<Predicate>
                        {
                            new Predicate()
                            {
                                Attribute = "BaseUserName",
                                Operator = "IsEqualTo",
                                Value = userRole
                            }
                        }
                    },
                    Archetype = "NxRole",
                    ArchetypeType = 1
                },
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetAdvancedLookUpAsync(sessionId, processItemId, body);
        }
    }
}
