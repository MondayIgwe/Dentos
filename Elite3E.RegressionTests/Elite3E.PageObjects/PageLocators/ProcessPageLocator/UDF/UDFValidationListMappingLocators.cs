using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.UDF
{
    public class UDFValidationListMappingLocators
    {
        public static IWebLocator parentList => L("parentList", By.XPath("//input[contains(@data-automation-id,'ParentList')]"));
        public static IWebLocator childList => L("childList", By.XPath("//input[contains(@data-automation-id,'ChildList')]"));
        public static IWebLocator parentValue => L("parentValue", By.XPath("//input[contains(@name,'UDFValidationMappingItem_ccc') and contains(@data-automation-id,'ParentItem')]"));
        public static IWebLocator childValue => L("childValue", By.XPath("//input[contains(@name,'UDFValidationMappingItem_ccc') and contains(@data-automation-id,'ChildItem')]"));
        public static IWebLocator isActive => L("IsActive", By.XPath("//mat-checkbox[contains(@data-automation-id,'IsActive')]"));
        public static IWebLocator isDefault => L("IsDefault", By.XPath("//mat-checkbox[contains(@data-automation-id,'Is_Default')]"));
        public static IWebLocator sortString => L("Sortstring", By.XPath("//input[contains(@data-automation-id,'SortString')]"));
        public static IWebLocator startDate => L("startDate", By.XPath("//input[contains(@data-automation-id,'StartDate')]"));
        public static IWebLocator endDate => L("endDate", By.XPath("//input[contains(@data-automation-id,'EndDate')]"));




    }
}
