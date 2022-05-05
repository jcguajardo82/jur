using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class SolicitudEtiqueta : Base
    {

        #region Campos

        private int idSolicitud;
        private int idEtiqueta;
        private string valor;
        private string etiqueta;

        #endregion

        #region Propiedades

        public int IdSolicitud
        {
            get
            {
                return idSolicitud;
            }
            set
            {
                idSolicitud = value;
            }
        }

        public int IdEtiqueta
        {
            get
            {
                return idEtiqueta;
            }
            set
            {
                idEtiqueta = value;
            }
        }

        public string Valor
        {
            get
            {
                return valor;
            }
            set
            {
                valor = value;
            }
        }

        public string Etiqueta
        {
            get
            {
                return etiqueta;
            }
            set
            {
                etiqueta = value;
            }
        }

        #endregion

    }
}
