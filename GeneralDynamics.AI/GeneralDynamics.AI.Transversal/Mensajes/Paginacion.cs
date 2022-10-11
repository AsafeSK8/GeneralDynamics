using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDynamics.AI.Transversal.Mensajes
{
  public  class Paginacion
    {
        public Paginacion(int numeroPagina, int elementosPorPagina)
        {
            PaginaActual = numeroPagina;
            ElementosPorPagina = elementosPorPagina;
        }

        public Paginacion(bool paginado, int numeroPagina, int elementosPorPagina)
        {
            Paginado = paginado;
            PaginaActual =numeroPagina;
            ElementosPorPagina=elementosPorPagina;
        }

        public Paginacion()
        {
        }

        public int? ElementosPorPagina { get; set; }
        public int? PaginaActual { get; set; }        
        public int? TotalElementos { get; set; }
        public int? TotalPaginas { get; set; }
        public bool Paginado { get; set; } = true;




    }
}
