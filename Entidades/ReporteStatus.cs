using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
   public class ReporteStatus:Base
    {
        private int solicitudes;
        private string nombre;

     
        public ReporteStatus()
        { 
        
        }

        public int Solicitudes
        {
            get { return solicitudes; }
            set { solicitudes = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

    }
}
