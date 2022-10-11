using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Context;
using GeneralDynamics.AI.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Transversal.Factorias;
using System.Reflection;
using System;

namespace GeneralDynamics.AI.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeneralDynamics.AI.API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddScoped<IRepository<Role>, Repository<Role>>();

            services.AddDbContext<GeneralDynamicsAIContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("GeneralDynamics")).ToArray();

            FactoryManager.ConfigureFactories(services);

            services.AddGeneralDynamicsAIConfig();

            //services.AddDbContext<GeneralDynamicsAIContext>();
            //var sqlConnectionConfiguration = new SqlConfiguration(Configuration.GetConnectionString("SqlConnection"));
            //services.AddSingleton(sqlConnectionConfiguration);
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeneralDynamics.AI.API v1"));
            }

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
