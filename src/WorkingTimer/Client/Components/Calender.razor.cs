using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Client.Components
{
    public partial class Calender : ComponentBase
    {
        //[Parameter] public EventCallback<CalendarDay> Select_Day { get; set; }
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationState { get; set; }
        private AddEvent _myPopup;
        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;
        public List<CalendarDay> days = new List<CalendarDay>();
        private int rowsCount = 0;

        private CalendarDay _jour = new CalendarDay();
        private bool _isOpened = false;

       private IEnumerable<CalenderEvents> events { get; set; }

        async Task SelectMonth(ChangeEventArgs e)
        {
            month = Convert.ToInt32(e.Value.ToString());
            //Refresh Calender
            UpdateCalender();
            await SynEventsToCalender();
        }

        async Task SelectYear(ChangeEventArgs e)
        {
            year = Convert.ToInt32(e.Value.ToString());
            //Refresh Calender
            UpdateCalender();
            await SynEventsToCalender();
        }
        
        void UpdateCalender()
        {
            days = new List<CalendarDay>();

            //Calculate the number of empty date
            var firstDayDate = new DateTime(year, month, 1);
            int weekDayNumber = (int)firstDayDate.DayOfWeek;
            int numbersOfEmptyDays = 0;
            if (weekDayNumber == 7)
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


            AddEvent.OnEventAdd += () =>
            {
                StateHasChanged();
            };
        }

        private async Task SynEventsToCalender()
        {
            var user = (await AuthenticationState.GetAuthenticationStateAsync()).User;
            string userId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;

            events = await HttpClient.GetFromJsonAsync<IEnumerable<CalenderEvents>>($"events/GetEvents?userId={userId}&year={year}&month={month}");

            foreach (var day in days)
            {
                if (day.IsEmpty)
                    continue;

                var eventDay = events.FirstOrDefault(e => e.Journee == day.Date);
                if(eventDay != null)
                {
                    day.Event = eventDay;
                }
            }
        }

        public void CallPopup(CalendarDay day)
        {
            _isOpened = true;
            _myPopup.Show(day);            
        }
       
    }
}
