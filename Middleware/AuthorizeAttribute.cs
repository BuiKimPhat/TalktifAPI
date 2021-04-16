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
        var check = (bool)context.HttpContext.Items["NeedRefreshToken"];
        var user = (ReadUserDto)context.HttpContext.Items["User"];
        if (user == null && check==true)
        {
            //not logged in
            context.Result = new JsonResult(new { message = "Unauthorized, Token Exp" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        if (user == null && check==false)
        {
            //not logged in
            context.Result = new JsonResult(new { message = "Unauthorized, Not Sign Up" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}