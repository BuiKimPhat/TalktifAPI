using System;  
using System.Text;  
using System.Security.Claims;  
using Microsoft.IdentityModel.Tokens;  
using System.IdentityModel.Tokens.Jwt;  
using Microsoft.Extensions.Configuration;  

namespace TalktifAPI.Data
{
    public class JwtRepo
    {
        private readonly string _secret;  
        private readonly string _expDate;  
  
        public JwtRepo(IConfiguration config)  
        {  
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;  
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInHours").Value;  
        }  
  
        public string GenerateSecurityToken(string email)  
        {  
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {  
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Email, email)  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddHours(double.Parse(_expDate)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };  
  
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            Console.WriteLine(token);
            Console.WriteLine(DateTime.UtcNow.AddHours(double.Parse(_expDate))+" " + DateTime.Now+"");
            return tokenHandler.WriteToken(token);  
        }  
        // public string ValidateSecurityToken(string email)  
        // {  
        //     var tokenHandler = new JwtSecurityTokenHandler();  
        //     var key = Encoding.ASCII.GetBytes(_secret);  
        //     //tokenHandler.
        //     //return tokenHandler.WriteToken(token);  
        // }  
    }
}