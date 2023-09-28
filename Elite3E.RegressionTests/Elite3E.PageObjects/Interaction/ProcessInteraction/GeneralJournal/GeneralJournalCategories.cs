using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Enums;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.GenearlJournal.GJCategoriesSetup;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.GeneralJournal
{
    public class GeneralJournalCategories : ITask
    {
        public string LabelName { get; }
        public CheckBox Action { get; }

        GeneralJournalCategories(string labelName, CheckBox action) 
        {
            LabelName = labelName;
            Action = action;
        }

        public static GeneralJournalCategories Select(string labelName, CheckBox action) => new(labelName, action);

        public void PerformAs(IActor actor)
        {
            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            var checkBoxStatus = driver.FindElement(GJJournalCategoriesLocators.CheckBox(LabelName).Query);

            switch (Action)
            {
                case CheckBox.Check:
                    if (checkBoxStatus.Selected == false)                    
                        checkBoxStatus.Click();
                    break;
                case CheckBox.Uncheck:
                    if (checkBoxStatus.Selected == true)
                        checkBoxStatus.Click();
                    break;
            }

        }
    }
}
