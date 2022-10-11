using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace GeneralDynamics.AI.Transversal.Mensajes
{

    [Serializable]
    [DataContract]
    public class Resultado<TEntity> : Resultado
    {
        public Resultado() : base()
        {
            this.Respuesta = default(TEntity);
            this.Paginacion = null;            
        }

        public Resultado(bool resultadoOperacion, string mensaje = null, string error = null, TEntity respuesta = default(TEntity),  Paginacion paginacion =null, IEnumerable<Mensaje> mensajes = null) : base(resultadoOperacion, mensaje, error, mensajes)
        {
            this.Respuesta = respuesta;
            this.Paginacion = paginacion;            
        }

        [DataMember]
        public TEntity Respuesta { get; set; }

        [DataMember]
        public Paginacion Paginacion { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Resultado 
    {

        Mensaje _mensajeError = null;
        Mensaje _mensajeInfornacion = null;
        
        public Resultado()
        {
            this.Mensajes = new List<Mensaje>();
            this.ResultadoOperacion = true;
            this.Mensaje = null;
            this.Error = null;                        
        }

        public Resultado(bool resultadoOperacion, string mensaje = null, string error = null, IEnumerable<Mensaje> mensajes = null)
        {
            if (mensajes == null)
            {
                Mensajes = new List<Mensaje>();
            }
            else
            {
                Mensajes = mensajes;
            }
            this.ResultadoOperacion = resultadoOperacion;
            this.Mensaje = mensaje;
            this.Error = error;
        }

        [DataMember]
        public bool ResultadoOperacion { get; set; }

        [DataMember]
        public string Mensaje {
            get
            {
                return _mensajeInfornacion?.Texto;
            }
            set
            {
                if (value == null)
                {
                    if (_mensajeInfornacion != null)
                    {
                        var toDelete = new List<Mensaje>();
                        toDelete.Add(_mensajeInfornacion);
                        this.Mensajes = this.Mensajes.Except(toDelete);
                    }
                } else
                {
                    if (_mensajeInfornacion == null)
                    {
                        _mensajeInfornacion = new Mensaje() { TipoMensaje = TipoMensaje.Info, Texto = value };
                        this.Mensajes = this.Mensajes.Append(_mensajeInfornacion);
                    }
                    _mensajeInfornacion.Texto = value;
                }
                
            }
        }

        [DataMember]
        public string Error {
            get {
                return _mensajeError?.Texto;
            }
            set {
                if (value == null)
                {
                    if (_mensajeError != null)
                    {
                        var toDelete = new List<Mensaje>();
                        toDelete.Add(_mensajeError);
                        this.Mensajes = this.Mensajes.Except(toDelete);
                    }
                } else
                {
                    if (_mensajeError == null)
                    {
                        _mensajeError = new Mensaje() { TipoMensaje = TipoMensaje.Error, Texto = value };
                        this.Mensajes = this.Mensajes.Append(_mensajeError);
                    }
                    _mensajeError.Texto = value;
                }
                
            }
        }

        [DataMember]
        public IEnumerable<Mensaje> Mensajes { get; set; }


        public override string ToString()
        {
            return  $"Respuesta: [ResultadoOperacion: {ResultadoOperacion.ToString()}]  [Mensaje: {Mensaje}] [Mensajes: {string.Join("#",Mensajes)}]";
        }
    }

    
}
