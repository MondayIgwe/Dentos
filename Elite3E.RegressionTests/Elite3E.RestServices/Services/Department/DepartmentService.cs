using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public ILookUpService LookUpService = new LookUpService();

        public async Task<IRestResponse> AddDepartmentCodeAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path =  "/objects/Department/rows/" + departmentEntity.Id + "/attributes/Code/value",
                        Value = departmentEntity.DepartmentCode
                    }
                }
            }, JsonHelper.Settings); 

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddDescriptionAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/Description/value",
                        Value = departmentEntity.Description
                    }
                }
            }, JsonHelper.Settings); 

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetGLDepartmentAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "GLDepartment",
                Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/GLDepartment/aliasValue",
                Text = departmentEntity.GLDepartmentAlias,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddGLDepartmentAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/GLDepartment/value",
                        Value = departmentEntity.GLDepartmentValue,
                        Alias = departmentEntity.GLDepartmentAlias,
                        Id = "GLDepartment"
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddDepartmentGroupAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/DepartmentGroup/value",
                        Value = departmentEntity.DepartmentGroupValue
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddIsDefaultCheckboxAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/Is_Default/value",
                        Value = departmentEntity.IsDefaultCheckBoxValue
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddIsActiveCheckboxAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/IsActive/value",
                        Value = departmentEntity.IsActiveCheckBoxValue
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddStartDateAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/StartDate/value",
                        Value = departmentEntity.StartDate //2022-02-17
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEndDateAsync(string sessionId, string processItemId, ApiDepartmentEntity departmentEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Department/rows/" + departmentEntity.Id + "/attributes/EndDate/value",
                        Value = departmentEntity.EndDate //2022-02-17
                    }
                }
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
    }
}
