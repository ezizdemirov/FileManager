﻿
using FileManagerAPI.Application.Repositories.FileManagerRepository;
using Microsoft.Extensions.DependencyInjection;
using FileManagerAPI.Persistence.Contexts;
using FileManagerAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FileManagerAPI.Persistence
{
    public  static class ServiceRegistration
    {
        public static void AddPesistenceServices(this IServiceCollection services )
        {
            //   services.AddSingleton<IFileManagerService, FileManagerService>();

        
            services.AddScoped<IFileManagerReadRepository, FileManagerReadRepository>();
            services.AddScoped<IFileManagerWriteRepository, FileManagerWriteRepository>();
            services.AddDbContext<FileManagerAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            //services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            //services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            //services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            //services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            //services.AddScoped<IProductReadRepository, ProductReadRepository>();
            //services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
