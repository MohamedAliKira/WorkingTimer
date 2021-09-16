using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Server.Services;
using WorkingTimer.Shared;
using WorkingTimer.Shared.Response;

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

        // events/NewEvent
        //[ProducesResponseType(200, Type = typeof(ApiResponse<CalenderEvents>))]
        //[ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPost("NewEvent")]
        public async Task<IActionResult> AddEventAsync([FromBody] CalenderEvents model)
        {
            if (ModelState.IsValid)
            {                
                var result = await _eventService.AddEventAsync(model);
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
    }
}
