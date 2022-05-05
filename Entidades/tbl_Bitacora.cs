using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class tbl_Bitacora 
    {
        public int id_solicitud { get; set; }
        public int id_usuario { get; set; }
        public int id_status { get; set; }
        public DateTime? fecha { get; set; }
        public string comentarios { get; set; }

        public string desc_usuario { get; set; }
        public string desc_estatus { get; set; }
    }
}
