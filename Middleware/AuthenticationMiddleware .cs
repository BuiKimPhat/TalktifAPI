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
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, token);

            await _next(context);
        }
         private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false, 
                }, out SecurityToken validatedToken);
            }
            catch(Exception err)
            {               
                Console.WriteLine(err.Message);
            }
        }
        // public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)  
        // {  
        //     var secret = config.GetSection("JwtConfig").GetSection("secret").Value;  
  
        //     var key = Encoding.ASCII.GetBytes(secret);  
        //     services.AddAuthentication(x =>  
        //     {  
        //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
        //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
        //     })  
        //     .AddJwtBearer(x =>  
        //     {  
        //         x.TokenValidationParameters = new TokenValidationParameters  
        //         {  
        //             ValidateIssuer = true,    
        //             ValidateAudience = true,    
        //             ValidateLifetime = true,    
        //             ValidateIssuerSigningKey = true,    
        //             ValidIssuer = config["JwtConfig:Issuer"],    
        //             ValidAudience = config["JwtConfig:Issuer"],    
        //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:secret"]))  
        //         };  
        //     });  
        //     // config.Events = new JwtBearerEvents  
        //     //  {  
        //     //    OnAuthenticationFailed = context =>  
        //     //  {  
        //     //      services.WriteLine("OnAuthenticationFailed: " +   
        //     //          context.Exception.Message);  
        //     //      return Task.CompletedTask;  
        //     //  },  
        //     // OnTokenValidated = context =>  
        //     // {  
        //     //      Console.WriteLine("OnTokenValidated: " +   
        //     //      context.SecurityToken);  
        //     //      return Task.CompletedTask;  
        //     // }  
        // //  };  
  
        //     return services;  
        // }  
    }
}