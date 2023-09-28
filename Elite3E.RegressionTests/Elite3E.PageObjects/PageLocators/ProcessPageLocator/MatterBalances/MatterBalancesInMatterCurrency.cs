using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.MatterBalances
{
    public  class MatterBalancesInMatterCurrency
    {
        public static IWebLocator MatterInput => L(
         "Matter Input",
        By.XPath("//input[contains(@name,'/attributes/Matter')]"));

        public static IWebLocator RunReport => L(
     "RunReport",
    By.XPath("//button/span[contains(text(),'Run Report')]"));

        public static IWebLocator TrustAmount => L(
    "TrustAmount",
   By.XPath("//div[@class='e3e-report-container ng-star-inserted']//tbody//td[@col-index='3']/div/div"));
    }
}
