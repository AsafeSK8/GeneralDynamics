using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Transversal.Factorias;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeneralDynamics.AI.Application
{
    public class FactoryFramework : FactoryConfigurator
    {
        public override IServiceCollection Configure(IServiceCollection services)
        {

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            InitializeProviders(services);
            return services;
        }

        /// <summary>
        /// Inicializa los proveedores de instancias por defecto
        /// </summary>
        /// <remarks></remarks>
        private static void InitializeProviders(IServiceCollection services)
        {
            try
            {
                // services.AddTransient<IRoleService, RoleService>(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
