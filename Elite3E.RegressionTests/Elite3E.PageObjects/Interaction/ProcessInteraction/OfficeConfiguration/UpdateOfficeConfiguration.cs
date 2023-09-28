using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.OfficeConfiguration;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.TaxRate;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.OfficeConfiguration
{
    public  class UpdateOfficeConfiguration :ITask
    {
        public OfficeConfigurationEntity OfficeEntity { get; }

        private UpdateOfficeConfiguration(OfficeConfigurationEntity officeEntity) =>
            OfficeEntity = officeEntity;

        public static UpdateOfficeConfiguration With(OfficeConfigurationEntity officeEntity) => new(officeEntity);
        public void PerformAs(IActor actor)
        {
            if(!(string.IsNullOrEmpty(OfficeEntity.GovtSysTemplate)))
            {
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Government System Upload Template", OfficeEntity.GovtSysTemplate));
            }
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Invoice Text"));
            if (actor.DoesElementExist(OfficeConfigurationLocator.MatterAttribute))
            {
                actor.AttemptsTo(Dropdown.SelectOptionByName(OfficeConfigurationLocator.InvoiceTextLanguage, OfficeEntity.Language));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(SendKeys.To(OfficeConfigurationLocator.MatterAttribute, OfficeEntity.MatterAttribute + Keys.Tab));
                if(actor.DoesElementExist(OfficeConfigurationLocator.CoverLetterNarrative))
                {
                    actor.GetDriver().FindElement(OfficeConfigurationLocator.CoverLetterNarrative.Query).SendKeys(OfficeEntity.CoverLetterNarrative);
                }
                else
                {
                    actor.GetDriver().FindElement(OfficeConfigurationLocator.EnteredCoverLetterNarrative.Query).Clear();
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.GetDriver().FindElement(OfficeConfigurationLocator.CoverLetterNarrative.Query).SendKeys(OfficeEntity.CoverLetterNarrative);
                }
                
                if(actor.DoesElementExist(OfficeConfigurationLocator.InvoiceNarrative))
                {
                    actor.GetDriver().FindElement(OfficeConfigurationLocator.InvoiceNarrative.Query).SendKeys(OfficeEntity.InvoiceNarrative);
                }
                else
                {
                    actor.GetDriver().FindElement(OfficeConfigurationLocator.EnteredInvoiceNarrative.Query).Clear();
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.GetDriver().FindElement(OfficeConfigurationLocator.InvoiceNarrative.Query).SendKeys(OfficeEntity.InvoiceNarrative);
                }

            }
            else
            {
                actor.AttemptsTo(ChildProcessMenu.ClickOn("Invoice Text", ChildProcessMenuAction.Add));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Dropdown.SelectOptionByName(OfficeConfigurationLocator.InvoiceTextLanguage, OfficeEntity.Language));
                actor.AttemptsTo(SendKeys.To(OfficeConfigurationLocator.MatterAttribute, OfficeEntity.MatterAttribute + Keys.Tab));
                actor.GetDriver().FindElement(OfficeConfigurationLocator.CoverLetterNarrative.Query).SendKeys(OfficeEntity.CoverLetterNarrative);
                actor.GetDriver().FindElement(OfficeConfigurationLocator.InvoiceNarrative.Query).SendKeys(OfficeEntity.InvoiceNarrative);
            }
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, "Legal Name"));

            if (actor.DoesElementExist(OfficeConfigurationLocator.LegalName))
            {
                actor.AttemptsTo(Dropdown.SelectOptionByName(OfficeConfigurationLocator.LegalNameLanguage, OfficeEntity.Language));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.GetDriver().FindElement(OfficeConfigurationLocator.LegalName.Query).Clear();
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.GetDriver().FindElement(OfficeConfigurationLocator.LegalName.Query).SendKeys(OfficeEntity.LegalName);
            }
            else
            {
                actor.AttemptsTo(ChildProcessMenu.ClickOn("Legal Name", ChildProcessMenuAction.Add));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Dropdown.SelectOptionByName(OfficeConfigurationLocator.LegalNameLanguage, OfficeEntity.Language));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.GetDriver().FindElement(OfficeConfigurationLocator.LegalName.Query).SendKeys(OfficeEntity.LegalName);
            }

            actor.AttemptsTo(Click.On(TaxRatesLocator.Submit));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
