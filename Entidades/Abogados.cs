using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Abogados : Base 
    {
        #region Campos

        private string nombre;
        private int id;


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
        public int Id1
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
    }
}
