using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public abstract class Base
    {
        #region Campos

        private int id;

        #endregion

        #region Propiedades

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        #endregion

    }
}
