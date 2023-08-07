using CleanApp.Application.DTO.Booking;
using CleanApp.Application.UseCases.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanApp.RestAPI.Controllers
{
    [Route("api/booking")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly CreateBookingUseCase _createBookingUseCase;
        private readonly GetBookingUseCase _getBookingUseCase;
        private readonly UpdateBookingUseCase _updateBookingUseCase;
        private readonly DeleteBookingUseCase _deleteBookingUseCase;

        public BookingController(CreateBookingUseCase createBookingUseCase, GetBookingUseCase getBookingUseCase, UpdateBookingUseCase updateBookingUseCase, DeleteBookingUseCase deleteBookingUseCase) 
        {
            this._createBookingUseCase = createBookingUseCase;
            this._getBookingUseCase = getBookingUseCase;
            this._updateBookingUseCase = updateBookingUseCase;
            this._deleteBookingUseCase = deleteBookingUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDTO request)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var response = await this._createBookingUseCase.Execute(userEmail, request);
            return Created(new Uri(string.Format("/api/booking/{0}", response.Id), UriKind.Relative), response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingDTO request)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var response = await this._updateBookingUseCase.Execute(userEmail, id, request);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var response = await this._getBookingUseCase.Execute(userEmail, id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            await this._deleteBookingUseCase.Execute(userEmail, id);
            return Ok();
        }
    }
}
