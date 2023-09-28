using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Enums;
using Elite3E.Infrastructure.Extensions;
using Elite3E.RestServices.Entity;

namespace Elite3E.RegressionTests.DataCreators.DefaultData
{
    public static class DefaultRegionalValues
    {
        public static ApiChargeTypeEntity GetChargeTypeDefaultValues(string description)
        {
            var chargeTypeEntity = new ApiChargeTypeEntity()
            {
                ChargeCode = "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10"),
                Description = description,
                Active = "1"
            };

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                case Regions.Europe:
                case Regions.Uk:
                case Regions.Us:
                case Regions.Singapore:
                    chargeTypeEntity.CategoryInput = "Billed on Account";
                    chargeTypeEntity.TransactionTypeAlias = "Anticipated Hard Cost";
                    break;

                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    chargeTypeEntity.CategoryInput = "Billed on Account";
                    chargeTypeEntity.TransactionTypeAlias = "Anticipated Hard Cost";
                    break;
            }

            return chargeTypeEntity;
        }

        public static ApiClientMaintenanceEntity GetClientMaintenanceDefaultValues(ApiClientMaintenanceEntity client)
        {
            //var client = new ApiClientMaintenanceEntity()
            //{
            //    Name = formattedName,
            //    InvoiceSiteName = "London UK site",

            //    Status = "Fully Open"
            //};

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "Vancouver" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "CANADA" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "CAD - Canadian Dollar" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "London UK site" : client.InvoiceSiteName;
                    break;
                case Regions.Europe:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "London (EU)" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "UNITED KINGDOM" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "EUR - Euro" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "London UK site" : client.InvoiceSiteName;
                    break;
                case Regions.Uk:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "London (UKIME)" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "UNITED KINGDOM" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "GBP - British Pound" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "London UK site" : client.InvoiceSiteName;
                    break;
                case Regions.Us:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "Chicago" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "UNITED STATES" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "USD - US Dollar" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "London UK site" : client.InvoiceSiteName;
                    break;
                case Regions.Singapore:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "Singapore" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "Singapore" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "SGD - Singapore Dollar" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "Singapore ltd" : client.InvoiceSiteName;
                    break;
                case Regions.Staging:
                case Regions.Training:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "London (UKIME)" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "UNITED KINGDOM" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "GBP - British Pound" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "London UK site" : client.InvoiceSiteName;
                    break;
                case Regions.Qa:
                case Regions.e2eft:
                case Regions.Ft:
                    client.Status = (string.IsNullOrEmpty(client.Status)) ? "Fully Open" : client.Status;
                    client.Office = (string.IsNullOrEmpty(client.Office)) ? "London (UKIME)" : client.Office;
                    client.Country = (string.IsNullOrEmpty(client.Country)) ? "UNITED KINGDOM" : client.Country;
                    client.Currency = (string.IsNullOrEmpty(client.Currency)) ? "GBP - British Pound" : client.Currency;
                    client.InvoiceSiteName = (string.IsNullOrEmpty(client.InvoiceSiteName)) ? "London UK site" : client.InvoiceSiteName;
                    break;

            }

            return client;
        }

        public static ApiFeeEarnerEntity GetFeeEarnerDefaultValues(ApiFeeEarnerEntity feeEarner, ApiEntity entity)
        {
            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    feeEarner.Office = (string.IsNullOrEmpty(feeEarner.Office)) ? "Vancouver" : feeEarner.Office;
                    feeEarner.Department = (string.IsNullOrEmpty(feeEarner.Department)) ? "Accounts" : feeEarner.Department;
                    feeEarner.Section = (string.IsNullOrEmpty(feeEarner.Section)) ? "Accounts" : feeEarner.Section;
                    feeEarner.Title = (string.IsNullOrEmpty(feeEarner.Title)) ? "Full Interest Partner" : feeEarner.Title;
                    feeEarner.RateClass = (string.IsNullOrEmpty(feeEarner.RateClass)) ? "Premium" : feeEarner.RateClass;
                    feeEarner.RateTypeDescription = (string.IsNullOrEmpty(feeEarner.RateTypeDescription)) ? GetFeeEarnerRateTypeDefaultValues().RateTypeDescription : feeEarner.RateTypeDescription;
                    feeEarner.EffectiveRateCurrencyDescription = (string.IsNullOrEmpty(feeEarner.EffectiveRateCurrencyDescription)) ? "CAD - Canadian Dollar" : feeEarner.EffectiveRateCurrencyDescription;
                    break;
                case Regions.Europe:
                    feeEarner.Office = (string.IsNullOrEmpty(feeEarner.Office)) ? "London(EU)" : feeEarner.Office;
                    feeEarner.Department = (string.IsNullOrEmpty(feeEarner.Department)) ? "Accounts" : feeEarner.Department;
                    feeEarner.Section = (string.IsNullOrEmpty(feeEarner.Section)) ? "Accounts" : feeEarner.Section;
                    feeEarner.Title = (string.IsNullOrEmpty(feeEarner.Title)) ? "Full Interest Partner" : feeEarner.Title;
                    feeEarner.RateClass = (string.IsNullOrEmpty(feeEarner.RateClass)) ? "Premium" : feeEarner.RateClass;
                    feeEarner.RateTypeDescription = (string.IsNullOrEmpty(feeEarner.RateTypeDescription)) ? GetFeeEarnerRateTypeDefaultValues().RateTypeDescription : feeEarner.RateTypeDescription;
                    feeEarner.EffectiveRateCurrencyDescription = (string.IsNullOrEmpty(feeEarner.EffectiveRateCurrencyDescription)) ? "EUR - Euro" : feeEarner.EffectiveRateCurrencyDescription;
                    break;
                case Regions.Uk:
                    feeEarner.Office = (string.IsNullOrEmpty(feeEarner.Office)) ? "London(UKIME)" : feeEarner.Office;
                    feeEarner.Department = (string.IsNullOrEmpty(feeEarner.Department)) ? "UKME_Accounts" : feeEarner.Department;
                    feeEarner.Section = (string.IsNullOrEmpty(feeEarner.Section)) ? "UKME_Accounts" : feeEarner.Section;
                    feeEarner.Title = (string.IsNullOrEmpty(feeEarner.Title)) ? "Full Interest Partner" : feeEarner.Title;
                    feeEarner.RateClass = (string.IsNullOrEmpty(feeEarner.RateClass)) ? "Premium" : feeEarner.RateClass;
                    feeEarner.RateTypeDescription = (string.IsNullOrEmpty(feeEarner.RateTypeDescription)) ? GetFeeEarnerRateTypeDefaultValues().RateTypeDescription : feeEarner.RateTypeDescription;
                    feeEarner.EffectiveRateCurrencyDescription = (string.IsNullOrEmpty(feeEarner.EffectiveRateCurrencyDescription)) ? "GBP - British Pound" : feeEarner.EffectiveRateCurrencyDescription;
                    break;
                case Regions.Us:
                    feeEarner.Office = (string.IsNullOrEmpty(feeEarner.Office)) ? "Chicago" : feeEarner.Office;
                    feeEarner.Department = (string.IsNullOrEmpty(feeEarner.Department)) ? "Accounts" : feeEarner.Department;
                    feeEarner.Section = (string.IsNullOrEmpty(feeEarner.Section)) ? "Accounts" : feeEarner.Section;
                    feeEarner.Title = (string.IsNullOrEmpty(feeEarner.Title)) ? "Full Interest Partner" : feeEarner.Title;
                    feeEarner.RateClass = (string.IsNullOrEmpty(feeEarner.RateClass)) ? "Premium" : feeEarner.RateClass;
                    feeEarner.RateTypeDescription = (string.IsNullOrEmpty(feeEarner.RateTypeDescription)) ? GetFeeEarnerRateTypeDefaultValues().RateTypeDescription : feeEarner.RateTypeDescription;
                    feeEarner.EffectiveRateCurrencyDescription = (string.IsNullOrEmpty(feeEarner.EffectiveRateCurrencyDescription)) ? "USD - US Dollar" : feeEarner.EffectiveRateCurrencyDescription;
                    break;
                case Regions.Singapore:
                    feeEarner.Office = (string.IsNullOrEmpty(feeEarner.Office)) ? "Singapore" : feeEarner.Office;
                    feeEarner.Department = (string.IsNullOrEmpty(feeEarner.Department)) ? "Corporate - Singapore" : feeEarner.Department;
                    feeEarner.Section = (string.IsNullOrEmpty(feeEarner.Section)) ? "Singapore Verein" : feeEarner.Section;
                    feeEarner.Title = (string.IsNullOrEmpty(feeEarner.Title)) ? "Full Interest Partner" : feeEarner.Title;
                    feeEarner.RateClass = (string.IsNullOrEmpty(feeEarner.RateClass)) ? "Premium" : feeEarner.RateClass;
                    feeEarner.RateTypeDescription = (string.IsNullOrEmpty(feeEarner.RateTypeDescription)) ? GetFeeEarnerRateTypeDefaultValues().RateTypeDescription : feeEarner.RateTypeDescription;
                    feeEarner.EffectiveRateCurrencyDescription = (string.IsNullOrEmpty(feeEarner.EffectiveRateCurrencyDescription)) ? "SGD - Singapore Dollar" : feeEarner.EffectiveRateCurrencyDescription;
                    break;

                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    feeEarner.Office = (string.IsNullOrEmpty(feeEarner.Office)) ? "Aberdeen" : feeEarner.Office;
                    feeEarner.Department = (string.IsNullOrEmpty(feeEarner.Department)) ? "UKME_Accounts" : feeEarner.Department;
                    feeEarner.Section = (string.IsNullOrEmpty(feeEarner.Section)) ? "UKME_Accounts" : feeEarner.Section;
                    feeEarner.Title = (string.IsNullOrEmpty(feeEarner.Title)) ? "Full Interest Partner" : feeEarner.Title;
                    feeEarner.RateClass = (string.IsNullOrEmpty(feeEarner.RateClass)) ? "Premium" : feeEarner.RateClass;
                    feeEarner.RateTypeDescription = (string.IsNullOrEmpty(feeEarner.RateTypeDescription)) ? GetFeeEarnerRateTypeDefaultValues().RateTypeDescription : feeEarner.RateTypeDescription;
                    feeEarner.EffectiveRateCurrencyDescription = (string.IsNullOrEmpty(feeEarner.EffectiveRateCurrencyDescription)) ? "GBP - British Pound" : feeEarner.EffectiveRateCurrencyDescription;

                    break;
            }

            feeEarner.EntityName = (string.IsNullOrEmpty(feeEarner.EntityName)) ? $"{entity.FirstName.String} {entity.LastName.String}" : feeEarner.EntityName;
            feeEarner.EffectiveRate = (string.IsNullOrEmpty(feeEarner.EffectiveRate.String)) ? "100" : feeEarner.EffectiveRate;
            feeEarner.RateTypeFirmStandardRate = (string.IsNullOrEmpty(feeEarner.RateTypeFirmStandardRate)) ? "Yes" : feeEarner.RateTypeFirmStandardRate;


            return feeEarner;
        }

        public static UserRoleManagementEntity GetUserRoleDefaultValues(UserRoleManagementEntity userEntity)
        {
            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada: 
                case Regions.Europe:               
                case Regions.Uk:               
                    userEntity.LanguageAlias = "English (United Kingdom)";
                    userEntity.DefaultOperatingAlias = (string.IsNullOrEmpty(userEntity.DefaultOperatingAlias)) ? "Firm" : userEntity.DefaultOperatingAlias;
                    break;
                case Regions.Us:
                    userEntity.LanguageAlias = "English (United Kingdom) (2057)";
                    userEntity.DefaultOperatingAlias = (string.IsNullOrEmpty(userEntity.DefaultOperatingAlias)) ? "Dentons United States, LLP" : userEntity.DefaultOperatingAlias;
                    break;
                case Regions.Singapore:
                    userEntity.LanguageAlias = "English (United Kingdom) (2057)";
                    userEntity.DefaultOperatingAlias = (string.IsNullOrEmpty(userEntity.DefaultOperatingAlias)) ? "Dentons Rodyk & Davidson LLP" : userEntity.DefaultOperatingAlias;
                    break;
                case Regions.Qa:
                case Regions.e2eft:
                case Regions.Ft:
                    userEntity.LanguageAlias = "English (United Kingdom)";
                    userEntity.DefaultOperatingAlias = (string.IsNullOrEmpty(userEntity.DefaultOperatingAlias)) ? "Default" : userEntity.DefaultOperatingAlias;
                    break;
                case Regions.Staging:
                    userEntity.LanguageAlias = "English (United Kingdom) (2057)";
                    userEntity.DefaultOperatingAlias = (string.IsNullOrEmpty(userEntity.DefaultOperatingAlias)) ? "Dentons UK and Middle East LLP" : userEntity.DefaultOperatingAlias;
                    break;
                case Regions.Training:
                    userEntity.LanguageAlias = "English (United Kingdom)";
                    userEntity.DefaultOperatingAlias = (string.IsNullOrEmpty(userEntity.DefaultOperatingAlias)) ? "Firm Unit" : userEntity.DefaultOperatingAlias;
                    break;
            }
            return userEntity;
        }

        public static ApiEntity GetApiEntityDefaultValues(ApiEntity entity)
        {

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    entity.FormatCode = (string.IsNullOrEmpty(entity.FormatCode.String)) ? "Full Name" : entity.FormatCode;
                    entity.PersonType = (string.IsNullOrEmpty(entity.PersonType)) ? "Employee" : entity.PersonType;
                    entity.SiteDescription = (string.IsNullOrEmpty(entity.SiteDescription.String)) ? "London UK site" : entity.SiteDescription;
                    entity.SiteType = (string.IsNullOrEmpty(entity.SiteType.String)) ? "BILLING" : entity.SiteType;
                    entity.CountryCode = (string.IsNullOrEmpty(entity.CountryCode.String)) ? "CA" : entity.CountryCode;
                    entity.Street = (string.IsNullOrEmpty(entity.Street.String)) ? "Street 123" : entity.Street;
                    entity.LanguageKey = (string.IsNullOrEmpty(entity.LanguageKey)) ? "English (United Kingdom)" : entity.LanguageKey;
                    entity.Country = (string.IsNullOrEmpty(entity.Country)) ? "CANADA" : entity.Country;
                    entity.EntityType = (string.IsNullOrEmpty(entity.EntityType.String)) ? "Entity" : entity.EntityType;
                    break;
                case Regions.Europe:
                    entity.FormatCode = (string.IsNullOrEmpty(entity.FormatCode.String)) ? "Full Name" : entity.FormatCode;
                    entity.PersonType = (string.IsNullOrEmpty(entity.PersonType)) ? "Employee" : entity.PersonType;
                    entity.SiteDescription = (string.IsNullOrEmpty(entity.SiteDescription.String)) ? "London UK site" : entity.SiteDescription;
                    entity.SiteType = (string.IsNullOrEmpty(entity.SiteType.String)) ? "BILLING" : entity.SiteType;
                    entity.CountryCode = (string.IsNullOrEmpty(entity.CountryCode.String)) ? "GB" : entity.CountryCode;
                    entity.Street = (string.IsNullOrEmpty(entity.Street.String)) ? "Street 123" : entity.Street;
                    entity.LanguageKey = (string.IsNullOrEmpty(entity.LanguageKey)) ? "English (United Kingdom)" : entity.LanguageKey;
                    entity.Country = (string.IsNullOrEmpty(entity.Country)) ? "UNITED KINGDOM" : entity.Country;
                    entity.EntityType = (string.IsNullOrEmpty(entity.EntityType.String)) ? "Entity" : entity.EntityType;
                    break;
                case Regions.Uk:
                    entity.FormatCode = (string.IsNullOrEmpty(entity.FormatCode.String)) ? "Full Name" : entity.FormatCode;
                    entity.PersonType = (string.IsNullOrEmpty(entity.PersonType)) ? "Employee" : entity.PersonType;
                    entity.SiteDescription = (string.IsNullOrEmpty(entity.SiteDescription.String)) ? "London UK site" : entity.SiteDescription;
                    entity.SiteType = (string.IsNullOrEmpty(entity.SiteType.String)) ? "BILLING" : entity.SiteType;
                    entity.CountryCode = (string.IsNullOrEmpty(entity.CountryCode.String)) ? "GB" : entity.CountryCode;
                    entity.Street = (string.IsNullOrEmpty(entity.Street.String)) ? "Street 123" : entity.Street;
                    entity.LanguageKey = (string.IsNullOrEmpty(entity.LanguageKey)) ? "English (United Kingdom)" : entity.LanguageKey;
                    entity.Country = (string.IsNullOrEmpty(entity.Country)) ? "UNITED KINGDOM" : entity.Country;
                    entity.EntityType = (string.IsNullOrEmpty(entity.EntityType.String)) ? "Entity" : entity.EntityType;
                    break;
                case Regions.Us:
                    entity.FormatCode = (string.IsNullOrEmpty(entity.FormatCode.String)) ? "Full Name" : entity.FormatCode;
                    entity.PersonType = (string.IsNullOrEmpty(entity.PersonType)) ? "Employee" : entity.PersonType;
                    entity.SiteDescription = (string.IsNullOrEmpty(entity.SiteDescription.String)) ? "London UK site" : entity.SiteDescription;
                    entity.SiteType = (string.IsNullOrEmpty(entity.SiteType.String)) ? "BILLING" : entity.SiteType;
                    entity.CountryCode = (string.IsNullOrEmpty(entity.CountryCode.String)) ? "US" : entity.CountryCode;
                    entity.Street = (string.IsNullOrEmpty(entity.Street.String)) ? "Street 123" : entity.Street;
                    entity.LanguageKey = (string.IsNullOrEmpty(entity.LanguageKey)) ? "English (United Kingdom)" : entity.LanguageKey;
                    entity.Country = (string.IsNullOrEmpty(entity.Country)) ? "UNITED STATES" : entity.Country;
                    entity.EntityType = (string.IsNullOrEmpty(entity.EntityType.String)) ? "Entity" : entity.EntityType;
                    break;
                case Regions.Singapore:
                    entity.FormatCode = (string.IsNullOrEmpty(entity.FormatCode.String)) ? "Full Name" : entity.FormatCode;
                    entity.PersonType = (string.IsNullOrEmpty(entity.PersonType)) ? "Employee" : entity.PersonType;
                    entity.SiteDescription = (string.IsNullOrEmpty(entity.SiteDescription.String)) ? "Singapore ltd" : entity.SiteDescription;
                    entity.SiteType = (string.IsNullOrEmpty(entity.SiteType.String)) ? "BILLING" : entity.SiteType;
                    entity.CountryCode = (string.IsNullOrEmpty(entity.CountryCode.String)) ? "SG" : entity.CountryCode;
                    entity.Street = (string.IsNullOrEmpty(entity.Street.String)) ? "Street 123" : entity.Street;
                    entity.LanguageKey = (string.IsNullOrEmpty(entity.LanguageKey)) ? "English (United Kingdom)" : entity.LanguageKey;
                    entity.Country = (string.IsNullOrEmpty(entity.Country)) ? "SINGAPORE" : entity.Country;
                    entity.EntityType = (string.IsNullOrEmpty(entity.EntityType.String)) ? "Entity" : entity.EntityType;
                    break;
                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    entity.FormatCode = (string.IsNullOrEmpty(entity.FormatCode.String)) ? "Full Name" : entity.FormatCode;
                    entity.PersonType = (string.IsNullOrEmpty(entity.PersonType)) ? "Employee" : entity.PersonType;
                    entity.SiteDescription = (string.IsNullOrEmpty(entity.SiteDescription.String)) ? "London UK site" : entity.SiteDescription;
                    entity.SiteType = (string.IsNullOrEmpty(entity.SiteType.String)) ? "BILLING" : entity.SiteType;
                    entity.CountryCode = (string.IsNullOrEmpty(entity.CountryCode.String)) ? "GB" : entity.CountryCode;
                    entity.Street = (string.IsNullOrEmpty(entity.Street.String)) ? "Street 123" : entity.Street;
                    entity.LanguageKey = (string.IsNullOrEmpty(entity.LanguageKey)) ? "English (United Kingdom)" : entity.LanguageKey;
                    entity.Country = (string.IsNullOrEmpty(entity.Country)) ? "UNITED KINGDOM" : entity.Country;
                    entity.EntityType = (string.IsNullOrEmpty(entity.EntityType.String)) ? "Entity" : entity.EntityType;
                    break;
            }
            return entity;
        }

        public static ApiOfficeConfiguration GetClientMaintenanceDefaultValues(ApiOfficeConfiguration officeConfig)
        {
            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                case Regions.Europe:
                case Regions.Uk:
                case Regions.Us:
                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    officeConfig.Office = (string.IsNullOrEmpty(officeConfig.Office)) ? "Amsterdam" : officeConfig.Office;
                    break;
                case Regions.Singapore:
                    officeConfig.Office = (string.IsNullOrEmpty(officeConfig.Office)) ? "Singapore" : officeConfig.Office;
                    break;
            }
            officeConfig.DisbursementTypeValue = (string.IsNullOrEmpty(officeConfig.DisbursementTypeValue)) ? "Completion BACS" : officeConfig.DisbursementTypeValue;

            return officeConfig;
        }


        public static ApiPayeeEntity GetPayeeEntityDefaultValues(string payeeName)
        {
            var payee = new ApiPayeeEntity()
            {
                Name = payeeName,
                PaymentTerm = "Immediate",
                Status = "Approved"
            };

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    payee.Office = "Vancouver";
                    payee.Unit = "Dentons Canada LLP";
                    payee.Currency = "CAD";
                    break;
                case Regions.Europe:
                    payee.Office = "London (EU)";
                    payee.Unit = "Dentons Europe LLP (UK)";
                    payee.Currency = "EUR";
                    break;
                case Regions.Uk:
                    payee.Office = "London (UKIME)";
                    payee.Unit = "Dentons UK and Middle East LLP";
                    payee.Currency = "GBP";
                    break;
                case Regions.Us:
                    payee.Office = "Chicago";
                    payee.Unit = "Dentons United States, LLP";
                    payee.Currency = "USD";
                    break;
                case Regions.Singapore:
                    payee.Office = "Singapore";
                    payee.Unit = "Dentons Rodyk & Davidson LLP";
                    payee.Currency = "SGD";
                    break;

                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    payee.Office = "Aberdeen";
                    payee.Unit = "Dentons UK and Middle East LLP";
                    payee.Currency = "GBP";
                    break;
            }

            return payee;
        }

        //Payer
        public static ApiPayerEntity GetPayerEntityDefaultValues(string payerName)
        {
            var payer = new ApiPayerEntity()
            {
                PayerName = (string.IsNullOrEmpty(payerName)) ? "PN_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+8") : payerName
            };

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Singapore:
                    payer.TaxArea = "Singapore";
                    payer.Site = "Singapore Ltd";
                    break;
                case Regions.Canada:
                case Regions.Us:
                    payer.TaxArea = "United States";
                    payer.Site = "London UK site";
                    break;
                case Regions.Qa:
                case Regions.Uk:
                case Regions.Ft:
                case Regions.Europe:
                case Regions.Staging:
                    payer.TaxArea = "United Kingdom";
                    payer.Site = "London UK site";
                    break;
                case Regions.e2eft:
                
                case Regions.Training:
                    payer.TaxArea = "Australia";
                    payer.Site = "London UK site";
                    break;
            }

            return payer;
        }

        public static ApiRateMaintenanceEntity GetRateDefaultValues(ApiRateMaintenanceEntity apiRateMaintenanceEntity)
        {
            apiRateMaintenanceEntity.Code = string.IsNullOrEmpty(apiRateMaintenanceEntity.Code.String) ? "Standard" : apiRateMaintenanceEntity.Code;
            apiRateMaintenanceEntity.Description = string.IsNullOrEmpty(apiRateMaintenanceEntity.Description.String) ? "Standard" : apiRateMaintenanceEntity.Description;

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    apiRateMaintenanceEntity.RateTypeDescription = "Rate 01";
                    apiRateMaintenanceEntity.Code = "Rate_01";
                    break;
                case Regions.Uk:
                    apiRateMaintenanceEntity.RateTypeDescription = "Standard";
                    break;
                case Regions.Europe:
                case Regions.Us:
                case Regions.Singapore:
                    apiRateMaintenanceEntity.RateTypeDescription = "Standard Rate";
                    break;
                case Regions.Qa:
                case Regions.Staging:
                case Regions.Training:
                    apiRateMaintenanceEntity.RateTypeDescription = "Standard Rate";
                    break;
                case Regions.Ft:
                case Regions.e2eft:
                    apiRateMaintenanceEntity.RateTypeDescription = "Standard";
                    apiRateMaintenanceEntity.Code = "STD";
                    break;
            }

            return apiRateMaintenanceEntity;
        }

        public static ApiRateTypeEntity GetRateTypeDefaultValues(ApiRateTypeEntity apiRateTypeEntity)
        {
            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    apiRateTypeEntity.RateTypeCurrencyDisplayName = "CAD - Canadian Dollar";
                    break;
                case Regions.Europe:
                    apiRateTypeEntity.RateTypeCurrencyDisplayName = "EUR - Euro";
                    break;
                case Regions.Us:
                    apiRateTypeEntity.RateTypeCurrencyDisplayName = "USD - US Dollar";
                    break;
                case Regions.Singapore:
                    apiRateTypeEntity.RateTypeCurrencyDisplayName = "SGD - Singapore Dollar";
                    break;
                case Regions.Uk:
                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    apiRateTypeEntity.RateTypeCurrencyDisplayName = "GBP - British Pound";
                    break;
            }

            return apiRateTypeEntity;
        }

        public static ApiRateTypeEntity GetFeeEarnerRateTypeDefaultValues()
        {
            ApiRateTypeEntity rateType = new ApiRateTypeEntity();
            rateType.RateTypeCode = "FEE_FIRM";
            rateType.RateTypeDescription = "Fee Earner Rate Type Firm Default";
            rateType.IsTimeKeeperCheckbox = "Yes";
            rateType.IsFirmDefaultCheckbox = "Yes";
            rateType.RateTypeCurrencyDisplayName = "GBP - British Pound";
            rateType.IsDisbursementCheckbox = "No";
            rateType.IsStandardRateCheckbox = "No";
            rateType.IsValidForTimekeeperCheckboxes = "No";
            rateType.IsValidForMatterCheckboxes = "No";

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    rateType.RateTypeCurrencyDisplayName = "CAD - Canadian Dollar";
                    break;
                case Regions.Europe:
                    rateType.RateTypeCurrencyDisplayName = "EUR - Euro";
                    break;
                case Regions.Us:
                    rateType.RateTypeCurrencyDisplayName = "USD - US Dollar";
                    break;
                case Regions.Singapore:
                    rateType.RateTypeCurrencyDisplayName = "SGD - Singapore Dollar";
                    break;
                case Regions.Qa:
                case Regions.e2eft:
                case Regions.Ft:
                    rateType.RateTypeCode = "FEE_FIRM";
                    rateType.RateTypeDescription = "Fee Earner Rate Type Firm Default";
                    rateType.IsTimeKeeperCheckbox = "Yes";
                    rateType.IsFirmDefaultCheckbox = "Yes";
                    rateType.RateTypeCurrencyDisplayName = "GBP - British Pound";
                    break;
                case Regions.Staging:
                case Regions.Training:
                case Regions.Uk:
                    rateType.RateTypeCurrencyDisplayName = "GBP - British Pound";
                    break;
            }
            return rateType;
        }

        public static ApiRateTypeEntity GetStandardRateTypeDefaultValues()
        {
            ApiRateTypeEntity rateType = new ApiRateTypeEntity();
            rateType.RateTypeCode = "STD";
            rateType.RateTypeDescription = "Standard Rate";
            rateType.IsValidForTimekeeperCheckboxes = "Yes";
            rateType.IsValidForMatterCheckboxes = "Yes";
            rateType.EffectiveDate = StepArgumentExtension.ReplaceDynamicValues("{Today}-1", "yyyy-MM-dd"); //EffectiveDate Is required for all Rate Types not Fee Earners
            rateType.DefaultRateAmount = "100";
            rateType.IsStandardRateCheckbox = "Yes";
            rateType.IsTimeKeeperCheckbox = "No";
            rateType.IsFirmDefaultCheckbox = "No";
            rateType.IsDisbursementCheckbox = "No";

            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                    rateType.RateTypeCurrencyDisplayName = "CAD - Canadian Dollar";
                    break;
                case Regions.Europe:
                    rateType.RateTypeCurrencyDisplayName = "EUR - Euro";
                    break;
                case Regions.Us:
                    rateType.RateTypeCurrencyDisplayName = "USD - US Dollar";
                    break;
                case Regions.Singapore:
                    rateType.RateTypeCurrencyDisplayName = "SGD - Singapore Dollar";
                    break;
                case Regions.Uk:
                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    rateType.RateTypeCurrencyDisplayName = "GBP - British Pound";

                    break;
            }
            return rateType;
        }

        public static ApiDepartmentEntity GetDynamicDepartmentDefaultValues(ApiDepartmentEntity departmentEntity = null)
        {
            departmentEntity = (departmentEntity == null) ? new ApiDepartmentEntity() : departmentEntity;
            switch (ApplicationConfigurationBuilder.Instance.Region)
            {
                case Regions.Canada:
                case Regions.Europe:
                case Regions.Uk:
                case Regions.Us:
                case Regions.Singapore:
                case Regions.Qa:
                case Regions.Ft:
                case Regions.e2eft:
                case Regions.Staging:
                case Regions.Training:
                    departmentEntity.DepartmentCode = (string.IsNullOrEmpty(departmentEntity.DepartmentCode)) ? "Code_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : departmentEntity.DepartmentCode;
                    departmentEntity.Description = (string.IsNullOrEmpty(departmentEntity.Description)) ? "Desc_" + StepArgumentExtension.ReplaceDynamicValues("{Auto}+10") : departmentEntity.Description;
                    departmentEntity.GLDepartmentAlias = (string.IsNullOrEmpty(departmentEntity.GLDepartmentAlias)) ? "Default" : departmentEntity.GLDepartmentAlias;
                    departmentEntity.DepartmentGroupAlias = (string.IsNullOrEmpty(departmentEntity.DepartmentGroupAlias)) ? "Business Services" : departmentEntity.DepartmentGroupAlias;
                    departmentEntity.IsDefaultCheckBoxAlias = (string.IsNullOrEmpty(departmentEntity.IsDefaultCheckBoxAlias)) ? "No" : departmentEntity.IsDefaultCheckBoxAlias;
                    departmentEntity.IsActiveCheckBoxAlias = (string.IsNullOrEmpty(departmentEntity.IsActiveCheckBoxAlias)) ? "Yes" : departmentEntity.IsActiveCheckBoxAlias;
                    break;
            }
            return departmentEntity;
        }
    }
}
