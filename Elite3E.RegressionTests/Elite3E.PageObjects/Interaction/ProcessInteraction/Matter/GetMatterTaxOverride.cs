using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class GetMatterTaxOverride : IQuestion<MatterTaxOverrideEntity>
    {

        private GetMatterTaxOverride()
        {
        }

        public static GetMatterTaxOverride Data() => new();

        public MatterTaxOverrideEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            var matterTaxOverrideEntity = new MatterTaxOverrideEntity
            {
                Code = actor.AsksFor(Text.Of(MatterTaxOverrideLocator.GetCode)).Trim(),
                Description = actor.AsksFor(ValueAttribute.Of(MatterTaxOverrideLocator.Description)),
                Default = driver.FindElements(MatterTaxOverrideLocator.GetCheckBox.Query)[0].Selected ? "Yes" : "No",
                Active = driver.FindElements(MatterTaxOverrideLocator.GetCheckBox.Query)[1].Selected ? "Yes" : "No",
                StartDate = Convert.ToDateTime(actor.AsksFor(ValueAttribute.Of(CommonLocator.FindInputElementUsingText(LocatorConstants.Start_Date)))).ToShortDateString(),
                EndDate = Convert.ToDateTime(actor.AsksFor(ValueAttribute.Of(MatterTaxOverrideLocator.EndDate))).ToShortDateString()
            };
            return matterTaxOverrideEntity;
        }
    }
    
}
