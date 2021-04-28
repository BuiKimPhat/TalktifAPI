using System.Text;  
using Microsoft.IdentityModel.Tokens;  
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using TalktifAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Options;

namespace TalktifAPI.Middleware
{
    public  class AuthenticationMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationMiddleware (RequestDelegate next, IOptions<JwtConfig> JwtConfig)
        {
            _next = next;
            this._jwtConfig = JwtConfig.Value;
        }
        public async Task Invoke(HttpContext context,IUserRepo userRepo)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserToContext(context, token,userRepo);
            await _next(context);           
        }
         private void attachUserToContext(HttpContext context, string token,IUserRepo userRepo)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.secret);
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                string mail = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Email").Value;
                Console.WriteLine(mail);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);
                jwtToken = (JwtSecurityToken)validatedToken;
                var userMail = jwtToken.Claims.First(claim => claim.Type == "Email").Value;;
                // attach user to context on successful jwt validation
                context.Items["User"] = userRepo.getInfoByEmail(userMail);   
                context.Items["TokenExp"] = false; 
            }
            catch(SecurityTokenExpiredException err)
            {          
                context.Items["TokenExp"] = true;     
                Console.WriteLine(err.Message+" 1");
            }
            catch(Exception err)
            {             
                context.Items["TokenExp"] = false; 
                context.Items["User"] = null;     
                Console.WriteLine(err.Message+" 2");
            }
        }
    }
}