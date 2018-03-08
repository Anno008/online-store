using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

using Backend.WebApi;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Backend.WebApi.Services;
using Backend.WebApi.Services.Security;
using Backend.WebApi.Validation;

namespace Backend.Tests.IntegrationTests.TestServerSetup
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IHostingEnvironment env)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            // To escape Debug/Release bin folder
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Name;

            var basePath = GetProjectPath(root, assembly);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.AddSingleton(Configuration);

            services.Configure<MvcOptions>(x => x.Conventions.Add(new ModelStateValidatorConvention()));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = GetTokenValidationParameters());

            services.AddAuthorization(options => options.AddPolicy(Role.User.ToString(), policy => policy.RequireRole(Role.User.ToString())));

            services.AddMvc();

            // Db
            services.AddDbContext<DatabaseContext>(option => option.UseInMemoryDatabase(Guid.NewGuid().ToString()));

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TestDataSet seedData)
        {
            // Seeding our in memory database with the test data
            seedData.Users.Select(x => app.ApplicationServices.GetService<UserService>().Register(x.Username, x.Password));
            seedData.Brands.Select(x => app.ApplicationServices.GetService<BrandRepository>().Create(x));

            app.UseMvc();
        }

        private TokenValidationParameters GetTokenValidationParameters() =>
          new TokenValidationParameters
          {
              ValidateActor = true,
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = Configuration.GetSection("JWTSettings:Issuer").Value,
              ValidAudience = Configuration.GetSection("JWTSettings:Audience").Value,
              ClockSkew = TimeSpan.FromSeconds(30),
              // Don't validate expiration time for for convenience's sake when testing
              RequireExpirationTime = false,
              IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration.GetSection("JWTSettings:SecretKey").Value))
          };

        private string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = AppContext.BaseDirectory;

            // Find the path to the target project
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;

                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                {
                    var projectFileInfo = new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj"));
                    if (projectFileInfo.Exists)
                    {
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                    }
                }
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }
    }
}
