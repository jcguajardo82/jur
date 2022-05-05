using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Plantillas_Plantillas : PaginaBase
{
    #region Campos

    int Tipo; //variable para saber si es tipo 1 = Poderes, 2 = Contratos
    int valorPlantillaId;

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Administrar Plantillas");

        if (!Page.IsPostBack)
        {
            Session["archivoPlantilla"] = null;

            if (Request.QueryString.Count > 0)
            {
                Tipo = ToInt32_0(Request.QueryString[0]);

                if (Tipo == 1)
                {
                    //lblTitulo.Text = "Plantilla de Poderes";
                    chkJuridico.Visible = true; //solojuridico enabled for poderes
                    grvEtiquetas.Columns[1].Visible = true;
                    lblChkSoloJuridico.Visible = true;
                }
                if (Tipo == 2)
                {
                    ////lblTitulo.Text = "Plantilla de Contratos";
                    //chkJuridico.Visible = false; //solojuridico disabled for contratos
                    //grvEtiquetas.Columns[1].Visible = false;
                    //lblChkSoloJuridico.Visible = false;
                }

                GridDataEtiquetas();
                CargarDDLCPlantilla();
                CargarDDLTPlantilla();
                CargarDDLVigencia();
                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                CargarLsbAutDisp();
                //BtnNueva.Visible = false;
                btn_eliminar.Visible = false;
                manageHideVigencia(ToInt32_0(DDLClaPlantillas.SelectedValue));

                RefreshPlantillasGrid();

                Session["archivoPlantilla"] = null;
            }                
            
            else
            {
               
                ////Response.Redirect(urlfix() + "/Plantillas/Plantillas.aspx?tipo=1");
                //CargarPlantilla(valor);
                //BtnGuardarPlantilla.Visible = false;
                ////BtnNueva.Visible = false;
                //BtnLimpiar.Visible = false;
                //Tab1.Visible = false;
                //Tab2.Visible = false;
                //Tab3.Visible = false;
                //return;

                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri + "?tipo=1");
            }
        }
    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
        while (lsbAutorizadores.Items.Count > 0 && lsbAutorizadores.SelectedItem != null)
        {
            ListItem selectedItem = lsbAutorizadores.SelectedItem;
            selectedItem.Selected = false;
            lsbAutDisp.Items.Add(selectedItem);
            lsbAutorizadores.Items.Remove(selectedItem);
        }
    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
        while (lsbAutDisp.Items.Count > 0 && lsbAutDisp.SelectedItem != null)
        {
            ListItem selectedItem = lsbAutDisp.SelectedItem;
            selectedItem.Selected = false;
            lsbAutorizadores.Items.Add(selectedItem);
            lsbAutDisp.Items.Remove(selectedItem);
        }
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        FileInfo fi = null;

        if (Fileload.HasFile)
        {
            fi = new FileInfo(Fileload.FileName);

            //revisar si el nombre no es demasiado largo
            if (fi.Name.Length > 70)
            {
                MostrarMensaje("El nombre del archivo es demasiado largo (maximo 65 caracteres, sin contar extensión)");
                return;
            }

            //revisar que sea archivo de office 2007 o superior
            else if (fi.Extension != ".docx")
            {
                MostrarMensaje("Solo se pueden cargar archivos tipo .docx");
                return;
            }

            else
            {
                CheckFileIntegrity(Fileload.FileBytes);

                Session["archivoPlantilla"] = new PlantillaArchivo() { Archivo = Fileload.FileBytes, Nombre = Fileload.FileName };
                lblCargadePlantilla.Text = "Archivo Cargado: " + Fileload.FileName;
                //archivoNombre = Fileload.FileName;
                lblCargadePlantilla.Visible = true;
                Fileload.Visible = false;
                btnUp.Visible = false;
                btn_eliminar.Visible = true;
            }
        }
    }

    protected void BtnGuardarPlantilla_Click(object sender, EventArgs e)
    {
        Guardarplantilla();
    }

    protected void btn_eliminar_Click(object sender, EventArgs e)
    {
        if (lblCargadePlantilla.Text != "")
        {
            Session["archivoPlantilla"] = null;
            btn_eliminar.Visible = false;
            Fileload.Visible = true;
            btnUp.Visible = true;
            lblCargadePlantilla.Text = "";
        }
        else
        {
            MostrarMensaje("No se puede borrar este Documento");
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (TxtNomPlantilla.Text.Trim() == string.Empty)
        {
            MostrarMensaje("Debe registrar un nombre.");
            return;
        }

        if (TxtDescripcion.Text.Trim() == string.Empty)
        {
            MostrarMensaje("Debe registrar una descripción breve.");
            return;
        }

        if (Session["Plantillaid"] == null)
        {
            MostrarMensaje("No se ha seleccionado plantilla para Actualizar");
            return;
        }

        Actualizarplantilla(Convert.ToInt32(Session["Plantillaid"]));
    }

    protected void DDLTipoPlantilla_SelectedIndexChanged(object sender, EventArgs e)
    {
        manageHideVigencia(ToInt32_0(DDLClaPlantillas.SelectedValue));
    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
    }

    protected void GDVPlantillas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Descargar")
        {
            DescargarDocumentos(1, Convert.ToInt32(e.CommandArgument));
        }
    }

    protected void Add(object sender, EventArgs e)
    {
        List<tbl_Etiquetas> etiquetas;

        etiquetas = new List<tbl_Etiquetas>();

        foreach (GridViewRow r in grvEtiquetas.Rows)
        {
            etiquetas.Add(new tbl_Etiquetas()
            {
                Pregunta = ((Label)r.FindControl("lblEtiqueta")).Text,
                Juridica = ((CheckBox)r.FindControl("chkSoloJuridico")).Checked
            });
        }

        if (grvEtiquetas.FooterRow != null)
        {
            etiquetas.Add(new tbl_Etiquetas()
            {
                Pregunta = ((TextBox)grvEtiquetas.FooterRow.FindControl("txtIdentificador")).Text,
                Juridica = ((CheckBox)grvEtiquetas.FooterRow.FindControl("CkbJuridico")).Checked
            });
        }

        grvEtiquetas.DataSource = etiquetas;
        grvEtiquetas.DataBind();
    }

    protected void GDVPlantillas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GDVPlantillas.PageIndex = e.NewPageIndex;
        CargarGridPlantillas();
    }

    protected void GDVPlantillas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GDVPlantillas.EditIndex = -1;
        CargarGridPlantillas();
    }

    protected void GDVPlantillas_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        CargarGridPlantillas();
    }

    protected void GDVPlantillas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool res;
        int plantillaId;

        plantillaId = Convert.ToInt32(((Label)GDVPlantillas.Rows[e.RowIndex].FindControl("lblIdPlantilla")).Text);

        res = DataAcces.DelPlantilla(plantillaId);

        if (res)
        {
            GDVPlantillas.SelectedIndex = -1;

            Session["PlantillasGridData"] = null;
            RefreshPlantillasGrid();
            limpiarCampos();

            MostrarMensaje("La Plantilla ha sido eliminada con éxito.");
        }
        else
        {
            MostrarMensaje("Ocurrió un error al eliminar la plantilla");
        }
    }

    protected void GDVPlantillas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GDVPlantillas.EditIndex = e.NewEditIndex;
        CargarGridPlantillas();
    }

    protected void GDVPlantillas_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["PlantillasGridData"] as DataTable;

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

            Session["sortExpression"] = e.SortExpression;

            GDVPlantillas.DataSource = dataView;
            GDVPlantillas.DataBind();
        }
    }

    protected void GDVPlantillas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int plantillaId = ToInt32_0(((Label)GDVPlantillas.Rows[GDVPlantillas.SelectedIndex].FindControl("lblIdPlantilla")).Text);

        if (plantillaId > 0)
        {
            Session["Plantillaid"] = plantillaId;
            valorPlantillaId = plantillaId;
            CargarPlantilla(plantillaId);
        }
    }

    protected void GDVPlantillas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Cells[1].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GDVPlantillas, "Select$" + e.Row.RowIndex);
            //e.Row.Cells[1].ToolTip = "Muestra la Plantilla";
            //e.Row.Cells[1].Attributes["style"] = "cursor:pointer";
            //e.Row.Cells[3].Attributes["style"] = "cursor:pointer";
            //e.Row.Cells[3].ToolTip = "Descarga la Plantilla";
            //e.Row.Cells[1].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GDVPlantillas, "Select$" + e.Row.RowIndex);
        }
    }

    protected void grvEtiquetas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {   
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)grvEtiquetas.Rows[index];

            List<tbl_Etiquetas> etiquetas;
            etiquetas = new List<tbl_Etiquetas>();

            if (grvEtiquetas.Rows.Count > 0)
            {
                foreach (GridViewRow r in grvEtiquetas.Rows)
                {
                    if (r.RowIndex!= index)
                    { 
                        etiquetas.Add(new tbl_Etiquetas()
                        {
                            Pregunta = ((Label)r.FindControl("lblEtiqueta")).Text,
                            Juridica = ((CheckBox)r.FindControl("chkSoloJuridico")).Checked,
                            id_etiquetas = r.RowIndex
                        });
                    }
                }

                if (etiquetas.Count == 0)
                {
                    tblEtiquetas.Visible = true;
                }
                
                grvEtiquetas.DataSource = etiquetas;
                grvEtiquetas.DataBind();
            }
        }
    }

    protected void grvEtiquetas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }

    private string getSortDirectionString(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;
        if (sortDirection == SortDirection.Ascending)
        {
            newSortDirection = "ASC";
        }
        else
        {
            newSortDirection = "DESC";
        }
        return newSortDirection;
    }

    protected void AgregarEtiqueta(object sender, EventArgs e)
    {
        if (txtIdentificador.Text.Trim() == string.Empty)
        {
            MostrarMensaje("Debe registrar una etiqueta.");

            return;
        }

        List<tbl_Etiquetas> etiquetas;

        etiquetas = new List<tbl_Etiquetas>();

        etiquetas.Add(new tbl_Etiquetas()
        {
            Pregunta = txtIdentificador.Text,
            Juridica = chkJuridico.Checked
        });

        grvEtiquetas.DataSource = etiquetas;
        grvEtiquetas.DataBind();

        grvEtiquetas.Visible = true;
        tblEtiquetas.Visible = false;

        txtIdentificador.Text = string.Empty;
        chkJuridico.Checked = false;
    }

    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        limpiarCampos();
    }

    protected void limpiarCampos()
    {
        TxtNomPlantilla.Text = "";
        TxtNomPlantilla.Enabled = true;
        TxtDescripcion.Text = "";
        txtIdentificador.Text = "";
        grvEtiquetas.Enabled = true;
        grvEtiquetas.DataSource = null;
        grvEtiquetas.DataBind();
        tblEtiquetas.Visible = true;
        lsbAutorizadores.Items.Clear();
        CargarLsbAutDisp();
        Session["archivoPlantilla"] = null;
        btn_eliminar.Visible = false;
        Fileload.Visible = true;
        btnUp.Visible = true;
        lblCargadePlantilla.Text = "";
        BtnGuardarPlantilla.Visible = true;

        int tipo = ToInt32_0(Request.QueryString["tipo"]);

        ddlvigencia.Enabled = tipo == 1 ? true : false;
        DDLTipoPlantilla.Enabled = tipo == 1 ? true : false;
        DDLTipoPlantilla.Visible = true;
        lblTipoPlantilla.Visible = true;

        DDLClaPlantillas.SelectedValue = tipo.ToString();

    }

    protected void DDLClaPlantillas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idClasificacion = Convert.ToInt32(DDLClaPlantillas.SelectedValue);
        DDLTipoPlantilla.DataSource = DataAcces.GetClasificacionPlantilla(idClasificacion);
        DDLTipoPlantilla.DataTextField = "Descripcion";
        DDLTipoPlantilla.DataValueField = "id_clasificacionplantilla";
        DDLTipoPlantilla.DataBind();
        ddlvigencia.Enabled = true;
        DDLTipoPlantilla.Enabled = true;
    }

    #endregion

    #region Metodos

    private void CargarPlantilla(int plantillaId)
    {
        Plantillas plantilla;

        plantilla = DataAcces.SelPlantilla(plantillaId);

        if (plantilla == null)
        {
            MostrarMensaje("No puede ser mostrada esta plantilla.");
        }
        else
        {
            BtnGuardarPlantilla.Enabled = false;

            //ddlvigencia.SelectedValue = null; *****

            Tipo = ToInt32_0(Request.QueryString["tipo"]);

                if (plantilla.Plantilla.id_clasificacionplantilla == 1)
                {
                    DDLTipoPlantilla.DataSource = DataAcces.GetClasificacionPlantilla(plantilla.Plantilla.id_tipoplantilla);
                    DDLTipoPlantilla.DataTextField = "Descripcion";
                    DDLTipoPlantilla.DataValueField = "id_clasificacionplantilla";
                    DDLTipoPlantilla.DataBind();

                    try 
                    {
                        DDLTipoPlantilla.SelectedValue = plantilla.Plantilla.cartaOescritura.ToString();
                    }
                    catch (Exception ex)
                    { 

                    }

                    DDLTipoPlantilla.Visible = true;
                    lblTipoPlantilla.Visible = true;
                }

                else
                {
                    DDLTipoPlantilla.Visible = false;
                    lblTipoPlantilla.Visible = false;
                }

            TxtDescripcion.Text = plantilla.Plantilla.Descripcion;
            TxtNomPlantilla.Text = plantilla.Plantilla.Nombre;
            //DDLClaPlantillas.SelectedValue = plantilla.Plantilla.id_clasificacionplantilla.ToString();
            DDLClaPlantillas.SelectedValue = plantilla.Plantilla.id_tipoplantilla.ToString();

            if (plantilla.Plantilla.id_tipovigencia > 0) { 
                ddlvigencia.SelectedValue = plantilla.Plantilla.id_tipovigencia.ToString(); 
            }

            lblCargadePlantilla.Visible = true;
            lblCargadePlantilla.Text = plantilla.Plantilla.PathArchivo;

            for (int i = 0; i < lsbAutDisp.Items.Count; i++)
            {
                if (plantilla.Autorizadores.Any(x => x.Id == Convert.ToInt32(lsbAutDisp.Items[i].Value)))
                {
                    lsbAutDisp.Items.RemoveAt(i);
                    i--;
                }
            }

            lsbAutorizadores.Items.Clear();

            lsbAutorizadores.DataSource = plantilla.Autorizadores;

            lsbAutorizadores.DataTextField = "Nombre";
            lsbAutorizadores.DataValueField = "Id";
            lsbAutorizadores.DataBind();

            grvEtiquetas.DataSource = plantilla.Etiquetas;
            grvEtiquetas.DataBind();

            ModoVista();

            lblCargadePlantilla.Text = plantilla.Plantilla.PathArchivo;
        }
    }

    private void DownloadPlantilla(int plantillaId)
    {
        PlantillaArchivo descarga = new PlantillaArchivo();
        descarga= DataAcces.GetPlantillaArchivo(plantillaId);
        Byte[] bytes = descarga.Archivo;
        Response.Buffer = true;
        Response.Charset = "";
       // Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = ".doc";
        Response.AddHeader("content-disposition", "attachment;filename=" + descarga.Nombre);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
        CargarPlantilla(plantillaId);
    }

    private bool ValidarPlantilla()
    {
        if (TxtNomPlantilla.Text.Trim() == string.Empty)
        {
            MostrarMensaje("Debe registrar un nombre.");
            return false;
        }

        if (TxtDescripcion.Text.Trim() == string.Empty)
        {
            MostrarMensaje("Debe registrar una descripción breve.");
            return false;
        }

        if (Session["archivoPlantilla"] == null)
        {
            MostrarMensaje("Es necesario cargar un documento.");
            return false;
        }

        return true;
    }

    private void CargarDDLCPlantilla()
    {
        DDLClaPlantillas.DataSource = DataAcces.GetPlantillas();
        DDLClaPlantillas.DataTextField = "Descripcion";
        DDLClaPlantillas.DataValueField = "id_tipoplantilla";
        DDLClaPlantillas.DataBind();

        if (Request.QueryString["tipo"] != null)
        {
            switch (Request.QueryString["tipo"].ToString())
            {
                case "1":
                    //DDLClaPlantillas.SelectedIndex = 0;
                    DDLClaPlantillas.SelectedValue = "1";
                    break;
                case "2":
                    //DDLClaPlantillas.SelectedIndex = 1;
                    DDLClaPlantillas.SelectedValue = "2";
                    break;
                default:
                    break;

            }
        }

        DDLClaPlantillas.Enabled = true;

    }

    private void CargarDDLTPlantilla()
    {
        DDLTipoPlantilla.DataSource = DataAcces.GetClasificacionPlantilla(Tipo);
        DDLTipoPlantilla.DataTextField = "Descripcion";
        DDLTipoPlantilla.DataValueField = "id_clasificacionplantilla";
        DDLTipoPlantilla.DataBind();
        ddlvigencia.Enabled = true;
        DDLTipoPlantilla.Enabled = true;

        //if (Tipo == 1)
        //{
        //    DDLTipoPlantilla.DataSource = DataAcces.GetClasificacionPlantilla(Tipo);
        //    DDLTipoPlantilla.DataTextField = "Descripcion";
        //    DDLTipoPlantilla.DataValueField = "id_clasificacionplantilla";
        //    DDLTipoPlantilla.DataBind();
        //    ddlvigencia.Enabled = true;
        //}
        //else
        //{
        //    //DDLTipoPlantilla.Text = "Contrato";
        //    //DDLTipoPlantilla.Enabled = false;
        //    DDLTipoPlantilla.Enabled = true;
        //    ddlvigencia.Enabled = false;
        //}
    }

    private void CargarDDLVigencia()
    {
        ddlvigencia.DataSource = DataAcces.GetVigencia();
        ddlvigencia.DataTextField = "Descripcion";
        ddlvigencia.DataValueField = "id_tipovigencia";
        ddlvigencia.DataBind();
    }

    protected void manageHideVigencia(int cPlantillaId)
    {
        if (cPlantillaId == 1)
        {
            hideVigencia(true);
        }
        else
        {
            hideVigencia(false);
        }
    }

    private void hideVigencia(bool hide)
    {
        if (hide == false)
        {
            lblvigencia.Visible = true;
            ddlvigencia.Visible = true;
        }
        else
        {
            lblvigencia.Visible = true;
            ddlvigencia.Visible = true;
        }
    }

    private void CargarLsbAutDisp()
    {
        lsbAutDisp.DataSource = DataAcces.GetAutorizador();
        lsbAutDisp.DataTextField = "Nombre";
        lsbAutDisp.DataValueField = "id_usuario";
        lsbAutDisp.DataBind();
    }

    private void Actualizarplantilla(int id)
    {
        bool result = false;
        List<tbl_Autorizador> autorizadores;
        autorizadores = new List<tbl_Autorizador>();

        if (lsbAutorizadores.Items.Count > 0)
        {
            foreach (ListItem li in lsbAutorizadores.Items)
            {
                autorizadores.Add(new tbl_Autorizador()
                {
                    id_usuario = Convert.ToInt32(li.Value)
                });
            }
        }
        else
        {
            MostrarMensaje("Se debe seleccionar al menos un Autorizador.");
            return;
        }

        result = DataAcces.ActualizarPlantilla(id, TxtDescripcion.Text, autorizadores);

        if (result == true)
        {
            MostrarMensaje("La Actualizacion de la Plantilla fue Exitosa");
            CargarPlantilla(id);
        }
        else
        {
            MostrarMensaje("Se encontro un Error al Actualizar la plantilla");
        }

    }

    private void Guardarplantilla()
    {
        tbl_PlantillasJuridicas pj;
        List<tbl_Autorizador> autorizadores;
        List<tbl_Etiquetas> etiquetas;
        bool res;
        PlantillaArchivo archivo;

        if (!ValidarPlantilla()) {
            return;
        }

        archivo = (PlantillaArchivo)Session["archivoPlantilla"];
        // archivo.Nombre = archivoNombre;

        autorizadores = new List<tbl_Autorizador>();

        if (lsbAutorizadores.Items.Count > 0)
        {
            foreach (ListItem li in lsbAutorizadores.Items)
            {
                autorizadores.Add(new tbl_Autorizador()
                {
                    id_usuario = Convert.ToInt32(li.Value)
                });
            }
        }
        else
        {
            MostrarMensaje("Se debe seleccionar al menos un Autorizador.");
            return;
        }

        Tipo = Convert.ToInt32(Request.QueryString["tipo"].ToString());

        pj = new tbl_PlantillasJuridicas()
        {
            //id_clasificacionplantilla = Convert.ToInt32(DDLClaPlantillas.SelectedValue),
            id_clasificacionplantilla = Convert.ToInt32(DDLTipoPlantilla.SelectedValue),
            Nombre = TxtNomPlantilla.Text,
            Descripcion = TxtDescripcion.Text,
            id_usuario = Convert.ToInt32(Session["idUsuario"].ToString()),
            //id_tipoplantilla = Tipo, 
            //id_tipoplantilla = Convert.ToInt32(DDLTipoPlantilla.SelectedValue),
            id_tipoplantilla = Convert.ToInt32(DDLClaPlantillas.SelectedValue),
            id_tipovigencia = Tipo == 1 ? ToInt32_0(ddlvigencia.SelectedValue) : 0,
            PathArchivo = string.Empty,
            GUID = Guid.NewGuid().ToString(),
            versionDoc = 1,
            cartaOescritura = ToInt32_0(DDLTipoPlantilla.SelectedValue)
        };

        etiquetas = new List<tbl_Etiquetas>();

        if (grvEtiquetas.Rows.Count > 0)
        {
            foreach (GridViewRow r in grvEtiquetas.Rows)
            {
                etiquetas.Add(new tbl_Etiquetas()
                {
                    Pregunta = ((Label)r.FindControl("lblEtiqueta")).Text,
                    Juridica = ((CheckBox)r.FindControl("chkSoloJuridico")).Checked
                });
            }
        }
        else
        {
            MostrarMensaje("Se debe capturar al menos una Etiqueta.");
            return;
        }

        //CheckFileIntegrity(archivo.Archivo); //No usar esta linea!! de alguna manera daña el archivo.Archivo, causando problemas extraños al leer el DOCX

        res = DataAcces.CreatePlantilla(pj, autorizadores, etiquetas, archivo);

        if (res)
        {
            ModoVista();
            //BtnNueva.Visible = true;
            BtnGuardarPlantilla.Visible = false;

            RefreshPlantillasGrid();

            MostrarMensaje("La plantilla fue guardada con éxito.");
            btn_eliminar.Visible = false;
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
            errorMsg.Text += ex1.Message;
            return;
        }
    }

    private void GridDataEtiquetas()
    {
        grvEtiquetas.Visible = false;
        tblEtiquetas.Visible = true;
    }

    private void ModoVista()
    {
        DDLClaPlantillas.Enabled = false;
        DDLTipoPlantilla.Enabled = false;
        TxtNomPlantilla.Enabled = false;
        TxtDescripcion.Enabled = true;
        Fileload.Visible = false;
        lblCargadePlantilla.Visible = true;
        lblCargadePlantilla.Text = "la plantilla fue cargada con exito";
        lsbAutDisp.Enabled = true;
        lsbAutorizadores.Enabled = true;
        grvEtiquetas.Enabled = false;
        BtnGuardarPlantilla.Visible = false;
        //BtnNueva.Visible = true;
        btnUp.Visible = false;
        ddlvigencia.Enabled = false;
        BtnAgregar.Enabled = true;
        BtnEliminar.Enabled = true;
        
        tblEtiquetas.Visible = false;
        grvEtiquetas.Visible = true;

        lsbAutDisp.Enabled = true;
        lsbAutorizadores.Enabled = true;
    }

    private void CargaArchivoPlantilla()
    {
        PlantillaArchivo plantilla;
        plantilla = DataAcces.GetPlantillaArchivo(11);
    }

    private void CargarGridPlantillas()
    {
        DataTable dataTable = Session["PlantillasGridData"] as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (Session["sorting"] != null)
            {
                string sort = Session["sorting"].ToString();
                dataView.Sort = Session["sortExpression"].ToString() + " " + sort;
            }

            GDVPlantillas.DataSource = dataView;
            GDVPlantillas.DataBind();
        }
    }

    protected void RefreshPlantillasGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ConvertToDataTable(DataAcces.GetPlantillaJuridica());
            Session["PlantillasGridData"] = dt;
            CargarGridPlantillas();
        }
        catch (Exception ex)
        {
            errorMsg.Text += ex.Message + " #7";
            return;
        }
    }

    #endregion

    //protected static DataTable ConvertToDataTable(List<Plantillas> list)
    //{
    //    //this function used solely for sorting in GDVPlantillas_Sorting()
    //    DataTable dt = new DataTable();

    //    dt.Columns.Add("Id");
    //    dt.Columns.Add("Nombre");
    //    dt.Columns.Add("Descripcion");
    //    dt.Columns.Add("VersionDoc");
    //    dt.Columns.Add("Archivo");

    //    foreach (var item in list)
    //    {
    //        var row = dt.NewRow();

    //        row["Id"] = item.Id;
    //        row["Nombre"] = item.Nombre;
    //        row["Descripcion"] = item.Descripcion;
    //        row["VersionDoc"] = item.VersionDoc;
    //        row["Archivo"] = item.Archivo;

    //        dt.Rows.Add(row);
    //    }

    //    return dt;
    //}
}