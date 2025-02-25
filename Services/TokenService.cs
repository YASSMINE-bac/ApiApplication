using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace API.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
   public string CreateToken(AppUser user)
   {
       var claims = new List<Claim>
       {
           new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
       };
      var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.Now.AddDays(5),
          SigningCredentials = cred

      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
   }
}
