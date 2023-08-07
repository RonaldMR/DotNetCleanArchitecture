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
        private readonly CreateBookingUseCase createBookingUseCase;
        private readonly GetBookingUseCase getBookingUseCase;
        private readonly UpdateBookingUseCase updateBookingUseCase;
        private readonly DeleteBookingUseCase deleteBookingUseCase;

        public BookingController(CreateBookingUseCase createBookingUseCase, GetBookingUseCase getBookingUseCase, UpdateBookingUseCase updateBookingUseCase, DeleteBookingUseCase deleteBookingUseCase) 
        {
            this.createBookingUseCase = createBookingUseCase;
            this.getBookingUseCase = getBookingUseCase;
            this.updateBookingUseCase = updateBookingUseCase;
            this.deleteBookingUseCase = deleteBookingUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDTO request)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var response = await this.createBookingUseCase.Execute(userEmail, request);
            return Created(new Uri(string.Format("/api/booking/{0}", response.Id), UriKind.Relative), response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingDTO request)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var response = await this.updateBookingUseCase.Execute(userEmail, id, request);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var response = await this.getBookingUseCase.Execute(userEmail, id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            await this.deleteBookingUseCase.Execute(userEmail, id);
            return Ok();
        }
    }
}
