using System.Collections.Generic;
using System.Linq;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using Elite3E.PageObjects.PageLocators.CommonLocators;
using Elite3E.Infrastructure.Selenium;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class DateControl : ITask
    {
        public string ElementInputName { get; }
        public string Date { get; }
        private Dictionary<int, string> _months;
        private string _selectDay = null, _selectMonth = null, _selectYear = null, _monthYear = null, _currentMonth = null, _currentYear = null;
        private int _currentMonthKey, _selectMonthKey;
        private readonly int _index;
        private readonly IWebLocator overrideCalendarIcon;

        private DateControl(string elementInputName, string date, int index)
        {
            ElementInputName = elementInputName;
            Date = date;
            _index = index;
        }
        private DateControl(IWebLocator overrideCalendarIcon, string date)
        {
            this.overrideCalendarIcon = overrideCalendarIcon;
            Date = date;
        }

        public static DateControl SelectDate(string elementInputName, string date, int index = 1) =>
            new(elementInputName, date, index);

        public static DateControl SelectDate(IWebLocator overrideCalendarIcon, string date) =>
            new(overrideCalendarIcon, date);

        public void PerformAs(IActor actor)
        {
            //Selects the date via the calendar widget. Optional index parameter if duplicate labels are found
            _months = new Dictionary<int, string>
                {
                    {1, "JAN"},
                    {2, "FEB"},
                    {3, "MAR"},
                    {4, "APR"},
                    {5, "MAY"},
                    {6, "JUN"},
                    {7, "JUL"},
                    {8, "AUG"},
                    {9, "SEP"},
                    {10, "OCT"},
                    {11, "NOV"},
                    {12, "DEC"}
                };

            var driver = actor.Using<BrowseTheWeb>().WebDriver;

            var splitDate = Date.Split("/");
            _selectDay = splitDate[1];
            _selectMonth = _months[int.Parse(splitDate[0])];
            _selectYear = splitDate[2];

            if (overrideCalendarIcon != null)
            {
                actor.AttemptToClick(overrideCalendarIcon, CommonLocator.Calendar);
            }
            else
            {
                var loc = _index == 1 ? CommonLocator.CalendarIcon(ElementInputName) 
                    : CommonLocator.CalendarIconWithIndex(ElementInputName, _index);
                actor.AttemptToClick(loc, CommonLocator.Calendar);
            }

            actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            actor.WaitsUntil(Appearance.Of(CommonLocator.Calendar), IsEqualTo.True());

            _monthYear = driver.FindElement(CommonLocator.CalendarMonthYear.Query).Text;
            GetCurrentMonth();
            _currentYear = _monthYear.Split(" ")[1];
            
            if (int.Parse(_currentYear) != int.Parse(_selectYear))
            {
                //select year
                actor.AttemptsTo(Click.On(CommonLocator.CalendarMonthYear));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(CommonLocator.CalendarSelectCell(_selectYear)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(CommonLocator.CalendarSelectCell(_selectMonth)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                actor.AttemptsTo(Click.On(CommonLocator.CalendarSelectCell(_selectDay)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            } else
            {
                //select month
                if (_currentMonthKey > _selectMonthKey)
                {
                    do
                    {
                        actor.AttemptsTo(Click.On(CommonLocator.CalendarPreviousMonth));
                        actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                        _monthYear = driver.FindElement(CommonLocator.CalendarMonthYear.Query).Text;
                        GetCurrentMonth();
                    } while (_currentMonthKey > _selectMonthKey);
                    
                } else if (_currentMonthKey < _selectMonthKey)
                {
                    do
                    {
                        actor.AttemptsTo(Click.On(CommonLocator.CalendarNextMonth));
                        actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());

                        _monthYear = driver.FindElement(CommonLocator.CalendarMonthYear.Query).Text;
                        GetCurrentMonth();
                    } while (_currentMonthKey < _selectMonthKey);
                }
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
                //select day
                actor.AttemptsTo(Click.On(CommonLocator.CalendarSelectCell(_selectDay)));
                actor.AttemptsTo(WaitFor.BackgroundProcessToComplete());
            }
        }

        private void GetCurrentMonth()
        {
            _currentMonth = _monthYear.Split(" ")[0];

            _currentMonthKey = _months.FirstOrDefault(x => x.Value == _currentMonth).Key;
            _selectMonthKey = _months.FirstOrDefault(x => x.Value == _selectMonth).Key;
        }
    }
}
