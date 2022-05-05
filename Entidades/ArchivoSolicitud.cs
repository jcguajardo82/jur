using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class ArchivoSolicitud : Base
    {

        #region Campos

        private int idArchivoSolicitud;
        private int idSolicitud;
        private string nombre;
        private string ruta;
        private byte[] archivo;
        private bool esNuevo;

        private int idTipoDocumento;
        private string tipoDocumento;

        #endregion

        #region Propiedades

        public int IdArchivoSolicitud
        {
            get
            {
                return idArchivoSolicitud;
            }
            set
            {
                idArchivoSolicitud = value;
            }
        }

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

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public string Ruta
        {
            get
            {
                return ruta;
            }
            set
            {
                ruta = value;
            }
        }

        public byte[] Archivo
        {
            get
            {
                if (archivo == null)
                {
                    archivo = new byte[] { };
                }

                return archivo;
            }
            set
            {
                archivo = value;
            }
        }

        public bool EsNuevo
        {
            get
            {
                return esNuevo;
            }
            set
            {
                esNuevo = value;
            }
        }

        public int IdTipoDocumento
        {
            get
            {
                return idTipoDocumento;
            }
            set
            {
                idTipoDocumento = value;
            }
        }

        public string TipoDocumento
        {
            get
            {
                return tipoDocumento;
            }
            set
            {
                tipoDocumento = value;
            }
        }


        #endregion
    }
}
