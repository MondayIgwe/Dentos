using System;

using Elite3E.SoapServices.Models.Vendor.RequestModel;

namespace Elite3E.SoapServices.Services.Builders
{
    public static class VendorBuilder
    {

        public static string VendorXMLString()
        {

            var vendorSrv = new VendorSrv {
                Initialize = new Initialize {
                    Add = new Add {
                        Vendor = new Vendor {
                            Attributes = new Attributes {
                                VendorNum = "1000",
                                Entity = "1847",
                                Name = Guid.NewGuid().ToString("N"),
                                VendorStatus = "APP",
                                IsConfidential = 1,
                                IsOneTime = 1,
                                IsAutoNumbering = 1,
                                IsAutoNumAfterSave = 1,
                                GlobalVendor = "AMEXGBLV"
                            } 
                        } 
                    }
                }
            };

            var xmlString = XmlExtensions.Serialize(vendorSrv);
            return xmlString;

        }       

    }    

}
