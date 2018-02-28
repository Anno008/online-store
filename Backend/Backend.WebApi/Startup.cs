using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Backend.WebApi.Repositories;
using Backend.WebApi.Services;
using Backend.WebApi.Services.Security;
using Backend.WebApi.Validation;

namespace Backend.WebApi
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

            // Adding our filter which will validate incoming dtos, based on the IValidatableObject.Validate method
            services.Configure<MvcOptions>(x => x.Conventions.Add(new ModelStateValidatorConvention()));

            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.AddSingleton<IConfiguration>(Configuration);

            // Enable token authentication
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = GetTokenValidationParameters());

            services.AddAuthorization(options => options.AddPolicy("User", policy => policy.RequireRole("User")));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            // Db
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite($"Data Source={Configuration["Connection"]}"));

            // Services
            services.AddTransient<UserService>();
            services.AddTransient<AuthService>();

            // Utils
            services.AddTransient<JWTHandler>();

            // Repositories
            services.AddTransient<TokenRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<BrandRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
              // The signing key must match
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration.GetSection("JWTSettings:SecretKey").Value)),

              // Validate the JWT issuer (Iss) claim
              ValidateIssuer = true,
              ValidIssuer = Configuration.GetSection("JWTSettings:Issuer").Value,

              // Validate the JWT audience (Aud) claim
              ValidateAudience = true,
              ValidAudience = Configuration.GetSection("JWTSettings:Audience").Value,

              // Validate token expiration
              ValidateLifetime = true,

              // Amount of clock drift
              ClockSkew = TimeSpan.FromSeconds(30),
          };
    }
}
