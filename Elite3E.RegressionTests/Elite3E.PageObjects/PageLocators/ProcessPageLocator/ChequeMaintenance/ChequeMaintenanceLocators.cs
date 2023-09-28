using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.ChequeMaintenance
{
    public class ChequeMaintenanceLocators
    {
        public static IWebLocator VoidChequeBox => L(
        "VoidChequeBox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsVoided')]"));

        public static IWebLocator VoidDate => L(
       "VoidDate",
       By.XPath("//mat-checkbox[contains(@data-automation-id,'VoidDate')]"));

        public static IWebLocator VoidReason => L(
       "VoidReason",
       By.XPath("//input[contains(@data-automation-id,'VoidReason')]"));

        public static IWebLocator AuditButton => L(
      "AuditButton",
      By.XPath("//span[text()=' Audit ']"));

    }
}
