using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Client.Components
{
    public partial class Informations : ComponentBase
    {
        [Parameter]
        public CalendarDay Selected_Day { get; set; }
    }
}
