using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.EntryAndModifyProcess;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.UserRoleManagement;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.UserRoleManagement
{
    public class EnterUserFeeEarnerMapping : ITask
    {
        private string SearchText { get; }
        private string FeeEarnerName { get; }

        public EnterUserFeeEarnerMapping(string searchText,string feeEarnerName)
        {
            SearchText = searchText;
            FeeEarnerName = feeEarnerName;
        }
        public static EnterUserFeeEarnerMapping SearchAndSelectIfFound(string searchText,string feeEarnerName) =>
            new(searchText, feeEarnerName);

        public void PerformAs(IActor _actor)
        {
            _actor.WaitsUntil(Appearance.Of(UserRoleManagementLocators.FeeEarnerMapFilterButton), IsEqualTo.True());
            _actor.AttemptsTo(Click.On(UserRoleManagementLocators.FeeEarnerMapFilterButton));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            _actor.AttemptsTo(SendKeys.To(UserRoleManagementLocators.FeeEarnerMapFilterInput, SearchText + Keys.Enter));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

            try
            {
                _actor.AttemptsTo(Checkbox.SetStatus(UserRoleManagementLocators.FeeEarnerCheckbox(SearchText), true));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                _actor.AttemptsTo(Click.On(UserRoleManagementLocators.FeeEarnerName(FeeEarnerName)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch (Exception e)
            {
                Console.WriteLine("No Fee Earners matching the criteria" + e.Message);
            }

        }
    }
}
