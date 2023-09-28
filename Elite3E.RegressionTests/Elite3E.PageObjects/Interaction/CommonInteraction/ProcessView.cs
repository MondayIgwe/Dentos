using System;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ProcessView : ITask
    {
        public string Section { get; }
        public ProcessFormView ProcessFormView;

        private ProcessView(ProcessFormView processView,string section)
        {
            ProcessFormView = processView;
            Section = section;
        }

        public static ProcessView Switch(ProcessFormView processView, string sectionToSelect) =>
            new(processView, sectionToSelect);

        public void PerformAs(IActor actor)
        {
            var  driver = actor.Using<BrowseTheWeb>().WebDriver;

            //Changes the main process view to either tabbed or stacked view.
            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            
            switch (ProcessFormView)
            {
                case ProcessFormView.StackedView:
                {
                    try
                    {
                        if (actor.WaitsUntil(Appearance.Of(CommonLocator.SwitchToView(LocatorConstants.StackedViewText)),
                            IsEqualTo.True(), 1))
                        {
                            actor.AttemptsTo(Click.On(CommonLocator.SwitchToView(LocatorConstants.StackedViewText)));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Already in the expected view:  Stacked View : "  + e.Message);
                    
                    }

                    if (string.IsNullOrEmpty(Section)) return;
                    driver.FindElements(CommonLocator.SelectChildSection.Query)
                        .FirstOrDefault(ele => ele.Text.Contains(Section))
                        ?.Click();

                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    break;
                }
                case ProcessFormView.TabbedView:
                {
                    try
                    {
                        if (actor.WaitsUntil(Appearance.Of(CommonLocator.SwitchToView(LocatorConstants.TabbedViewText)),
                            IsEqualTo.True(), 1))
                        {

                            actor.AttemptsTo(Click.On(CommonLocator.SwitchToView(LocatorConstants.TabbedViewText)));
                            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Already in the expected view: Tabbed View : " + e.Message);
                    }
                    if(!string.IsNullOrEmpty(Section))
                        actor.AttemptsTo(Click.On(CommonLocator.TabbedViewChildForm(Section)));
                    actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                    break;
                }
            }
        }
    }
    
}
