using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Services.Section
{
    public class SectionSetUpService : ISectionSetUpService
    {
        public ILookUpService LookUpService = new LookUpService();
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> AddSectionAsync(string sessionId, string processItemId, ApiSectionEntity sectionEntity)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Section/rows/" + sectionEntity.SectionId + "/attributes/Code/value",
                        Value = sectionEntity.Code
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/Section/rows/" + sectionEntity.SectionId + "/attributes/Description/value",
                        Value = sectionEntity.Description
                    },
                    new()
                    {                      
                        Op = "replace",
                        Path = "/objects/Section/rows/" + sectionEntity.SectionId + "/attributes/GLSection/aliasValue",
                        Value = sectionEntity.GLSection
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetGLSectionSearchGLTypeValueAsync(string sessionId, string processItemId, ApiSectionEntity sectionEntity)
        {
            var body = JsonConvert.SerializeObject(new QuickSearchModel()
            {
                ArchetypeId = "GLSection",
                Path = "/objects/Section/rows/" + sectionEntity.SectionId + "/attributes/GLSection/aliasValue",
                Text = sectionEntity.GLSection,
                Toprows = 100,
                ProcessItemId = processItemId,
                AddIdAttribute = false
            }, JsonHelper.Settings);

            return await LookUpService.GetLookUpAsync(sessionId, processItemId, body);
        }
    }
}
