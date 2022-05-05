using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class tbl_Autorizador
    {
        public int id_Autorizador { get; set; }
        public Nullable<int> id_PlantillaJuridica { get; set; }
        public Nullable<int> id_usuario { get; set; }
    }
}
