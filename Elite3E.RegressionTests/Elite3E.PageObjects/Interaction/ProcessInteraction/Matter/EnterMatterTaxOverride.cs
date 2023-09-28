using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Extensions;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using OpenQA.Selenium;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterMatterTaxOverride : ITask
    {
        public MatterTaxOverrideEntity MatterTaxOverrideEntity { get; }

        private EnterMatterTaxOverride(MatterTaxOverrideEntity matterTaxOverrideEntity) =>
            MatterTaxOverrideEntity = matterTaxOverrideEntity;

        public static EnterMatterTaxOverride With(MatterTaxOverrideEntity matterTaxOverrideEntity) =>
            new(matterTaxOverrideEntity);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(MatterTaxOverrideLocator.SetCode, MatterTaxOverrideEntity.Code + Keys.Tab));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(SendKeys.To(MatterTaxOverrideLocator.Description, MatterTaxOverrideEntity.Description + Keys.Tab));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            bool selected;

            IWebElement defaultCheckBoxGet;

            IWebElement defaultCheckBoxSet;

            if (!string.IsNullOrEmpty(MatterTaxOverrideEntity.Default))
            {
                defaultCheckBoxGet = driver.FindElements(MatterTaxOverrideLocator.GetCheckBox.Query)[0];
                defaultCheckBoxSet = driver.FindElements(MatterTaxOverrideLocator.SetCheckBox.Query)[0];
                selected = defaultCheckBoxGet.Selected;

                switch (selected)
                {
                    case false when MatterTaxOverrideEntity.Default.ToBoolean():
                    case true when !MatterTaxOverrideEntity.Default.ToBoolean():
                        defaultCheckBoxSet.Click();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(MatterTaxOverrideEntity.Active))
            {
                
                defaultCheckBoxGet = driver.FindElements(MatterTaxOverrideLocator.GetCheckBox.Query)[1]; 
                defaultCheckBoxSet = driver.FindElements(MatterTaxOverrideLocator.SetCheckBox.Query)[1];
                selected = defaultCheckBoxGet.Selected;

                switch (selected)
                {
                    case false when MatterTaxOverrideEntity.Active.ToBoolean():
                    case true when !MatterTaxOverrideEntity.Active.ToBoolean():
                        defaultCheckBoxSet.Click();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(MatterTaxOverrideEntity.StartDate))
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.StartDate, MatterTaxOverrideEntity.StartDate));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            if (!string.IsNullOrEmpty(MatterTaxOverrideEntity.EndDate))
                actor.AttemptsTo(DateControl.SelectDate(GlobalConstants.EndDate, MatterTaxOverrideEntity.EndDate));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
