using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Bank
{
    public class CreateBankStatementLocator
    {
        public static IWebLocator BankGroup => L(
           "Bank Group",
           By.XPath("//input[contains(@name,'BankGroup')]"));


        public static IWebLocator StatementNumber => L(
           "Bank Group",
           By.XPath("//input[contains(@name,'StmtNum')]"));

        public static IWebLocator BeginningBalance => L(
            "BeginningBalance",
            By.XPath("//input[contains(@name,'/BeginningBal')]"));

        public static IWebLocator EndingBalance => L(
        "EndingBalance",
        By.XPath("//input[contains(@name,'/EndingBal')]"));

        public static IWebLocator Deposit => L(
    "Deposit",
    By.XPath("//input[contains(@name,'/Deposits')]"));

        public static IWebLocator Withdrawals => L(
        "Withdrawals",
        By.XPath("//input[contains(@name,'/Withdrawals')]"));

        public static IWebLocator Description => L(
        "Description",
        By.XPath("//div/textarea[contains(@data-automation-id,'/Description')]"));

        public static IWebLocator Reference => L(
        "Reference",
        By.XPath("//input[contains(@data-automation-id,'/RefNum')]"));

        public static IWebLocator Amount => L(
        "Amount",
        By.XPath("//input[contains(@data-automation-id,'/Amount')]"));

        public static IWebLocator ClearDate => L(
        "ClearDate",
        By.XPath("//input[contains(@name,'ClearDate')]"));

        public static IWebLocator BankStmtDescription => L(
        "BankStmtDescription",
        By.XPath("//input[contains(@data-automation-id,'/Description')]"));

        // Using index here as no relationship between this button and ending balance locator
        public static IWebLocator CalculateEndingBalance => L(
        "CalculateEndingBalance",
        By.XPath("//div[15]//span[contains(text(),'Calculate')]"));

        public static IWebLocator BankTransactionsNextButton => L("BankTransactionsNextButton",
        By.XPath("//mat-icon[contains(@class,'next-button')][text()='play_arrow']"));

        public static IWebLocator TrnAmount(string amount) => L(
        "Transaction Amount",
        By.XPath("//div[@col-id='Amount']//span[text()='" + amount + "']"));
    }
}
