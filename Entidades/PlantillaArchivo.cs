using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class PlantillaArchivo : Base
    {

        #region Campos

        private int idPlantilla;
        private byte[] archivo;
        private string nombre;

      

        #endregion

        #region Propiedades

        public int IdPlantilla
        {
            get
            {
                return idPlantilla;
            }
            set
            {
                idPlantilla = value;
            }
        }

        public byte[] Archivo
        {
            get
            {
                return archivo;
            }
            set
            {
                archivo = value;
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        #endregion

    }
}
