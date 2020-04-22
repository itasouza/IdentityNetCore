using System;
using IdentityNetCore.Data;
using IdentityNetCore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityNetCore.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); 

            services.Configure<IdentityOptions>(options => {

                options.Password.RequiredLength = 3; //tamanho minimo da senha
                options.Password.RequireDigit = true; //exigir um número dentro da senha
                options.Password.RequireNonAlphanumeric = false; //um caractere especial

                options.Lockout.MaxFailedAccessAttempts = 3; //numero de tentativa de falhas no login
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); ////determina o tempo que o usuario podera ser bloqueado

                options.SignIn.RequireConfirmedEmail = true; //confirmar por e-mail 

            });

            services.ConfigureApplicationCookie(option => {
                option.LoginPath = "/Identity/Signin"; //Caminho de login
                option.AccessDeniedPath = "/Identity/AccessDenied"; //Caminho de Acesso Negado
                option.ExpireTimeSpan = TimeSpan.FromHours(10); //Período de expiração
            });

            //confugações do smtp para envio de e-mail
            services.Configure<ConfiguracoesEmail>(configuration.GetSection("ConfiguracoesEmail"));

            //auto mapper
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }
    }
}