using Elite3E.SoapServices.Models.Matter.Create;

namespace Elite3E.SoapServices.Services.Builders
{
    public static class MatterBuilder
    {

        public static string MatterXmlString()
        {
            var displayName = "Test Auto" + Guid.NewGuid().ToString("N");
            
            var matterSrv = new MatterSrv {
                Initialize = new Initialize {
                    Add = new Add {
                        Matter = new Matter {
                            Attributes = new Attributes {
                                Client = "1",
                                MattStatus = "OP",
                                OpenDate = DateTime.Today.ToString("s"),
                                DisplayName = displayName,
                                Description = displayName,
                                Language = "1033",
                                IsEngageLetterReq = "1",
                                IsWaiverLetterReq = "1",
                                Currency = "USD",
                                CurrencyDateList = "Bill"
                            },
                            Children = new Children {
                                MattDate = new MattDate {
                                    Add = new Add {
                                        MattDate = new MattDate {
                                            Attributes = new Attributes {
                                                //EffStart = DateTime.Today.ToString("s"),
                                               // BillTkpr = "100004",
                                               // RspTkpr = "100001",
                                               // SpvTkpr = "100001",
                                                Office = "8051",
                                               // Department = "Default",
                                               // Section = "Default",
                                               // PracticeGroup = "Default",
                                               // Arrangement = "Hourly",
                                            }
                                       }
                                    }
                                },
                                MattRate = new MattRate {
                                    Add = new Add {
                                        MattRate = new MattRate {
                                            Attributes = new Attributes {
                                                Rate = "STD",
                                                IsActive = "1"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };


            var xmlString = XmlExtensions.Serialize(matterSrv);
            return xmlString;

        }
    }
}
