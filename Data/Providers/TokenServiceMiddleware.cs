using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public class TokenServiceMiddleware : IMiddleware
    {
        public TokenService tokenService { get; set; }
        public TokenServiceMiddleware(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await tokenService.IsTokenActive())
            {
                await next(context);
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
