using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.FiscalInvoice
{

    public class FiscalInvoiceEntity : ITask
    {
        private FiscalInvoiceEntity() { }


        public static FiscalInvoiceEntity Select() => new();
        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            IWebElement chkActiveBox = driver.FindElement(By.XPath("/html[1]/body[1]/e3e-root[1]/e3e-navigation[1]/particle-side-menu[1]/mat-sidenav-container[1]/mat-sidenav-content[1]/div[1]/div[2]/main[1]/e3e-router-process-page[1]/e3e-process-page[1]/div[1]/as-split[1]/as-split-area[2]/div[1]/e3e-process[1]/div[1]/mat-card[1]/e3e-process-item[1]/div[2]/e3e-form-renderer[1]/e3e-form[1]/e3e-form-tabbed-view[1]/div[1]/div[1]/e3e-form-container-view[1]/div[7]/div[1]/e3e-bound-input[1]/div[1]/div[1]/e3e-boolean-input[1]/div[1]/mat-checkbox[1]/label[1]"));

            chkActiveBox.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); //Todo:

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
