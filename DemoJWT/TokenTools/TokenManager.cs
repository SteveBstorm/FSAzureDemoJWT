using DemoJWT.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoJWT.TokenTools
{
    public class TokenManager : ITokenManager
    {
        public static string SecretKey = "Ma super clé secrète pour crypter mon token";
        public static string Issuer = "monsiteapi.com";
        public static string Audience = "maconsommation.com";

        public User Authentitcate(User user)
        {
            if (user.Email is null)
            {
                throw new ArgumentNullException();
            }

            //Création des crédential du token
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Objet de sécurité contenant les info User 
            Claim[] MyClaims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, (user.IsAdmin ? "admin" : "user")),
                new Claim("UserId", user.Id.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: MyClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials,
                issuer: Issuer,
                audience: Audience
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
