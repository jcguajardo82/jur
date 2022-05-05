using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Procesos_ConsultarReasignacion : PaginaBase
{
    #region Campos

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Consultar y Reasignar Solicitudes");

        if (!Page.IsPostBack)
        {
            int perf = ToInt32_0(Session["perfil"]);

            List<int> PerfilesPermitidos = new List<int> { 1, 3 }; // “AdmGral” y “AsisCont”

            if (PerfilesPermitidos.Contains(perf))
            {
                CargarDDLs();
                Session["archivos"] = null;
                txtFechaInicio.Text = DateTime.Today.ToString("MM/dd/yyyy");
                txtFechaFin.Text = DateTime.Today.ToString("MM/dd/yyyy");
            }

            else
            {
                deshabilitarAcceso();
            }
        }

        if (Request.Form["__EVENTTARGET"] == "btnGrabar")
        {
            // Fire event
            btnGrabar_Click(this, new EventArgs());
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
    //    Consultar(solicitudId);
    //}

    protected void grvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //old name: CargarLineaSolicitud

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvSolicitudes, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
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

        if (e.CommandName == "Bitacora")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            PanelBitacora.Visible = true;
            PanelBotonesPrincipales.Visible = false; // *****
            tblPrincipal.Visible = false;

            //load bitacora with solicitudId
            CargarGridBitacora(SolicitudId);
        }

        if (e.CommandName == "Reasignar")
        {
            GridViewRow row = (GridViewRow)grvSolicitudes.Rows[Convert.ToInt32(e.CommandArgument) % grvSolicitudes.PageSize];
            int SolicitudId = Convert.ToInt32(((Label)row.FindControl("lblSolicitudId")).Text);

            if (SolicitudId > 0)
            {
                CargarAbogados(SolicitudId);
                hdnFolio.Value = ((Label)row.FindControl("lblFolio")).Text;
            }
        }
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

    protected void grvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSolicitudes.PageIndex = e.NewPageIndex;
        CargarGridSolicitudes(false);
    }

    private void CargarGridSolicitudes(bool RefreshGrid)
    {
        if (!Page.IsPostBack || (RefreshGrid))
        {   //borrar datatable del grid si se está recargando la página para que muestre los datos mas recientes:
            Session["DTGridSolicitudesReasig"] = null;
        }

        int idUsuario = ToInt32_0(Session["idUsuario"]);

        if (idUsuario > 0)
        {
            DataTable dt = new DataTable();

            if (Session["DTGridSolicitudesReasig"] == null)
            {
                //dt = ConvertToDataTable(DataAcces.GetSolicitudesRecep(idUsuario));
                //Session["DTGridSolicitudesReasig"] = dt;
            }

            else
            {
                dt = (DataTable)Session["DTGridSolicitudesReasig"];
            }

            grvSolicitudes.DataSource = dt;
            grvSolicitudes.DataBind();
        }
    }

    protected void grvSolicitudes_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["DTGridSolicitudesReasig"] as DataTable;

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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime? startDate = ToDateTime(txtFechaInicio.Text);
            DateTime? endDate = ToDateTime(txtFechaFin.Text);

            if (startDate > endDate)
            {
                MostrarMensaje("La fecha inicial no puede ser mayor a fecha final");
                return;
            }

            if (endDate < startDate)
            {
                MostrarMensaje("La fecha final no puede ser menor a fecha inicial");
                return;
            }

            BuscarSolicitudes();
        }
        catch (Exception ex)
        {
            errorMsg.Text = ex.Message;
        }
    }

    protected void btnGraficaStatus_Click(object sender, EventArgs e)
    {
        int ClasPlantilla = ToInt32_0(cmbClasPlantilla.SelectedValue);

        if (ClasPlantilla > 0)
        {
            switch (ClasPlantilla)
            {
                case 1: VerReporte(3);
                    break;
                case 2: VerReporte(4);
                    break;
            }
        }
        else
        {
            MostrarMensaje("Debe seleccionar ´Poderes´ o ´Contratos´ en ´Clasificación de plantilla´");
        }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        GuardarAsignacion(Request.Form["__EVENTARGUMENT"]);
    }

    protected void btnGraficaPlantilla_Click(object sender, EventArgs e)
    {
        int ClasPlantilla = ToInt32_0(cmbClasPlantilla.SelectedValue);

        if (ClasPlantilla > 0) {
            VerReporte(ClasPlantilla);
        }
        else {
            MostrarMensaje("Debe seleccionar ´Poderes´ o ´Contratos´ en ´Clasificación de plantilla´");
        }
    }

    #endregion

    #region Metodos

    private void CargarDDLs()
    {
        CargarDDLabogados();
        CargarDDLTipoPlantilla();
    }

    private void CargarDDLabogados()
    {
        List<Abogados> abogados;

        abogados = DataAcces.GetAbogado();
        abogados.Insert(0, new Abogados() { Id1 = -1, Nombre = "Todos" });

        cmbNomAbogado.DataSource = abogados;

        cmbNomAbogado.DataTextField = "Nombre";
        cmbNomAbogado.DataValueField = "Id1";

        cmbNomAbogado.DataBind();
    }

    private void CargarDDLTipoPlantilla()
    {
        List<tbl_TipoPlantilla> tipoPlantillas;

        tipoPlantillas = DataAcces.GetPlantillas();
        tipoPlantillas.Insert(0, new tbl_TipoPlantilla() { id_tipoplantilla = -1, Descripcion = "Ambos" });

        cmbClasPlantilla.DataSource = tipoPlantillas;

        cmbClasPlantilla.DataTextField = "Descripcion";
        cmbClasPlantilla.DataValueField = "id_tipoplantilla";

        cmbClasPlantilla.DataBind();
    }

    private void BuscarSolicitudes()
    {
        List<Solicitud> solicitudes;
        int idUsuario;
        int id_nperfil;
        int? selectedAbogado;
        int? tipoPlantilla;
        DateTime? startDate = ToDateTime(txtFechaInicio.Text);
        DateTime? endDate = ToDateTime(txtFechaFin.Text);

        if (Session["idUsuario"] == null)
        {
            return;
        }
        else
        {
            int.TryParse(Session["idUsuario"].ToString(), out idUsuario);
            id_nperfil = ToInt32_0(Session["Perfil"]);
        }

        selectedAbogado = cmbNomAbogado.SelectedValue == "-1" ? new int?() : Convert.ToInt32(cmbNomAbogado.SelectedValue);
        tipoPlantilla = cmbClasPlantilla.SelectedValue == "-1" ? new int?() : Convert.ToInt32(cmbClasPlantilla.SelectedValue);

        solicitudes = DataAcces.GetSolicitudesFiltradas(idUsuario, id_nperfil, tipoPlantilla, selectedAbogado, startDate, endDate);

        if (solicitudes != null)
        {
            DataTable dt = ConvertToDataTable(solicitudes);

            Session["DTGridSolicitudesReasig"] = dt;

            grvSolicitudes.DataSource = dt;
            grvSolicitudes.DataBind();
        }

        else
        {
            MostrarMensaje("No se encontraron solicitudes con esas características.");
        }
    }

    //private void Consultar(int id)
    //{
    //    ConsultaSolicitud sol;

    //    sol = DataAcces.llenadoConsultaSolicitud(id);

    //    if (sol != null)
    //    {
    //        //CargarSolicitud(sol); //disabled
    //        return;
    //    }

    //    MostrarMensage("No se encontró la solicitud!");
    //}

    private void CargarAbogados(int param)
    {
        List<Abogados> abogados;
        HtmlTableRow tr;
        HtmlTableCell td;
        HtmlTableCell td2;
        HtmlInputRadioButton rb;

        abogados = DataAcces.GetAbogado();

        foreach (Abogados abogado in abogados)
        {
            tr = new HtmlTableRow();
            td = new HtmlTableCell();
            rb = new HtmlInputRadioButton();
            td2 = new HtmlTableCell();

            rb.Name = "gpoAbogados";
            rb.Attributes.Add("value", abogado.Id1.ToString());
            rb.Attributes.Add("text", abogado.Nombre);
            rb.ID = "rbt" + abogado.Id1.ToString();
            rb.Attributes.Add("onclick", "onSelectAbogado('" + abogado.Id1.ToString() + "')");

            td2.InnerText = abogado.Nombre;

            td.Controls.Add(rb);
            tr.Cells.Add(td);
            tr.Cells.Add(td2);

            tblAbogados.Rows.Add(tr);
        }

        MostrarAbogados();
        hdnSelSol.Value = param.ToString();
    }

    private void GuardarAsignacion(string abogadoId)
    {
        try
        {
            int solId;

            solId = Convert.ToInt32(hdnSelSol.Value);
            int abogadoId_ = ToInt32_0(abogadoId);

            if (!(abogadoId_ > 0)) {
                return;
            }

            if (abogadoId_ > 0)
            {
                if ((DataAcces.UpdSolicitudAsignado(solId, abogadoId_)) > 0)
                {
                    MostrarMensaje("La solicitud fue asignada con éxito.");
                }
            }

            //send out email:
            Usuario usuario = DataAcces.GetSpecificUserInfo(abogadoId_);

            if (IsValidEmail(usuario.Email))
            {
                string folio = hdnFolio.Value;
                string link = String.Format("<a href='{0}/Solicitudes/ConsultarSolicitud.aspx?id={1}&action=5'>{2}</a>", sistemaURL, solId.ToString(), folio);
                string email = usuario.Email;
                string subject = "ASIGNACIÓN DE FOLIO DE SOLICITUD";
                string body = String.Format("Atención,<br>Se le ha asignado el folio de solicitud {0} para su seguimiento.", link);

                EnviarCorreo(email, subject, body);
            }
            else
            {
                MostrarMensaje("La dirección de email configurada para este abogado no es válida. No se puede enviar notificación.");
            }

            grvSolicitudes.EditIndex = -1;
            BuscarSolicitudes();
            updMain.Update();
        }
        catch (Exception ex)
        {
            MostrarMensaje(ex.Message);
            errorMsg.Text += ex.Message;
        }
    }

    private void MostrarAbogados()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page,
                this.Page.GetType(),
                "script",
                "<script type='text/javascript'>mostrarAbogados();</script>", false);
    }

    private void VerReporte(int tipoReporte)
    {
        Response.Redirect("../Reportes/reportes.aspx?tipo=" + tipoReporte.ToString());
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        PanelBitacora.Visible = false;
        PanelBotonesPrincipales.Visible = true;
        tblPrincipal.Visible = true;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (grvSolicitudes.Rows.Count > 0)
        {
            ExportToExcel(sender, e);
        }

        else
        {
            MostrarMensaje("El grid no contiene ningún resultado para exportar a Excel. Debe hacer una búsqueda primero.");
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        cmbClasPlantilla.SelectedValue = "-1";
        cmbNomAbogado.SelectedValue = "-1";
        Session["archivos"] = null;
        txtFechaInicio.Text = DateTime.Today.ToString("MM/dd/yyyy");
        txtFechaFin.Text = DateTime.Today.ToString("MM/dd/yyyy");
        grvBitacora.DataSource = null;
        grvBitacora.DataBind();
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grvSolicitudes.AllowPaging = false;
            CargarGridSolicitudes(false);

            grvSolicitudes.Columns[13].Visible = false;
            grvSolicitudes.Columns[14].Visible = false;

            grvSolicitudes.HeaderRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell cell in grvSolicitudes.HeaderRow.Cells)
            {
                cell.BackColor = grvSolicitudes.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvSolicitudes.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvSolicitudes.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvSolicitudes.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvSolicitudes.RenderControl(hw); //El control 'Contenido_grvSolicitudes' de tipo 'GridView' debe colocarse dentro de una etiqueta de formulario con runat=server. (solucion: agregué: VerifyRenderingInServerForm(Control control))

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    #endregion
}