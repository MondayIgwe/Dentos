
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using System;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterMatterCreditDetails : ITask
    {

        public MatterEntity MatterEntity { get; }
        public string ChildForm { get; }

        private EnterMatterCreditDetails(MatterEntity matterEntity, string childForm)
        {
            MatterEntity = matterEntity;
            ChildForm = childForm;
        }

        public static EnterMatterCreditDetails With(MatterEntity matter, string childForm) => new(matter, childForm);

        public void PerformAs(IActor actor)
        {
            switch (ChildForm)
            {
                case "Client Relationship Credit":

                    actor.AttemptsTo(Click.On(MatterLocator.ClientRelationshipCreditForm));
                    actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(GlobalConstants.ClientRelationshipCredit, LocatorConstants.AddButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actor.AttemptsTo(SendKeys.To(MatterLocator.StartDate, MatterEntity.StartDate));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    break;
                case "Project Management Credit":

                    actor.AttemptsTo(Click.On(MatterLocator.ProjectManagementCreditForm));
                    actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(GlobalConstants.ProjectManagementCredit, LocatorConstants.AddButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actor.AttemptsTo(SendKeys.To(MatterLocator.StartDate, MatterEntity.StartDate));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    break;
                case "Relationship Enhancement Credit":

                    actor.AttemptsTo(Click.On(MatterLocator.RelationshipEnhancementCredit));
                    actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(GlobalConstants.RelationshipEnhancementCredit, LocatorConstants.AddButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actor.AttemptsTo(SendKeys.To(MatterLocator.StartDate, MatterEntity.StartDate));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    break;
                default:
                    Console.WriteLine("No Child Form Found");
                    break;
            }




        }
    }
}
