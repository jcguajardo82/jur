using DACJuridico;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Procesos_Bitacora : PaginaBase
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Bitácora");

        int perf = ToInt32_0(Session["perfil"]);

        if (perf == 1 | perf == 2 | perf == 6) //admin general, asis poderes, abogados. Caso #8
        {
            CargarGridSolicitudes(true);

        }
        else
        {
            deshabilitarAcceso();
        }
    }

    protected void deshabilitarAcceso()
    {
        updMain.Visible = false;
        MostrarMensaje("El acceso no está permitido para su tipo de perfil.");
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        updMain.Visible = true;
        PanelBitacora.Visible = false;
        PanelOtorganteEtiqueta.Visible = false;
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        limpiarCampos();
    }

    protected void limpiarCampos()
    {
        TxtComentario.Text = "";
        ddlEstatus.SelectedIndex = 0;
        txtFecha.Text = DateTime.Today.ToString("MM/dd/yyyy");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        //save changes, save new status, save comment, then display folio proceso on textbox

        int solicitudId = ToInt32_0(hdnSolicitud.Value);

        if (ToInt32_0(ddlEstatus.SelectedValue) == 0)
        {
            MostrarMensaje("Debe seleccionar un estatus.");
            return;
        }

        if (TxtComentario.Text == "")
        {
            MostrarMensaje("Debe escribir un comentario.");
            return;
        }

        tbl_Bitacora Bitacora;
        int res = 0;
        string FolioProceso = "";

        Bitacora = new tbl_Bitacora()
        {
            id_solicitud = solicitudId,
            id_usuario = ToInt32_0(Session["idUsuario"]),
            id_status = ToInt32_0(ddlEstatus.SelectedValue),
            fecha = ToDateTime(txtFecha.Text),
            comentarios = TxtComentario.Text
        };

        res = DataAcces.CreateBitacoraRegistro(Bitacora, ref FolioProceso);

        if (res > 0)
        {
            MostrarMensaje("Los cambios han sido guardados a la bitácora");

            CargarGridBitacora(solicitudId);

            if (FolioProceso != "")
            {
                txtFolioProceso.Text = FolioProceso;
            }

            //insertar recordatorio:
            bool res2 = DataAcces.ManageRecordatorios(solicitudId, Bitacora.id_status, 1);

            if (!res2)
            {
                MostrarMensaje("Hubo un problema al guardar el pendiente de aviso recordatorio.");
            }

            //job en SQL Server para aviso recordatorio de inactividad 72 horas *****

            TxtComentario.Text = "";
            ddlEstatus.ClearSelection();
        }
    }

    protected void grvEtiquetasJuridicas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DropDownList ddlOtorganteTestigo = (DropDownList)e.Row.FindControl("ddlOtorganteTestigo");
                Label lbl_id_usuarioLigado = (Label)e.Row.FindControl("lbl_id_usuarioLigado");
                LinkButton btnSeleccionar = (LinkButton)e.Row.FindControl("btnSeleccionar");

                if (Session["ListaOtorgantesTestigos"] == null) {
                    List<Usuario> OtorgantesTestigos = DataAcces.GetOtorgantesTestigos();
                    Session["ListaOtorgantesTestigos"] = OtorgantesTestigos;
                }

                ddlOtorganteTestigo.DataSource = (List<Usuario>)Session["ListaOtorgantesTestigos"];
                ddlOtorganteTestigo.DataTextField = "Nombre";
                ddlOtorganteTestigo.DataValueField = "NEmpleado";
                ddlOtorganteTestigo.DataBind();
                ddlOtorganteTestigo.Items.Insert(0, new ListItem("-- Seleccione un Otorgante ó Testigo --", "0"));
                ddlOtorganteTestigo.SelectedValue = lbl_id_usuarioLigado.Text;

                if (ddlOtorganteTestigo.SelectedValue != "0")
                {
                    btnSeleccionar.Visible = false;
                }
            }

            catch (Exception ex)
            {

            }
        }
    }

    protected void grvEtiquetasJuridicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SeleccionarCampo")
        {
            GridViewRow row = (GridViewRow)grvEtiquetasJuridicas.Rows[Convert.ToInt32(e.CommandArgument)];
            DropDownList ddlOtorganteTestigo = (DropDownList)row.FindControl("ddlOtorganteTestigo");

            LinkButton btnSeleccionar = (LinkButton)row.FindControl("btnSeleccionar");

            if (!ddlOtorganteTestigo.Enabled)
            {
                ddlOtorganteTestigo.Enabled = true;
                btnSeleccionar.Text = "Desactivar";
            }

            else if (ddlOtorganteTestigo.Enabled)
            {
                ddlOtorganteTestigo.Enabled = false;
                ddlOtorganteTestigo.SelectedValue = "0";
                btnSeleccionar.Text = "Activar";
            }
        }
    }

    protected void grvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerSolicitud")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            //Redirigir a Solicitudes2.aspx para visualizar Solicitud
            Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + SolicitudId.ToString() + "&action=5");
        }

        if (e.CommandName == "Ver")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            DescargarDocumentos(2, SolicitudId);
        }

        if (e.CommandName == "Bitacora")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);
            int PlantillaId = Convert.ToInt32(((Label)row.FindControl("lblPlantilla")).Text);

            updMain.Visible = false;
            PanelBitacora.Visible = true;

            //BoundField field = (BoundField)((DataControlFieldCell)row.Cells[5]).ContainingField; //shows header text instead

            //add folio to textbox, from grid
            string Folio = ((Label)row.FindControl("lblFolio")).Text;
            txtFolio.Text = Folio;

            //add plantilla name to textbox, from grid
            string TipoPlantilla = ((Label)row.FindControl("lblTipo")).Text;
            txtTipoPlantilla.Text = TipoPlantilla;

            //add plantilla ID to hidden field:
            hdnPlantillaId.Value = PlantillaId.ToString();

            //load tipo estatuses onto ddl
            cargarDDLEstatus(SolicitudId);

            //copy solicitudId to hidden field
            hdnSolicitud.Value = SolicitudId.ToString();

            txtFecha.Text = DateTime.Today.ToString("MM/dd/yyyy");

            //agregar folio proceso a textbox:
            string FolioProceso = ((Label)row.FindControl("lblFolioProceso")).Text;

            if (FolioProceso != "-") {
                txtFolioProceso.Text = FolioProceso;
            }
            else {
                txtFolioProceso.Text = "-- Pendiente a asignar --";
            }

            PanelBotonEtiquetasOtorgantes.Visible = Folio.Substring(0, 1) == "P" ? true : false; //solo para poderes

            //load Bitacora historical grid, folio proceso ID (if it exists)
            CargarGridBitacora(SolicitudId);
        }
    }

    protected void cargarDDLEstatus(int solicitudId)
    {
        ddlEstatus.DataSource = DataAcces.GetBitacoraStatusDDL(solicitudId);
        ddlEstatus.DataTextField = "Descripcion";
        ddlEstatus.DataValueField = "id_status";
        ddlEstatus.DataBind();
        ddlEstatus.Items.Insert(0, new ListItem("<Seleccione un Estatus>", "0"));
        ddlEstatus.SelectedValue = "0";
    }

    protected void grvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSolicitudes.PageIndex = e.NewPageIndex;
        CargarGridSolicitudes(false);
    }

    private void CargarGridBitacora(int solicitudId)
    {
        int idUsuario = ToInt32_0(Session["idUsuario"]);

        if (idUsuario > 0)
        {
            try
            {
                DataTable dt = new DataTable();

                List<tbl_Bitacora> Bitacora;

                Bitacora = DataAcces.GetBitacoraSolicitud(idUsuario, solicitudId);

                dt = ConvertToDataTable(Bitacora);

                grvBitacora.DataSource = dt;
                grvBitacora.DataBind();
            }

            catch (Exception ex)
            {

            }
        }
    }

    private void CargarGridSolicitudes(bool RefreshGrid)
    {
        if (!Page.IsPostBack || (RefreshGrid))
        {   //borrar datatable del grid si se está recargando la página para que muestre los datos mas recientes:
            Session["DTBitacoraMainGrid"] = null;
        }

        int idUsuario = ToInt32_0(Session["idUsuario"]);
        int id_nperfil = ToInt32_0(Session["Perfil"]);

        if (idUsuario > 0)
        {
            try
            {
                DataTable dt = new DataTable();

                if (Session["DTBitacoraMainGrid"] == null)
                {
                    List<Solicitud> solicitudes;

                    solicitudes = DataAcces.GetSolicitudesBitacora(idUsuario, id_nperfil);

                    if (solicitudes != null)
                    {
                        dt = ConvertToDataTable(solicitudes);

                        Session["DTBitacoraMainGrid"] = dt;
                    }
                }

                else
                {
                    dt = (DataTable)Session["DTBitacoraMainGrid"];
                }

                if (dt.Rows.Count > 0)
                {
                    grvSolicitudes.DataSource = dt;
                    grvSolicitudes.DataBind();
                }
                else
                {
                    MostrarMensaje("No se encontraron solicitudes para consultar.");
                }
            }

            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
        }
    }

    private void CargarGridEtiquetasJuridicas(int solicitudId)
    {
        int idUsuario = ToInt32_0(Session["idUsuario"]);

        if (idUsuario > 0)
        {
            try
            {
                DataTable dt = new DataTable();

                List<tblSolicitudEtiquetas> Etiquetas;

                Etiquetas = DataAcces.GetEtiquetasJuridico(idUsuario, solicitudId);

                dt = ConvertToDataTable(Etiquetas);

                grvEtiquetasJuridicas.DataSource = dt;
                grvEtiquetasJuridicas.DataBind();
            }

            catch (Exception ex)
            {

            }
        }
    }

    protected void grvSolicitudes_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["DTBitacoraMainGrid"] as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (Session["sorting"] != null)
            {
                string sort = getSortDirectionString(Session["sorting"].ToString());
                dataView.Sort = e.SortExpression + " " + sort;
                Session["sorting"] = sort;
            }

            else
            {
                dataView.Sort = e.SortExpression + " " + "ASC";
                Session["sorting"] = "ASC";
            }

            grvSolicitudes.DataSource = dataView;
            grvSolicitudes.DataBind();
        }
    }

    protected void btnEtiquetasOtorgantes_Click(object sender, EventArgs e)
    {
        if (!PanelOtorganteEtiqueta.Visible)
        {
            PanelOtorganteEtiqueta.Visible = true;
            CargarGridEtiquetasJuridicas(Convert.ToInt32(hdnSolicitud.Value));
        }
        else
        {
            PanelOtorganteEtiqueta.Visible = false;
        }
    }

    protected void btnGuardarEtiquetas_Click(object sender, EventArgs e)
    {
        try
        {
            //primero checar que el grid tenga algo que grabar
            List<tblSolicitudEtiquetas> etiquetas = new List<tblSolicitudEtiquetas>();

            foreach (GridViewRow r in grvEtiquetasJuridicas.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    int idUsuarioLigado = ToInt32_0(((DropDownList)r.FindControl("ddlOtorganteTestigo")).SelectedValue);

                    if (idUsuarioLigado > 0)
                    {
                        etiquetas.Add(new tblSolicitudEtiquetas()
                        {
                            IdSolicitudEtiqueta = Convert.ToInt32(((Label)r.FindControl("lblIdSolicitudEtiqueta")).Text),
                            id_usuarioLigado = idUsuarioLigado
                        });
                    }
                }
            }

            //si no se detectó ningun row con cambios:
            if (etiquetas.Count == 0)
            {
                MostrarMensaje("No hay ningún cambio para guardar.");
                return;
            }

            else
            {
                bool res = false;
                res = DataAcces.UpdEtiquetas(etiquetas);

                if (res)
                {
                    ActualizarDocumentoSolicitud();

                    MostrarMensaje("Los nombres de los Otorgantes-Testigos se copiaron a las etiquetas correctamente, y se actualizó el Documento de la Solicitud.");
                    grvEtiquetasJuridicas.Enabled = false;
                    btnEtiquetasOtorgantes.Enabled = false;

                    updMain.Visible = true;
                    PanelBitacora.Visible = false;
                    PanelOtorganteEtiqueta.Visible = false;
                }
            }
        }

        catch (Exception ex)
        {
            
        }
    }

    protected void ActualizarDocumentoSolicitud()
    { 
        //actualiza el documento Word de la solicitud con el contenido nuevo de etiquetas solo juridico.

        List<SolicitudEtiqueta> etiquetas;
        bool res;
        byte[] archivo;
        PlantillaArchivo plantilla;
        int solicitudId = Convert.ToInt32(hdnSolicitud.Value);


        DataTable dt = ConvertToDataTable(DataAcces.GetEtiquetasAll(-1, solicitudId));

        etiquetas = new List<SolicitudEtiqueta>(); //need this datatype


        foreach (DataRow row in dt.Rows)
        {
            etiquetas.Add(new SolicitudEtiqueta()
            {
                IdEtiqueta = ToInt32_0(row["IdSolicitudEtiqueta"]),
                Valor = row["Valor"].ToString(),
                Etiqueta = "&lt;" + row["Pregunta"].ToString().Split('<')[1].Replace(">", "&gt;")
            });
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


        res = DataAcces.UpdateSolicitudDocument(solicitudId, archivo);

        if (res)
        {
            //MostrarMensage("");
        }
        else
        {
            MostrarMensaje("Ocurrió un error al actualizar el documento.");
        }


        ////send out email:
        //Usuario usuario = DataAcces.GetSpecificUserInfo(SelectedAbogadoUserId);

        //if (IsValidEmail(usuario.Email))
        //{
        //    string folio = hdnFolio.Value;
        //    string link = String.Format("<a href='{0}/Solicitudes/ConsultarSolicitud.aspx?id={1}&action=5'>{2}</a>", sistemaURL, solicitudId.ToString(), folio);
        //    string email = usuario.Email;
        //    string subject = "ACTUALIZACIÓN DE SOLICITUD";
        //    string body = String.Format("Atención,<br>Se ha actualizado la solicitud {0} la cual se encuentra asignada a usted.", link);

        //    EnviarCorreo(email, subject, body);
        //}
        //else
        //{
        //    MostrarMensage("La dirección de email configurada para este abogado no es válida. No se puede enviar notificación.");
        //}


    }

    #endregion

    #region Metodos

    //falta organizar

    #endregion
}