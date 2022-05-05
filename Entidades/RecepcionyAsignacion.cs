using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class RecepcionyAsignacion:Base
    {
        #region Campos
        private DateTime fechaSolicitud;
        private string folioSolicitud;
        private string tipoPlantilla;
        private string nombreSolicitador;
        private string clasificacionPlantilla;
        private DateTime fechaAutorizacion;
        private string nombreAutorizador;
        private string status;
       

        #endregion
        #region Propiedades
        public DateTime FechaSolicitud
        {
            get { return fechaSolicitud; }
            set { fechaSolicitud = value; }
        }
        public string FolioSolicitud
        {
            get { return folioSolicitud; }
            set { folioSolicitud = value; }
        }

        public string TipoPlantilla
        {
            get { return tipoPlantilla; }
            set { tipoPlantilla = value; }
        }

        public string NombreSolicitador
        {
            get { return nombreSolicitador; }
            set { nombreSolicitador = value; }
        }

        public string ClasificacionPlantilla
        {
            get { return clasificacionPlantilla; }
            set { clasificacionPlantilla = value; }
        }

        public DateTime FechaAutorizacion
        {
            get { return fechaAutorizacion; }
            set { fechaAutorizacion = value; }
        }

        public string NombreAutorizador
        {
            get { return nombreAutorizador; }
            set { nombreAutorizador = value; }
        }

       
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
       
        #endregion
    }
}
