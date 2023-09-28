using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;


namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.PaymentSelectionGeneration
{
    public class PaymentSelectionGenerationLocators
    {
        public static IWebLocator Description => L(
       "Description",
        By.XPath("//input[contains(@data-automation-id,'Description')]"));

        public static IWebLocator TestSelectionButton => L(
       "TestSelectionButton",
        By.XPath("//button//span[text()=' Test Selection ']"));

        public static IWebLocator TestSelectionResults => L(
      "TestSelectionResults",
       By.XPath("//textarea[contains(@data-automation-id,'TestString')]"));

        public static IWebLocator ProcesPaymentsHeader => L(
      "ProcesPaymentsHeader",
       By.XPath("//h3[text()='Process Payments']"));

        public static IWebLocator PaymentSelectionIndexDiv => L(
     "PaymentSelectionIndexDiv",
      By.XPath("//div[contains(@data-automation-id,'CkSel_BOUND')]//e3e-readonly-input//div[contains(@data-automation-id,'CkSel')]"));

        public static IWebLocator ChequePrinter => L(
     "ChequePrinter",
      By.XPath("//input[contains(@data-automation-id,'NxPrinter_BOUND')]"));

        public static IWebLocator AllocateButton => L(
      "AllocateButton",
       By.XPath("//button//span[text()=' Allocate ']"));

        public static IWebLocator ProposedPaymentChildHeader => L(
      "ProposedPaymentChildHeader",
       By.XPath("//span[contains(text(),'Proposed Payments')]"));

        public static IWebLocator PayElectronicallyCheckbox => L(
       "PayElectronicallyCheckbox",
        By.XPath("//mat-checkbox[contains(@data-automation-id,'IsElectronicPay')]"));

        public static IWebLocator ProcessPaymentsButton => L(
      "ProcessPaymentsButton",
       By.XPath("//button//span[contains(text(),'Process Payments')]"));

        public static IWebLocator ProcessPaymentsHeader => L(
     "ProcessPaymentsHeader",
      By.XPath("//mat-toolbar//h3[text()='Process Payments']"));

        public static IWebLocator ProcessPaymentsPrintButton => L(
     "ProcessPaymentsPrintButton",
      By.XPath("//button[@data-automation-id='Print']"));

        public static IWebLocator PaymentSelectionEditHeader => L(
     "PaymentSelectionEditHeader",
      By.XPath("//mat-toolbar//h3[text()='Payment Selection Edit']"));

        public static IWebLocator SearchByFieldColumnInput => L(
    "SearchByFieldColumnInput",
     By.XPath("//input[@name='advancedParameters[CkPreviewDetail].where.predicates.0.attribute']"));

        public static IWebLocator StatusInput => L(
    "StatusInput",
     By.XPath("//input[contains(@name,'CkSelStatusList')]"));

    public static IWebLocator OperatorLocatorDropdown => L(
    "OperatorLocatorDropdown",
     By.XPath("//mat-select[contains(@data-automation-id,'advancedParameters[CkPreviewDetail].where.predicates.0.operator')]"));

        public static IWebLocator SearchByEqualOperator => L(
   "SearchByEqualOperator",
    By.XPath("//mat-option//span[text()=' Equals ']"));

        public static IWebLocator SearchbyValueInput => L(
    "SearchbyValueInput",
     By.XPath("//input[@data-automation-id='advancedParameters[CkPreviewDetail].where.predicates.0.value']"));

        public static IWebLocator PaymentPreviewOption => L(
    "PaymentPreviewOption",
     By.XPath("//mat-option//span[contains(text(),'Payment Prev')]"));

        public static IWebLocator ReportDataDiv => L(
    "ReportDataDiv",
     By.XPath("//e3e-report-data[@id='reportData']"));

        public static IWebLocator ListingDropdown => L(
        "ListingDropdown",
         By.XPath("//button//span[text()=' Listing ']//following::button//mat-icon"));

        public static IWebLocator DetailListingOption => L(
    "DetailListingOption",
     By.XPath("//button//span[text()=' Detail Listing']"));

    }
}
