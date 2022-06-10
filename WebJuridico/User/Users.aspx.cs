using DACJuridico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class User_Users : PaginaBase
{
    private static IntelexDA ida;

    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Administración De Usuarios");

        if (!Page.IsPostBack)
        {
            Session["UsuariosGridData"] = null;

            CargarGrid();
            CargarDDL();
        }
    }

    protected void LoadUsuariosIntoSession()
    {
        if (Session["UsuariosGridData"] == null)
        {
            Exception ex = new Exception();

            try
            {
                DataTable dt = new DataTable();
                dt = ConvertToDataTable(DataAcces.GetUsuarios(ref ex));

                if (dt == null)
                {
                    errorMsg.Text += "dt is null. " + ex.Message;
                }

                else
                {
                    Session["UsuariosGridData"] = dt;
                }
            }
            catch (Exception ex2)
            {
                errorMsg.Text += ex.Message + "; " + ex2.Message;
                return;
            }
        }

        ////replace all of the above with:

        //if (Session["UsuariosGridData"] == null)
        //{
        //    DataTable dt = new DataTable();
        //    dt = ConvertToDataTable(DataAcces.GetUsuarios());
        //    Session["UsuariosGridData"] = dt;
        //}
    }

    #region "Menu"

    private void CargarDDL()
    {
        DDLTusuario.DataSource = DataAcces.GetPerfiles();
        DDLTusuario.DataTextField = "Nombre";
        DDLTusuario.DataValueField = "id_nperfil";
        DDLTusuario.DataBind();
        DDLTusuario.Items.Insert(0, new ListItem("<Seleccione un Perfil>", "0"));
    }

    private void GuardarUsuario()
    {
        tbl_usuario user;
        int res = 0;

        if (ToInt32_0(DDLTusuario.SelectedValue) == 0)
        {
            MostrarMensaje("Debe escoger un perfil.");
            return;
        }

        user = new tbl_usuario()
        {
            NEmpleado = ToInt32_0(TxtNoEmpleado.Text),
            Nombre = TxtNEmpleado.Text,
            id_nperfil = Convert.ToInt32(DDLTusuario.SelectedValue),
            Email = TxtEmailEmpleado.Text
        };
        if (TxtNEmpleado.Text == "")
        {
            if (DDLTusuario.SelectedValue == "7")
            {
                MostrarMensaje("Registra al menos un carácter.");
            }
            else
            {
                res = DataAcces.CreateUsuario(user);
            }
        }
        else
        {
            res = DataAcces.CreateUsuario(user);
        }


        if (res > 0)
        {
            limpiarCampos();

            Session["UsuariosGridData"] = null;
            CargarGrid();

            MostrarMensaje("Usuario grabado con éxito.");
        }
        else if (res == -1)
        {
            limpiarCampos();
            MostrarMensaje("No se puede grabar: El número ya ha sido usado anteriormente.");
        }
        else
        {
            limpiarCampos();
            MostrarMensaje("Ocurrió un error al guardar el usuario.");
        }
    }

    #endregion

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {
        if (ToInt32_0(TxtNoEmpleado.Text) == 0)
        {
            if (DDLTusuario.SelectedValue != "7") // 7 = Otorgante-Testigo
            {
                MostrarMensaje("No se puede grabar: Capture el número del empleado");
                TxtNoEmpleado.Text = "";
                return;
            }
        }

        GuardarUsuario();
        limpiarCampos();
        CargarGrid();
    }

    protected void DDLTusuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        BtnGuardar.Enabled = true;
        if (DDLTusuario.SelectedValue == "7")
        {
            TxtNoEmpleado.Text = "";
            TxtNEmpleado.Text = "";
            TxtNoEmpleado.Enabled = false;
            TxtNoEmpleado.BackColor = Color.WhiteSmoke;
            TxtNEmpleado.Enabled = true;
            TxtNEmpleado.BackColor = Color.White;
        }
        else
        {
            TxtNoEmpleado.Text = "";
            TxtNEmpleado.Text = "";
            TxtNoEmpleado.Enabled = true;
            TxtNoEmpleado.BackColor = Color.White;
            TxtNEmpleado.Enabled = false;
            TxtNEmpleado.BackColor = Color.WhiteSmoke;
        }
        if (Convert.ToInt16(DDLTusuario.SelectedValue) == 0)
        {
            BtnGuardar.Enabled = false;
        }
        else
        {
            BtnGuardar.Enabled = true;
        }
    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        Usuario usuario;
        int numEmpleado;
        int resultId = -1;

        if (string.IsNullOrEmpty(TxtNoEmpleado.Text))
        {
            MostrarMensaje("Debe capturar el numero de empleado.");
            limpiarCampos();
            return;
        }

        if (int.TryParse(TxtNoEmpleado.Text, out numEmpleado))
        {
            //buscar usuario en Base de Datos local:
            usuario = DataAcces.GetUser(numEmpleado);

            if (usuario == null)
            {
                if (ConfiguradoParaDesarrollo == true)
                {
                    TxtNEmpleado.Text = "NOMBRE DE PRUEBA DESARROLLO";
                    BtnGuardar.Enabled = true;
                    return;
                }
                else
                {
                    //si no existe en Base de Datos local, buscar en Intelexion (Eslabon):
                    usuario = Intelex.GetEslavonUsuario(numEmpleado, ref resultId);
                }

                if (usuario == null)
                {
                    //si el usuario no está en base de datos local, ni en Intelexion:
                    MostrarMensaje("El número de empleado no existe.");
                    limpiarCampos();
                    BtnGuardar.Enabled = false;
                }
                else
                {
                    TxtNEmpleado.Text = usuario.Nombre;
                    BtnGuardar.Enabled = true;
                }
            }
            else
            {
                MostrarMensaje("El número de empleado ya existe en Sistema Juridico.");
                DDLTusuario.SelectedValue = usuario.PerfilId.ToString();
                TxtNEmpleado.Text = usuario.Nombre;
                BtnGuardar.Enabled = false;
            }
        }
        else
        {
            MostrarMensaje("Debe capturar un número válido de empleado.");
            TxtNoEmpleado.Text = "";
        }
    }

    private void limpiarCampos()
    {
        DDLTusuario.SelectedIndex = 0;
        BtnGuardar.Enabled = false;
        TxtNEmpleado.Text = "";
        TxtNEmpleado.Enabled = true;
        TxtNoEmpleado.Enabled = true;
        TxtNEmpleado.BackColor = Color.White;
        TxtNoEmpleado.BackColor = Color.White;
        TxtNoEmpleado.Text = "";
        TxtEmailEmpleado.Text = "";
    }

    #region "Gridview"

    private void CargarGrid()
    {
        LoadUsuariosIntoSession(); //cargar usuarios en sesion en caso de que aun no esten

        DataTable dataTable = Session["UsuariosGridData"] as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (Session["sorting"] != null)
            {
                string sort = Session["sorting"].ToString();
                dataView.Sort = Session["sortExpression"].ToString() + " " + sort;
            }

            grvUsuarios.DataSource = dataView;
            grvUsuarios.DataBind();
        }
    }

    protected void grvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvUsuarios.PageIndex = e.NewPageIndex;
        CargarGrid();
    }

    protected void grvUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvUsuarios.EditIndex = -1;
        CargarGrid();
    }

    protected void grvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int usuarioId;
        bool borrado;

        usuarioId = Convert.ToInt32(((Label)grvUsuarios.Rows[e.RowIndex].FindControl("lblIdUsuario")).Text);

        Exception ex = new Exception();

        borrado = DataAcces.DeleteUsuario(usuarioId, ref ex);

        if (borrado)
        {
            try
            {
                CargarGrid();
                limpiarCampos();

                Session["UsuariosGridData"] = null;
                CargarGrid();

                MostrarMensaje("El Usuario ha sido borrado con éxito.");
            }
            catch (Exception ex2)
            {
                MostrarMensaje("Ocurrió un error en el borrado. #2, " + ex2.Message);
                errorMsg.Text = "2#: " + ex2.Message;
            }
        }
        else
        {
            MostrarMensaje("Ocurrió un error en el borrado. " + ex.Message);
            errorMsg.Text = ex.Message;
        }
    }

    protected void grvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvUsuarios.EditIndex = e.NewEditIndex;
        CargarGrid();
    }
    #endregion

    protected void grvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Nombre")
        {
            // Retrieve the row index stored in the CommandArgument property.
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = grvUsuarios.Rows[index];

            //copy to numero empleado
            TxtNoEmpleado.Text = row.Cells[3].Text;

            if (row.RowType == DataControlRowType.DataRow)
            {
                //copy to nombre empleado
                TxtNEmpleado.Text = (row.FindControl("lblNombre") as Label).Text;

                //copy to tipo de usuario
                DDLTusuario.SelectedValue = (row.FindControl("lblPerfilId") as Label).Text;

                //copy email
                TxtEmailEmpleado.Text = (row.FindControl("lblEmailEmpleado") as Label).Text;
            }
        }
    }

    protected void grvUsuarios_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = Session["UsuariosGridData"] as DataTable;

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

            grvUsuarios.DataSource = dataView;
            grvUsuarios.DataBind();
        }
    }

    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        limpiarCampos();
    }

    #region Propiedades

    private static IntelexDA Intelex
    {
        get
        {
            if (ida == null)
            {
                ida = new IntelexDA();
            }

            return ida;
        }
    }

    protected void grvUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //SqlConnection conn = new SqlConnection(Constantes.CadenaDeConeccion);
        //string id = grvUsuarios.Rows[e.RowIndex].Cells[0].Text;
        //string nombre = grvUsuarios.Rows[e.RowIndex].Cells[0].Text;
        //string TipoPuesto = grvUsuarios.Rows[e.RowIndex].Cells[0].Text;
        //TextBox textadd = (TextBox)row.FindControl("txtadd");
        //TextBox textc = (TextBox)row.FindControl("txtc");
        grvUsuarios.EditIndex = -1;
        //conn.Open();
        //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);
        //SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", conn);
        //cmd.ExecuteNonQuery();
        //conn.Close();
        CargarGrid();
        //grvUsuarios.DataBind();
    }

    #endregion

}