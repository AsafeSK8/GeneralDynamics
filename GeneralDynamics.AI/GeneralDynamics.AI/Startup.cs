using Blazored.LocalStorage;
using BlazorStrap;
using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Handlers;
using GeneralDynamics.AI.Services;
using GeneralDynamics.AI.Services.Generic;
using GeneralDynamics.AI.Transversal.Mensajes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazorStrap();
            services.AddBlazoredLocalStorage();
            services.AddTransient<ValidateHeaderHandler>();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddHttpClient<ITokenManagerService, TokenManagerService>();
            services.AddHttpClient<ISessionService, SessionService>(
                client => { client.BaseAddress = new Uri("https://localhost:44319"); });
            services.AddHttpClient<IUserService, UserService>(
                client => { client.BaseAddress = new Uri("https://localhost:44319"); })
                .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IGenericService, GenericService>(
                client => { client.BaseAddress = new Uri("https://localhost:44319"); })
                .AddHttpMessageHandler<ValidateHeaderHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
