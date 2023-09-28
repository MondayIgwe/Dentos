using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.Infrastructure.Selenium;
using System.Linq;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class Lookup : ITask
    {
        public string LookupFieldName { get; }
        public string LookupValue { get; }

        private Lookup(string lookupFieldName, string lookupValue)
        {
            LookupValue = lookupValue;
            LookupFieldName = lookupFieldName;
        }

        public static Lookup SearchAndSelectSingle(string lookupFieldName, string lookupValue) =>
            new(lookupFieldName, lookupValue);

        public void PerformAs(IActor actor)
        {

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.AttemptsTo(Click.On(CommonLocator.LookupInput(LookupFieldName)));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.FindOne(CommonLocator.LookupInput(LookupFieldName)).Clear();

            actor.AttemptsTo(SendKeys.To(CommonLocator.LookupInput(LookupFieldName), LookupValue));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(CommonLocator.LookupSearchButton(LookupFieldName)));

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            //for debugging purposes.
            var textList = actor.GetElementTextList(CommonLocator.GetAllTheRowsFromSearchResults, 20);

            var resultList = actor.FindAll(CommonLocator.GetAllTheRowsFromSearchResults, 20);
            resultList.Where(x => x.Text.Trim().Equals(LookupValue, System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Click();
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            actor.AttemptsTo(Click.On(CommonLocator.LookupSelectButton));
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

        }
    }

}
