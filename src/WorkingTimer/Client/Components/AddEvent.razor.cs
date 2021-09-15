using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Client.Components
{
    public partial class AddEvent : ComponentBase
    {
        [Parameter]
        public CalendarDay selectedDay { get; set; }
        [Parameter]
        public bool IsOpened { get; set; }

        string cssClass => IsOpened ? "show" : "hide";
        public bool _isBusy = false;
        private CalenderEvents events = new CalenderEvents() { Subject ="", StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(0, 0, 0) };

        private void AddEventTimer()
        {
            _isBusy = true;
            Task.Delay(7000);
            _isBusy = false;
        }
    }
}


