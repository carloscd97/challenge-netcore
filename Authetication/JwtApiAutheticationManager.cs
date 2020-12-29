using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Security.Claims;
using BcpChallenge.Models;
using BcpChallenge.ViewModels;

namespace BcpChallenge.Authetication
{
    public class JwtApiAutheticationManager : IJwtAutheticationManager
    {

        private readonly ApiContext _context;

        public JwtApiAutheticationManager(ApiContext context)
        {
            _context = context;
        }

        public UserAuthenticatedViewModel Autheticate(string username, string password)
        {
            var user = _context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("secret_token_bcp_challenge");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserAuthenticatedViewModel { 
                Username = user.Username,
                FullName = user.FullName,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
