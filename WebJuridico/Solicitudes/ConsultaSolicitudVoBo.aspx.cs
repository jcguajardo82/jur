using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes_ConsultaSolicitudVoBo : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Configuración Vo.Bo. de Plantillas");

        int perf = ToInt32_0(Session["perfil"]);

        List<int> PerfilesPermitidos = new List<int> { 1, 4 }; // “AdmGral” y “Solicitador” 

        if (PerfilesPermitidos.Contains(perf))
        {
            if (!this.IsPostBack)
            {
                var dt = ConvertToDataTable(DataAcces.tbl_VoBoSolicitudes_sUp());
                Session["DTGridSolicitudes"] = dt;
                grvSolicitudes.DataSource = dt;
                grvSolicitudes.DataBind();
            }
        }
        else
        {
            lblTitulo.Text = "";
            //MostrarMensage("El acceso no está permitido para su tipo de perfil.");
            return;
        }
    }

    protected void grvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSolicitudes.PageIndex = e.NewPageIndex;
        CargarGridSolicitudes(false);
    }

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

    protected void grvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerSolicitud")
        {
            int solicitudId = Convert.ToInt32(e.CommandArgument);

            //Redirigir a Solicitudes2.aspx para visualizar Solicitud
            Response.Redirect("~/Solicitudes/SolicitudVoBoRetro.aspx?id=" + solicitudId.ToString());
        }

       
    }
    #region Metodos

    private void CargarGridSolicitudes(bool RefreshGrid)
    {
        if ((RefreshGrid))
        {   //borrar datatable del grid si se está recargando la página para que muestre los datos mas recientes:
            Session["DTGridSolicitudes"] = null;
        }

        int idUsuario = ToInt32_0(Session["idUsuario"]);

        if (idUsuario > 0)
        {
            DataTable dt = new DataTable();

            if (Session["DTGridSolicitudes"] == null)
            {
                dt = ConvertToDataTable(DataAcces.tbl_VoBoSolicitudes_sUp());
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



    #endregion
}