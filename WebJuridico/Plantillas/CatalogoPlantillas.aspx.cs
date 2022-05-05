using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Plantillas_CatalogoPlantillas : PaginaBase
{
    static string editaClasificacion = "";
    static string editaTipo = "";
    static int editaDDLClas = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Administrar Catálogos Tipos Plantilla");

        if (!Page.IsPostBack)
        {
            Session["archivoPlantilla"] = null;

            Tab1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;

            CargarDdlClasificacionPlantilla();
            CargaPlantillaTiposGrid();
            CargaPlantillaClasificacionGrid(0, 0);
        }

        

    }

    private void CargarDdlClasificacionPlantilla()
    {
        ddlClasPlantilla.DataSource = DataAcces.GetPlantillas();
        ddlClasPlantilla.DataTextField = "Descripcion";
        ddlClasPlantilla.DataValueField = "id_tipoplantilla";
        ddlClasPlantilla.DataBind();

        ddlClasPlantilla.Items.Insert(0, new ListItem(String.Empty, "0"));
        ddlClasPlantilla.SelectedIndex = 0;
    }    

    #region "Eventos"

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        MainView.ActiveViewIndex = 1;
    }

    /// <summary>
    /// Botón que guarda una nueva clasificación de plantilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveClasiPlantilla_Click(object sender, EventArgs e)
    {
        string nombreTipoPlantilla = txtClasPlantilla.Text.Trim();

        tbl_TipoPlantilla tp;
        int id;

        if (nombreTipoPlantilla == string.Empty)
        {
            txtClasPlantilla.Text = editaClasificacion;
            MostrarMensaje("Ingrese nombre de descripción de Clasificación de Plantilla");
            return;
        }

        tp = new tbl_TipoPlantilla()
        {
            Descripcion = nombreTipoPlantilla,
            id_tipoplantilla = Convert.ToInt32(hdfEdita.Value)
        };

        string msj = "";

        if (tp.id_tipoplantilla == 0)
        {
            //Inserta Nuevo Registro
            id = DataAcces.CreateTipoPlantilla(tp);
            msj = "El tipo de Clasificación de Plantilla fue guardado con éxito";
        }
        else
        {
            //Actualiza
            id = DataAcces.UpdateDeleteTipoPlantilla(tp, 'U');
            msj = "Se ha actualizado el tipo de Clasifiación de Plantilla Seleccionado";
        }

        if (id > 0)
        {
            CargaPlantillaTiposGrid();
            CargarDdlClasificacionPlantilla();
            MostrarMensaje(msj);
        }else if(id == -3)
        {
            MostrarMensaje("Ya existe una clasificación con el nombre ingresado, introduzca uno nuevo.");
        }

        hdfEdita.Value = "0";
        lblTipoPlan.Text = "Ingrese Nombre Clasificación de Plantilla";
        btnSaveClasiPlantilla.Text = "Agregar";
        LimpiarComponentes();
        btnCancelClasiPlantilla.Visible = false;
    }

    /// <summary>
    /// Botón que guarda nuevo tipo de plantilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveTipoPlantilla_Click(object sender, EventArgs e)
    {

        string nombreClasificacionPlantilla = txtTipoPlantilla.Text.Trim();
        int idClasPlantilla = int.Parse(ddlClasPlantilla.SelectedValue);
        //Se manda a ejecutar SP para insertar a BD

        tbl_ClasificacionPlantilla cp;
        int id;

        if (nombreClasificacionPlantilla == string.Empty)
        {
            MostrarMensaje("Ingrese nombre de descripción de tipo de plantilla");
            return;
        }

        if (idClasPlantilla <= 0)
        {
            MostrarMensaje("Seleccione una clasificación de plantilla");
            return;
        }

        cp = new tbl_ClasificacionPlantilla()
        {
            id_tipoplantilla = idClasPlantilla,
            id_clasificacionplantilla = Convert.ToInt32(hdfEdita.Value),
            Descripcion = nombreClasificacionPlantilla
            
        };

        string msj = "";
        if(hdfEdita.Value == "0")
        {
            //Inserta registro
            id = DataAcces.CreateClasificacionPlantilla(cp);
            msj = "El tipo de plantilla fue guardado con éxito";
        }else
        {
            //Actualiza
            id = DataAcces.UpdateDeleteClasificacionPlantilla(cp, 'U');
            msj = "Se ha actualizado el tipo de Plantilla";
        }      

        if (id > 0)
        {
            CargaPlantillaClasificacionGrid(idClasPlantilla, 0);

            MostrarMensaje(msj);
        }
        else if(id == -3){
            MostrarMensaje("Ya existe un registro con clasificación y tipo de plantilla ingresado, introduzca uno nuevo");
        }

        hdfEdita.Value = "0";
        lblClasif.Text = "Ingrese el Nombre Tipo de Plantilla";
        btnSaveTipoPlantilla.Text = "Agregar";

        LimpiarComponentes();
        btnCancelTipoPlantilla.Visible = false;

    }

    protected void btnCancelTipoPlantilla_Click(object sender, EventArgs e)
    {
        hdfEdita.Value = "0";
        lblClasif.Text = "Ingrese el Nombre Tipo de Plantilla";
        btnSaveTipoPlantilla.Text = "Agregar";
        LimpiarComponentes();
        btnCancelTipoPlantilla.Visible = false;
        CargaPlantillaClasificacionGrid(0, 0);
    }

    protected void btnCancelClasiPlantilla_Click(object sender, EventArgs e)
    {
        hdfEdita.Value = "0";
        lblTipoPlan.Text = "Ingrese Nombre Clasificación de Plantilla";
        btnSaveClasiPlantilla.Text = "Agregar";
        LimpiarComponentes();
        btnCancelClasiPlantilla.Visible = false;

    }

    protected void gvClasificacionesPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idClasPlan = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditaClas")
        {
            EditarClasificacion(idClasPlan);
        }
        else if (e.CommandName == "EliminaClas")
        {
            EliminaClasificacion(idClasPlan);
        }

        CargaPlantillaTiposGrid();
    }

    protected void gvTiposPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idTipoPlan = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "EditaTipo")
        {
            EditarTipoPlantilla(idTipoPlan);
        }
        else if (e.CommandName == "EliminaTipo")
        {
            EliminarTipoPlan(idTipoPlan);
        }
    }

    protected void ddlClasPlantilla_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idClasSelected = int.Parse(ddlClasPlantilla.SelectedValue);
        CargaPlantillaClasificacionGrid(idClasSelected, 0);
        //btnCancelClasiPlantilla.Visible = false;
        //hdfEdita.Value = "0";
    }

    #endregion

    #region "Metodos"

    public void verificarSesionAbierta()
    {
        if (!HttpContext.Current.Request.Url.AbsolutePath.Contains("LogIn.aspx"))
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("~/LogIn.aspx");
            }
        }
    }

    private void CargaPlantillaTiposGrid()
    {
        DataTable dt = new DataTable();
        dt = ConvertToDataTable(DataAcces.GetPlantillas());

        if (dt != null)
        {
            DataView dv = new DataView(dt);

            gvClasificacionesPlan.DataSource = dv;
            gvClasificacionesPlan.DataBind();
        }

    }

    private void CargaPlantillaClasificacionGrid(int idTipo, int idClas)
    {
        DataTable dt = new DataTable();
        dt = ConvertToDataTable(DataAcces.GetClasificacionTipoPlantilla(idTipo, idClas));

        if (dt != null)
        {
            DataView dv = new DataView(dt);

            gvTiposPlan.DataSource = dv;
            gvTiposPlan.DataBind();
        }

    }  

    private void EditarClasificacion(int idClasPlan)
    {
        hdfEdita.Value = idClasPlan.ToString();
        lblTipoPlan.Text = "Edite el Nombre de Clasificación de Plantilla";
        btnSaveClasiPlantilla.Text = "Actualizar";

        List<tbl_TipoPlantilla> ltTipoPlan; //clasificacion pero en tablas estan como tipo
        ltTipoPlan = DataAcces.GetTipoPlantilla(idClasPlan);

        foreach (tbl_TipoPlantilla tp in ltTipoPlan)
        {
            txtClasPlantilla.Text = tp.Descripcion;
            hdfEdita.Value = tp.id_tipoplantilla.ToString();
            editaClasificacion = tp.Descripcion;
        }

        btnCancelClasiPlantilla.Visible = true;
    }

    private void EliminaClasificacion(int idClasPlan)
    {
        hdfEdita.Value = idClasPlan.ToString();
        //string msjConfirm = "¿Está seguro en Elminar el Mensaje?";
        //ClientScript.RegisterOnSubmitStatement(this.GetType(), "confirm", "return confirm('" + msjConfirm + "');");
        //ClientScript.RegisterStartupScript(this.GetType(), "confirm", "confirm('"+ msjConfirm +"');", true);

        int val;

        tbl_TipoPlantilla tp = new tbl_TipoPlantilla() { id_tipoplantilla = idClasPlan };

        val = DataAcces.UpdateDeleteTipoPlantilla(tp, 'D');
        hdfEdita.Value = "0";

        if (val > 0)
        {
            MostrarMensaje("Se ha Eliminado la clasificación Plantilla Seleccionada");
        }
        else if (val == -3)
        {
            MostrarMensaje("El registro seleccionado ya ha sido eliminado anteriormente, actualice su pantalla");
        }
        else if (val == -4)
        {
            MostrarMensaje("No se puede eliminar debido a que tiene otros tipos de plantilla a esta clásificación, elimine en sección TIPO DE PLANTILLA");
        }

    }

    private void EliminarTipoPlan(int idTipoPlan)
    {
        hdfEdita.Value = idTipoPlan.ToString();

        int val;

        tbl_ClasificacionPlantilla clasp = new tbl_ClasificacionPlantilla() { id_clasificacionplantilla = idTipoPlan };

        val = DataAcces.UpdateDeleteClasificacionPlantilla(clasp, 'D');
        hdfEdita.Value = "0";

        if(val > 0)
        {
            MostrarMensaje("Se ha Eliminado el tipo de plantilla seleccionado");
        }else if(val == -3)
        {
            MostrarMensaje("El registro seleccionado ya ha sido eliminado anteriormente, actualice su pantalla");
        }

        CargaPlantillaClasificacionGrid(0, 0);
        ddlClasPlantilla.SelectedValue = "0";
    }

    private void EditarTipoPlantilla(int idTipoPlan)
    {
        hdfEdita.Value = idTipoPlan.ToString();
        lblClasif.Text = "Edite el Tipo de Plantilla";
        btnSaveTipoPlantilla.Text = "Actualizar";

        List<ClasificacionPlantillas> ltClasificaion; //Tipos pero en tablas estan definidas como clasificación o al revés.
        ltClasificaion = DataAcces.GetClasificacionTipoPlantilla(0, idTipoPlan);

        foreach (ClasificacionPlantillas cp in ltClasificaion)
        {
            txtTipoPlantilla.Text = cp.ClasificacionPlantilla;
            ddlClasPlantilla.SelectedValue = cp.IDTipoPlantilla.ToString();
            editaTipo = cp.ClasificacionPlantilla;
            editaDDLClas = cp.IDTipoPlantilla;
        }

        btnCancelTipoPlantilla.Visible = true;

    }

    private void LimpiarComponentes()
    {
        txtClasPlantilla.Text = "";
        txtTipoPlantilla.Text = "";
        ddlClasPlantilla.SelectedIndex = 0;
        editaClasificacion = "";
        editaDDLClas = 0;
        editaTipo = "";
    }

    #endregion
}