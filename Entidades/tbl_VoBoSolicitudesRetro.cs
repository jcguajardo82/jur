using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class tbl_VoBoSolicitudesRetro
    {

        public int Id_voBoSolRetro { get; set; }
        public int Id_voBoSol { get; set; }
        public string correo { get; set; }
        public string comentariosNegocio { get; set; }
        public string riesgosDestacados { get; set; }
        public bool? autorizado { get; set; }
        public DateTime? fec_autorizado { get; set; }
        public DateTime fec_movto { get; set; }
        public int id_user { get; set; }


    }
}
