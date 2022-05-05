using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class ClasificacionPlantillas : Base
    {
        private int id_clasificacionplantilla;
        private int id_tipoplantilla;
        private string tipoPlantilla;
        private string clasificacionPlantilla;

        public int IDClasificacion
        {
            get
            {
                return id_clasificacionplantilla;
            }
            set
            {
                id_clasificacionplantilla = value;
            }
        }

        public int IDTipoPlantilla
        {
            get
            {
                return id_tipoplantilla;
            }
            set
            {
                id_tipoplantilla = value;
            }
        }
        
        public string TipoPlantilla
        {
            get
            {
                return tipoPlantilla;
            }
            set
            {
                tipoPlantilla = value;
            }
        }

        public string ClasificacionPlantilla
        {
            get
            {
                return clasificacionPlantilla;
            }
            set
            {
                clasificacionPlantilla = value;
            }
        }
    }
}
