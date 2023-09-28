using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Extensions;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Purge;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Purge
{
    public class EnterPurgeDetail : ITask
    {
        public PurgeDetailEntity PurgeDetailEntity { get; }

        private EnterPurgeDetail(PurgeDetailEntity purgeDetailEntity) =>
            PurgeDetailEntity = purgeDetailEntity;

        public static EnterPurgeDetail With(PurgeDetailEntity purgeDetailEntity) => new(purgeDetailEntity);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, PurgeDetailEntity.StartDate));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(SendKeys.To(PurgeDetailLocator.PurgeType, PurgeDetailEntity.PurgeType));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.WaitsUntil(Appearance.Of(PurgeDetailLocator.CalculateButton), IsEqualTo.True(), 8);

            actor.AttemptsTo(SendKeys.To(PurgeDetailLocator.EndDate, PurgeDetailEntity.EndDate));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            IWebElement defaultCheckboxGet;
            IWebElement defaultCheckboxSet;
            bool selected;

            if (!string.IsNullOrEmpty(PurgeDetailEntity.PurgeDisbursement))
            {

                defaultCheckboxGet = driver.FindElement(PurgeDetailLocator.GetPurgeDisbursement.Query);

                defaultCheckboxSet = driver.FindElement(PurgeDetailLocator.SetPurgeDisbursement.Query);
                selected = defaultCheckboxGet.Selected;

                switch (selected)
                {
                    case false when PurgeDetailEntity.PurgeDisbursement.ToBoolean():
                    case true when !PurgeDetailEntity.PurgeDisbursement.ToBoolean():
                        defaultCheckboxSet.Click();
                        break;
                }
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (!string.IsNullOrEmpty(PurgeDetailEntity.ExcludeAnticipated))
            {
                 defaultCheckboxGet = driver.FindElement(PurgeDetailLocator.GetExcludeAnticipated.Query);
                 defaultCheckboxSet = driver.FindElement(PurgeDetailLocator.SetExcludeAnticipated.Query);

                 selected = defaultCheckboxGet.Selected;

                switch (selected)
                {
                    case false when PurgeDetailEntity.ExcludeAnticipated.ToBoolean():
                    case true when !PurgeDetailEntity.ExcludeAnticipated.ToBoolean():
                        defaultCheckboxSet.Click();
                        break;
                }
            }
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
