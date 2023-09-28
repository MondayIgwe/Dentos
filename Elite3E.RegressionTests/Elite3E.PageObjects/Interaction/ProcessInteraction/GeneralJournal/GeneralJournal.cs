using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity.GenearlJournal;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.GeneralJournal
{
    public class GeneralJournal : ITask
    {
        public GeneralJournalEntity GlEntity { get; set; }
        GeneralJournal(GeneralJournalEntity glEntity)
        {
            GlEntity = glEntity;
        }

        public static GeneralJournal With(GeneralJournalEntity glEntity) => new(glEntity);


        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!string.IsNullOrEmpty(GlEntity.Journal))
            {
                actor.AttemptsTo(SendKeys.To(GeneralJournalLocators.Journal, GlEntity.Journal));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            if (!string.IsNullOrEmpty(GlEntity.Currency))
                actor.AttemptsTo(Dropdown.SelectOptionByName(GeneralJournalLocators.Currency, GlEntity.Currency));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(GlEntity.GLBook))
                actor.AttemptsTo(Dropdown.SelectOptionByName(GeneralJournalLocators.GLBook, GlEntity.GLBook));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(GlEntity.GLType))
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("GL Type", GlEntity.GLType));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(GlEntity.Category))
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("Category", GlEntity.Category));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
