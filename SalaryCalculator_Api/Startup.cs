using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalaryCalculator_Core.Factories;
using SalaryCalculator_Core.Services;
using SalaryCalculator_Data.EntityProfiles;
using SalaryCalculator_Data.Repositories;

namespace Sprout.SalaryCalculator
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEmployeeFactory, EmployeeFactory>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService,  EmployeeService>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EmployeeProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(
                        Configuration["ApiAllowedOrigin:DefaultLocalhost"].ToString()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod(); ;
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
