using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Client.Components
{
    public partial class Calender : ComponentBase
    {
        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;
        private List<CalendarDay> days = new List<CalendarDay>();
        private int rowsCount = 0;
        CalendarDay selected_Day = new CalendarDay();
        [Parameter]
        public EventCallback<CalendarDay> Select_Day { get; set; }


        void SelectMonth(ChangeEventArgs e)
        {
            month = Convert.ToInt32(e.Value.ToString());
            //Refresh Calender
            UpdateCalender();
        }

        void SelectYear(ChangeEventArgs e)
        {
            year = Convert.ToInt32(e.Value.ToString());
            //Refresh Calender
            UpdateCalender();
        }

        void UpdateCalender()
        {
            days = new List<CalendarDay>();

            //Calculate the number of empty date
            var firstDayDate = new DateTime(year, month, 1);
            int weekDayNumber = (int)firstDayDate.DayOfWeek;
            int numbersOfEmptyDays = 0;
            if(weekDayNumber == 7)
                numbersOfEmptyDays = 0;
            else
                numbersOfEmptyDays = weekDayNumber;

            //Add the Empty Days
            for (int i = 0; i < numbersOfEmptyDays; i++)
            {
                days.Add(new CalendarDay
                {
                    DayNumber = 0,
                    IsEmpty = true
                });
            }

            //Add the Month Days
            int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = 0; i < numberOfDaysInMonth; i++)
            {
                days.Add(new CalendarDay
                {
                    DayNumber = i + 1,
                    IsEmpty = false,
                    Date = new DateTime(year, month, i + 1)
                });
            }

            //Calculate the number of rows
            int reaming = days.Count % 7;
            if (reaming == 0)
                rowsCount = days.Count / 7;
            else
                rowsCount = Convert.ToInt32(days.Count / 7) + 1;

            //Console.WriteLine($"Total Rows : { rowsCount} | Number Of Day Empty : {numbersOfEmptyDays} | Month Days : {numberOfDaysInMonth}");
        }
    }
}
