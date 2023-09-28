using OpenQA.Selenium;
using Boa.Constrictor.WebDriver;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Reciepts
{
    public static class GLRecieptsLocators
    {
        public static IWebLocator SearchButton => L(
               "SearchButton",
         By.ClassName("mat-icon notranslate ng-tns-c69-2614 material-icons mat-icon-no-color"));

        public static IWebLocator SetRecieptbox => L(
               "GetRecieptbox",
             By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReceiptDefault_ccc')]/label/div"));

        public static IWebLocator GetRecieptbox => L(
         "SetRecieptbox",
         By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReceiptDefault_ccc')]/label/div/input"));


        public static IWebLocator Chkboxenabled => L(
       "Chkboxenabled",
        By.Id("mat-checkbox-2591-input"));

        public static IWebLocator SearchBtn => L(
           "SearchBtn",
       By.XPath("//button[contains(@class,'toggler mat-elevation-z4 mat-mini-fab mat-primary')]"));

        public static IWebLocator GLForm => L(
                "GLForm",
                By.XPath("//span[contains(text(),' General Ledger ')]"));
 
        public static IWebLocator Gltypeinput => L(
               "Gltypeinput",
       By.XPath("//input[contains(@data-automation-id,'GLType')]"));

        public static IWebLocator advancedsearchresult => L(
              "advancedsearchresult",
       By.XPath("//button[@id='select-all-title-button']"));

        public static IWebLocator advancedsearchselect => L(
              "advancedsearchselect",
       By.XPath("//button[@id='select-title-button']"));
    }
}