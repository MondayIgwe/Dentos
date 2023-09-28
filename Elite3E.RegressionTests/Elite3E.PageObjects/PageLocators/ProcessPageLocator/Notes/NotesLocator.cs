using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter_Notes
{
    public  class NotesLocator
    {
        public static IWebLocator Note => L(
        "Note",
        By.XPath("//textarea[contains(@data-automation-id,'/Note')]"));

        public static IWebLocator MatterNoteType => L(
      "MatterNoteType",
      By.XPath("//input[contains(@data-automation-id,'/MattNoteType')]"));

        public static IWebLocator ClientNoteType => L(
       "ClientNoteType",
       By.XPath("//input[contains(@data-automation-id,'/CliNoteType')]"));

        public static IWebLocator RunMetricButton => L(
       "RunMetricButton",
       By.XPath("//button[contains(@data-automation-id,'RunMetric')]"));

        public static IWebLocator ClientNote => L(
        "ClientNote",
        By.XPath("//div/textarea[contains(@data-automation-id,'/attributes/Note')]"));
     
        public static IWebLocator NextActionOwner => L(
      "NextActionOwner",
      By.XPath("//input[contains(@data-automation-id,'NextActionOwner')]"));

        public static IWebLocator ProformaCheckbox => L(
      "ProformaCheckbox",
      By.XPath("//mat-checkbox[contains(@data-automation-id,'IsProforma_ccc')]/label/div/input"));
    }
}
