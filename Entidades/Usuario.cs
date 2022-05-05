using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Usuario : Base
    {

        #region Campos

        private string nombre;
        private int nEmpleado;
        private string email;
        private string perfilDesc;
        private int perfilId;

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

        public int NEmpleado
        {
            get
            {
                return nEmpleado;
            }
            set
            {
                nEmpleado = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string PerfilDesc
        {
            get
            {
                return perfilDesc;
            }
            set
            {
                perfilDesc = value;
            }
        }

        public int PerfilId
        {
            get
            {
                return perfilId;
            }
            set
            {
                perfilId = value;
            }
        }

        #endregion

    }
}
