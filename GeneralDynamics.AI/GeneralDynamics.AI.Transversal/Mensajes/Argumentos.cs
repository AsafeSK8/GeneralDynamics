using GeneralDynamics.AI.Transversal.Ordenacion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GeneralDynamics.AI.Transversal.Mensajes
{
    public class Argumentos
    {
        public Argumentos()
        {        
        }

        public Paginacion Paginacion { get; set; }

        public object Filtros { get; set; }

        public List<Orden> Orden { get; set; }

        public object Argumento { get; set; }

        public string IncludeProperties { get; set; }

        public bool ElevarExcepcion { get; set; } = false;
    }
}
