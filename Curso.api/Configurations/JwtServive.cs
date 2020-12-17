using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Curso.api.Controllers;
using Curso.api.models.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Curso.api.Configurations
{
    public class JwtServive : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public JwtServive(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput)
        {
            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations:Secret").Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                        new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                        new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
                    }
                )
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return token;
        }
    }
}