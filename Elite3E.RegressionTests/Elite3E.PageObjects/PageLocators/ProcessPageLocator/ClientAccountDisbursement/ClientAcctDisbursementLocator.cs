using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ClientAccountDisbursement
{
    public class ClientAcctDisbursementLocator
    {
        public static IWebLocator DisbursementType => L(
     "DisbursementType",
      By.XPath("//input[contains(@data-automation-id, 'TrustDisbursementType')]"));

        public static IWebLocator ClientAccountAcct => L(
    "BankAcctTrust",
     By.XPath("//input[contains(@data-automation-id, 'BankAcctTrust')]"));

        public static IWebLocator FinalPayment => L(
     "IsFinalPayment_ccc",
     By.XPath("//mat-checkbox[contains(@data-automation-id, 'IsFinalPayment_ccc')]"));

        public static IWebLocator IsRefund => L(
"IsRefund_ccc",
By.XPath("//mat-checkbox[contains(@data-automation-id, 'IsRefund_ccc')]"));

        public static IWebLocator VerificationMethod => L(
     "VerificationMethod_ccc",
     By.XPath("//input[contains(@data-automation-id, 'VerificationMethod_ccc')]"));

        public static IWebLocator Matter => L(
     "Matter",
      By.XPath("//input[contains(@data-automation-id, 'Matter')]"));

        public static IWebLocator Amount => L(
    "Amount",
     By.XPath("//input[contains(@data-automation-id, 'Amount')]"));

        public static IWebLocator TrustIntendedUse => L(
    "TrustIntendedUse",
     By.XPath("//input[contains(@data-automation-id, 'TrustIntendedUse')]"));

        public static IWebLocator IsPaymentInfoVerified => L(
    "IsPaymentInfoVerified",
     By.XPath("//mat-checkbox[contains(@data-automation-id, 'IsPaymentInfoVerified_ccc')]"));

        public static IWebLocator IsClientApprovalObtained => L(
     "IsClientApprovalObtained",
      By.XPath("//mat-checkbox[contains(@data-automation-id, 'IsClientApprovalObtained_ccc')]"));

        public static IWebLocator PaymentName => L(
    "IsClientApprovalObtained",
     By.XPath("//input[contains(@name, 'PayeeName')]"));

        public static IWebLocator OpenButton => L(
       "OpenButton",
       By.XPath("//div[@class='filter-container action-active']//following::e3e-workflow-inbox-grid-actions//button//span[text()=' Open ']"));

        public static IWebLocator IsApproved => L(
    "IsApproved",
     By.XPath("//mat-checkbox[contains(@data-automation-id, 'IsApproved')]"));


    }
}
