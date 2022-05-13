using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
   public partial class PlantillasVoBo
    {
        public int Id_PlantillaJuridica { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DescClas { get; set; }
        public bool voBo { get; set; }
    }
}
