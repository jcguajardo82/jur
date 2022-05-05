using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class tbl_usuario
    {
        public tbl_usuario()
        {
        }

        public int id_usuario { get; set; }
        public Nullable<int> NEmpleado { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> id_nperfil { get; set; }
        public string Email { get; set; }
        public string pass { get; set; }

    }
}
