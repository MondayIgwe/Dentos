using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ProxyAs : ITask
    {
        public string UserName { get; }

        private ProxyAs(string userName) =>
            UserName = userName;

        public static ProxyAs User(string userName) => new(userName);

        public void PerformAs(IActor actor)
        {
            //Changes the user.
            actor.AttemptsTo(Hover.Over(CommonLocator.ThreeEIcon));
            actor.AttemptsTo(Click.On(CommonLocator.ThreeEIcon));

            actor.WaitsUntil(Appearance.Of(CommonLocator.UserSettings), IsEqualTo.True());
            actor.AttemptsTo(Click.On(CommonLocator.UserSettings));

            actor.WaitsUntil(Appearance.Of(CommonLocator.ProxyAs), IsEqualTo.True());
            
            actor.AttemptToClick(CommonLocator.ProxyAs, CommonLocator.ProxyUser, 3, 10);
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            //This is a selenium wait. The boa-constrictor 'actor.WaitsUntil' is not waiting the full duration.
            actor.DoesElementExist(CommonLocator.ProxyUser, 30);

            actor.WaitsUntil(Appearance.Of(CommonLocator.ProxyUser), IsEqualTo.True());
            actor.AttemptsTo(SendKeys.To(CommonLocator.ProxyUser, UserName + Keys.Enter));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            if (actor.DoesElementExist(CommonLocator.ProxyUser))
            {
                actor.WaitsUntil(Existence.Of(CommonLocator.ProxyUserOption(UserName)), IsEqualTo.True());
                actor.AttemptsTo(Click.On(CommonLocator.ProxyUserOption(UserName)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            actor.WaitsUntil(Existence.Of(CommonLocator.Avatar), IsEqualTo.True());
            actor.AttemptsTo(Hover.Over(CommonLocator.ThreeEIcon));
            actor.AttemptsTo(Click.On(CommonLocator.ThreeEIcon));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Existence.Of(CommonLocator.YouAreProxyingAs(UserName)), IsEqualTo.True());
            actor.WaitsUntil(Existence.Of(CommonLocator.CloseSideMenu), IsEqualTo.True());
            actor.AttemptsTo(Hover.Over(CommonLocator.CloseSideMenu));
            actor.AttemptsTo(Click.On(CommonLocator.CloseSideMenu));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Existence.Of(CommonLocator.Avatar), IsEqualTo.True());
        }
    }
}
