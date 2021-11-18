using Application.BackgroundService;
using Domain.IRepositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application
{
    public static class ApplicationInjectionExtension
    {
        public static IServiceCollection AddAppInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ITopGameService, TopGameManager>();
            services.AddScoped<ITopListDetailsCommandMongoRepository, TopListDetailsRepository>();
            services.AddScoped<ITopListDetailsQueryMongoRepository, TopListDetailsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGetJwtToken, GetJwtToken>();

            return services;
        }
    }
}
