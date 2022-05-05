using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class download : PaginaBase
{
    int Tipo; //1-Plantillas, 2-Solicitudes, 3-PDFs de la solicitud
    int idarchivo;
    string nombre = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        if (Request.QueryString.Count > 0)
        {
            Tipo      = ToInt32_0(Request.QueryString[0]);
            idarchivo = ToInt32_0(Request.QueryString[1]);

            List<int> IDsDownloadFromDB = new List<int> { 1, 2, 3 };

            if (IDsDownloadFromDB.Contains(Tipo))
            {
                DownloadFile(Tipo, idarchivo);
            }

            if (Tipo == 4)
            { 
                nombre = Request.QueryString[1].ToString();
                DownloadTemporal(nombre);
            }
        }
    }

    private void DownloadFile(int tipo, int ID)
    {
        if (tipo == 1) //1 - Plantillas (DOCX)
        {
            PlantillaArchivo descarga = new PlantillaArchivo();
            descarga = DataAcces.GetPlantillaArchivo(ID);
            //Byte[] bytes = new byte[descarga.Archivo.Length - 1];
            Byte[] bytes = descarga.Archivo;
            Response.Buffer = false;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.AddHeader("content-disposition", "attachment;filename=" + descarga.Nombre);
            //Response.BinaryWrite(bytes);
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.Flush();
            Response.End();
        }

        if (tipo == 2) //2 - Solicitudes (DOCX editados)
        {
            PlantillaArchivo descarga = new PlantillaArchivo();
            descarga = DataAcces.GetSolicitudArchivo(ID);
            Byte[] bytes = descarga.Archivo;
            Response.Buffer = true;
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("content-disposition", "attachment;filename=" + descarga.Nombre);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        if (tipo == 3) //3 - PDFs de la solicitud
        {
            PlantillaArchivo descarga = new PlantillaArchivo();
            descarga = DataAcces.GetArchivosSolicitud(ID);
            Byte[] bytes = descarga.Archivo;
            Response.Buffer = true;
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("content-disposition", "attachment;filename=" + descarga.Nombre);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }

    private void DownloadTemporal(string nombre) // 4 - Archivos en Sesión
    {
        byte[] bytes = (byte[])Session["ArchivoTemporal"];
        Response.Buffer = true;
        // Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("content-disposition", "attachment;filename=" + nombre);
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}