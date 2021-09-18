using API.Data;
using API.Data.Models;
using API.Data.Repositories;
using API.Handlers;
using API.Services;
using GAE.AIQ.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This base in the pattern repository with some adjustments. lack of security controls 
        public void ConfigureServices(IServiceCollection services)
        {
            string NameConnection = "DB";

            var apiConfig = Configuration.GetSection("ApiConfig");
            services.Configure<ApiConfig>(apiConfig);
            //generaci�n y valdiacion de tokens
            ApiConfig appSettings = apiConfig.Get<ApiConfig>();

            services.AddControllers();
            services.AddDbContext<IDbContext, ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(NameConnection)), ServiceLifetime.Transient);
            services.AddTransient<IDataBaseFactory, DataBaseFactory>();

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IFunctionalCharacteristicRepository, FunctionalCharacteristicRepository>();
            services.AddTransient<IFunctionalCharacteristicByRoleRepository, FunctionalCharacteristicByRoleRepository>();

            //Service
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IValueEncript, ValueEncript>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArandaAPI", Version = "v1" });
            });
            services.AddCors();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
            // register the scope authorization handler
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionPolicy>();

            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(x => x
                 .WithOrigins("http://localhost:8080", "http://localhost:8081", "http://192.168.1.11:8080", "http://192.168.1.11:8081")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
