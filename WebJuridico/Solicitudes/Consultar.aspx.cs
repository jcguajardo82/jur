using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Solicitudes_Consultar : PaginaBase
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Mis Solicitudes");

        int perf = ToInt32_0(Session["perfil"]);

        List<int> PerfilesPermitidos = new List<int> { 1, 4 }; // “AdmGral” y “Solicitador” 

        if (PerfilesPermitidos.Contains(perf))
        {
            CargarGridSolicitudes(true);
        }
        else
        {
            lblTitulo.Text = "";
            //MostrarMensage("El acceso no está permitido para su tipo de perfil.");
            return;
        }
    }

    protected void grvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Cargar Linea Solicitud
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvSolicitudes, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";

            try
            {
                int idStatus = Convert.ToInt32(((Label)e.Row.FindControl("lblStatusId")).Text);

                LinkButton btnComplementar = new LinkButton();
                btnComplementar = (LinkButton)e.Row.FindControl("btnComplementar");

                //if (idStatus == 1 || idStatus == 11) // status: 1 (en "visto bueno"), 11 ("Complementada"), no mostrar
                //{
                //    btnComplementar.Visible = false;
                //}

                List<int> StatusIDsPermitidos = new List<int> { 9, 10 };

                if (StatusIDsPermitidos.Contains(idStatus)) // status: 9 ("Falta Información"), 10 ("Entrevista")
                {
                    btnComplementar.Visible = true;
                }
                else
                {
                    btnComplementar.Visible = false;
                }

            }
            catch { }
        }
    }

    protected void grvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerSolicitud")
        {
            int solicitudId = Convert.ToInt32(e.CommandArgument);

            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(gvr.RowIndex);

            //Reference the GridView Row.
            GridViewRow row = grvSolicitudes.Rows[rowIndex];

            //Fetch value of Name.
            var estatusPrev = grvSolicitudes.Rows[rowIndex].Cells[8].Text;

            if (estatusPrev.Equals("Rechazado"))
            {
                Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?action=1&id=" + solicitudId.ToString());
            }
            else
            {
                //Redirigir a Solicitudes2.aspx para visualizar Solicitud
                Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + solicitudId.ToString());
            }
        }

        if (e.CommandName == "Complementar")
        {
            int solicitudId = Convert.ToInt32(e.CommandArgument);

            //Redirigir a Solicitudes2.aspx para complementar Solicitud
            Response.Redirect("~/Solicitudes/ConsultarSolicitud.aspx?id=" + solicitudId.ToString() + "&action=2");
        }

        if (e.CommandName == "VerSolicitudVobo")
        {
            int solicitudId = Convert.ToInt32(e.CommandArgument);
            var mail = ((LinkButton)e.CommandSource).Text;


            Response.Redirect("~/Solicitudes/SolicitudVoBoRetro.aspx?id=" + solicitudId.ToString() + "&correo=" + mail);
        }
    }

    protected void grvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSolicitudes.PageIndex = e.NewPageIndex;
        CargarGridSolicitudes(false);
    }

    //protected void btnEliminar_Click(object sender, EventArgs e)
    //{
    //    EliminarSolicitud(Convert.ToInt32(HFSolicitudId.Value));
    //}

    protected void grvSolicitudes_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["DTGridSolicitudes"] as DataTable;

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

    #endregion

    #region Metodos

    private void CargarGridSolicitudes(bool RefreshGrid)
    {
        if (!Page.IsPostBack || (RefreshGrid))
        {   //borrar datatable del grid si se está recargando la página para que muestre los datos mas recientes:
            Session["DTGridSolicitudes"] = null;
        }

        int idUsuario = ToInt32_0(Session["idUsuario"]);

        if (idUsuario > 0)
        {
            DataTable dt = new DataTable();

            if (Session["DTGridSolicitudes"] == null)
            {
                dt = ConvertToDataTable(DataAcces.GetSolicitudesPorUsuario(idUsuario));
                Session["DTGridSolicitudes"] = dt;
            }

            else
            {
                dt = (DataTable)Session["DTGridSolicitudes"];
            }

            if (dt.Rows.Count > 0)
            {
                grvSolicitudes.DataSource = dt;
                grvSolicitudes.DataBind();
                grvSolicitudes.SelectedIndex = -1;
            }

            else
            {
                MostrarMensaje("No se encontraron solicitudes para consultar.");
            }

        }
    }

    private void ObtenerDetallesSolicitud(int id)
    {
        //Solicitud sol = DataAcces.GetSolicitud(id);

        //if (sol == null)
        //{
        //    MostrarMensage("No se encontró la solicitud!");
        //    return;
        //}

        //else
        //{
        //    MostrarDetallesSolicitud(sol);
        //    return;
        //}
    }

    private void MostrarDetallesSolicitud(Solicitud sol)
    {
        //List<string> status;

        //status = new List<string>() { "", "", ""};

        //lblTipo.Text = sol.Tipo;
        //lblDesc.Text = sol.Descripcion;
        //lblSolicitante.Text = sol.Solicitante;
        //lblFecha.Text = sol.Fecha.HasValue ? sol.Fecha.Value.ToString("yyyy-MM-dd") : " - ";
        //lblEstatus.Text = sol.Status;
        //lblFolio.Text = string.Format(lblFolio.Text, sol.Folio);

        //grvEtiquetas.DataSource = sol.Etiquetas;
        //grvEtiquetas.DataBind();

        //tblConsulta.Visible = true;
        //tblGridSolicitudes.Visible = false;

        //hdnSolicitud.Value = sol.Id.ToString();


        //List<int> statusIdsNoPermitidos = new List<int> { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

        //if (statusIdsNoPermitidos.Contains(sol.IdStatus))
        //{
        //    //btnEliminar.Enabled = false;
        //}

        //else if (sol.IdStatus == 1 || sol.IdStatus == 2) // 1 = En Visto Bueno, 2 = Autorizada
        //{
        //    //btnEliminar.Enabled = true;
        //}
    }


    private void EliminarSolicitud(int solicitudId)
    {
        //if (DataAcces.DelSolicitud(Convert.ToInt32(hdnSolicitud.Value)))
        //{
        //    MostrarMensage("La solicitud ha sido eliminada.");            
        //}
        //else
        //{
        //    MostrarMensage("Ocurrio un error al eliminar la solicitud.");
        //}

        //if (DataAcces.UpdSolicitudStatusId(solicitudId, 6))
        //{
        //    MostrarMensage("La solicitud ha sido marcada como eliminada.");
        //}

        //CargarGridSolicitudes(true);
    }

    #endregion
}