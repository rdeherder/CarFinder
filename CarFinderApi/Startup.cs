using CarFinderApi.Configurations;
using CarFinderApi.Data;
using CarFinderApi.Library.Api;
using CarFinderApi.Library.ExternalDataAccess;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CarFinderApi
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
            services.AddAutoMapper(typeof(MapperInitializer));

            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy_AllowAll",
                                  builder => builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader());
            });

            services.ConfigureSqlServer(Configuration);

            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<IExternalCarsData, ExternalCarsData>();

            services.ConfigureHangfire(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarFinderApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarFinderApi v1"));
            }

            app.EnsureDatabaseIsCreated();

            app.UseHttpsRedirection();

            app.UseCors("MyCorsPolicy_AllowAll");

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureHangfireJobs(Configuration, recurringJobs);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
