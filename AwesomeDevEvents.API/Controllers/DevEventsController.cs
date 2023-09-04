﻿using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        //  api/dev-events  GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(devEvent => !devEvent.IsDeleted).ToList();

            return Ok(devEvents);
        }

        //  api/dev-events/{id}  GET
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents
                .Include(devEvent => devEvent.Speakers)
                .SingleOrDefault(devEvent => devEvent.Id == id && !devEvent.IsDeleted);

            if (devEvent == null)
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        //  api/dev-events  POST
        [HttpPost]
        public IActionResult Post([FromBody] DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        //  api/dev-events/{id}  PUT
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] DevEvent devEventInput)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(
                devEventInput.Title,
                devEventInput.Description,
                devEventInput.StartDate,
                devEventInput.EndDate
            );

            _context.DevEvents.Update(devEvent);
            _context.SaveChanges();

            return NoContent();
        }

        //  api/dev-events/{id}  DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            _context.SaveChanges();

            return NoContent();
        }

        //  api/dev-events/{id}/speakers  POST
        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, [FromBody] DevEventSpeaker devEventSpeaker)
        {
            devEventSpeaker.DevEventId = id;

            var devEvent = _context.DevEvents.Any(devEvent => devEvent.Id == id);

            if (!devEvent)
            {
                return NotFound();
            }

            _context.DevEventSpeakers.Add(devEventSpeaker);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
