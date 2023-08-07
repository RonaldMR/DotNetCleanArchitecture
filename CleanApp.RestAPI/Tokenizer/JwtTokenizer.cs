using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanApp.RestAPI.Tokenizer
{
    public class JwtTokenizer
    {
        private const int ExpirationMinutes = 5;

        public string Tokenize(string userEmailAddress)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

            var token = CreateJwtToken(
                CreateClaims(userEmailAddress),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        private List<Claim> CreateClaims(string userEmailAddress)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userEmailAddress),
                    new Claim(ClaimTypes.Email, userEmailAddress)
                };
                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            "apiWithAuthBackend",
            "apiWithAuthBackend",
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("$R0na1dM4rR0u$$$$$R0na1dM4rR0u$$$$$R0na1dM4rR0u$$$$")
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
