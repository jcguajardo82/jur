using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public partial class tbl_PlantillasJuridicas
    {
        public int Id_PlantillaJuridica { get; set; }
        public int id_clasificacionplantilla { get; set; }
        public string Nombre { get; set; }
        public int id_usuario { get; set; }
        public string Descripcion { get; set; }
        public int id_tipoplantilla { get; set; }
        public string PathArchivo { get; set; }
        public string GUID { get; set; }
        public int id_tipovigencia { get; set; }
        public int versionDoc { get; set; }
        public int cartaOescritura { get; set; }
    }
}
