using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class MotivoRechazo
    {
        #region Campos

        private byte idMotivoRechazo;
        private string descripcion;

        #endregion

        #region Propiedades

        public byte IdMotivoRechazo
        {
            get
            {
                return idMotivoRechazo;
            }
            set
            {
                idMotivoRechazo = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
            }
        }

        #endregion

    }
}
