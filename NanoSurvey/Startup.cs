using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NanoSurvey.Data;
using NanoSurvey.Services;
using Npgsql;

namespace NanoSurvey
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<SurveyContext>(options =>
                options.UseNpgsql(GetDbConnectionString()));
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<Func<SurveyContext>>(sp => () =>
            {
                var context = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                return context.RequestServices.GetRequiredService<SurveyContext>();
            });

            services.AddSingleton<ISurveyService, SurveyService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //можно вынести мигратор и заполнение данными в отдельный сервис или оставить только для дев-базы
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            if (serviceScope == null) return;
            var context = serviceScope.ServiceProvider.GetRequiredService<SurveyContext>();
            context.Database.Migrate();
            DbInitializer.Initialize(context);
        }

        private string GetDbConnectionString()
        {
            NpgsqlConnectionStringBuilder dbConnectionStringBuilder;
            if (env.IsProduction())
                dbConnectionStringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = Environment.GetEnvironmentVariable("POSTGRES_HOST"),
                    Port = Convert.ToInt32(Environment.GetEnvironmentVariable("POSTGRES_PORT")),
                    Database = Environment.GetEnvironmentVariable("POSTGRES_DB"),
                    Username = Environment.GetEnvironmentVariable("POSTGRES_USER"),
                    Password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")
                };
            else
                dbConnectionStringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = "localhost",
                    Port = 5432,
                    Database = "postgres",
                    Username = "postgres",
                    Password = "123456789"
                };

            return dbConnectionStringBuilder.ToString();
        }
    }
}