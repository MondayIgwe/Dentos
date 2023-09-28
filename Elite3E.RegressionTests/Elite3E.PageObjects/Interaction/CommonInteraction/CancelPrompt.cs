using System;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class CancelPrompt : ITask
    {   
        public bool Discard { get; }
        private CancelPrompt(bool discard)
        {
            Discard = discard;
        }

        public static CancelPrompt DiscardChanges(bool discard) => new(discard);

        public void PerformAs(IActor actor)
        {
            if(actor.DoesElementExist(CommonLocator.CancelDialog,1))
            {
                actor.WaitsUntil(Appearance.Of(CommonLocator.CancelDialog), IsEqualTo.True(), 1);

                actor.AttemptsTo(Discard ? Click.On(CommonLocator.Yes) : Click.On(CommonLocator.No));

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }
    }
    
}
