

namespace GeneralDynamics.AI.Transversal.Ordenacion
{
    public class Orden
    {
        public Orden(string campo, ModoOrdenacion modo = ModoOrdenacion.Ascendente)
        {
            Campo = campo;
            Modo = modo;
        }

        /// <summary>
        /// Campo de ordenación
        /// </summary>
        public string Campo { get; set; }

        /// <summary>
        /// Sentido de la ordenación
        /// </summary>
        public ModoOrdenacion Modo { get; set; }
    }
}
