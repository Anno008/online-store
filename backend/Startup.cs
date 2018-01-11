using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repositories;
using Backend.Services;
using Backend.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable CORS requests
            services.AddCors();

            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => GetTokenValidationParameters());

            services.AddMvc();

            services.AddDbContext<DatabaseContext>();
            services.AddTransient<UserService>();
            services.AddTransient<UserRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // handle exceptions globally
            AppDomain.CurrentDomain.UnhandledException += HandleErrors;

            // configure CORS
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc();
        }

        private void HandleErrors(object sender, UnhandledExceptionEventArgs args)
        {
            if (sender is Exception)
            {
                Console.WriteLine((sender as Exception).Message);
            }
        }

        private TokenValidationParameters GetTokenValidationParameters() =>
          new TokenValidationParameters
          {
              // The signing key must match!
              ValidateIssuerSigningKey = true,
              // Validate the JWT Issuer (iss) claim
              ValidateIssuer = true,
              // Validate the JWT Audience (aud) claim
              ValidateAudience = true,
              ValidIssuer = Configuration.Get<JWTSettings>().Issuer,
              ValidAudience = Configuration.Get<JWTSettings>().Audience,
              NameClaimType = "sub",
              // Validate the token expiry
              ValidateLifetime = true,
              // Amount of clock drift -
              ClockSkew = TimeSpan.FromSeconds(30),
              IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration.Get<JWTSettings>().SecretKey))
          };
    }
}
