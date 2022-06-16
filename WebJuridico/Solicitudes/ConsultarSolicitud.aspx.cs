using DACJuridico;
using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes_ConsultarSolicitud : PaginaBase
{
    #region Campos

    int solicitudId;
    int action;

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Consultar Solicitud");

        solicitudId = ToInt32_0(Request.QueryString["id"]);
        action = ToInt32_0(Request.QueryString["action"]);
        int perf = ToInt32_0(Session["perfil"]);

        if (!Page.IsPostBack)
        {
            Session["archivos"] = null;

            if (solicitudId > 0)
            {
                CargarSolicitud(solicitudId);
                CargarGridDocumentos(solicitudId);

                if (action == 1) //si se desea modificar (editar sin cambiar statusId):
                {
                    tblAdjuntarDocumentos.Visible = true;
                    btnEliminar.Visible = false;
                    btnModificar.Visible = false;
                    btnGuardar.Visible = true;
                    PanelMasInfo.Visible = false;
                }

                else if (action == 2) //si se desea complementar (editar + cambiar statusId a "complementado")
                {
                    tblAdjuntarDocumentos.Visible = true;
                    btnEliminar.Visible = false;
                    btnModificar.Visible = false;
                    btnGuardar.Visible = true;
                    PanelMasInfo.Visible = false;
                }

                else if (action == 3) //si se desea revisar para autorización o rechazo
                {
                    if (perf == 1 | perf == 5) //admin general, autorizador
                    {
                        PanelBotonesAutorizar.Visible = true;
                        PanelModificarComplementar.Visible = false;

                        //convertir columna de respuesta a solo texto
                        grvEtiquetas.Columns[3].Visible = false;
                        grvEtiquetas.Columns[4].Visible = true;
                    }
                    else
                    {
                        deshabilitarAcceso();
                    }
                }

                else if (action == 4)
                {
                    ////Si se desea permitir que un abogado rechaze una solicitud:
                    //if (perf == 1 | perf == 2 | perf == 3) //admin general, asis poderes, asis contratos. Caso #6
                    //{
                    //    PanelModificarComplementar.Visible = false;
                    //    PanelBotonesAbogado.Visible = true;

                    //    //si el estatus de la solicitud es = 5 (liberada por autorizador), solo al ver la solicitud cambiar a 7 (recibida en juridico)
                    //    //DataAcces.UpdSolicitudStatusId(solicitudId, 7); //pero solo si el estatusId es 5! cambiar ****
                    //}
                    //else
                    //{
                    //    deshabilitarAcceso();
                    //}
                }

                else if (action == 5) //Si se desea visualizar la solicitud PERO sin boton Eliminar:
                {
                    PanelModificarComplementar.Visible = false;

                    //convertir columna de respuesta a solo texto
                    grvEtiquetas.Columns[3].Visible = false;
                    grvEtiquetas.Columns[4].Visible = true;
                }

                else //si solo se desea visualizar la solicitud:
                {
                    if (perf == 1 | perf == 4) //administradores = 1, solicitantes = 4
                    {
                        btnEliminar.Visible = lblEstatusId.Text == "1" ? true : false; // 1 = En Visto Bueno
                        btnModificar.Visible = lblEstatusId.Text == "3" ? true : false; // 3 = Rechazada por Autorizador
                    }

                    btnGuardar.Visible = false;

                    //convertir columna de respuesta a solo texto
                    grvEtiquetas.Columns[3].Visible = false;
                    grvEtiquetas.Columns[4].Visible = true;
                }
            }

            else
            {
                updMain.Visible = false;
                MostrarMensaje("No se especificó un ID de solicitud!");
                return;
            }
        }
    }

    protected void deshabilitarAcceso()
    {
        updMain.Visible = false;
        MostrarMensaje("El acceso no está permitido para su tipo de perfil.");
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        CargarArchivo();
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        EliminarSolicitud(solicitudId);
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (solicitudId > 0)
        {
            Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + solicitudId.ToString() + "&action=1");
        }
    }

    protected void btnRechazar_Click(object sender, EventArgs e)
    {
        if (PanelMotivoRechazo.Visible == false)
        {
            CargarddlMotivo();
            PanelMotivoRechazo.Visible = true;
            return;
        }

        if (TxtMotivo.Text.Length < 1)
        {
            MostrarMensaje("Por favor escriba una observación.");
            return;
        }

        if (ddlMotivo.SelectedValue == "0")
        {
            MostrarMensaje("Por favor seleccione un motivo.");
            return;
        }

        bool res = DataAcces.RechazarSolicitud(solicitudId, 3, ToInt32_0(ddlMotivo.SelectedValue), TxtMotivo.Text);

        if (res)
        {
            MostrarMensaje("Se ha rechazado la solicitud.");
            PanelBotonesAutorizar.Visible = false;
            PanelMotivoRechazo.Visible = false;
            lblEstatus.Text = "Rechazada por Autorizador";
        }

        else
        {
            MostrarMensaje("Ocurrió un problema al intentar rechazar la solicitud.");
        }
    }

    protected void btnAutorizar_Click(object sender, EventArgs e) //Boton Visto Bueno
    {
        int usuarioId = ToInt32_0(Session["idUsuario"]);


        string validacion = DataAcces.ValidaSolicitudVoboPlantilla_sUp(solicitudId);

        switch (validacion)
        {
            case "0":
                bool res = DataAcces.UpdSolicitudStatusId(solicitudId, 2, usuarioId);

                if (res)
                {
                    MostrarMensaje("Se ha dado el visto bueno a la solicitud.");
                    btnRechazar.Visible = false;
                    btnAutorizar.Visible = false;
                    lblEstatus.Text = "Autorizada";
                }

                else
                {
                    MostrarMensaje("Ocurrió un problema al intentar dar visto bueno a la solicitud.");
                }
                break;
            case "1":
                MostrarMensaje("Solicitud requiere visto bueno adicional.Favor de generalo y autorizarlos. ");
                break;
            case "2":
                MostrarMensaje("Solicitud requiere que todas las áreas involuvradas den su visto bueno.Favor de solicitarlo a las areas pendientes.");
                break;
            default:
                MostrarMensaje("Ocurrió un problema al intentar dar visto bueno a la solicitud.");
                break;
        }


    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        List<SolicitudEtiqueta> etiquetas;
        List<ArchivoSolicitud> archivos;
        List<ArchivoSolicitud> archivosNuevos = new List<ArchivoSolicitud>();
        List<ArchivoSolicitud> archivosEliminados = (List<ArchivoSolicitud>)Session["archivosEliminados"];
        int res;
        byte[] archivo;
        PlantillaArchivo plantilla;
        string Folio = null;
        int Consecutivo;


        if (grvDocumentos.Rows.Count == 0)
        {
            MostrarMensaje("Debe cargar al menos un archivo.");
            return;
        }


        Folio = hdnFolio.Value;


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

        foreach (ArchivoSolicitud ar in archivos)
        {
            if (ar.EsNuevo == true)
            {
                archivosNuevos.Add(new ArchivoSolicitud()
                {
                    IdTipoDocumento = ar.IdTipoDocumento,
                    Nombre = ar.Nombre,
                    Archivo = ar.Archivo
                });
            }
        }

        plantilla = DataAcces.GetPlantillaArchivo(Convert.ToInt32(hdnPlantillaId.Value));

        Exception ex1 = new Exception();

        archivo = ODataAcces.ReplaceOpenFormat(etiquetas, plantilla, ref ex1); //office file processing thing

        if (archivo == null)
        {
            string error = "Error al integrar etiquetas en archivo plantilla de Office Word. " + ex1.Message;
            MostrarMensaje(error);
            errorMsg.Text += ex1.Message;
            return;
        }

        int usuarioId = ToInt32_0(Session["idUsuario"]);

        if (action == 1) //si se desea editar, cambiar statusId a "en visto bueno"
        {
            DataAcces.UpdSolicitudStatusId(solicitudId, 1, usuarioId);
        }
        if (action == 2) //si se desea complementar (es decir, editar + cambiar statusId a "complementada")
        {
            DataAcces.UpdSolicitudStatusId(solicitudId, 11, usuarioId);
        }


        res = DataAcces.ComplementarModificarSolicitud(action, solicitudId, etiquetas, archivo, archivosNuevos, archivosEliminados);
        Consecutivo = res;

        if (res > 0)
        {
            if (action == 1) //modificar
            {
                MostrarMensaje("La operación fue exitosa. Consulta en tu opción del módulo \"<a href=\"/Solicitudes/Consultar.aspx\">Mis Solicitudes</a>\" para darle el seguimiento correspondiente");
            }

            if (action == 2) //complementar
            {
                MostrarMensaje(
                String.Format("Folio modificado a: {0}/{1}/{2}. La operación fue exitosa. Consulta en tu opción del módulo \"<a href=\"/Solicitudes/Consultar.aspx\">Mis Solicitudes</a>\" para darle el seguimiento correspondiente",
                Folio, Convert.ToInt32(Consecutivo.ToString("D2")), lblFechaSolicitud.Text.Substring(0, 4))
                );
            }
        }
        else
        {
            MostrarMensaje("Ocurrió un error al crear la solicitud.");
        }


        grvEtiquetas.Enabled = false;
        grvDocumentos.Enabled = false;

        tblAdjuntarDocumentos.Visible = false;

        btnGuardar.Visible = false;


        //notificar al abogado que la solicitud fue complementada
        //(no se puede poner aun porque no viene en el caso de uso, “4ConsultarYComplementarMisSolicitudes_ECU(2)(22Ago2014).doc”.
        //Karla Zendejas preguntó si se enviaba un correo al abogado al complementar


        ////send out email:
        //Usuario usuario = DataAcces.GetSpecificUserInfo(SelectedAbogadoUserId);

        //if (IsValidEmail(usuario.Email))
        //{
        //    string folio = hdnFolio.Value;
        //    string link = String.Format("<a href='{0}/Solicitudes/ConsultarSolicitud.aspx?id={1}&action=5'>{2}</a>", sistemaURL, solicitudId.ToString(), folio);
        //    string email = usuario.Email;
        //    string subject = "COMPLEMENTACIÓN DE SOLICITUD";
        //    string body = String.Format("Atención,<br>Se ha complementado la solicitud {0} la cual se encuentra asignada a usted.", link);

        //    EnviarCorreo(email, subject, body);
        //}
        //else
        //{
        //    MostrarMensage("La dirección de email configurada para este abogado no es válida. No se puede enviar notificación.");
        //}


    }

    protected void grvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvDocumentos, "Select$" + e.Row.RowIndex);
            //e.Row.Attributes["style"] = "cursor:pointer";

            e.Row.Cells[3].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvDocumentos, "Select$" + e.Row.RowIndex);
            e.Row.Cells[3].Attributes["style"] = "cursor:pointer";
            e.Row.Cells[3].ToolTip = "Descargar El Archivo";

            if (action == 1 || action == 2)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                btnDelete.Visible = true;
            }
        }
    }

    protected void grvDocumentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int IdArchivoSolicitud = ToInt32_0(((Label)grvDocumentos.Rows[grvDocumentos.SelectedIndex].FindControl("lblDocumentoId")).Text);
        string ArchivoFileName = ((Label)grvDocumentos.Rows[grvDocumentos.SelectedIndex].FindControl("lblArchivoPath")).Text;

        DownloadFile(IdArchivoSolicitud, ArchivoFileName);
    }

    protected void DownloadFile(int IdArchivoSolicitud, string ArchivoFileName)
    {
        byte[] archivoBinaryFile = null;
        string FileName = "";

        if (IdArchivoSolicitud > 0)
        {
            DescargarArchivoDesdeDB(IdArchivoSolicitud);
        }

        else  // si IdArchivoSolicitud no es > 0, es porque el archivo es nuevo, y solo existe en Sesion como un byte[]
        {
            List<ArchivoSolicitud> archivos = (List<ArchivoSolicitud>)Session["archivos"];


            foreach (ArchivoSolicitud ar in archivos)
            {
                if (ar.EsNuevo == true)
                {
                    if (ar.Nombre == ArchivoFileName)
                    {
                        archivoBinaryFile = ar.Archivo; //si hay mas de 1 con el mismo nombre, agarrar el primero que se encuentre
                        FileName = ar.Nombre;
                        break;
                    }
                }
            }

            DescargarArchivoBinario(archivoBinaryFile, FileName);
        }
    }

    protected void grvDocumentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //requerida
    }

    protected void grvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = (GridViewRow)grvDocumentos.Rows[index];

        int documentoId = Convert.ToInt32(((Label)row.FindControl("lblDocumentoId")).Text);

        if (e.CommandName == "Delete")
        {
            //copiar id archivo a lista de archivos por eliminar:    
            List<ArchivoSolicitud> archivosEliminados = new List<ArchivoSolicitud>();


            if (Session["archivosEliminados"] != null)
            {
                archivosEliminados = (List<ArchivoSolicitud>)Session["archivosEliminados"];
            }

            archivosEliminados.Add(new ArchivoSolicitud()
            {
                IdArchivoSolicitud = documentoId
            });

            Session["archivosEliminados"] = archivosEliminados;


            //quitar row del archivo del grid en sesion:
            List<ArchivoSolicitud> archivos = (List<ArchivoSolicitud>)Session["archivos"];
            List<ArchivoSolicitud> archivos_n = new List<ArchivoSolicitud>();

            if (ToInt32_0(archivos.Count) > 1)
            {
                foreach (ArchivoSolicitud ar in archivos)
                {
                    if (ar.Id != documentoId)
                    {
                        archivos_n.Add(new ArchivoSolicitud()
                        {
                            Id = ar.Id,
                            IdArchivoSolicitud = ar.IdArchivoSolicitud,
                            IdSolicitud = ar.IdSolicitud,
                            Nombre = ar.Nombre,
                            Archivo = ar.Archivo,
                            EsNuevo = ar.EsNuevo,
                            IdTipoDocumento = ar.IdTipoDocumento,
                            TipoDocumento = ar.TipoDocumento
                        });
                    }
                }
            }

            //recargar grid despues de borrar archivo:
            grvDocumentos.DataSource = archivos_n;
            grvDocumentos.DataBind();

            Session["archivos"] = archivos_n;
        }
        if (e.CommandName == "Download")
        {
            int IdArchivoSolicitud = documentoId;
            string ArchivoFileName = ((Label)row.FindControl("lblArchivoPath")).Text;

            DownloadFile(IdArchivoSolicitud, ArchivoFileName);
        }
    }

    #endregion

    #region Metodos

    private void CargarTipoArchivo(int tipoPlantilla)
    {
        ddlTipoDocumento.DataSource = DataAcces.GetTipoDocumento(tipoPlantilla);
        ddlTipoDocumento.DataTextField = "Descripcion";
        ddlTipoDocumento.DataValueField = "Id";

        ddlTipoDocumento.DataBind();
    }

    public void CargarArchivo()
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

                archivos.Add(new ArchivoSolicitud()
                {
                    Nombre = fi.Name,
                    IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                    TipoDocumento = ddlTipoDocumento.SelectedItem.Text,
                    Archivo = fluDocumento.FileBytes,
                    EsNuevo = true //linea especial
                });

                grvDocumentos.DataSource = archivos;
                grvDocumentos.DataBind();
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

    public void DescargarArchivoBinario(byte[] archivoBinaryFile, string FileName)
    {
        DescargarDocumentosArchivoTemporal(archivoBinaryFile, FileName);
    }

    public void DescargarArchivoDesdeDB(int IdArchivoSolicitud)
    {
        DescargarDocumentos(3, IdArchivoSolicitud);
    }

    private void CargarSolicitud(int solicitudId)
    {
        Solicitud sol = DataAcces.GetSolicitud(solicitudId);

        if (sol == null)
        {
            MostrarMensaje("No se encontró la solicitud!");
            return;
        }

        else
        {
            if (action == 1 || action == 2)
            {
                CargarTipoArchivo(sol.IdTipoPlantilla); //llena drop-down de tipo de documento
            }

            MostrarDetallesSolicitud(sol);
            return;
        }
    }

    private void CargarddlMotivo()
    {
        List<MotivoRechazo> MotivosRechazo;

        MotivosRechazo = DataAcces.GetMotivosRechazo();

        if (MotivosRechazo.Count > 0)
        {
            ddlMotivo.DataSource = MotivosRechazo;
            ddlMotivo.DataTextField = "Descripcion";
            ddlMotivo.DataValueField = "IdMotivoRechazo";
            ddlMotivo.DataBind();
            ddlMotivo.Items.Insert(0, new ListItem("-- Elige un motivo para rechazar --", "0"));
            ddlMotivo.SelectedValue = "0";
        }
        else
        {
            ddlMotivo.Enabled = false;
            ddlMotivo.Text = "Error al cargar datos";
        }
    }

    private void MostrarDetallesSolicitud(Solicitud sol)
    {
        if (action == 2 && sol.IdStatus == 11) // si se esta intentando complementar pero ya está complementada:
        {
            MostrarMensaje("Esta solicitud ya está complementada.");

            btnEliminar.Visible = false;
            btnModificar.Visible = false;
            btnGuardar.Visible = false;
            grvEtiquetas.Enabled = false;

            //return;
        }

        List<int> StatusPermitidos = new List<int> { 1, 11 };

        if ((!StatusPermitidos.Contains(sol.IdStatus)) && action == 3) // si se esta intentando rechazar o autorizar pero no está en estatus visto bueno (o complementada):
        {
            MostrarMensaje("Esta solicitud no está en el estatus requerido para permitir rechazar o autorizar.");
            PanelBotonesAutorizar.Visible = false;
        }

        if (sol.IdStatus == 6) //si la solicitud está marcada como eliminada, ya no mostrar boton eliminar:
        {
            btnEliminar.Visible = false;
        }

        List<string> status;

        status = new List<string>() { "", "", "" };

        lblNombrePlantilla.Text = sol.Tipo;
        lblTipo.Text = sol.Clasificacion;
        lblDesc.Text = sol.Descripcion;
        lblSolicitante.Text = sol.Solicitante;
        lblFechaSolicitud.Text = sol.Fecha.HasValue ? sol.Fecha.Value.ToString("yyyy-MM-dd") : " - ";
        lblEstatus.Text = sol.Status;
        lblEstatusId.Text = sol.IdStatus.ToString();
        lblPageTitle.Text = "Detalle De Solicitud - Folio " + sol.Folio + "/" + sol.Consecutivo + "/" + sol.Fecha.Value.Year.ToString();
        hdnPlantillaId.Value = sol.IdPlantilla.ToString();
        hdnFolio.Value = sol.Folio;

        if (sol.IdStatus != 3) // 3 = Rechazada por autorizador (mostrar boton modificar solo con este estatus)
        {
            btnModificar.Visible = false;
        }

        grvEtiquetas.DataSource = sol.Etiquetas;
        grvEtiquetas.DataBind();



        List<int> statusIdsNoPermitidos = new List<int> { 3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }; //removed: 5, 

        if (statusIdsNoPermitidos.Contains(sol.IdStatus))
        {
            btnEliminar.Visible = false;
        }

        else if (sol.IdStatus == 1 || sol.IdStatus == 2) // 1 = En Visto Bueno, 2 = Autorizada
        {
            btnEliminar.Enabled = true;
        }
    }

    private void EliminarSolicitud(int solicitudId)
    {
        //if (DataAcces.DelSolicitud(Convert.ToInt32(hdnSolicitud.Value))) //funcion que se usaba previamente
        //{
        //    MostrarMensage("La solicitud ha sido eliminada.");            
        //}

        int usuarioId = ToInt32_0(Session["idUsuario"]);

        if (DataAcces.UpdSolicitudStatusId(solicitudId, 6, usuarioId))
        {
            MostrarMensaje("La solicitud ha sido marcada como eliminada.");
        }
        else
        {
            MostrarMensaje("Ocurrio un error al eliminar la solicitud.");
        }
    }

    private void CargarGridDocumentos(int solicitudId)
    {
        ConsultaSolicitud sol;

        sol = DataAcces.llenadoConsultaSolicitud(solicitudId);

        Session["archivos"] = sol.Archivos;

        grvDocumentos.DataSource = sol.Archivos;
        grvDocumentos.DataBind();
    }

    #endregion

}

// ***** Begin disabled code ***** //

//protected void CargarRblAbogado()
//{
//    DataTable dt = ConvertToDataTable(DataAcces.GetAbogado());
//    RblAbogado.DataSource = dt;
//    RblAbogado.DataTextField = "Nombre";
//    RblAbogado.DataValueField = "Id1";
//    RblAbogado.DataBind();
//}

//protected void BtnAbogadoRechazar_Click(object sender, EventArgs e)
//{
//    //esto se necesita activar desde una pregunta yes/no *****

//    bool res = DataAcces.UpdSolicitudStatusId(solicitudId, 4);

//    if (res)
//    {
//        MostrarMensage("Se ha rechazado la solicitud.");
//        PanelBotonesAbogado.Visible = false;
//        lblEstatus.Text = "Rechazada en Jurídico";
//    }

//    else
//    {
//        MostrarMensage("Ocurrió un problema al intentar rechazar la solicitud.");
//    }
//}

//protected void BtnAbogadoAsignar_Click(object sender, EventArgs e)
//{
//    if (PanelAsignarAbogado.Visible == false)
//    {
//        CargarRblAbogado();
//        PanelAsignarAbogado.Visible = true;
//        BtnAbogadoRechazar.Visible = false;
//        return;
//    }

//    if (RblAbogado.SelectedValue == "0")
//    {
//        MostrarMensage("Por favor seleccione un abogado.");
//        return;
//    }

//    int result = DataAcces.UpdSolicitudAsignado(solicitudId, Convert.ToInt32(RblAbogado.SelectedValue));
//    if (result > 0)
//    {
//        MostrarMensage("Se ha asignado la solicitud.");
//        PanelBotonesAbogado.Visible = false;
//        PanelAsignarAbogado.Visible = false;

//        switch (result) //status depende si la plantilla es de poderes o contratos
//        {
//            case 19: lblEstatus.Text = "En Creación";
//                break;
//            case 20: lblEstatus.Text = "En Proceso";
//                break;
//        }
//    }

//    else
//    {
//        MostrarMensage("Ocurrió un problema al intentar asignar la solicitud.");
//    }
//}

// ***** End disabled code ***** //

//private void RechazarSolicitudJuridico(int idStatus, int solicitudId)
//{
//    //funcion boton rechazar solicitud

//    if (DataAcces.UpdSolicitudStatusId(solicitudId, idStatus))
//    {
//        if (idStatus == 2)
//        {
//            MostrarMensage("Se ha rechazado la solicitud.");
//        }
//        if (idStatus == 4)
//        {
//            MostrarMensage("Se ha asignado la solicitud.");
//        }
//    }
//    //CargarDatos();
//}

//protected void BtnAsignarAbogado_Click(object sender, EventArgs e)
//{
//int total = 0;
//for (int i = 0; i < ChkAbogado.Items.Count; i++)
//{
//    if (ChkAbogado.Items[i].Selected)
//    {
//        total += 1;

//    }
//}

//if (total > 1)
//{
//    MostrarMensage("solo debe de seleccionar un Abogado");
//}
//else
//{
//    if (total == 0)
//    {
//        MostrarMensage("solo debe de seleccionar a un Abogado");
//    }
//    else
//    { 
//int id = Convert.ToInt32(ChkAbogado.Items[1].Value.ToString());
//RechazarSolicitudJuridico(4, solicitudId);
//ActualizarAsignado(id, solicitudId);
////}

//}
//CargarDatos();
//}

