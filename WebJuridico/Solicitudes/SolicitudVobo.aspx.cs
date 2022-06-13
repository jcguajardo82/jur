using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes_SolicitudVobo : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        verificarSesionAbierta();

        PageTitle("Áreas de afectación a involucrar en el proceso");

        int perf = ToInt32_0(Session["perfil"]);

        List<int> PerfilesPermitidos = new List<int> { 1, 4 }; // “AdmGral” y “Solicitador” 

        if (!PerfilesPermitidos.Contains(perf))
        {

            //MostrarMensage("El acceso no está permitido para su tipo de perfil.");
            PanelBotonesPrincipales.Visible = false;
            PanelSol.Visible = false;
            return;
        }
        else
        {
            PanelBotonesPrincipales.Visible = true;
        }

        if (!IsPostBack)
        {
            iniciaControles();
        }
    }

    protected void iniciaControles()
    {
        Session.Remove("lstArchivoPlantilla");
        txtCorreo1.Text = string.Empty;
        txtCorreo2.Text = string.Empty;
        txtCorreo3.Text = string.Empty;
        txtCorreo4.Text = string.Empty;
        txtCorreo5.Text = string.Empty;
        txtBusqueda.Text = string.Empty;
        txtDesc.Text = string.Empty;
        hddIdSol.Value = "0";
        PanelSol.Visible = false;
        txtBusqueda.Focus();
    }
    protected void btnGenerarSol_Click(object sender, EventArgs e)
    {
        int idUsuario = ToInt32_0(Session["idUsuario"]);

        if (idUsuario > 0)
        {

            int totalCorreos = 0;
            List<string> correos = new List<string>();

            if (hddIdSol.Value == "0")
            {
                MostrarMensaje("Debe ingresar un folio de solicitud valido.");
                txtBusqueda.Focus();
                return;
            }


            if (txtCorreo1.Text.Trim() != string.Empty)
            {
                totalCorreos++;
            }

            if (txtCorreo2.Text.Trim() != string.Empty)
            {
                totalCorreos++;
            }

            if (txtCorreo3.Text.Trim() != string.Empty)
            {
                totalCorreos++;
            }

            if (txtCorreo4.Text.Trim() != string.Empty)
            {
                totalCorreos++;
            }

            if (txtCorreo5.Text.Trim() != string.Empty)
            {
                totalCorreos++;
            }


            if (totalCorreos == 0)
            {
                MostrarMensaje("Debe ingresar correo electronico.");
                txtCorreo1.Focus();
                return;
            }

            if (txtDesc.Text.Trim() == string.Empty)
            {
                MostrarMensaje("Debe ingresar la descripcion");
                txtDesc.Focus();
                return;
            }


            if (Session["lstArchivoPlantilla"] == null)
            {
                MostrarMensaje("Debe subir por lo menos un archivo.");
                FileUpload1.Focus();
                return;
            }

            var voBo = new tbl_VoBoSolicitudes()
            {
                idSolicitud = int.Parse(hddIdSol.Value.ToString()),
                comentarios = txtDesc.Text,
                correo1 = txtCorreo1.Text,
                correo2 = txtCorreo2.Text,
                correo3 = txtCorreo3.Text,
                correo4 = txtCorreo4.Text,
                correo5 = txtCorreo5.Text,
                id_user = idUsuario
            };


            correos.Add(txtCorreo1.Text);
            correos.Add(txtCorreo2.Text);
            correos.Add(txtCorreo3.Text);
            correos.Add(txtCorreo4.Text);
            correos.Add(txtCorreo5.Text);


            var archivos = (List<PlantillaArchivo>)Session["lstArchivoPlantilla"];
            DataAcces.tbl_VoBoSolicitudes_iUp(voBo, archivos, correos);


            try
            {
                if (!EnvioCorreo.Plantilla4(correos, txtBusqueda.Text, Session["email"].ToString(), txtDesc.Text))
                {
                    MostrarMensaje("La solicitud se ha generado con éxito. No se ha podido mandar los correos de notificacion a las áreas correspondientes.");
                }


            }
            catch (Exception ex)
            {
                MostrarMensaje("La solicitud se ha generado con éxito. No se ha podido mandar los correos de notificacion a las áreas correspondientes.");
            }

            iniciaControles();

            MostrarMensaje("La solicitud se ha generado con éxito.");
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (txtBusqueda.Text.Length == 0)
        {
            MostrarMensaje("Debe ingresar un folio de solicitud valido.");
            txtBusqueda.Focus();
            return;
        }

        int? verificador = DataAcces.ValidaFolioSolicitud_sUp(txtBusqueda.Text);
        if (verificador == 0)
        {
            MostrarMensaje("El folio de la solicitud ingresado, no existe en el sistema.");
            return;
        }
        if (verificador == null)
        {
            MostrarMensaje("Ha ocurrido un error al consultar el folio.");
            return;
        }

        hddIdSol.Value = verificador.ToString();
        PanelSol.Visible = true;
        txtCorreo1.Focus();

    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;

        if (Session["lstArchivoPlantilla"] != null)
        {
            var lstArch = (List<PlantillaArchivo>)Session["lstArchivoPlantilla"];
            var filteredFile = lstArch.Where(o => o.Nombre == filePath).FirstOrDefault();
            lstArch.Remove(filteredFile);
            Session["lstArchivoPlantilla"] = lstArch;
            GridView1.DataSource = lstArch;
            GridView1.DataBind();
        }
    }



    protected void CheckFileIntegrity(byte[] archivo)
    {
        //verify file for corruption
        Exception ex1 = new Exception();

        DACJuridico.OfficeDataAcces office = new DACJuridico.OfficeDataAcces();

        byte[] archivoPrueba = office.CheckFileIntegrity(archivo, ref ex1);

        if (archivoPrueba == null)
        {
            string error = "Problema con plantilla de Office Word. " + ex1.Message;
            MostrarMensaje(error);
            //errorMsg.Text += ex1.Message;
            return;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        FileInfo fi = null;
        if (FileUpload1.HasFile)
        {
            fi = new FileInfo(FileUpload1.FileName);

            //revisar si el nombre no es demasiado largo
            if (fi.Name.Length > 70)
            {
                MostrarMensaje("El nombre del archivo es demasiado largo (maximo 65 caracteres, sin contar extensión)");
                return;
            }

            //revisar que sea archivo de office 2007 o superior
            //else if (fi.Extension != ".docx")
            //{
            //    //MostrarMensaje("Solo se pueden cargar archivos tipo .docx");
            //    //return;
            //}

            else
            {
               // CheckFileIntegrity(FileUpload1.FileBytes);

                var lstArch = new List<PlantillaArchivo>();


                if (Session["lstArchivoPlantilla"] != null)
                {
                    lstArch = (List<PlantillaArchivo>)Session["lstArchivoPlantilla"];
                    lstArch.Add(new PlantillaArchivo() { Archivo = FileUpload1.FileBytes, Nombre = FileUpload1.FileName });
                    Session["lstArchivoPlantilla"] = lstArch;
                }
                else
                {
                    lstArch.Add(new PlantillaArchivo() { Archivo = FileUpload1.FileBytes, Nombre = FileUpload1.FileName });
                    Session["lstArchivoPlantilla"] = lstArch;
                }




                GridView1.DataSource = lstArch;
                GridView1.DataBind();

                //lblCargadePlantilla.Text = "Archivo Cargado: " + FileUpload1.FileName;
                ////archivoNombre = Fileload.FileName;
                //lblCargadePlantilla.Visible = true;
                //Fileload.Visible = false;
                //btnUp.Visible = false;
                //btn_eliminar.Visible = true;
            }
            //string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}