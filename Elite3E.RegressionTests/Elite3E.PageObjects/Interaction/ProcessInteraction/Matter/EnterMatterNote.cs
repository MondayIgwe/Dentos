using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.Infrastructure.Constant;
using Elite3E.Infrastructure.Entity;
using Elite3E.Infrastructure.Selenium;
using Elite3E.PageObjects.Interaction.CommonInteraction;
using Elite3E.PageObjects.PageLocators;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.PageObjects.PageLocators.ProcessPageLocator.Matter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.PageObjects.Interaction.ProcessInteraction.Matter
{
    public class EnterMatterNote : ITask
    {
        public MatterNoteEntity entity { get; }

        private EnterMatterNote(MatterNoteEntity entity) =>
            this.entity = entity;

        public static EnterMatterNote With(MatterNoteEntity entity) =>
            new EnterMatterNote(entity);

        public void PerformAs(IActor _actor)
        {
            string matterNotes = "Matter Notes";

            _actor.AttemptsTo(Click.On(MatterLocator.MatterCardTitle(matterNotes)));
            _actor.AttemptsTo(ChildProcessView.SwitchToView(matterNotes, GlobalConstants.Form));
            if(!(_actor.DoesElementExist(MatterLocator.MatterNoteType)))
            {
                _actor.AttemptsTo(JScript.ClickOn(CommonLocator.ChildFormAction(matterNotes, LocatorConstants.AddButton)));
                _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }

            _actor.AttemptsTo(DateControl.SelectDate("Date Entered", entity.DateEntered));
            _actor.AttemptsTo(Lookup.SearchAndSelectSingle("Entry User", entity.EntryUser));
            _actor.AttemptsTo(Dropdown.SelectOptionByName(MatterLocator.MatterNoteType, entity.NoteType));
            if(!(string.IsNullOrEmpty(entity.ActionOwner)))
            {
                _actor.AttemptsTo(SendKeys.To(MatterLocator.NextActionOwner, entity.ActionOwner));
            }
            if (!(string.IsNullOrEmpty(entity.ActionDate)))
            {
                _actor.AttemptsTo(DateControl.SelectDate("Next Action Date", entity.ActionDate));
            }
            _actor.AttemptsTo(SendKeys.To(MatterLocator.MatterNote, entity.Note));
            _actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
        }
    }
}
