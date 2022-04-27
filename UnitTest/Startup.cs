using API.Data.Models;
using API.Data;
using API.Data.Repositories;
using API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using API.Controllers;

namespace UnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            ApiConfig apiConfig = new ApiConfig() { ExpirationMinutesCredentials = 8, Secret = ";7J!J6X:Ag^<.}KpqU[@736nBb7}d}|P_-q]Vb+E<oCWj6UD{7t9#Hm}*ZJ3&ls" };
            services.AddSingleton<ApiConfig>(apiConfig);
            //generaci�n y valdiacion de tokens
            ApiConfig appSettings = apiConfig;

            services.AddControllers();
            services.AddDbContext<IDbContext,PersistenceContext>(options=>options.UseInMemoryDatabase(databaseName: "ArandaSoft"));

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IFunctionalCharacteristicRepository, FunctionalCharacteristicRepository>();
            services.AddTransient<IFunctionalCharacteristicByRoleRepository, FunctionalCharacteristicByRoleRepository>();

            //Service
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityService, SecurityService>();

            //Controller
            services.AddScoped<RoleController>();
            services.AddScoped<UserController>();
        }

        public void Configure(IServiceProvider provider)
        {

        }
    }
}
