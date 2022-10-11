using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDynamics.AI.Transversal.Context
{

    /// <summary>
    /// Clase abstracta para definir mapeos y relaciones del entidades del modelo
    /// </summary>
    public abstract class ContextModelBuilder : IContextModelBuilder
    {

        /// <summary>
        /// Método que define los mapeos de entidades del modelo
        /// </summary>
        /// <param name="modelBuilder">Constructor de mapeos y modelado del contexto</param>
        public abstract void Map(ModelBuilder modelBuilder);        

    }
}
