using Microsoft.Extensions.DependencyInjection;

namespace GeneralDynamics.AI.Transversal.Factorias
{

    /// <summary>
    /// Clase abstracta para definir configuraciones de la factoría
    /// </summary>
    public abstract class FactoryConfigurator : IFactoryConfigurator
    {

        /// <summary>
        /// Método que define los mapeos de entidades para las factorías
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        public abstract IServiceCollection Configure(IServiceCollection services);
        
    }
}
