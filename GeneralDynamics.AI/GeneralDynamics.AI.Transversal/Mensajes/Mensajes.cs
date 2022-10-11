using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralDynamics.AI.Transversal.Mensajes
{
    public enum TipoMensaje
    {
        Error = 0,
        Warning = 1,
        Info = 2
    }

    public class Mensaje
    {
        public TipoMensaje TipoMensaje { get; set; }

        public string Texto { get; set; }

        public string Referencia { get; set; }

    }
}
