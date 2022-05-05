using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Tbl_Menu
    {
        public int idMenu { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> PadreId { get; set; }
        public Nullable<int> Posicion { get; set; }
        public Nullable<bool> Habilitada { get; set; }
        public string Url { get; set; }
        public Nullable<bool> P1 { get; set; }
        public Nullable<bool> P2 { get; set; }
        public Nullable<bool> P3 { get; set; }
        public Nullable<bool> P4 { get; set; }
        public Nullable<bool> P5 { get; set; }
        public Nullable<bool> P6 { get; set; }
        public Nullable<bool> P7 { get; set; }
    }
}
