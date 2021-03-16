using AttendanceSystemApp.Lists;
using AttendanceSystemApp.Core;
using AttendanceSystemPersistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AttendanceSystemApi.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AttendanceSystemApi", Version = "v1" });
            });
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddMediatR(typeof(StudentList.Handler).Assembly);
            services.AddMediatR(typeof(HomeWorkList.Handler).Assembly);
            services.AddMediatR(typeof(LecturerList.Handler).Assembly);
            services.AddMediatR(typeof(LectureList.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingData).Assembly);

            return services;
        }
    }
}
