using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.PhaseList
{
    public class PhaseListLocators
    {
        public static IWebLocator PhaseListCode => L(
          "PhaseListCode",
          By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator FirmPhaseListCode => L(
        "FirmPhaseListCode",
        By.XPath("//input[contains(@name,'Code')]"));

        public static IWebLocator Description => L(
         "Description",
         By.XPath("//input[contains(@name,'Description')]"));

        public static IWebLocator PhaseCode => L(
        "PhaseCode",
        By.XPath("//div[@role='row']//input[contains(@name,'Code')]"));

        public static IWebLocator PhaseDescription => L(
       "PhaseDescription",
       By.XPath("//div[@role='row']//input[contains(@name,'Description')]"));

        public static IWebLocator FirmPhaseDiv(int index) => L(
      "FirmPhase Div",
      By.XPath("//div[@aria-rowindex='" + index + "']//div[@col-id='FirmPhase']"));

        public static IWebLocator FirmPhaseInput => L(
       "FirmPhaseInput ",
       By.XPath("//input[contains(@data-automation-id,'FirmPhase')]"));

        public static IWebLocator FirmPhaseCode(string code) => L(
       "FirmPhaseCode ",
       By.XPath("//span[text()='"+code+"']"));
    }

}
