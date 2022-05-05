using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes_Autorizar : PaginaBase
{
    #region Metodos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Autorizar Solicitudes");

        CargarGridSolicitudes(true);
    }

    private void CargarGridSolicitudes(bool RefreshGrid)
    {
        if (!Page.IsPostBack || (RefreshGrid))
        {   //borrar datatable del grid si se está recargando la página para que muestre los datos mas recientes:
            Session["DTGridSolicitudesAut"] = null;
        }

        int idUsuario = ToInt32_0(Session["idUsuario"]);
        int id_nperfil = ToInt32_0(Session["Perfil"]);

        if (idUsuario > 0)
        {
            DataTable dt = new DataTable();

            if (Session["DTGridSolicitudesAut"] == null)
            {
                dt = ConvertToDataTable(DataAcces.GetSolicitudesAutorizablesPorUsuario(idUsuario, id_nperfil));
                Session["DTGridSolicitudesAut"] = dt;
            }

            else
            {
                dt = (DataTable)Session["DTGridSolicitudesAut"];
            }

            if (dt.Rows.Count > 0)
            {
                grvSolicitudes.DataSource = dt;
                grvSolicitudes.DataBind();
                grvSolicitudes.SelectedIndex = -1;
            }

            else
            {
                MostrarMensaje("No se encontraron solicitudes para autorizar.");
            }
        }
    }


    #endregion

    #region Eventos

    protected void grvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSolicitudes.PageIndex = e.NewPageIndex;
        CargarGridSolicitudes(false);
    }

    protected void grvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerSolicitud")
        {
            //Redirigir a Solicitudes2.aspx para visualizar Solicitud
            Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + Convert.ToInt32(e.CommandArgument).ToString());
        }

        if (e.CommandName == "Revisar")
        {
            //Redirigir a Solicitudes2.aspx para revisar Solicitud (para rechazar o dar visto bueno)
            Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + Convert.ToInt32(e.CommandArgument).ToString() + "&action=3");
        }
    }

    protected void grvSolicitudes_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["DTGridSolicitudesAut"] as DataTable;

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

    protected void grvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvSolicitudes, "Select$" + e.Row.RowIndex);
            //e.Row.Attributes["style"] = "cursor:pointer";

            int idStatus = Convert.ToInt32(((Label)e.Row.FindControl("lblStatusId")).Text);

            LinkButton btnRevisar = new LinkButton();
            btnRevisar = (LinkButton)e.Row.FindControl("btnRevisar");

            if (idStatus == 1 | idStatus == 11) // status: 1 "En Visto Bueno", 11 = Complementada
            {
                btnRevisar.Visible = true;
            }
            else
            {
                btnRevisar.Visible = false;
            }
        }
    }

    //protected void btnRechazar_Click(object sender, EventArgs e)
    //{
    //    ActualizarSolicitud(3);
    //}

    //protected void btnAutorizar_Click(object sender, EventArgs e)
    //{
    //    ActualizarSolicitud(2);
    //}

    //protected void btnRegresar_Click(object sender, EventArgs e)
    //{
    //    CargarGridSolicitudes(true);
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

    //    MostrarMensage("No se encontró la solicitud!");
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

    //private void ModoVista(int solicitudId)
    //{
    //    Response.Redirect("~/Solicitudes/ConsultaSolicitudLectura.aspx?sol=" + solicitudId.ToString());
    //}

    //protected void grvSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int solicitudId = ToInt32_0(((Label)grvSolicitudes.Rows[grvSolicitudes.SelectedIndex].FindControl("lblSolicitudId")).Text);

    //    if (solicitudId > 0)
    //    ModoVista(solicitudId);
    //}

    #endregion


}