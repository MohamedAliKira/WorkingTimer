using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Server.Services;
using WorkingTimer.Shared;

namespace WorkingTimer.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventsService _eventService;
        
        public EventsController(IEventsService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("newEvent")]
        public async Task<IActionResult> PostEventAsync([FromBody] CalenderEvents calenderEvents)
        {
            if (ModelState.IsValid)
            {                
                var result = await _eventService.AddEventAsync(calenderEvents);
                return Ok(result);// new ApiResponse<CalenderEvents>(result, "Event created successfully"));
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }
        
        // events/GetEvents?userId=userId&year=year&month=month
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents(string userId, int year, int month)
        {
            if (month < 13 && month > 0 && year <= DateTime.Now.Year)
            {
                var result = await _eventService.GetEventsAsync(userId, year, month);
                return Ok(result); 
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }

        [HttpPut("EditEvent/{Id}")]
        public async Task<IActionResult> EditEvent([FromQuery] string Id, [FromBody] CalenderEvents calenderEvents)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var result = await _eventService.UpdateEventAsync(Id, calenderEvents);
                return Ok(result);
            }

            return BadRequest("Some properties are not valid");  // status code : 400
        }
    }
}
