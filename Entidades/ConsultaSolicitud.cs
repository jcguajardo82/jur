using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ConsultaSolicitud : Base
    {
        #region Campos

        private int idConsuta;
        private string tipo;
        private string descripcion;
        private string solicitante;
        private DateTime fechaSolicitud;
        private string status;
        private List<EtiquetaConsulta> etiquetas;
        private List<ArchivoSolicitud> archivos;
        private PlantillaArchivo archivo;

        #endregion

        #region Propiedades
        public int IdConsuta
        {
            get { return idConsuta; }
            set { idConsuta = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string Solicitante
        {
            get { return solicitante; }
            set { solicitante = value; }
        }

        public DateTime FechaSolicitud
        {
            get { return fechaSolicitud; }
            set { fechaSolicitud = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public List<EtiquetaConsulta> Etiquetas
        {
            get { return etiquetas; }
            set { etiquetas = value; }
        }
        public PlantillaArchivo Archivo
        {
            get { return archivo; }
            set { archivo = value; }
        }

        public List<ArchivoSolicitud> Archivos
        {
            get
            {
                return archivos;
            }
            set
            {
                archivos = value;
            }
        }

        #endregion
    }
}
