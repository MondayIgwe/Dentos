using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.RateTypeGlobalChange
{
    public class RateTypeGlobalChangeLocators
    {
        public static IWebLocator RateType => L(
       "RateType",
      By.XPath("//input[contains(@name, 'RateType')]"));
        public static IWebLocator BasedOnDate => L(
       "RateType",
      By.XPath("//input[contains(@name, 'BasedOnDate')]"));

        public static IWebLocator EffecctiveDate => L(
       "EffecctiveDate",
      By.XPath("//input[contains(@name, 'EffDate')]"));

        public static IWebLocator ReasonType => L(
       "ReasonType",
      By.XPath("//input[contains(@name, 'ReasonType')]"));

        public static IWebLocator DefaultRate => L(
      "DefaultRate",
     By.XPath("//input[contains(@name, 'DefaultRate')]"));

        public static IWebLocator DefaultCurrency => L(
     "DefaultCurrency",
     By.XPath("//input[contains(@name, 'DefaultCurrency')]"));

        public static IWebLocator RoundingMethod => L(
    "RoundingMethod",
    By.XPath("//input[contains(@name, 'RoundingMethod')]"));

        public static IWebLocator ChangeAmount => L(
    "Change Amount",
    By.XPath("//input[contains(@name, 'ChangeAmt')]"));
    }
}
