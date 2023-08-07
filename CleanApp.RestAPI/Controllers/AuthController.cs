using CleanApp.Application.DTO.User;
using CleanApp.Application.UseCases.User;
using CleanApp.RestAPI.Tokenizer;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.RestAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CreateUserUseCase createUserUseCase;
        private readonly LogInUserUseCase logInUserUseCase;
        private readonly JwtTokenizer jwtTokenizer;

        public AuthController(CreateUserUseCase createUserUseCase, LogInUserUseCase logInUserUseCase, JwtTokenizer tokenizer)
        {
            this.createUserUseCase = createUserUseCase;
            this.logInUserUseCase = logInUserUseCase;
            this.jwtTokenizer = tokenizer;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO request)
        {
            var response = await this.createUserUseCase.Execute(request);
            return Created(new Uri(string.Format("/api/booking/{0}", response.Id), UriKind.Relative), response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Register([FromBody] LoginUserDTO request)
        {
            var user = await this.logInUserUseCase.Execute(request);

            var token = this.jwtTokenizer.Tokenize(user.EmailAddress);

            var response = new
            {
                user.FirstName,
                user.LastName,
                EmailAddres = user.EmailAddress,
                Token = token,
            };

            return Ok(response);
        }
    }
}
