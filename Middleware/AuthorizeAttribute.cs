using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TalktifAPI.Dtos;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try{
        var check = (bool)context.HttpContext.Items["TokenExp"];
        var user = (ReadUserDto)context.HttpContext.Items["User"];   
        if (check==true)
        {
            context.Result = new JsonResult(new { message = "Unauthorized, Token Exp" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        }catch(Exception){
            context.Result = new JsonResult(new { message = "Unauthorized, Not Sign Up" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}