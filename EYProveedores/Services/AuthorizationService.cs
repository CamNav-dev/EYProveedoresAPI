using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EYProveedores.Models;
using EYProveedores.Models.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace EYProveedores.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly EyproveedoresContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationService(EyproveedoresContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private string GenerarToken(string IdUser)
        {

            var key = _configuration.GetValue<string>("JwtSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, IdUser));
            var credencialesToken = new SigningCredentials(
                
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            
                );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = credencialesToken
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string tokenCreado = tokenHandler.WriteToken(tokenConfig);
            return tokenCreado; 
        }
        public async Task<AuthorizationResponse> DevolverToken(AuthorizationRequest authorization)
        {
            var usuario_encontrado = _context.Users.FirstOrDefault(x =>
                x.Username ==authorization.Username && x.Password == authorization.Password
            );

            if(usuario_encontrado == null)
            {
                return await Task.FromResult<AuthorizationResponse>(null);
            }

            string tokenCreado = GenerarToken(usuario_encontrado.IdUser.ToString());
            return new AuthorizationResponse() { Token = tokenCreado, Resultado = true, Msg = "OK" };
        }
    }
}
