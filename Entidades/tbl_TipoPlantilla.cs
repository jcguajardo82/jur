using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public partial class tbl_TipoPlantilla
    {
        public tbl_TipoPlantilla()
        {
        }

        public int id_tipoplantilla { get; set; }
        public string Descripcion { get; set; }
    }
}
