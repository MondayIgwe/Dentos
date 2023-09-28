using Elite3E.RestServices.Models.ModelHelper;
using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;
using RestSharp;
using Elite3E.RestServices.Models.RequestModels.OpenProcess;
using Action = Elite3E.RestServices.Models.RequestModels.OpenProcess.Action;

namespace Elite3E.RestServices.Services.OpenProcess
{
    public class OpenProcessService : IOpenProcessService
    {
        public IProcessDataService ProcessDataService = new ProcessDataService();
        public async Task<IRestResponse> GetParameterDataAsync(string sessionId, string id, string value)
        {
            var body = new OpenProcessRequestModel()
            {
                PageId = "NxOpenProcessPO",
                Data = new Data()
                {
                    Type = 27,
                    Objects = new Objects()
                    {

                        NxOpenProcessPo = new NxOpenProcessPo()
                        {
                            Actions = new Actions()
                            {

                                AddAction = new Action()
                                {
                                    AccessType = 3,
                                    Path = "/objects/NxOpenProcessPO/actions/Add"
                                },
                                DeleteAction = new Action()
                                {
                                    AccessType = 3,
                                    Path = "/objects/NxOpenProcessPO/actions/Delete"
                                }
                            },
                            ActualRowCount = 1,
                            Id = "NxOpenProcessPO",
                            ObjectId = "NxOpenProcessPO",
                            RowCount = 1,
                            Rows = new Rows()
                            {
                                ReplaceValue = new Replace()
                                {
                                    Id = id,
                                    Attributes = new Attributes()
                                    {
                                        NxOpenProcessPoid = new User()
                                        {
                                            DataType = 7,
                                            Value = value,
                                            Id = "NxOpenProcessPOID",
                                            Path = "/objects/NxOpenProcessPO/rows/" + id +
                                                   "/attributes/NxOpenProcessPOID"

                                        },
                                        NxBaseUser = new User()
                                        {
                                            DataType = 7,
                                            Id = "NxBaseUser",
                                            Path = "/objects/NxOpenProcessPO/rows/" + id + "/attributes/NxBaseUser"
                                        },
                                        Process = new User()
                                        {
                                            DataType = 7,
                                            Id = "Process",
                                            Path = "/objects/NxOpenProcessPO/rows/" + id + "/attributes/Process"
                                        },
                                        NxBaseUserRelBaseUserName = new User()
                                        {
                                            AccessType = 2,
                                            DataType = 15,
                                            Id = "NxBaseUserRel.BaseUserName",
                                            Path = "/objects/NxOpenProcessPO/rows/" + id +
                                                   "/attributes/NxBaseUserRel.BaseUserName"
                                        },
                                        NxFwkAppObjectRelAppObjectCode = new User()
                                        {
                                            AccessType = 2,
                                            DataType = 15,
                                            Id = "NxFWKAppObjectRel.AppObjectCode",
                                            Path = "/objects/NxOpenProcessPO/rows/" + id +
                                                   "/attributes/NxFWKAppObjectRel.AppObjectCode"
                                        }

                                    },
                                    Index = 0,
                                    RowState = 1,
                                    SubclassId = "NxOpenProcessPO",
                                    Path = "/objects/NxOpenProcessPO/rows/" + id + ""
                                }
                            },
                            Path = "/objects/NxOpenProcessPO"

                        },
                    }
                }
            };

            var serializedBody = JsonConvert.SerializeObject(body, JsonHelper.Settings).Replace("replace", id);
            var urlExtension = "data/parameters";
            return await ProcessDataService.PostAsync(sessionId, urlExtension,serializedBody);
        }

        public async Task<IRestResponse> SearchUserDataAsync(string sessionId, string requestId, string userName, string contextId)
        {
            var body = JsonConvert.SerializeObject(new ChildFormModel()
            {
                Changes = new List<Changes>()
                {
                    new()
                    {
                        Op = "replace",
                        Path = "/objects/NxOpenProcessPO/rows/" + requestId + "/attributes/NxBaseUser/aliasValue",
                        Value = userName
                    }
                },
                ContextId = contextId
            }, JsonHelper.Settings);

            return await ProcessDataService.UpdateAsync(sessionId, body);
        }

        public async Task<IRestResponse> PostReportDataAsync(string sessionId, string processValue, string userValue, string userName, string id)
        {
            var body = new ProcessReportRequestModel()
            {
                Presentation = new Presentation()
                {
                    PageDimensions = new List<EDimension>()
                    {
                        new EDimension()
                        {
                            Id = "NxFWKProcessItem",
                            Type = 1,
                            ShowSubTotalsWithSingleRows = true,
                            ShowGroupWithRowsAllZeros = true,
                            ShowRowsWithAllZeros = true,
                            Indent = true,
                            ShowTotals = true,
                            Caption = "Group By Process",
                            SortAttributes = new List<SortAttribute>()
                            {
                                new SortAttribute()
                                {
                                    Id = "Name",
                                    SortDirection = 1
                                }
                            }
                        }
                    },
                    ReportAvailableDimensions = new List<EDimension>()
                    {
                        new EDimension()
                        {
                            Id = "NxFWKProcessItem",
                            Type = 1,
                            ShowSubTotalsWithSingleRows = true,
                            ShowGroupWithRowsAllZeros = true,
                            ShowRowsWithAllZeros = true,
                            Indent = true,
                            ShowTotals = true,
                            Caption = "Group By Process",
                            SortAttributes = new List<SortAttribute>()
                            {
                                new SortAttribute()
                                {
                                    Id = "Name",
                                    SortDirection = 1
                                }
                            }
                        }
                    },
                    ViewMode = 2,
                    BoundId = "NxOpenProcesses",
                    BoundType = 4,
                    Id = "NxOpenProcesses",
                    Caption = "Open Processes",
                    Type = 40,
                    Version = "2.8.0.3"
                },
                TopRows = 1000,
                ParameterSet = new ParameterSet()
                {
                    Objects = new Objects()
                    {
                        NxOpenProcessPo = new NxOpenProcessPo()
                        {
                            ObjectId = "NxOpenProcessPO",
                            Rows = new Rows()
                            {
                                ReplaceValue = new Replace()
                                {
                                    SubclassId = "NxOpenProcessPO",
                                    Attributes = new Attributes()
                                    {
                                        NxOpenProcessPoid = new User()
                                        {
                                            Value = processValue
                                        },
                                        NxBaseUser = new User()
                                        {
                                            Value = userValue,
                                            AliasValue = userName
                                        }, 
                                        NxBaseUserRelBaseUserName =  new User()
                                        {
                                            Value = userName,
                                            AccessType = 2
                                        },
                                        NxFwkAppObjectRelAppObjectCode = new User()
                                        {
                                            AccessType = 2
                                        }
                                    },
                                    RowState = 1
                                }
                            }
                        }

                    }
                },
                Filters = new Filters()
                {
                    NxOpenProcesses = new NxOpenProcesses()
                    {
                        Archetype = "NxFWKProcessItemStep",
                        ArchetypeType = 1
                    }
                },
                CurrentDay = DateTime.Today.Day.ToString(),
                CurrentMonth = DateTime.Today.Month.ToString(),
                CurrentYear = DateTime.Today.Year.ToString()
            };

            var serializedBody = JsonConvert.SerializeObject(body, JsonHelper.Settings).Replace("replace", id);
            var urlExtension = "presentation/NxOpenProcesses/reportdata";
            return await ProcessDataService.PostAsync(sessionId, urlExtension, serializedBody);
        }

        public async Task<IRestResponse> PostOpenProcessCancelActionAsync(string sessionId, string actionData, string processValue, string userValue, string userName, string id)
        {
            var body = new ProcessActionRequestModel()
            {
                ActionData = actionData,
                Filters = new Filters()
                {
                    NxOpenProcesses = new NxOpenProcesses()
                    {
                        ArchetypeType = 1,
                        Archetype = "NxFWKProcessItemStep"
                    }
                },
                IsReload = false,
                ParameterSet = new ParameterSet()
                {
                    Objects = new Objects()
                    {
                        NxOpenProcessPo = new NxOpenProcessPo()
                        {
                            ObjectId = "NxOpenProcessPO",
                            Rows = new Rows()
                            {
                                ReplaceValue = new Replace()
                                {
                                    Attributes = new Attributes()
                                    {
                                        NxOpenProcessPoid = new User()
                                        {
                                            Value = processValue
                                        },
                                        NxBaseUser = new User()
                                        {
                                            Value = userValue,
                                            AliasValue = userName
                                        },
                                        NxBaseUserRelBaseUserName = new User()
                                        {
                                            Value = userName,
                                            AccessType = 2
                                        },
                                        NxFwkAppObjectRelAppObjectCode = new User()
                                        {
                                            AccessType = 2
                                        }
                                    },
                                    RowState = 1
                                }
                            }
                        }
                        
                    }
                },
                Presentation = new Presentation()
                {
                    PageDimensions = new List<EDimension>()
                    {
                        new EDimension()
                        {
                            Id = "NxFWKProcessItem",
                            Type = 1,
                            ShowSubTotalsWithSingleRows = true,
                            ShowRowsWithAllZeros = true,
                            ShowGroupWithRowsAllZeros = true,
                            Indent = true,
                            ShowTotals = true,
                            Caption = "Group By Process",
                            SortAttributes = new List<SortAttribute>()
                            {
                                new SortAttribute()
                                {
                                    Id = "Name",
                                    SortDirection = 1
                                }
                            }
                        }
                    },
                    ReportAvailableDimensions = new List<EDimension>()
                    {
                        new EDimension()
                        {
                            Id = "NxFWKProcessItem",
                            Type = 1,
                            ShowSubTotalsWithSingleRows = true,
                            ShowRowsWithAllZeros = true,
                            ShowGroupWithRowsAllZeros = true,
                            Indent = true,
                            ShowTotals = true,
                            Caption = "Group By Process",
                            SortAttributes = new List<SortAttribute>()
                            {
                                new SortAttribute()
                                {
                                    Id = "Name",
                                    SortDirection = 1
                                }
                            }
                        }
                    },
                    ViewMode = 2,
                    BoundId = "NxOpenProcesses",
                    BoundType = 4,
                    Id = "NxOpenProcesses",
                    Caption = "OPen Processes",
                    Version = "2.8.0.3"
                },
                StartDate = DateTime.Today.Date,
                MetricRowsRequest = new MetricRowsRequest()
                {
                    UseCache = false,
                    StartRow = 0,
                    RowCount = 100000,
                    StartColumn = 0,
                    ColumnCount = 0,
                    IncludeHeader = true,
                    HeaderColumnCount = 10000

                },
                TopRows = 1000
            };

            var serializedBody = JsonConvert.SerializeObject(body, JsonHelper.Settings).Replace("replace", id);
            var urlExtension = "presentation/NxOpenProcesses/action";
            return await ProcessDataService.PostAsync(sessionId, urlExtension, serializedBody);
        }
    }
}
