using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Client;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Client
{
    public class EnterClientNotes :ITask
    {
        public MatterNoteEntity entity { get; }

        private EnterClientNotes(MatterNoteEntity entity) =>
            this.entity = entity;

        public static EnterClientNotes With(MatterNoteEntity entity) =>
            new EnterClientNotes(entity);

        public void PerformAs(IActor _actor)
        {
            string clientNotes = "Client Notes";

            _actor.AttemptsTo(Click.On(MatterLocator.MatterCardTitle(clientNotes)));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(clientNotes, GlobalConstants.Form));
            if (!(_actor.DoesElementExist(ClientLocators.ClientNoteType)))
            {
                _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(clientNotes, LocatorConstants.AddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
            _actor.AttemptsTo(DateControl.SelectDate("Date Entered", entity.DateEntered));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Entry User", entity.EntryUser));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(ClientLocators.ClientNoteType, entity.NoteType));
            if (!(string.IsNullOrEmpty(entity.ActionOwner)))
            {
                _actor.AttemptsTo(SendKeys.To(ClientLocators.NextActionOwner, entity.ActionOwner));
            }
            if (!(string.IsNullOrEmpty(entity.ActionDate)))
            {
                _actor.AttemptsTo(DateControl.SelectDate("Next Action Date", entity.ActionDate));
            }
            _actor.AttemptsTo(SendKeys.To(ClientLocators.ClientNote, entity.Note));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
