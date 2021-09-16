using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Server.Models;
using WorkingTimer.Server.Options;
using WorkingTimer.Shared;

namespace WorkingTimer.Server.Services
{
    public interface IEventsService
    {
        Task<CalenderEvents> AddEventAsync(CalenderEvents model);
        Task<IEnumerable<CalenderEvents>> GetEventsAsync(string userId, int year, int month);
    }

    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public EventsService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<CalenderEvents> AddEventAsync(CalenderEvents model)
        {
            if (model == null)
                throw new NullReferenceException("Events Model is null");

            var _event = new CalenderEvents
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                UserId = model.UserId,
                IsModified= false,
                Duree = model.Duree,
                Subject = model.Subject,
                Journee = model.Journee,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            await _db.Events.AddAsync(_event);
            await _db.SaveChangesAsync();
            return _event;
        }

        public async Task<IEnumerable<CalenderEvents>> GetEventsAsync(string userId, int year, int month)
        {
            var events = await (from p in _db.Events
                                where p.UserId == userId 
                                   && p.Journee.Year == year
                                   && p.Journee.Month == month
                        select p).ToListAsync();

            return events;
        }
    }
}


//