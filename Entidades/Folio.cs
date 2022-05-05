using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Folio
    {
        #region Campos

        private string numFolio;
        private short consecutivo;
        private short año;
        private string folioCompleto;

        #endregion

        #region Propiedades

        public string NumFolio
        {
            get { return numFolio; }
            set { numFolio = value; }
        }

        public short Consecutivo
        {
            get { return consecutivo; }
            set { consecutivo = value; }
        }

        public short Año
        {
            get { return año; }
            set { año = value; }
        }

        public string FolioCompleto
        {
            get { return folioCompleto; }
            set { folioCompleto = value; }
        }

        #endregion

    }
}
