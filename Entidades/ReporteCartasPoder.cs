using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ReporteCartasPoder : Base
    {

        #region Campos

        private int solicitudes;
        private string nombre;

        #endregion

        #region Constructor

        public ReporteCartasPoder()
        {
        }

        #endregion

        #region Porpiedades

        public int Solicitudes
        {
            get
            {
                return solicitudes;
            }
            set
            {
                solicitudes = value;
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

        #endregion

    }
}
