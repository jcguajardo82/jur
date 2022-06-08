using DACJuridico;
using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes_Solicitudes : PaginaBase
{
    #region Constructor

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Nueva Solicitud");

        int tipo;

        if (!Page.IsPostBack)
        {
            int.TryParse(Request.QueryString["tipo"], out tipo);

            if (tipo > 0)
            {
                if (Session["archivos"] != null)
                {
                    Session["archivos"] = null;
                }

                //if (Session["plantilla"] != null)
                //{
                //    Session["plantilla"] = null;
                //}


                if (tipo == 1) // 1 = Poderes
                {
                    lblTitulo.Text = "Seleccione una plantilla de poderes";
                    ddlPoder.Visible = true;
                    CargarPoderes();
                }

                if (tipo == 2) // 2 = Contratos
                {
                    lblTitulo.Text = "Seleccione una plantilla de contratos";
                    ddlContrato.Visible = true;
                    CargarContratos();
                }

                if (tipo == 3) // 2 = Servicios Notariales
                {
                    lblTitulo.Text = "Seleccione una plantilla de Servicios Notariales";
                    ddlServiciosNot.Visible = true;
                    CargarServiciosNotariales();
                }
            }
            else
            {
                btnElegir.Visible = false;
                lblTitulo.Text = "Para utilizar esta pantalla, la URL debe de traer el argumento TIPO y debe ser igual a 1 o 2.";
            }

            int plantillaId = ToInt32_0(Request.QueryString["plantilla"]);

            if (plantillaId > 0)
            {
                CargarPlantilla(plantillaId);
                return;
            }
        }
    }

    protected void btnElegir_Click(object sender, EventArgs e)
    {
        CargarDatosPlantilla(Convert.ToInt32(Request.QueryString["tipo"]));
    }

    //protected void rdbPoder_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdbPoder.Checked)
    //    {
    //        ddlContrato.Enabled = false;
    //        ddlPoder.Enabled = true;
    //    }
    //}

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        CargarArchivo();
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        if (grvDocumentos.Rows.Count <= 0)
        {
            MostrarMensaje("Necesita agregar al menos un archivo.");
        }

        else //solicitud nueva
        {
            CrearSolicitud(Convert.ToInt32(Request.QueryString["tipo"]));
        }
    }

    protected void btnNuevaSolicitud_Click(object sender, EventArgs e)
    {
        HabilitarControles();
    }

    #endregion

    #region Metodos

    private void CargarPoderes()
    {
        List<Plantillas> plantillas;

        //rdbPoder.Checked = true;
        //ddlContrato.Enabled = false;

        plantillas = DataAcces.GetPlantillasPorTipo(1);

        if (plantillas.Count > 0)
        {
            ddlPoder.DataSource = plantillas;
            ddlPoder.DataTextField = "Nombre";
            ddlPoder.DataValueField = "Id";

            ddlPoder.DataBind();
        }
        else
        {
            ddlPoder.Enabled = false;
            MostrarMensaje("No se encuentran poderes para generar la solicitud.");
            btnElegir.Enabled = false;
        }
    }

    private void CargarContratos()
    {
        List<Plantillas> plantillas;

        //rdbContrato.Checked = true;
        //ddlPoder.Enabled = false;

        plantillas = DataAcces.GetPlantillasPorTipo(2);

        if (plantillas.Count > 0)
        {
            ddlContrato.DataSource = plantillas;
            ddlContrato.DataTextField = "Nombre";
            ddlContrato.DataValueField = "Id";

            ddlContrato.DataBind();
        }
        else
        {
            ddlContrato.Enabled = false;
            MostrarMensaje("No se encuentran contratos para generar la solicitud.");
            btnElegir.Enabled = false;
        }
    }


    private void CargarServiciosNotariales()
    {
        List<Plantillas> plantillas;

        //rdbContrato.Checked = true;
        //ddlPoder.Enabled = false;

        plantillas = DataAcces.GetPlantillasPorTipo(3);

        if (plantillas.Count > 0)
        {
            ddlServiciosNot.DataSource = plantillas;
            ddlServiciosNot.DataTextField = "Nombre";
            ddlServiciosNot.DataValueField = "Id";

            ddlServiciosNot.DataBind();
        }
        else
        {
            ddlServiciosNot.Enabled = false;
            MostrarMensaje("No se encuentran contratos para generar la solicitud.");
            btnElegir.Enabled = false;
        }
    }

    private void CargarTipoArchivo(int tipoPlantilla)
    {
        ddlTipoDocumento.DataSource = DataAcces.GetTipoDocumento(tipoPlantilla);
        ddlTipoDocumento.DataTextField = "Descripcion";
        ddlTipoDocumento.DataValueField = "Id";

        ddlTipoDocumento.DataBind();
    }

    private void CargarDatosPlantilla(int tipo)
    {
        Plantillas plantilla = null;
        int idPlantillaJuridica = 0;

        switch (tipo)
        {
            case 1:// 1 = Poderes
                idPlantillaJuridica = Convert.ToInt32(ddlPoder.SelectedValue);
                break;
            case 2:// 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlContrato.SelectedValue);
                break;
            case 3: // 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlServiciosNot.SelectedValue);
                break;
            default:
                break;
        }


        //if (tipo == 1) // 1 = Poderes
        //{         
        //    plantilla = DataAcces.SelPlantillaSolicitudes(Convert.ToInt32(ddlPoder.SelectedValue));
        //}
        //if (tipo == 2) // 2 = Contratos
        //{          
        //    plantilla = DataAcces.SelPlantillaSolicitudes(Convert.ToInt32(ddlContrato.SelectedValue));
        //}
        //if (tipo == 3) // 2 = Contratos
        //{        
        //    plantilla = DataAcces.SelPlantillaSolicitudes(Convert.ToInt32(ddlServiciosNot.SelectedValue));
        //}

        plantilla = DataAcces.SelPlantillaSolicitudes(idPlantillaJuridica);

        if (plantilla == null)
        {
            MostrarMensaje("Falló la carga de los datos de plantilla.");
            return;
        }

        tblDetalleSolicitud.Visible = true;
        tblSelSolicitud.Visible = false;

        grvEtiquetas.DataSource = plantilla.Etiquetas;
        grvEtiquetas.DataBind();

        //grvDocumentos.DataSource = new List<ArchivoSolicitud>();


        lblDescValue.Text = plantilla.Plantilla.Descripcion;
        lblNombrePlantillaValue.Text = plantilla.Plantilla.Nombre;

        CargarTipoArchivo(tipo);

        //Session["plantilla"] = plantilla;

        GenerarBorrador(tipo, idPlantillaJuridica, plantilla.Etiquetas);

        var archivos = DataAcces.SelArchivoSolicitudTemp(Convert.ToInt32(Session["idUsuario"]), tipo, idPlantillaJuridica);
        Session["archivos"] = archivos;
        grvDocumentos.DataSource = archivos;
        grvDocumentos.DataBind();
    }

    public void CargarArchivo()
    {
        try
        {
            FileInfo fi;
            List<ArchivoSolicitud> archivos;


            if (Session["archivos"] == null)
            {
                Session["archivos"] = new List<ArchivoSolicitud>();
            }

            archivos = (List<ArchivoSolicitud>)Session["archivos"];

            if (fluDocumento.HasFile)
            {
                fi = new FileInfo(fluDocumento.FileName);

                if (fi.Extension == ".pdf")
                {
                    //revisar si el nombre no es demasiado largo
                    string fullName = fi.FullName;

                    if (fi.Name.Length > 70)
                    {
                        MostrarMensaje("El nombre del archivo es demasiado largo (maximo 65 caracteres, sin contar extensión)");
                        return;
                    }

                    //checar que el nombre no se repita
                    foreach (ArchivoSolicitud ar in archivos)
                    {
                        if (ar.Nombre == fi.Name)
                        {
                            MostrarMensaje("Ya hay un archivo con ese nombre. Por favor cambie el nombre del archivo que desea subir e intente de nuevo.");
                            return;
                        }
                    }

                    var archivo = new ArchivoSolicitud();

                    archivo.Nombre = fi.Name;
                    archivo.IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                    archivo.TipoDocumento = ddlTipoDocumento.SelectedItem.Text;
                    archivo.Archivo = fluDocumento.FileBytes;

                    archivos.Add(archivo);
                    //archivos.Add(new ArchivoSolicitud()
                    //{
                    //    Nombre = fi.Name,
                    //    IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                    //    TipoDocumento = ddlTipoDocumento.SelectedItem.Text,
                    //    Archivo = fluDocumento.FileBytes
                    //    //Ruta = string.Empty
                    //});


                    InsArchivoTemp(archivo);



                    grvDocumentos.DataSource = archivos;
                    grvDocumentos.DataBind();

                    //que no falta aqui esto???: Session["archivos"] = archivos; (lo puse yo) ******
                }
                else
                {
                    MostrarMensaje("Debe seleccionar un archivo tipo pdf.");
                }
            }
            else
            {
                MostrarMensaje("Debe seleccionar un archivo.");
            }
        }

        catch (Exception ex)
        {
            MostrarMensaje("Falló la carga del archivo. " + ex.Message);
            errorMsg.Text = ex.Message;
        }
    }


    private void InsArchivoTemp(ArchivoSolicitud archivo)
    {
        int idPlantillaJuridica = 0;
        int tipo = Convert.ToInt32(Request.QueryString["tipo"]);
        switch (tipo)
        {
            case 1:// 1 = Poderes
                idPlantillaJuridica = Convert.ToInt32(ddlPoder.SelectedValue);
                break;
            case 2:// 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlContrato.SelectedValue);
                break;
            case 3: // 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlServiciosNot.SelectedValue);
                break;
            default:
                break;
        }
        DataAcces.InsArchivoSolicitudTemp(archivo, Convert.ToInt32(Session["idUsuario"]), tipo, idPlantillaJuridica);
    }

    private void DelArchivoTemp(ArchivoSolicitud archivo)
    {
        int idPlantillaJuridica = 0;
        int tipo = Convert.ToInt32(Request.QueryString["tipo"]);
        switch (tipo)
        {
            case 1:// 1 = Poderes
                idPlantillaJuridica = Convert.ToInt32(ddlPoder.SelectedValue);
                break;
            case 2:// 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlContrato.SelectedValue);
                break;
            case 3: // 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlServiciosNot.SelectedValue);
                break;
            default:
                break;
        }
        DataAcces.DelArchivoSolicitudTemp(archivo, Convert.ToInt32(Session["idUsuario"]), tipo, idPlantillaJuridica);
    }




    public void CrearSolicitud(int tipo)
    {
        try
        {
            Solicitud sol;
            List<ArchivoSolicitud> archivos;
            List<SolicitudEtiqueta> etiquetas;
            bool res;
            byte[] archivo;
            PlantillaArchivo plantilla;
            string Folio;
            int Consecutivo;

            if (Session["archivos"] == null || ((List<ArchivoSolicitud>)Session["archivos"]).Count == 0)
            {
                MostrarMensaje("Debe cargar al menos un archivo.");
            }


            Folio = DataAcces.GetFolio(tipo);

            int in_IdPlantilla = 1;

            switch (tipo)
            {

                case 1:
                    in_IdPlantilla = Convert.ToInt32(ddlPoder.SelectedValue);
                    break;
                case 2:
                    in_IdPlantilla = Convert.ToInt32(ddlContrato.SelectedValue);
                    break;
                case 3:
                    in_IdPlantilla = Convert.ToInt32(ddlServiciosNot.SelectedValue);
                    break;
            }

            sol = new Solicitud()
            {
                //IdAutorizador = -1,
                IdPlantilla = in_IdPlantilla,//Convert.ToInt32(tipo == 1 ? ddlPoder.SelectedValue : ddlContrato.SelectedValue),
                IdStatus = 1,
                IdUsuario = Convert.ToInt32(Session["idUsuario"]),
                Folio = Folio
            };

            plantilla = DataAcces.GetPlantillaArchivo(sol.IdPlantilla);


            archivos = (List<ArchivoSolicitud>)Session["archivos"];

            etiquetas = new List<SolicitudEtiqueta>();

            foreach (GridViewRow r in grvEtiquetas.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    etiquetas.Add(new SolicitudEtiqueta()
                    {
                        IdEtiqueta = Convert.ToInt32(((Label)r.FindControl("lblEtiquetaId")).Text),
                        Valor = ((TextBox)r.FindControl("txtValorEtiqueta")).Text,
                        //Etiqueta = "&lt;" + ((Label)r.FindControl("lblEtiqueta")).Text.Split('<')[1].Replace(">", "&gt;")
                        Etiqueta = "&lt;" + ((Label)r.FindControl("lblEtiqueta")).Text.Replace(">", "&gt;")
                    });
                }
            }

            Exception ex1 = new Exception();
            Exception ex2 = new Exception();

            archivo = ODataAcces.ReplaceOpenFormat(etiquetas, plantilla, ref ex1);

            if (archivo == null)
            {
                string error = "Error al integrar etiquetas en archivo plantilla de Office Word. " + ex1.Message;
                MostrarMensaje(error);
                errorMsg.Text += ex1.Message;
                return;
            }

            res = DataAcces.CreateSolicitud(sol, etiquetas, archivos, archivo, ref ex2);
            Consecutivo = 1; //siempre es 1 cuando es nueva

            if (res)
            {
                DataAcces.tbl_SolicitudesTemp_dUp(Convert.ToInt32(Session["idUsuario"]), tipo, in_IdPlantilla);

                MostrarMensaje(
                String.Format("Folio asignado: {0}/{1}/{2}. La solicitud se ha creado con éxito. Consulta en tu opción del módulo \"<a href=\"/Solicitudes/Consultar.aspx\">Mis Solicitudes</a>\" para darle el seguimiento correspondiente",
                Folio, Consecutivo, DateTime.Today.Year.ToString())
                );
            }
            else
            {
                MostrarMensaje("Ocurrió un error al crear la solicitud. " + ex2.Message);
                errorMsg.Text += ex2.Message;
                return;
            }

            ModoVista();
        }

        catch (Exception ex3)
        {
            MostrarMensaje("Ocurrió un error al crear la solicitud. " + ex3.Message);
            errorMsg.Text += ex3.Message;
            return;
        }
    }

    private void ModoVista()
    {
        grvDocumentos.Enabled = false;
        grvEtiquetas.Enabled = false;
        fluDocumento.Enabled = false;
        btnCargar.Enabled = false;
        ddlTipoDocumento.Enabled = false;

        btnNueva.Visible = true;
        btnSolicitar.Visible = false;
    }

    public void HabilitarControles()
    {
        grvDocumentos.Enabled = true;
        grvEtiquetas.Enabled = true;
        fluDocumento.Enabled = true;
        btnCargar.Enabled = true;
        ddlTipoDocumento.Enabled = true;
        btnNueva.Visible = false;
        btnSolicitar.Visible = true;

        tblSelSolicitud.Visible = true;
        tblDetalleSolicitud.Visible = false;

        Session["archivos"] = null;
    }

    public void CargarPlantilla(int plantillaId)
    {
        Plantillas plantilla;

        tblDetalleSolicitud.Visible = true;
        tblSelSolicitud.Visible = false;

        plantilla = DataAcces.SelPlantillaSolicitudes(plantillaId);

        if (plantilla.Plantilla.Id_PlantillaJuridica < 1)
        {
            updMain.Visible = false;
            tblDetalleSolicitud.Visible = false;

            MostrarMensaje("Esta plantilla no existe!");
        }

        grvEtiquetas.DataSource = plantilla.Etiquetas;
        grvEtiquetas.DataBind();

        grvDocumentos.DataSource = new List<ArchivoSolicitud>();
        grvDocumentos.DataBind();

        lblDescValue.Text = plantilla.Plantilla.Descripcion;
        lblNombrePlantillaValue.Text = plantilla.Plantilla.Nombre;

        CargarTipoArchivo(plantilla.Plantilla.id_clasificacionplantilla);
        //Session["plantilla"] = plantilla;
    }

    protected void grvDocumentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //requerida
    }

    protected void grvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = (GridViewRow)grvDocumentos.Rows[index];

        if (e.CommandName == "Delete")
        {
            List<ArchivoSolicitud> archivos = (List<ArchivoSolicitud>)Session["archivos"];
            List<ArchivoSolicitud> archivos_n = new List<ArchivoSolicitud>();

            string nombreArchivo = ((Label)row.FindControl("lblArchivoPath")).Text;

            if (archivos.Count > 1)
            {
                foreach (ArchivoSolicitud ar in archivos)
                {
                    if (ar.Nombre != nombreArchivo)
                    {
                        archivos_n.Add(new ArchivoSolicitud()
                        {
                            Id = ar.Id,
                            IdArchivoSolicitud = ar.IdArchivoSolicitud,
                            IdSolicitud = ar.IdSolicitud,
                            Nombre = ar.Nombre,
                            Archivo = ar.Archivo,
                            IdTipoDocumento = ar.IdTipoDocumento,
                            TipoDocumento = ar.TipoDocumento
                        });
                    }
                   
                }
            }

            var artoDel = archivos.Where(x => x.Nombre == nombreArchivo).FirstOrDefault();
            DelArchivoTemp(artoDel);


            grvDocumentos.DataSource = archivos_n;
            grvDocumentos.DataBind();

            Session["archivos"] = archivos_n;
        }

        if (e.CommandName == "Download")
        {
            string ArchivoFileName = ((Label)row.FindControl("lblArchivoPath")).Text;

            DownloadFile(ArchivoFileName);
        }
    }

    protected void DownloadFile(string ArchivoFileName)
    {
        byte[] archivoBinaryFile = null;
        string FileName = "";

        List<ArchivoSolicitud> archivos = (List<ArchivoSolicitud>)Session["archivos"];

        foreach (ArchivoSolicitud ar in archivos)
        {
            if (ar.Nombre == ArchivoFileName)
            {
                archivoBinaryFile = ar.Archivo; //si hay mas de 1 con el mismo nombre, agarrar el primero que se encuentre
                FileName = ar.Nombre;
                break;
            }
        }

        DescargarDocumentosArchivoTemporal(archivoBinaryFile, FileName);
    }

    protected void grvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvDocumentos, "Select$" + e.Row.RowIndex);
            e.Row.Cells[2].Attributes["style"] = "cursor:pointer";
            e.Row.Cells[2].ToolTip = "Descargar El Archivo";
        }
    }
    protected void grvEtiquetas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var txt = (TextBox)e.Row.FindControl("txtValorEtiqueta");
            var etiqueta = (Label)e.Row.FindControl("lblEtiquetaId");
            int tipo = Convert.ToInt32(Request.QueryString["tipo"]);
            int idPlantillaJuridica = 0;


            switch (tipo)
            {
                case 1:// 1 = Poderes
                    idPlantillaJuridica = Convert.ToInt32(ddlPoder.SelectedValue);
                    break;
                case 2:// 2 = Contratos
                    idPlantillaJuridica = Convert.ToInt32(ddlContrato.SelectedValue);
                    break;
                case 3: // 2 = Contratos
                    idPlantillaJuridica = Convert.ToInt32(ddlServiciosNot.SelectedValue);
                    break;
                default:
                    break;
            }


            txt.Text = DataAcces.tbl_EtiquetasTemp_sUp(Convert.ToInt32(Session["idUsuario"]), tipo, int.Parse(etiqueta.Text), idPlantillaJuridica, 0);

        }
    }

    protected void txtValorEtiqueta_TextChanged(object sender, EventArgs e)
    {
        TextBox tb = (TextBox)sender;
        GridViewRow gvr = (GridViewRow)tb.Parent.Parent;
        int rowindex = gvr.RowIndex;

        var etiqueta = (Label)grvEtiquetas.Rows[rowindex].FindControl("lblEtiquetaId");


        int tipo = Convert.ToInt32(Request.QueryString["tipo"]);
        int idPlantillaJuridica = 0;
        switch (tipo)
        {
            case 1:// 1 = Poderes
                idPlantillaJuridica = Convert.ToInt32(ddlPoder.SelectedValue);
                break;
            case 2:// 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlContrato.SelectedValue);
                break;
            case 3: // 2 = Contratos
                idPlantillaJuridica = Convert.ToInt32(ddlServiciosNot.SelectedValue);
                break;
            default:
                break;
        }


        DataAcces.tbl_EtiquetasTemp_uUp(Convert.ToInt32(Session["idUsuario"]), tipo, int.Parse(etiqueta.Text), idPlantillaJuridica, 0, tb.Text);

    }

    protected void GenerarBorrador(int tipo, int idPlantilla, List<tbl_Etiquetas> etiquetas)
    {

        int idUsuario = Convert.ToInt32(Session["idUsuario"]);
        DataAcces.tbl_SolicitudesTemp_iUp(idUsuario, tipo, idPlantilla);

        foreach (tbl_Etiquetas item in etiquetas)
        {
            DataAcces.tbl_EtiquetasTemp_iUp(idUsuario, tipo, item.id_etiquetas, idPlantilla, item.id_PlantillaJuridica, string.Empty);
        }
    }

    #endregion

    #region Propiedades

    #endregion




}