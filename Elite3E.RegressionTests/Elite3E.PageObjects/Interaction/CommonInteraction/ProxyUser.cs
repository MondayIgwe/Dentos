using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;
using System;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ProxyUser : ITask
    {
        private ProxyUser()
        { }

        public static ProxyUser CancelProxy() => new();

        public void PerformAs(IActor actor)
        {
            //Cancels the new user and reverts to the original user.
            actor.AttemptsTo(Hover.Over(CommonLocator.ThreeEIcon));
            actor.AttemptsTo(Click.On(CommonLocator.ThreeEIcon));

            actor.AttemptsTo(Hover.Over(CommonLocator.CancelProxyUserIcon));
            actor.AttemptsTo(Click.On(CommonLocator.CancelProxyUserCloseButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.WaitsUntil(Appearance.Of(CommonLocator.CancelProxyUserIcon), IsEqualTo.False());
        }
    }
}
