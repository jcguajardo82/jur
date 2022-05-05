using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Plantillas : Base
    {

        #region Campos

        private string idPlantilla;

        
        private string nombre;
        private string descripcion;
        private int versionDoc;
        private string nombrearchivo;

       

        private tbl_PlantillasJuridicas plantilla;
        private List<Autorizador> autorizadores;
        private List<tbl_Etiquetas> etiquetas;
        private PlantillaArchivo archivo;

        #endregion

        #region Propiedades

        public string IdPlantilla
        {
            get { return idPlantilla; }
            set { idPlantilla = value; }
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
            }
        }

        public int VersionDoc
        {
            get
            {
                return versionDoc;
            }
            set
            {
                versionDoc = value;
            }
        }

        public List<Autorizador> Autorizadores
        {
            get
            {
                return autorizadores ?? new List<Autorizador>();
            }
            set
            {
                autorizadores = value;
            }
        }

        public List<tbl_Etiquetas> Etiquetas
        {
            get
            {
                return etiquetas ?? new List<tbl_Etiquetas>();
            }
            set
            {
                etiquetas = value;
            }
        }

        public tbl_PlantillasJuridicas Plantilla
        {
            get
            {
                return plantilla ?? new tbl_PlantillasJuridicas();
            }
            set
            {
                plantilla = value;
            }
        }

        public PlantillaArchivo Archivo
        {
            get
            {
                if (archivo == null)
                {
                    archivo = new PlantillaArchivo();
                }

                return archivo;
            }
            set
            {
                archivo = value;
            }
        }

        public string Nombrearchivo
        {
            get { return nombrearchivo; }
            set { nombrearchivo = value; }
        }
        #endregion

    }
}
