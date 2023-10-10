using bffKeepSafe.Domain.Models.Pessoas;
using KeepSafe.Application.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bffKeepSafe.Domain.Tokens
{
    public class TokenService
    {
        public static object GenerateToken(PessoasResponse usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", usuario.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);
            var TokenExpira = tokenConfig.Expires.Value.AddHours(-3);
            return new
            {
                token = tokenString,
                TokenExpira

            };
            //return tokenHandler.WriteToken(token);
        }
    }
}
