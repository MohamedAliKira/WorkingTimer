using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Client.Components
{
    public partial class NewEvent : ComponentBase
    {
        [Parameter] public CalendarDay SelectedDay { get; set; }
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public bool _isBusy = false;
        public bool _isEditMode = false;
        private string _errorMessage = string.Empty;
        private CalenderEvents model = new();


        private async Task AddEventToSelectedDay()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await Map(model);
                if (SelectedDay.Event == null)
                    SelectedDay.Event = new CalenderEvents();

                // insert to table Events
                var response = _isEditMode
                        ? await HttpClient.PutAsJsonAsync($"events/EditEvent/{model.Id}", model)
                        : await HttpClient.PostAsJsonAsync("events/newEvent", model);

                var result = await response.Content.ReadFromJsonAsync<CalenderEvents>();

                if (response.IsSuccessStatusCode)
                {

                    /*selectedDay.Event.Journee = selectedDay.Date;
                    selectedDay.Event.Subject = model.Subject;
                    selectedDay.Event.StartTime = model.StartTime;
                    selectedDay.Event.EndTime = model.EndTime;*/
                    //selectedDay.Event.Duree = model.EndTime - model.StartTime;
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            //model = new CalenderEventViewModel();
            _isBusy = false;
            //OnEventAdd.Invoke();
        }

        private async Task Map(CalenderEvents calenderEvents)
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            string userId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;
            DateTime sTime = new DateTime(SelectedDay.Date.Year,
                SelectedDay.Date.Month, SelectedDay.Date.Day, SelectedDay.Event.StartTime.Hour, SelectedDay.Event.StartTime.Minute, 0);

            DateTime eTime = new DateTime(SelectedDay.Date.Year,
                SelectedDay.Date.Month, SelectedDay.Date.Day, SelectedDay.Event.EndTime.Hour, SelectedDay.Event.EndTime.Minute, 0);

            calenderEvents.UserId = userId;
            calenderEvents.Journee = SelectedDay.Date;
            calenderEvents.StartTime = sTime;
            calenderEvents.EndTime = eTime;
            calenderEvents.Duree = (eTime.TimeOfDay - sTime.TimeOfDay).ToString();
            calenderEvents.CreatedDate = _isEditMode ? SelectedDay.Event.CreatedDate : DateTime.UtcNow;
            calenderEvents.Id = SelectedDay.Event.Id ?? Guid.NewGuid().ToString();
            calenderEvents.ModifiedDate = DateTime.UtcNow;
        }


        public static event Action OnEventAdd = () => { };


    }
}
