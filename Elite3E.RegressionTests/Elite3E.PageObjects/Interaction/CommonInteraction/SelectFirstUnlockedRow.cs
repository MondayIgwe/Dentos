using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class SelectFirstUnlockedRow :ITask
    {
        private SelectFirstUnlockedRow()
        { }

        public static SelectFirstUnlockedRow Select() => new();
        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            var resultList = driver.FindElements(By.XPath("//input[@ref='eInput']//ancestor::div[@role='row' and @aria-label='Press SPACE to select this row.']"));
            foreach (var row in resultList)
            {
                try
                {
                    
                    if (row.FindElement(By.XPath(".//e3e-locked-column/mat-icon")).Displayed)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    }
                }
                catch
                {
                    row.Click();
                    break;
                }
                
            }


        }
    }
}
