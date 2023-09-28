using Elite3E.SoapServices.Models.PersonEntity.Create;

namespace Elite3E.SoapServices.Services.Builders
{
    public static class PersonEntityBuilder
    {
        public static string PersonEntityXmlString()
        {

            var personSrv = new EntityPersonSrv {
                Initialize = new Initialize {
                    Add = new Add {
                        EntityPerson = new EntityPerson {
                            Attributes = new Attributes {
                                FirstName = "First" + Guid.NewGuid().ToString("N"),
                                LastName = "Last" + Guid.NewGuid().ToString("N"),
                                EntityType = "Contact",
                                FormattedName = "Default" // not formated code 
                            },
                            Children = new Children {
                                Relate = new Relate {
                                    Add = new Add {
                                        Relate = new Relate {
                                            Children = new Children {
                                                Site = new Site {
                                                    Add = new Add {
                                                        Site = new Site {
                                                            Attributes = new Attributes {
                                                                SiteType = "Billing",
                                                                IsDefault = "1",
                                                                Street = "LOndon Road",
                                                                ZipCode = "E1 0SA",
                                                                City = "London",
                                                                Country = "GB"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                        
                    }
                }
            };


            var xmlString = XmlExtensions.Serialize(personSrv);
            return xmlString;

        }
    }
}
