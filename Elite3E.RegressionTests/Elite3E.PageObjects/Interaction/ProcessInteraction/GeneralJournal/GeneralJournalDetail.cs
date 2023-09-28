using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Entity.GenearlJournal;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.GeneralJournal
{
    public class GeneralJournalDetail : ITask
    {
        public GeneralJournalDetailEntity GlDetails { get; set; }
        GeneralJournalDetail(GeneralJournalDetailEntity glDetails)
        {
            GlDetails = glDetails;
        }

        public static GeneralJournalDetail AddChildForm(GeneralJournalDetailEntity glDetails) => new(glDetails);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.GeneralJournalDetailChildForm));
            actor.AttemptsTo(ChildProcessMenu.ClickOn(GlobalConstants.GeneralJournalDetailChildForm, ChildProcessMenuAction.Add));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(ChildProcessView.SwitchToView(GlobalConstants.GeneralJournalDetailChildForm, "Simple Entry Form"));

            if (!string.IsNullOrEmpty(GlDetails.GLAccount))
            {
                actor.AttemptsTo(Click.On(GeneralJournalLocators.GlAccountSearch));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(SendKeys.To(CommonLocator.SearchByInput, GlDetails.GLAccount));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SearchTitleButton)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                actor.AttemptsTo(Click.On(CommonLocator.Record(GlDetails.GLAccount)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(CommonLocator.ButtonElementById(LocatorConstants.SelectTitleButton)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            }
            if (!string.IsNullOrEmpty(GlDetails.OriginalDR))
                actor.AttemptsTo(SendKeys.To(GeneralJournalLocators.OriginalDR, GlDetails.OriginalDR));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(GlDetails.OriginalCR))
                actor.AttemptsTo(SendKeys.To(GeneralJournalLocators.OriginalCR, GlDetails.OriginalCR));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(GlDetails.GJCategory))
                actor.AttemptsTo(Lookup.SearchAndSelectSingle("General Journal Category", GlDetails.GJCategory));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }

    }
}
