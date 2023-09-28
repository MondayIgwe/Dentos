using Elite3E.RestServices.Builders;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services
{
    public class ProcessService : IProcessService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();

        public async Task<IRestResponse> GetProcessItemIdAsync(string sessionId, string processName)
        {
            var urlExtension = "process/" + processName;
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();
        }

        public async Task<IRestResponse> AddNewProcessAsync(string sessionId, string processId, string processName)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/" + processName + "/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = processName
                        }
                    }
                }

            },
           JsonHelper.Settings
           );

            return await ProcessDataService.UpdateDataAsync(sessionId, processId, body);
        }

        public async Task<IRestResponse> AddNewProcessAsync(string sessionId, string processId, string process, string processName)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/" + process + "/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = processName
                        }
                    }
                }

            },
           JsonHelper.Settings
           );

            return await ProcessDataService.UpdateDataAsync(sessionId, processId, body);
        }

        public async Task<IRestResponse> AddNewSubProcessAsync(string sessionId, string processId, string instanceId, string process, string subProcessName)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/" + process + "/rows/"+instanceId+"/childObjects/" +subProcessName+"/rows/-",
                        Value = new ValueClass()
                        {
                            SubclassId = subProcessName
                        }
                    }
                }

            },
           JsonHelper.Settings
           );

            return await ProcessDataService.UpdateDataAsync(sessionId, processId, body);
        }

        public async Task<IRestResponse> PostReleaseProcessAsync(string sessionId, string processItemId, string processName)
        {
            var body = JsonConvert.SerializeObject(new SubmitModel()
            {
                PageId = processName,
                IsPrint = false
            }, JsonHelper.Settings);
            return await ProcessDataService.ReleaseFormAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> PostSubmitProcessAsync(string sessionId, string processItemId, string processName)
        {
            var body = JsonConvert.SerializeObject(new SubmitModel()
            {
                PageId = processName,
                IsPrint = false
            }, JsonHelper.Settings);
            return await ProcessDataService.SubmitFormAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> PostAllProcessAsync(string sessionId, string processItemId, string processName)
        {
            var body = JsonConvert.SerializeObject(new SubmitModel()
            {
                PageId = processName,
                IsPrint = false
            }, JsonHelper.Settings);
            return await ProcessDataService.PostAllFormAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> PostCancelProcessAsync(string sessionId, string processItemId)
        {
            var urlExtension = "Cancel";
            var body = JsonConvert.SerializeObject(new SubmitModel()
            {
                IsPrint = false
            }, JsonHelper.Settings);
            return await ProcessDataService.PostOutPutProcessAsync(sessionId, processItemId, body, urlExtension);
        }

        public async Task<IRestResponse> GetPresentationItemIdAsync(string sessionId, string processName)
        {
            var urlExtension = "presentation/" + processName;
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();
        }
        public async Task<IRestResponse> GetPageItemIdAsync(string sessionId, string processName)
        {
            var urlExtension = "metadata/page/" + processName;
            return await new RestClientBuilder()
                .Create()
                .ForResource(urlExtension, Method.GET)
                .WithHeader("X-3E-SessionId", sessionId)
                .ExecuteRequestAsync();
        }

    }
}
