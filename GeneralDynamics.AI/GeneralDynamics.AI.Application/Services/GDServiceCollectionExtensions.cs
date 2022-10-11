using GeneralDynamics.AI.Transversal.Factorias;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Application.Services
{
    public static class GDServiceCollectionExtensions
    {

        public static IServiceCollection AddGeneralDynamicsAIConfig(this IServiceCollection services)
        {

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("GeneralDynamics")).ToArray();
            FactoryManager.ConfigureFactories(services, assemblies);
            AutoMapperConfig.RegisterMappings();

            return FactoryManager.ServiceCollection;
        }

    }
}
