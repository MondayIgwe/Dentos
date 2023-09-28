using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.BOAMetric
{
   public  class BOAMetricLocators
    {   
        public static IWebLocator CurrencyInput => L(
            "CurrencyInput",
            By.XPath("//input[contains(@name,'/CurrencyCode')]")); 

        public static IWebLocator RunMetricButton => L(
            "RunMetricButton",
            By.XPath("//span[contains(text(),' Run Metric ')]//parent::button"));
    }
}
