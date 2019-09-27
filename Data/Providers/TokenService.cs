using Data.Models;
using Data.VirtualModels;
using ElmLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public class TokenService
    {
        public static readonly SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e6a271ce4ae045bcb40c08bce60ddccd83951bcc87814514bf8e6401ae7db5a3c48f51c24cb247d182a747e38514e5b685bc1d2279d846d1b7844d15080f2709fa1b9dd586b54fd493bd6695c8476e7d40a2af7421104054b60dad799c286b6ff5b790a09ede41e3bf7da1df193d2906229d7ce04f1e473bb5e62e7b6d8c1ab20bb67a46267449fca6a306c70dda7be4aadd171ebb564f99ac4fa87c9726347f6212179e11b64b13986489185030ea69a8ca0dc8e425411f879cbe32d18ff8f0da28049e07b0473d993518c919d398a83de41a0bb3a04a48b7d6d9a0afb3f4023c5c269b45114d679fc6325da041aa619877198fb0a0415991ce2277ae2806bd"));

        private readonly IDistributedCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SigningCredentials signingCredentials;
        public TokenService(IDistributedCache cache, IHttpContextAccessor httpContextAccessor, IConfiguration config, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
            this.config = config;
            this.userManager = userManager;
            this.roleManager = roleManager;
            signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public async Task<ResponseModal> CreateToken(string username, string password)
        {
            var res = new ResponseModal();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                res.Message = "Username or password cannot be null.";
                return res;
            }
            var user = await userManager.FindByEmailAsync(username);
            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("id", user.Id));
                userManager.GetRolesAsync(user).Result.ToList().ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));

                res.Data = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                   issuer: config["Domain"],
                   audience: config["Domain"],
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: signingCredentials,
                   claims: claims,
                   notBefore: DateTime.Now
                ));
                return res;
            }
            res.Message = "Username or password wrong.";
            return res;
        }
        public async Task<ResponseModal> CancelToken() => await DeactiveCurrentToken();

        private async Task<ResponseModal> DeactiveCurrentToken()
        {
            var res = new ResponseModal();
            try
            {
                await cache.SetStringAsync(GetKey(GetCurrentToken()), " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });
                res.Data = "Succesful";
            }
            catch { res.Message = "Error on 'TokenService.DeactiveCurrentToken()'"; }
            return res;
        }

        public string GetCurrentToken()
        {
            var tokenObj = httpContextAccessor.HttpContext.Request.Headers["authorization"];
            return tokenObj == StringValues.Empty ? string.Empty : tokenObj.Single().Split(" ").Last();
        }

        public async Task<bool> IsTokenActive() => await cache.GetStringAsync(GetKey(GetCurrentToken())) == null;

        private string GetKey(string token) => $"tokens:{token}:deactivated";

        #region Static

        public static string CreateCryptedSignInToken(ApplicationUser user, string signInToken) => Crypto.Encrypt(signInToken, user.SecurityStamp);
        public static string ReadCryptedSignInToken(string userId, string signInToken) => Crypto.Decrypt(signInToken, userId);


        #endregion
    }
}
