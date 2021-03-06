using Application;
using Application.MappingProfiles;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;

namespace Spex.Stock.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddCors(options =>
                options.AddPolicy(Common.Constants.WebApi.CORS_POLICY_NAME,
                    builder =>
                    {
                        builder.AllowAnyHeader()
                                .AllowAnyMethod()
                                .SetIsOriginAllowed(host => true)
                                .AllowCredentials();
                    }));
            services.AddControllers().AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spex.Stock.Api", Version = "v1" });
                c.IgnoreObsoleteActions();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spex.Stock.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(Common.Constants.WebApi.CORS_POLICY_NAME);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplyDatabaseMigration();
        }
    }
}
