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
    public partial class Informations : ComponentBase
    {
        [Parameter] public List<CalendarDay> Days { get; set; }
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationState { get; set; }
        private List<CalenderEvents> events = new List<CalenderEvents>();

        private TimeSpan DureeCumuled = TimeSpan.Parse("00:00:00");
        private int DayCummuled = 0;
        private TimeSpan TimeCumuled = TimeSpan.Parse("00:00:00");


        protected override void OnParametersSet()
        {
            LoadData();
        }
        private void LoadData()
        {
            TimeCumuled = TimeSpan.Parse("00:00:00");
            DureeCumuled = TimeSpan.Parse("00:00:00");
            DayCummuled = 0;
            events = new List<CalenderEvents>();
            foreach (var _event in Days.Where(e => e.IsEmpty == false))
            {
                if (_event.Event != null)
                { 
                    events.Add(_event.Event); 
                }
            }
            
            DayCummuled = events.Count();

            var allTime = events.Where(e => e.Subject == "T").Select(e => e.Duree);
            foreach (var item in allTime)
            {
                var convertTotime = TimeSpan.Parse(item);
                TimeCumuled += convertTotime;
            }

            var TotalTime = DayCummuled * TimeSpan.Parse("08:00:00");
            DureeCumuled = TimeCumuled - TotalTime;
        }



    }
}
