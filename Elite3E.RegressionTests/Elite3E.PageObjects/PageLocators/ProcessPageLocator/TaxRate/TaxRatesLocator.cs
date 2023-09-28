using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate
{
    public static class TaxRatesLocator
    {
        public static IWebLocator CreateNewSet => L(
        "CreateNewSet",
        By.XPath("//span[contains(text(),'Create New Set')]"));

        public static IWebLocator HomePage => L(
        "HomePage",
        By.XPath("//span[contains(text(),'Home Page')]"));

        public static IWebLocator ChildformAdd => L(
         "ChildformAdd",

        By.XPath ("//button[contains(text(),'Add')]"));

        public static IWebLocator SEARCH => L(
        "SEARCH",
        By.XPath("//span[contains(text(),' SEARCH ')]"));


        public static IWebLocator Delete => L(
        "Delete",
      By.XPath( "//span[contains(text(),' Delete ')]"));


        public static IWebLocator EffectiveDatedRatesTab => L(
       "EffectiveDatedRatesTab",
      By.XPath("//span[contains(text(),' Effective Dated Rates ')]"));


        public static IWebLocator Code => L(
        "Code",
        By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
      "Description",
       By.XPath ("//input[contains(@name,'Description')]"));

        public static IWebLocator Amount => L(
   "Amount",
    By.XPath ("//input[contains(@name,'attributes/Rate')]  [not(contains(@name,'attributes/RateClass'))]"));

        public static IWebLocator GetTitleChkBx => L(
   "GetTitleChkBx",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsTitle')]/label/div/input"));

        public static IWebLocator SetTitleChkBx => L(
"SetTitleChkBx",
       By.XPath ("//mat-checkbox[contains(@data-automation-id,'IsTitle')]/label/div"));


        public static IWebLocator GetOfficeChkBx => L(
"GetOfficeChkBx",
      By.XPath  ("//mat-checkbox[contains(@data-automation-id,'IsOffice')]/label/div/input"));

        public static IWebLocator SetOfficeChkBx => L(
"SetOfficeChkBx",
By.XPath("//mat-checkbox[contains(@data-automation-id,'IsOffice')]/label/div"));

        public static IWebLocator GetRateChkBx => L(
       " GetRateChkBx",
By.XPath ("//mat-checkbox[contains(@data-automation-id,'IsRateClass')]/label/div/input"));

        public static IWebLocator SetRateChkBx => L(
"SetRateChkBx",
By.XPath("//mat-checkbox[contains(@data-automation-id,'IsRateClass')]/label/div"));

        public static IWebLocator CurrencyDefault => L(
    "CurrencyDefault",
       By.XPath ("//input[contains(@name,'CurrencyDefault')]"));

        public static IWebLocator FutureCheckBox => L(
        "FutureCheckBox",
        By.XPath("//div[@col-id='IsFuture']//i[contains(text(), 'check_box_outline_blank')]"));

        public static IWebLocator CreateButtonForm => L(
        "CreateButtonForm",
        By.XPath("//div[contains(@class, 'buttons-bar')]//span[contains(text(),'Create')]"));

        public static IWebLocator CreateNewRateSet => L(
        "CreateNewRateSet",
        By.XPath("//span[contains(text(),'Create New Set')]"));
        

        public static IWebLocator CutOffDate => L(
        "StartDate",
        By.XPath("//input[contains(@name,'CutOffDate_ccc')]"));

        public static IWebLocator Title => L(
     "Title",
     By.XPath("//input[contains(@name,'Title')]"));


        public static IWebLocator Office => L(
         "Office",
       By.XPath ("//input[contains(@name,'Office')]"));

        public static IWebLocator EffDateCurrency => L(
        "EffDateCurrency",
        By.XPath("//input[contains(@name, 'Currency') and not(contains(@name, 'CurrencyLookBackDate')) and not(contains(@name, 'CurrencyDefault'))]"));

        public static IWebLocator EffStart => L(
        "EffStart",
        By.XPath("//input[contains(@name,'StartDate')]"));

        public static IWebLocator NewRateCurrency => L(
        "NewRateCurrency",
        By.XPath("//span[contains(text(), 'New Rate Set Details')]/../../..//span[text()='Currency']/../..//input"));

        public static IWebLocator RatesReport => L(
       "RatesReport",
        By.XPath("//span[contains(text(),'Rates Report')]"));

        public static IWebLocator NewRateAmount => L(
        "NewRateAmount",
         By.XPath("//span[contains(text(), 'New Rate Set Details')]/../../..//span[text()='Amount']/../..//input"));

        public static IWebLocator NewRateTitle => L(
            "NewRateTitle",
            By.XPath("//span[contains(text(), 'New Rate Set Details')]/../../..//span[text()='Title']/../..//input"));

        public static IWebLocator NewRateOffice => L(
            "NewRateOffice",
            By.XPath("//span[contains(text(), 'New Rate Set Details')]/../../..//span[text()='Office']/../..//input"));

        public static IWebLocator CreateButton => L(
        "CreateButton",
        By.XPath("//span[text()=' Create ']"));

        public static IWebLocator Ratereptbl => L(
        "Ratereptbl",
         By.TagName("e3e-report-data"));
        
        public static IWebLocator EffectiveRateTab => L(
        "EffectiveRateTab",
         By.XPath("//div[@id='mat-tab-label-64-0']"));

        public static IWebLocator Submit => L(
        "Submit",
        By.XPath ("//span[contains(text(),'Submit')]"));

        public static IWebLocator RateDetailsAmt => L(
        "RateDetailsAmt",
        By.XPath("//input[contains(@name,'Rate')]"));

        public static IWebLocator CloseChildForm => L(
        "CloseChildForm",
         By.XPath("//button[@class='child-form-tabs-btn child-form-close-btn ng-star-inserted active']"));

        public static IWebLocator RateDetailsGrid => L(
        "RateDetailsGrid",
         By.CssSelector("e3e-form-anchor-view div[ref='eLeftContainer'] div[role='row']"));

        public static IWebLocator RateDetailsGridAmount => L(
        "RateDetailsGridAmount",
        By.XPath("e3e-form-anchor-view div[ref= 'eCenterColsClipper'] div[role = 'row'] div[aria-colindex='3']"));

        public static IWebLocator GridColumns => L(
       "GridColumns",
        By.CssSelector("div[role='gridcell']"));

        public static IWebLocator InputElement => L(
        "InputElement",
        By.TagName("input"));


        public static IWebLocator ExportReport => L(
       "ExportReport",
        By.XPath("//span[contains(text(),'Export')]"));
        
        public static IWebLocator Inputcntfd => L(
       "Inputcntfd",
        By.ClassName("mat-form-field check-box-container"));

        public static IWebLocator checkboxbrderfd => L(
       "checkboxbrderfd",
        By.ClassName("mat-form-field check-box-container"));

        public static IWebLocator FutureDateBox => L(
       "FutureDateBox",
        By.XPath("//i[contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator FutureRecordsGrid => L(
      "Futurerecordsgrid",
       By.ClassName("//i[contains(text(),'check_box_outline_blank')]"));

        public static IWebLocator FutureRateDetailText => L(
     "Futureratedetailtext",
       By.CssSelector("form-header form-header-generation-2"));

        public static IWebLocator NewRateTab => L(
 "NewRateTab",
    By.XPath("//h5[contains(text(),'New Rate Set')]"));


        public static IWebLocator DeleteNewRate => L(
 "DeleteNewRate",
    By.XPath("//span[text()='New Rate Set ']/..//span[contains(text(),' Delete ')]"));
 }

}