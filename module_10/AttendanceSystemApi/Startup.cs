using AttendanceSystemApi.Extensions;
using AttendanceSystemApp.HomeWorkManager;
using AttendanceSystemApp.LectureManager;
using AttendanceSystemApp.LecturerManagers;
using AttendanceSystemApp.Managers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<CreateHomeWork>();
                config.RegisterValidatorsFromAssemblyContaining<CreateLecture>();
                config.RegisterValidatorsFromAssemblyContaining<CreateLecturer>();
                config.RegisterValidatorsFromAssemblyContaining<CreateStudent>();
                config.RegisterValidatorsFromAssemblyContaining<EditHomeWork>();
                config.RegisterValidatorsFromAssemblyContaining<EditLecture>();
                config.RegisterValidatorsFromAssemblyContaining<EditLecturer>();
                config.RegisterValidatorsFromAssemblyContaining<EditStudent>();
            });
            services.AddApplicationServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AttendanceSystemApi v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
