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

public partial class Procesos_RecepcionAsig : PaginaBase
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Recepción y Asignación de Solicitudes");

        int perf = ToInt32_0(Session["perfil"]);
     
        if (perf == 1 | perf == 2 | perf == 3) //admin general, asis poderes, asis contratos. Caso #6
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

    //protected void grvSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int solicitudId = ToInt32_0(((Label)grvSolicitudes.Rows[grvSolicitudes.SelectedIndex].FindControl("lblSolicitudId")).Text);

    //    if (solicitudId > 0)
    //        ModoVista(solicitudId);
    //}

    protected void grvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvSolicitudes, "Select$" + e.Row.RowIndex);
            //e.Row.Attributes["style"] = "cursor:pointer";

            int idStatus = Convert.ToInt32(((Label)e.Row.FindControl("lblStatusId")).Text);


            LinkButton btnRechazar = new LinkButton();
            btnRechazar = (LinkButton)e.Row.FindControl("btnRechazar");

            LinkButton btnAsignar = new LinkButton();
            btnAsignar = (LinkButton)e.Row.FindControl("btnAsignar");

            if (idStatus == 4 | idStatus == 19 | idStatus == 20) //4 = Rechazada en Jurídico, 19 = En Creación, 20 = En Proceso
            {
                btnRechazar.Visible = false;
                btnAsignar.Visible = false;
            }
        }
    }

    protected void grvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSolicitudes.PageIndex = e.NewPageIndex;
        CargarGridSolicitudes(false);
    }

    protected void grvSolicitudes_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["DTGridSolicitudesAsig"] as DataTable;

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

    protected void grvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerSolicitud")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            //Redirigir a Solicitudes2.aspx para visualizar Solicitud
            Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + SolicitudId.ToString() + "&action=5");
        }

        if (e.CommandName == "Rechazar")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            hdnSolicitudId.Value = SolicitudId.ToString();
            PanelRechazar.Visible = true;
            PanelAsignarAbogado.Visible = false;
        }

        if (e.CommandName == "Asignar")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            hdnSolicitudId.Value = SolicitudId.ToString();
            hdnFolio.Value = ((Label)row.FindControl("lblFolio")).Text;
            CargarRblAbogado();
            PanelAsignarAbogado.Visible = true;
            PanelRechazar.Visible = false;
        }

        //if (e.CommandName == "RechazarAsignar")
        //{
        //    //Redirigir a Solicitudes2.aspx para visualizar Solicitud
        //    Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + Convert.ToInt32(e.CommandArgument).ToString() + "&action=4");
        //}
    }

    //protected void SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int solicitudId = ToInt32_0(((Label)grvSolicitudes.Rows[grvSolicitudes.SelectedIndex].FindControl("lblSolicitudId")).Text);

    //    if (solicitudId > 0)
    //    ModoVista(solicitudId);
    //}

    protected void CargarRblAbogado()
    {
        DataTable dt = ConvertToDataTable(DataAcces.GetAbogado());
        RblAbogado.DataSource = dt;
        RblAbogado.DataTextField = "Nombre";
        RblAbogado.DataValueField = "Id1";
        RblAbogado.DataBind();
    }

    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        PanelRechazar.Visible = false;
        hdnSolicitudId.Value = "";
    }

    protected void BtnAbogadoRechazar_Click(object sender, EventArgs e)
    {
        int solicitudId = ToInt32_0(hdnSolicitudId.Value);

        if (solicitudId > 0)
        AbogadoRechazar(solicitudId);
    }

    protected void AbogadoRechazar(int solicitudId)
    {
        int usuarioId = ToInt32_0(Session["idUsuario"]);

        bool res = DataAcces.UpdSolicitudStatusId(solicitudId, 4, usuarioId);

        if (res)
        {
            CargarGridSolicitudes(true);
            MostrarMensaje("Se ha rechazado la solicitud.");
            PanelRechazar.Visible = false;
            hdnSolicitudId.Value = "";
        }

        else
        {
            MostrarMensaje("Ocurrió un problema al intentar rechazar la solicitud.");
        }
    }

    protected void BtnAbogadoAsignar_Click(object sender, EventArgs e)
    {
        if (RblAbogado.SelectedValue == "")
        {
            MostrarMensaje("Por favor seleccione un abogado.");
            return;
        }

        int solicitudId = ToInt32_0(hdnSolicitudId.Value);
        int SelectedAbogadoUserId = Convert.ToInt32(RblAbogado.SelectedValue);
        int result = DataAcces.UpdSolicitudAsignado(solicitudId, SelectedAbogadoUserId);


        if (result > 0)
        {
            CargarGridSolicitudes(true);
            MostrarMensaje("Se ha asignado la solicitud.");
            PanelAsignarAbogado.Visible = false;
            hdnSolicitudId.Value = "";
        }

        else
        {
            MostrarMensaje("Ocurrió un problema al intentar asignar la solicitud.");
        }

        //send out email:
        Usuario usuario = DataAcces.GetSpecificUserInfo(SelectedAbogadoUserId);

        if (IsValidEmail(usuario.Email))
        {
            string folio = hdnFolio.Value;
            string link = String.Format("<a href='{0}/Solicitudes/ConsultarSolicitud.aspx?id={1}&action=5'>{2}</a>", sistemaURL, solicitudId.ToString(), folio);
            string email = usuario.Email;
            string subject = "ASIGNACIÓN DE FOLIO DE SOLICITUD";
            string body = String.Format("Atención,<br>Se le ha asignado el folio de solicitud {0} para su seguimiento.", link);

            EnviarCorreo(email, subject, body);
        }
        else
        {
            MostrarMensaje("La dirección de email configurada para este abogado no es válida. No se puede enviar notificación.");
        }
    }

    #endregion

    #region Metodos

    private void CargarGridSolicitudes(bool RefreshGrid)
    {
        if (!Page.IsPostBack || (RefreshGrid))
        {   //borrar datatable del grid si se está recargando la página para que muestre los datos mas recientes:
            Session["DTGridSolicitudesAsig"] = null;
        }

        int idUsuario = ToInt32_0(Session["idUsuario"]);
        int id_nperfil = ToInt32_0(Session["Perfil"]);

        if (idUsuario > 0)
        {
            DataTable dt = new DataTable();

            if (Session["DTGridSolicitudesAsig"] == null)
            {
                dt = ConvertToDataTable(DataAcces.GetSolicitudesRecep(idUsuario, id_nperfil));
                Session["DTGridSolicitudesAsig"] = dt;
            }

            else
            {
                dt = (DataTable)Session["DTGridSolicitudesAsig"];
            }

            if (dt.Rows.Count > 0)
            {
                grvSolicitudes.DataSource = dt;
                grvSolicitudes.DataBind();
            }

            else
            {
                MostrarMensaje("No se encontraron solicitudes para asignar.");
            }
        }
    }

    //private void ModoVista(int solicitudId)
    //{
    //    Response.Redirect("~/Solicitudes/ConsultaSolicitudLectura.aspx?sol=" + solicitudId.ToString());
    //}

    //protected void RevisarSolicitud(object sender, GridViewEditEventArgs e)
    //{
    //    int solicitudId;

    //    if (string.IsNullOrEmpty(((Label)grvSolicitudes.Rows[e.NewEditIndex].FindControl("lblSolicitudId")).Text))
    //    {
    //        return;
    //    }

    //    solicitudId = Convert.ToInt32(((Label)grvSolicitudes.Rows[e.NewEditIndex].FindControl("lblSolicitudId")).Text);


    //    Consultar(solicitudId);
    //    //Response.Redirect("~/Solicitudes/Solicitudes.aspx?plantilla=" + idPlantilla.ToString());
    //}

    //private void CargarDatos()
    //{
    //    Panel1.Visible = false;
    //    List<Solicitud> datos;
    //    int idUsuario;

    //    if (Session["idUsuario"] == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        int.TryParse(Session["idUsuario"].ToString(), out idUsuario);
    //    }

    //    datos = DataAcces.GetSolicitudesPorUsuario(idUsuario);

    //    Session["solicitudes"] = datos;

    //    grvSolicitudes.SelectedIndex = -1;
    //    grvSolicitudes.EditIndex = -1;

    //    grvSolicitudes.DataSource = datos;
    //    grvSolicitudes.DataBind();

    //    tblConsulta.Visible = false;
    //    tblSelSolicitud.Visible = true;
    //    Asignacion.Visible = false;
    //}

    //private void Consultar(int id)
    //{
    //    Solicitud sol;

    //    sol = DataAcces.GetSolicitud(id);

    //    if (sol != null)
    //    {
    //        CargarSolicitud(sol);
    //        return;
    //    }

    //    MostrarMensage("No se encontro la solicitud!");
    //}

    //private void CargarSolicitud(Solicitud sol)
    //{
    //    List<string> status;

    //    status = new List<string>() { "", "", "" };

    //    lblTipo.Text = sol.Tipo;
    //    lblDesc.Text = sol.Descripcion;
    //    lblSolicitante.Text = sol.Solicitante;
    //    lblFecha.Text = sol.Fecha.HasValue ? sol.Fecha.Value.ToString("yyyy-MM-dd") : " - ";
    //    lblEstatus.Text = sol.Status;
    //    lblFolio.Text = string.Format(lblFolio.Text, sol.Folio);

    //    grvEtiquetas.DataSource = sol.Etiquetas;
    //    grvEtiquetas.DataBind();

    //    tblConsulta.Visible = true;
    //    tblSelSolicitud.Visible = false;

    //    hdnSolicitudId.Value = sol.Id.ToString();
    //}

    #endregion

}