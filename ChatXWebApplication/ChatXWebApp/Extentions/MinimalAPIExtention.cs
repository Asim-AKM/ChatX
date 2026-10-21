using Application_Layer.Services.Interfaces;
using ChatX.Application.DTOs.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatXWebApp.Extentions
{
    public static class MinimalAPIExtention
    {
        public static WebApplication MapMinimalAPI(this WebApplication app)
        {
            app.MapPost("/auth/logout", async (HttpContext ctx) =>
            {
                await ctx.SignOutAsync();
                return Results.Redirect("/login");
            }).DisableAntiforgery();

            return app;
        }
    }
}
