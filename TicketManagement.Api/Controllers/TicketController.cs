using Microsoft.AspNetCore.Mvc;
using TicketManagement.Model.Contracts;
using TicketManagement.Model;
using Microsoft.AspNetCore.Authorization;

namespace TicketManagement.Api.Controllers
{

    [Authorize(Roles = "User")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("reservation")]
        public async Task<IActionResult> ReserveTickets([FromBody] Reservation request)
        {
            var reservation = await _ticketService.ReserveTicketsAsync(
                request.EventTicketId,
                request.Quantity,
                request.UserId);
            return Ok(reservation);
        }

        [HttpPost("reservation/{id}/purchase")]
        public async Task<IActionResult> PurchaseTickets(int id)
        {
            var success = await _ticketService.PurchaseTicketsAsync(id);
            return success ? Ok() : BadRequest("Purchase failed");
        }

        [HttpDelete("reservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var success = await _ticketService.CancelReservationAsync(id);
            return Ok(success);
        }

        [HttpGet("reservation/{ticketTypeId}/availability")]
        public async Task<IActionResult> GetAvailability(int ticketTypeId)
        {
            var available = await _ticketService.GetAvailableTicketsAsync(ticketTypeId);
            return Ok(available);
        }

        [HttpGet("reservation/{userId}/history")]
        public async Task<IActionResult> GetReservationsHistory(int userId)
        {
            var available = await _ticketService.GetReservationsByUserId(userId);
            return Ok(available);
        }
    }
}
