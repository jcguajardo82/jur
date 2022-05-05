using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Autorizador : Base
    {

        #region Campos

        private string nombre;

        #endregion

        #region Propiedades

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

        #endregion

    }
}
