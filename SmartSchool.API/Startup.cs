using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSchool.API.Data;
using System;

namespace SmartSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            services.AddControllers().AddNewtonsoftJson(
                option => option.SerializerSettings.ReferenceLoopHandling = 
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IRepository, Repository>();
            services.AddSwaggerGen(
                options => { options.SwaggerDoc("smartschoolapi", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Smart School API",
                    Version = "1.0"
                });
            });
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseRouting();
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/smartschoolapi/swagger.json", "smartschoolapi");
                options.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
