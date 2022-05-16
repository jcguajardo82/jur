using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class tbl_VoBoSolicitudes
    {
        public int Id_voBoSol { get; set; }
        public int idSolicitud { get; set; }
        public string comentarios { get; set; }
        public string correo1 { get; set; }
        public string correo2 { get; set; }
        public string correo3 { get; set; }
        public string correo4 { get; set; }
        public string correo5 { get; set; }
        public DateTime fec_movto { get; set; }
        public int id_user { get; set; }

        public string folio { get; set; }
    }
}
