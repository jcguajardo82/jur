using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class TipoDocumento : Base
    {

        #region Campos

        private string tipoDocumento;

        #endregion

        #region Propiedades

        public string Descripcion
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
