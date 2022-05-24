using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.CustormerEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Interfaz;
using SocialMedia.Infraestructure.Options;
using SocialMedia.Infraestructure.Repositorios;
using SocialMedia.Infraestructure.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Extensions
{
    public static  class ServiceCollectionExtension
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMediaContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("variable")));
        }
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(configuration.GetSection("Pagination"));
            services.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));
            return services;

        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUrl);
            });
            return services;
        }
    }
}
