using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class tbl_Etiquetas
    {
        public int id_etiquetas { get; set; }
        public int id_PlantillaJuridica { get; set; }
        public string Pregunta { get; set; }
        public bool Juridica { get; set; }
    }
}
