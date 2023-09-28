using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Vendor;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.VendorPayeeMaintenance
{
    public class EnterEntityInformation : ITask
    {
        public VendorEntity VendorEntity { get; }


        private EnterEntityInformation(VendorEntity vendorEntity)
        {
            VendorEntity = vendorEntity;
        }

        public static EnterEntityInformation EnterEntityData(VendorEntity vendorEntity) => new(vendorEntity);

        public void PerformAs(IActor actor)
        {
            switch (VendorEntity.EntityType)
            {
                case "EntityOrganisation":
                    actor.AttemptsTo(Click.On(VendorLocators.EntityEditDropDown));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Click.On(VendorLocators.NewOrganisation));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actor.AttemptsTo(SendKeys.To(VendorLocators.OrganisationName, VendorEntity.OrganisationName));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actor.AttemptsTo(Click.On(VendorLocators.CommentsLocator));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    //Check for Organisation Duplicates
                    actor.AttemptsTo(JScript.ClickOn(VendorLocators.CheckOrganisation));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    var message = actor.AsksFor(Text.Of(CommonLocator.InformationMessage));
                    message.Contains(VendorEntity.Message).Equals(true);
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    //Navigate to the Relationship child form and add sites and fill data
                    actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, GlobalConstants.Relationships));
                    actor.AttemptsTo(JScript.ClickOn(VendorLocators.SiteAdd));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                   
                    actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.SiteType, VendorEntity.SiteType));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(SendKeys.To(VendorLocators.AddressStreet, VendorEntity.Street));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                   
                    actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.AddressCountry, VendorEntity.Country));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Dropdown.SelectOptionByName(VendorLocators.Language, VendorEntity.Language));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(ProcessRibbonMenu.ClickOn(RibbonAction.Submit));
                   
                    break;
            }
        }
    }
}
