using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Model;
using TicketManagement.Model.Contracts;

namespace TicketManagement.Api.Controllers
{
    [Authorize(Roles = "Event Manager")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("event")]
        public async Task<IActionResult> CreateEvent([FromBody] Event eventEntity)
        {
            var result = await _eventService.CreateEventAsync(eventEntity);
            return new ObjectResult(result.Id) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("event/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event eventEntity)
        {
            eventEntity.Id = id;
            var result = await _eventService.UpdateEventAsync(eventEntity);
            return Ok(result);
        }

        [HttpPut("event/{id}/eventticket")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventTicket eventTicketEntity)
        {
            eventTicketEntity.Id = id;
            var result = await _eventService.UpdateEventTicketAsync(eventTicketEntity);
            return Ok(result);
        }

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _eventService.GetEventAsync(id);
            return Ok(result);
        }
    }
}
