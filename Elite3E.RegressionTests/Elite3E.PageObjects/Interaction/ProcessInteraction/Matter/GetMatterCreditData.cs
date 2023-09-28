using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using System;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class GetMatterCreditData : IQuestion<MatterEntity>
    {
        public string ChildForm { get; }

        private GetMatterCreditData(string childForm)
        {
            ChildForm = childForm;
        }

        public static GetMatterCreditData Data(string childForm) => new(childForm);

        public MatterEntity RequestAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;
            var actualStartDate = "";
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            switch (ChildForm)
            {
                case "Client Relationship Credit":

                    actor.AttemptsTo(Click.On(MatterLocator.ClientRelationshipCreditForm));
                   // actor.AttemptsTo(Click.On(MatterLocator.ExpandChildForm(ChildForm)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actualStartDate = driver.FindElement(MatterLocator.StartDate.Query).GetAttribute("value");

                    break;
                case "Project Management Credit":

                    actor.AttemptsTo(Click.On(MatterLocator.ProjectManagementCreditForm));
               //     actor.AttemptsTo(Click.On(MatterLocator.ExpandChildForm(ChildForm)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actualStartDate = driver.FindElement(MatterLocator.StartDate.Query).GetAttribute("value");

                    break;
                case "Relationship Enhancement Credit":

                    actor.AttemptsTo(Click.On(MatterLocator.RelationshipEnhancementCredit));
                  //  actor.AttemptsTo(Click.On(MatterLocator.ExpandChildForm(ChildForm)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                    actualStartDate = driver.FindElement(MatterLocator.StartDate.Query).GetAttribute("value");

                    break;
                default:
                    Console.WriteLine("No Child Form Found");
                    break;
            }

            var matterStartDate = new MatterEntity()
            {
                StartDate = actualStartDate
            };
            
            return matterStartDate;
        }

    }
}