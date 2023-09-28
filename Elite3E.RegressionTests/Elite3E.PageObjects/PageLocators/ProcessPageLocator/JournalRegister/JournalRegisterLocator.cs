using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.JournalRegister
{
    public class JournalRegisterLocator
    {
        public static IWebLocator MaskedAliasValues => L(
           "MaskedAliasValues",
           By.XPath("//td[@col-index=//div[contains(text(),'MaskedAlias')]//ancestor::th/@col-index]/div/div"));
    }
}
