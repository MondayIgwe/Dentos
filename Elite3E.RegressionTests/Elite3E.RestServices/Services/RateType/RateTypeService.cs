using Elite3E.RestServices.Entity;
using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Elite3E.RestServices.Services.RateType
{
    public class RateTypeService : IRateTypeService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();

        public async Task<IRestResponse> AddRateTypeDataCode(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId + "/attributes/Code/value",
                        Value = rateType.RateTypeCode
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddRateTypeDataDescription(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId + "/attributes/Description/value",
                        Value = rateType.RateTypeDescription
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddRateTypeDataCurrency(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId + "/attributes/CurrencyDefault/value",
                        Value = rateType.RateTypeCurrency
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddRateTypeDataTimekeeperCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId +"/attributes/IsTimekeeper/value",
                        Value = rateType.IsTimeKeeperCheckboxValue
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddRateTypeDataDisbursementCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {

                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsCostType/value",
                        Value = rateType.IsDisbursementCheckboxValue
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

         public async Task<IRestResponse> AddRateTypeDataStandardCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsStandard/value",
                        Value = rateType.IsStandardRateCheckboxValue
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

         public async Task<IRestResponse> AddRateTypeDataFirmDefaultCheckbox(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsFirmDefault/value",
                        Value = rateType.IsFirmDefaultCheckboxValue
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

         public async Task<IRestResponse> AddRateTypeDataValidForTimekeeperCheckboxes(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {

                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsTitle/value",
                        Value = rateType.IsValidForTimekeeperCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsRateClass/value",
                        Value = rateType.IsValidForTimekeeperCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsDept/value",
                        Value = rateType.IsValidForTimekeeperCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsOffice/value",
                        Value = rateType.IsValidForTimekeeperCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsSection/value",
                        Value = rateType.IsValidForTimekeeperCheckboxesValues
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }
        

         public async Task<IRestResponse> AddRateTypeDataValidForMatterCheckboxes(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {

                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsMattPractice/value",
                        Value = rateType.IsValidMatterCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsArrangement/value",
                        Value = rateType.IsValidMatterCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsMattDept/value",
                        Value = rateType.IsValidMatterCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsMattOffice/value",
                        Value = rateType.IsValidMatterCheckboxesValues
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/"+ rateType.RateTypeId+"/attributes/IsMattSection/value",
                        Value = rateType.IsValidMatterCheckboxesValues
                    }
                }

            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetEffectiveInformationAsync(string sessionId, string processItemId, ApiRateTypeEntity rateType)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/RateType/rows/" + rateType.RateTypeId + "/childObjects/RateTypeDate/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "RateTypeDate"
                        }
                    }
                }                      

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> GetRateDetailsAsync(string sessionId, string processItemId, string rowId, ApiRateTypeEntity rateType)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "add",
                        Path = "/objects/RateType/rows/" + rateType.RateTypeId + "/childObjects/RateTypeDate/rows/"+ rowId +"/childObjects/RateDateDet/rows/-",
                        Value = new ValueClass
                        {
                            SubclassId = "RateDateDet"
                        }
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddEffectiveInformationAsync(string sessionId, string processItemId, string rowId, ApiRateTypeEntity rateType)
        {

            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/" + rateType.RateTypeId + "/childObjects/RateTypeDate/rows/"+ rowId +"/attributes/EffStart/value",
                        Value = rateType.EffectiveDate
                    },
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/" + rateType.RateTypeId + "/childObjects/RateTypeDate/rows/"+ rowId +"/attributes/DefaultRate/value",
                        Value = rateType.DefaultRateAmount
                    }
                }

            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddRateDetailsAsync(string sessionId, string processItemId, string rowId, string childRowId, ApiRateTypeEntity rateType)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/RateType/rows/" + rateType.RateTypeId + "/childObjects/RateTypeDate/rows/"+ rowId +"/childObjects/RateDateDet/rows/" + childRowId + "/attributes/Rate/value",
                        Value = rateType.DefaultRateAmount
                    }
                }
            }, JsonHelper.Settings);
            return await ProcessDataService.UpdateDataAsync(sessionId, processItemId, body);
        }

        public async Task<IRestResponse> AddSearchResultToWorklistAsync(string sessionId, string processItemId, string BatchAddRowKey)
        {
            var body = JsonConvert.SerializeObject(new AddBatchModel()
            {
                Path = "/objects/RateType/rows/-",
                ItemIDs = new List<System.Guid>(){
                    {new(BatchAddRowKey) }
                }

            }, JsonHelper.Settings); 

            var urlExtension = "data/batchadd";
            return await ProcessDataService.AddDataAsync(sessionId, processItemId, body, urlExtension);
        }

    }
}
