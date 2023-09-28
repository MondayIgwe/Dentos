using System;
using System.Threading;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.PageLocators.CommonLocators;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class ProcessRibbonMenu : ITask
    {
        public RibbonAction Action { get; }
        private ProcessRibbonMenu(RibbonAction action)
        {
            Action = action;
        }

        public static ProcessRibbonMenu ClickOn(RibbonAction action) => new(action);

        public void PerformAs(IActor actor)
        {

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            // Thread.sleep added as screenshots suggest that submit is clicked but the page is still on the same process
            Thread.Sleep(TimeSpan.FromSeconds(3));

            //Selects a main process in the menu.
            try
            {
                switch (Action)
                {
                    case RibbonAction.Save:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Save), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Save));
                        break;
                    case RibbonAction.Terminate:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Terminate), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Terminate));
                        break;
                    case RibbonAction.Approve:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Approve), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Approve));
                        break;
                    case RibbonAction.Submit:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Submit), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Submit));
                        break;
                    case RibbonAction.Cancel:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Cancel), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Cancel));
                        actor.AttemptsTo(CancelPrompt.DiscardChanges(true));
                        break;
                    case RibbonAction.PostAll:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.PostAll), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.PostAll));
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                        break;
                    case RibbonAction.Generate:
                        if (actor.DoesElementExist(CommonLocator.Generate))
                        {
                            actor.WaitsUntil(Appearance.Of(CommonLocator.Generate), IsEqualTo.True(), 1);
                            actor.AttemptsTo(JScript.ClickOn(CommonLocator.Generate));
                        }
                        else
                        {
                            actor.WaitsUntil(Appearance.Of(CommonLocator.GeneratePreview), IsEqualTo.True(), 1);
                            actor.AttemptsTo(JScript.ClickOn(CommonLocator.GeneratePreview));
                        }
                        break;
                    case RibbonAction.BillNoPrint:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.BillNoPrint), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.BillNoPrint));
                        break;
                    case RibbonAction.ProcExclude:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.ProcExclude), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.ProcExclude));
                        break;
                    case RibbonAction.Print:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Print), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Print));
                        break;
                    case RibbonAction.RunReport:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.RunReport), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.RunReport));
                        break;
                    case RibbonAction.GenerateEdit:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.GenerateEdit), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.GenerateEdit));
                        break;
                    case RibbonAction.Split:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Split), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Split));
                        break;
                    case RibbonAction.Bill:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Bill), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Bill));
                        break;
                    case RibbonAction.Reject:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Reject), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Reject));
                        break;
                    case RibbonAction.Close:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.RibbonActionClose), IsEqualTo.True(), 1);
                        actor.AttemptToClick(CommonLocator.RibbonActionClose);
                        break;
                    case RibbonAction.ApprovalRequired:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.ApprovalRequired), IsEqualTo.True(), 1);
                        actor.AttemptToClick(CommonLocator.ApprovalRequired);
                        break;

                    case RibbonAction.Return:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Return), IsEqualTo.True(), 1);
                        actor.AttemptToClick(CommonLocator.Return);
                        break;

                    case RibbonAction.Ok:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.OkButton), IsEqualTo.True(), 1);
                        actor.AttemptToClick(CommonLocator.OkButton);
                        break;
                        
                    case RibbonAction.ReturnReject:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.ReturnReject), IsEqualTo.True(), 1);
                        actor.AttemptToClick(CommonLocator.ReturnReject);
                        break;

                    case RibbonAction.SubmitStay:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.SubmitStay), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.SubmitStay));
                        break;
                    case RibbonAction.Sent:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.Sent), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.Sent));
                        break;
                    case RibbonAction.CloseProforma:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.CloseProforma), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.CloseProforma));
                        break;
                    case RibbonAction.BillGroup:
                        actor.WaitsUntil(Appearance.Of(CommonLocator.BillGroup), IsEqualTo.True(), 1);
                        actor.AttemptsTo(Click.On(CommonLocator.BillGroup));
                        break;
                }

                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);
                Console.Write($"Error occurred performing action{Action}");
            }
        }
    }

}
