using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.DirectCheque
{

    public class DirectChequeLocators
    {
        public static IWebLocator GetClientRefundCheckbox => L(
                "GetClientRefundCheckbox",
        By.XPath("//span[text()='Client Refund']/../..//input[@type='checkbox']"));

        public static IWebLocator SetClientRefundCheckbox => L(
                "SetClientRefundCheckbox",
        By.XPath("//span[text()='Client Refund']/../..//input[@type='checkbox']/.."));

        public static IWebLocator Client => L(
                "Client",
        By.XPath("//input[contains(@name, 'Client')]"));

        public static IWebLocator ReceiptType => L(
                "ReceiptType",
        By.XPath("//input[contains(@name, 'ReceiptType')]"));

        public static IWebLocator DocumentNumber => L(
                "DocumentNumber",
        By.XPath("//input[contains(@name, 'DocumentNum')]"));

        public static IWebLocator ChildForm(string childForm) => L(
                "ChildForm",//h5[text()='Client Refund']
        By.XPath("//h5[text()='" + childForm + "']"));

        public static IWebLocator VerifyAmount(string amount) => L(
                "VerifyAmount",
        By.XPath("//span[text()='(" + amount + ")']"));

        public static IWebLocator BankAccount => L(
                "BankAccount",
        By.XPath("//input[contains(@name, 'BankAcctAP')]"));

        public static IWebLocator WriteChequeNumber => L(
                "ChequeNumber",
        By.XPath("//input[contains(@name, 'CkNum')]"));

        public static IWebLocator ReadChequeNumber => L(
            "ChequeNumber",
            By.XPath("//div[contains(@name, 'CkNum')]"));

        public static IWebLocator PayerClientRefundDiv => L(
        "PayerClientRefundDiv",
        By.XPath("//div[@role='row' and @row-index='0']//div[@col-id='Payor']//span//div//span"));

        public static IWebLocator HorizontalScrollBar => L(
        "HorizontalScrollBar",
        By.XPath("//span[contains(text(),'Client Refund')]//following::div[@class='ag-body-horizontal-scroll-viewport']"));

        public static IWebLocator ChequeDate => L(
                "ChequeDate",
        By.XPath("//input[contains(@name, 'CkDate')]"));

        public static IWebLocator Amount => L(
                "Amount",
        By.XPath("//input[contains(@name, 'Amount') and contains(@name,'CkDirectRefund')=false]"));

        public static IWebLocator TransactionType => L(
                "TransactionType",
        By.XPath("//input[contains(@name, 'APTranType')]"));

        public static IWebLocator OfficeDropDown => L(
                "OfficeDropDown",
        By.XPath("//span[text()='Office']/../..//button"));

        public static IWebLocator ChequeTemplate => L(
                "ChequeTemplate",
        By.XPath("//input[contains(@name, 'NxPrinterTemplate')]"));

        public static IWebLocator ChequePrinter => L(
                "ChequePrinter",
        By.XPath("//input[contains(@name, 'NxPrinter') and not(contains(@name, 'NxPrinterTemplate'))]"));

        public static IWebLocator Payee => L(
                "Payee",
        By.XPath("//input[contains(@name, 'Payee') and not(contains(@name, 'PayeeSite'))]"));

        public static IWebLocator ChildFormAmount => L(
                "ChildFormAmount",
        By.XPath("//input[contains(@name, 'Amount')][contains(@name, 'childObjects')]"));

        public static IWebLocator ClientRefundAmount => L(
               "ChildFormAmount",
       By.XPath("//input[contains(@data-automation-id,'CkDirectRefund')]"));

        public static IWebLocator SetAmounts => L(
          "SetAmounts",
         By.XPath("//button//span[text()=' Set Amounts ']"));

        public static IWebLocator Office => L(
              "office",
        By.XPath("//input[contains(@data-automation-id,'Office')]"));

        public static IWebLocator AddChildForm(string childForm) => L(
                "AddChildForm",
        By.XPath("//span[text()=' " + childForm + " ']"));

        public static IWebLocator ChildFormElipsis => L(
                "ChildFormElipsis",
        By.XPath("//button[@class='child-form-tabs-btn options-menu']"));

        public static IWebLocator ChildFormOptionCheckbox(string option) => L(
                "ChildFormOptionCheckbox",//div[@role='menu']//span[contains(text(),'Client Refund')]//parent::label//input
        By.XPath("//div[@role='menu']//span[contains(text(),'" + option + "')]//parent::label//input"));

        public static IWebLocator ChildFormAmountDiv => L(
               "ChildFormAmountDiv",
       By.XPath("//span[text()='Client Refund ']//ancestor::e3e-form-anchor-view//e3e-grid-view//div[contains(@col-id,'Amount')][@role='gridcell']"));

        public static IWebLocator ChildForm1099 => L(
        "ChildForm1099",
        By.XPath("//mat-card[contains(text(),'1099')]"));

        public static IWebLocator ClientRefund => L(
        "ClientRefund",
        By.XPath("//mat-card[contains(text(),'Client Refund')]"));
    }
}
