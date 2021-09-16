using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkingTimer.Client.Models;
using WorkingTimer.Shared;
using WorkingTimer.Shared.Response;

namespace WorkingTimer.Client.Components
{
    [Authorize]
    public partial class AddEvent : ComponentBase
    {

        [Parameter] public CalendarDay selectedDay { get; set; }
        [Parameter] public bool IsOpened { get; set; }
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        string cssClass => IsOpened ? "show" : "hide";
        public bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private CalenderEventViewModel model = new();
        private CalenderEvents calenderEvents = new();

        protected override void OnParametersSet()
        {
            if (selectedDay.Event != null && IsOpened)
            {
                model.Subject = selectedDay.Event.Subject;
                model.StartTime = selectedDay.Event.StartTime;
                model.EndTime = selectedDay.Event.EndTime;
            }
            else
                model = new();
        }

        /*protected override void OnAfterRender(bool firstRender)
        {
            if (selectedDay.Event != null && IsOpened)
            {
                model.Subject = selectedDay.Event.Subject;
                model.StartTime = selectedDay.Event.StartTime;
                model.EndTime = selectedDay.Event.EndTime;
            }
        }*/

        private async Task AddEventTimer()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {

                await Map(calenderEvents, model);
                // insert to table Events
                var response = await HttpClient.PostAsJsonAsync("events/NewEvent", calenderEvents);
                if (response.IsSuccessStatusCode)
                {
                    if (selectedDay.Event == null)
                        selectedDay.Event = new CalenderEvents();

                    selectedDay.Event.Journee = selectedDay.Date;
                    selectedDay.Event.Subject = model.Subject;
                    selectedDay.Event.StartTime = model.StartTime;
                    selectedDay.Event.EndTime = model.EndTime;
                    //selectedDay.Event.Duree = model.EndTime - model.StartTime;
                }
                else
                {
                    var rslt = response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }


            model = new CalenderEventViewModel();
            calenderEvents = new CalenderEvents();
            _isBusy = false;
            IsOpened = false;
            OnEventAdd.Invoke();
        }

        private async Task Map(CalenderEvents calenderEvents, CalenderEventViewModel model)
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            string userId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;
            DateTime sTime = new DateTime(selectedDay.Date.Year,
                selectedDay.Date.Month, selectedDay.Date.Day, model.StartTime.Hour, model.StartTime.Minute, 0);

            DateTime eTime = new DateTime(selectedDay.Date.Year,
                selectedDay.Date.Month, selectedDay.Date.Day, model.EndTime.Hour, model.EndTime.Minute, 0);

            calenderEvents.UserId = userId;
            calenderEvents.Journee = selectedDay.Date;
            calenderEvents.Subject = model.Subject;
            calenderEvents.StartTime = model.StartTime;
            calenderEvents.EndTime = model.EndTime;
            calenderEvents.Duree = (eTime.TimeOfDay - sTime.TimeOfDay).ToString();
            calenderEvents.CreatedDate = DateTime.UtcNow;
            calenderEvents.Id = Guid.NewGuid().ToString();
            calenderEvents.ModifiedDate = DateTime.UtcNow;
        }


        public static event Action OnEventAdd = () => { };
    }
}


