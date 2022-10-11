using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDynamics.AI.Transversal.Context
{
    /// <summary>
    /// Interfaz para clases de construcción de mapeos de contexto
    /// </summary>
    public interface IContextModelBuilder
    {

        /// <summary>
        /// Método que define los mapeos de entidades del modelo
        /// </summary>
        /// <param name="modelBuilder">Constructor de mapeos y modelado del contexto</param>
        void Map(ModelBuilder modelBuilder);

    }
}
