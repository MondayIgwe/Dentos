using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountCheque
{
    public class ClientAccountChequeLocator
    {
        public static IWebLocator ChequeNumber => L(
        "ChequeNumber",
        By.XPath("//input[contains(@name,'CkNum')]"));

        public static IWebLocator NameCheque => L(
              "NameCheque",
         By.XPath("//input[contains(@name,'PayeeName')]"));

        public static IWebLocator IsVoidedChequebox => L(
            "IsVoidedChequebox",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'IsVoided')]"));

        public static IWebLocator DisbursementNumberDiv => L(
          "DisbursementNumberDiv",
     By.XPath("//e3e-readonly-input//div[contains(@data-automation-id,'TrustDisbursement') and contains(@name,'TrustCheckDet')]"));

        public static IWebLocator ReverseDisbursementCheckbox => L(
        "ReverseDisbursementCheckbox",
   By.XPath("//mat-checkbox[contains(@data-automation-id,'IsReverseDisbursement')]//input[@aria-checked='true']"));


        public static IWebLocator SelectNewDisbursementDropDown => L(
        "SelectNewDisbursementDropDown",
        By.XPath("//button[contains(@data-automation-id,'Select')]//mat-icon[text()='arrow_drop_down']"));

        public static IWebLocator SelectNewDisbursementDropDownOption => L(
       "SelectNewDisbursementDropDownOption",
       By.XPath("//button//span[text()=' New']"));



    }
}
