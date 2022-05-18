using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Solicitud : Base
    {

        #region Campos

        private int idPlantilla;
        private int idTipoPlantilla;
        private int idAutorizador;
        private int idStatus;
        private int idUsuario;
        private string folio;
        private string folioProceso;
        private string consecutivo;
        private string clasificacion;
        private string tipo;
        private string status;
        private string descipcion;
        private DateTime? fecha;
        private DateTime? fechaAutorizacion;
        private DateTime? fechaAsignacion;
        private string solicitante;
        private string nombreAutorizador;
        private string nombreAbogado;
        private string correo;
        private string estatusAutPrev;
        private int id_voBoSol;
        
        private List<SolicitudEtiqueta> etiquetas;

        #endregion

        #region Propiedades

        public int IdPlantilla
        {
            get
            {
                return idPlantilla;
            }
            set
            {
                idPlantilla = value;
            }
        }

        public int IdTipoPlantilla
        {
            get
            {
                return idTipoPlantilla;
            }
            set
            {
                idTipoPlantilla = value;
            }
        }

        public int IdAutorizador
        {
            get
            {
                return idAutorizador;
            }
            set
            {
                idAutorizador = value;
            }
        }

        public int IdStatus
        {
            get
            {
                return idStatus;
            }
            set
            {
                idStatus = value;
            }
        }

        public int IdUsuario
        {
            get
            {
                return idUsuario;
            }
            set
            {
                idUsuario = value;
            }
        }

        public string Folio
        {
            get
            {
                return folio;
            }
            set
            {
                folio = value;
            }
        }

        public string FolioProceso
        {
            get
            {
                return folioProceso;
            }
            set
            {
                folioProceso = value;
            }
        }

        public string Consecutivo
        {
            get
            {
                return consecutivo;
            }
            set
            {
                consecutivo = value;
            }
        }

        public string Clasificacion
        {
            get
            {
                return clasificacion;
            }
            set
            {
                clasificacion = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public DateTime? Fecha
        {
            get
            {
                return fecha;
            }
            set
            {
                fecha = value;
            }
        }

        public DateTime? FechaAutorizacion
        {
            get
            {
                return fechaAutorizacion;
            }
            set
            {
                fechaAutorizacion = value;
            }
        }
        public DateTime? FechaAsignacion
        {
            get
            {
                return fechaAsignacion;
            }
            set
            {
                fechaAsignacion = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return descipcion;
            }
            set
            {
                descipcion = value;
            }
        }

        public string Solicitante
        {
            get
            {
                return solicitante;
            }
            set
            {
                solicitante = value;
            }
        }

        public List<SolicitudEtiqueta> Etiquetas
        {
            get
            {
                return etiquetas;
            }
            set
            {
                etiquetas = value;
            }
        }
        public string NombreAutorizador
        {
            get { return nombreAutorizador; }
            set { nombreAutorizador = value; }
        }

        public string NombreAbogado
        {
            get { return nombreAbogado; }
            set { nombreAbogado = value; }
        }


        public string Correo
        {
            get
            {
                return correo;
            }
            set
            {
                correo = value;
            }
        }

        public string EstatusAutPrev
        {
            get
            {
                return estatusAutPrev;
            }
            set
            {
                estatusAutPrev = value;
            }
        }

        public int Id_voBoSol
        {
            get
            {
                return id_voBoSol;
            }
            set
            {
                id_voBoSol = value;
            }
        }

        #endregion

    }
}
