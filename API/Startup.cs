using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Data.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"\\server\share\directory\"));
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<TokenService>();
            services.AddTransient<TokenServiceMiddleware>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer((Action<JwtBearerOptions>)(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = TokenService.securityKey,
                        ValidIssuer = Configuration["Domain"],
                        ValidAudience = Configuration["Domain"],
                    };

                }));
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Montajix API", Version = "v1" });
                option.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Bearer token 'Authorization'",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
            });

            services.AddCors();

            //services.AddHttpClient<RandevuController>();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowMyOrigin",
            //    builder => builder.WithOrigins("http://localhost:8080"));
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });

            SeedDataBase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseAuthentication();
            app.UseMiddleware<TokenServiceMiddleware>();
            app.UseHttpsRedirection();
            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseSwagger(option => { option.RouteTemplate = "swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(option => { option.SwaggerEndpoint("v1/swagger.json", "API"); });
        }
    }
}
