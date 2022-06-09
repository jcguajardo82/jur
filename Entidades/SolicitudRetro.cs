using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class SolicitudRetro : tbl_VoBoSolicitudesRetro
    {
        public string Detalle { get; set; }

        public string FolioCompleto { get; set; }
        public int id_Solicitud { get; set; }
    }
}
