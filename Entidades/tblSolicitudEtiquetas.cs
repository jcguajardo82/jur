using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class tblSolicitudEtiquetas
    {
        public int IdSolicitudEtiqueta { get; set; }
        public int id_etiquetas { get; set; }
        public int id_Solicitud { get; set; }
        public string Valor { get; set; }
        public string Pregunta { get; set; }
        public int id_usuarioLigado { get; set; }
    }
}
