using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDynamics.AI.Transversal.Factorias
{
    /// <summary>
    /// Interfaz para clases de construcción de mapeos de contexto
    /// </summary>
    public interface IFactoryConfigurator
    {

        /// <summary>
        /// Método que define los mapeos de entidades para las factorías
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        IServiceCollection Configure(IServiceCollection services);

    }
}
