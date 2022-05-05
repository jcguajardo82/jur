using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public partial class tbl_ClasificacionPlantilla
    {
        public tbl_ClasificacionPlantilla()
        {
        }

        public int id_clasificacionplantilla { get; set; }
        public int id_tipoplantilla { get; set; }
        public string Descripcion { get; set; }

    }
}
