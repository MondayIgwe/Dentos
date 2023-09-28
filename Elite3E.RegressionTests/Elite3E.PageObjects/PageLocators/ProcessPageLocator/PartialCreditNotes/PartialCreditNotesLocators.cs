using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.PartialCreditNotes
{
    public class PartialCreditNotesLocators
    {
        public static IWebLocator ReceiptTypeInput => L(
            "ReceiptTypeInput",
        By.XPath("//input[contains(@data-automation-id,'ReceiptType')]"));

        public static IWebLocator ProformaStatusInput => L(
         "ProformaStatusInput",
     By.XPath("//input[contains(@data-automation-id,'ProfStatus')]"));

        public static IWebLocator CreditNoteAdjustmentTypeInput => L(
         "CreditNoteInput",
     By.XPath("//input[contains(@data-automation-id,'CreditNote') and contains(@data-automation-id,'AdjType')]"));

        public static IWebLocator SystemDefaultDiv(string optionName) => L(
         "SystemDefaultDiv",
     By.XPath("//span[text()=' "+ optionName + " ']//following::e3e-server-page-label-control[@title='OVERRIDE THIS']"));

        //(//span[text()=' Partial_Cr_Note_Prof_ccc ']//following::div[@aria-colindex])[count(//span[text()='Unit Override']//ancestor::div[@role='columnheader']/preceding-sibling::div[@role='columnheader'])]//span
        public static IWebLocator ValueBasedOnColNOption(string optionName, string columnName) => L(
            "ValueBasedOnColNOption",
            By.XPath(" (//span[normalize-space()='" + optionName + "']//following::div[@aria-colindex])[count(//span[normalize-space()='" + columnName + "']//ancestor::div[@role='columnheader']/preceding-sibling::div[@role='columnheader'])]//span"));
       
    }
}
