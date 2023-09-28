using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.SetUpPropagation;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.SetUpPropagation
{
    public  class SearchAndAddOrUpdateInstances: ITask
    {
        public SetupPropagationEntity SetupPropagationEntity { get; }
        public const string ThreeInstances = "3E Instances";

        private SearchAndAddOrUpdateInstances(SetupPropagationEntity setupPropagationEntity) =>
            SetupPropagationEntity = setupPropagationEntity;

        public static SearchAndAddOrUpdateInstances With(SetupPropagationEntity setupPropagationEntity) => new(setupPropagationEntity);

        public void PerformAs(IActor actor)
        {
            // switch to stacked view 
            actor.AttemptsTo(ProcessView.Switch(ProcessFormView.StackedView, ThreeInstances));

            // get row count 
            var getInstancesCount = actor.DoesElementExist(SetUpPropagationLocators.InstancesRowCount)
                ? int.Parse(actor.GetElementText(SetUpPropagationLocators.InstancesRowCount)) : 0;

            if (getInstancesCount == 0)
            {
                foreach (var value in SetupPropagationEntity.InstanceList)
                {
                    actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(ThreeInstances,
                        LocatorConstants.AddButton)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.Instance, value));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.ControlSource,
                        SetupPropagationEntity.ControlSource));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                }
            }
            else if (getInstancesCount > 0)
            {
                // Check each instance exits and  if exist skip adding and only update values if needed ?
                foreach (var value in SetupPropagationEntity.InstanceList)
                {
                    for (var count = 1; count <= getInstancesCount; count++)
                    {
                        var getCurrentInstanceText = actor.GetElementText(SetUpPropagationLocators.Instance);
                        var getCurrentControlText = actor.GetElementText(SetUpPropagationLocators.ControlSource);
                        var getIsIncludeState = actor.AsksFor(SelectedState.Of(SetUpPropagationLocators.IncludeList));
                        var getIsExcludeState = actor.AsksFor(SelectedState.Of(SetUpPropagationLocators.ExcludeList));
                        var instanceFound = false;
                        if (getCurrentInstanceText == value)
                        {
                            instanceFound = true;
                            // check the  instance control source value  
                            if (getCurrentControlText != SetupPropagationEntity.ControlSource)
                            {
                                actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.ControlSource,
                                    SetupPropagationEntity.ControlSource));
                                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                                if (SetupPropagationEntity.IncludeList == true)
                                {
                                    if (!getIsIncludeState)
                                        actor.AttemptsTo(Click.On(SetUpPropagationLocators.IncludeList));
                                }
                                else
                                {
                                    if (SetupPropagationEntity.ExcludeList == true)
                                    {
                                        if (!getIsExcludeState)
                                            actor.AttemptsTo(Click.On(SetUpPropagationLocators.ExcludeList));
                                    }
                                }
                            }

                        }
                        if (instanceFound)
                        {
                            actor.AttemptsTo(Click.On(SetUpPropagationLocators.InstanceSkipToFirstButton));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                            break;
                        }
                        else
                        {
                            actor.AttemptsTo(Click.On(SetUpPropagationLocators.InstanceNextRowButton));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                            if (count != getInstancesCount) continue;
                            actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(ThreeInstances, LocatorConstants.AddButton)));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                            actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.Instance, value));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                            actor.AttemptsTo(Dropdown.SelectOptionByName(SetUpPropagationLocators.ControlSource, SetupPropagationEntity.ControlSource));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                            if (SetupPropagationEntity.IncludeList == true)
                            {
                                if (getIsIncludeState) continue;
                                actor.AttemptsTo(Click.On(SetUpPropagationLocators.IncludeList));
                                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                            }
                            else
                            {
                                if (SetupPropagationEntity.IncludeList != true) continue;
                                if (getIsExcludeState) continue;
                                actor.AttemptsTo(Click.On(SetUpPropagationLocators.ExcludeList));
                                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                            }
                        }

                    }
                }

            }

        }
    }
}
