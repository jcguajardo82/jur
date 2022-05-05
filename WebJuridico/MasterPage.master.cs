using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : MasterBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["appTitle"] = System.Configuration.ConfigurationManager.AppSettings["appTitle"].ToString();

        Label1.Text= System.Configuration.ConfigurationManager.AppSettings["appTitle"].ToString();
        //imageApp.Src = System.Configuration.ConfigurationManager.AppSettings["appLogo"].ToString();
        
        if (!Page.IsPostBack)
        {
            if (!HttpContext.Current.Request.Url.AbsolutePath.Contains("LogIn.aspx"))
            {
                if (Session["idUsuario"] == null)
                {
                    Response.Redirect("~/LogIn.aspx");
                }

                else
                {
                    cargarMenu();
                    mostrarNombre();
                }
            }
        }
    }

    private void cargarMenu()
    {
        List<Tbl_Menu> menus = new List<Tbl_Menu>();

        //Traemos los datos.
        if (Session["Menu"] == null)
        {
            menus = DataAcces.GetMenuItems(Session["Perfil"].ToString());
            Session["Menu"] = menus;
        }
        else
        {
            menus = (List<Tbl_Menu>)Session["Menu"];
        }

        foreach (Tbl_Menu menu in menus)
        {
            //esta condicion indica q son elementos padre.
            if (menu.idMenu == menu.PadreId)
            {
                MenuItem mnuMenuItem = new MenuItem();
                mnuMenuItem.Value = menu.idMenu.ToString();
                mnuMenuItem.Text = menu.Descripcion;
                mnuMenuItem.NavigateUrl = menu.Url.Replace("..", "~");

                //agregamos el Ítem al menú
                mnuPrincipal.Items.Add(mnuMenuItem);

                //hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                AddMenuItem(mnuMenuItem, menus);
            }
        }
    }

    private void AddMenuItem(MenuItem mnuMenuItem, List<Tbl_Menu> dtMenuItems)
    {
        //recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
        //del menuitem dado pasado como parametro ByRef.
        foreach (Tbl_Menu menu in dtMenuItems.Where(x => x.PadreId.ToString() == mnuMenuItem.Value && x.PadreId != x.idMenu))
        {
            MenuItem mnuNewMenuItem = new MenuItem();
            mnuNewMenuItem.Value = menu.idMenu.ToString();
            mnuNewMenuItem.Text = menu.Descripcion.ToString();
            mnuNewMenuItem.NavigateUrl = menu.Url.Replace("..", "~"); ;

            //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
            mnuMenuItem.ChildItems.Add(mnuNewMenuItem);

            //llamada recursiva para ver si el nuevo menú ítem aun tiene elementos hijos.
            AddMenuItem2(mnuNewMenuItem, dtMenuItems);
        }
    }

    private void AddMenuItem2(MenuItem mnuMenuItem, List<Tbl_Menu> dtMenuItems)
    {
        //recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
        //del menuitem dado pasado como parametro ByRef.
        foreach (Tbl_Menu drMenuItem in dtMenuItems.Where(x => x.PadreId.ToString() == mnuMenuItem.Value && x.idMenu != x.PadreId))
        {
            MenuItem mnuNewMenuItem = new MenuItem();
            mnuNewMenuItem.Value = drMenuItem.idMenu.ToString();
            mnuNewMenuItem.Text = drMenuItem.Descripcion;
            mnuNewMenuItem.NavigateUrl = drMenuItem.Url.Replace("..", "~");

            //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
            mnuMenuItem.ChildItems.Add(mnuNewMenuItem);
        }
    }

    private void mostrarNombre()
    {
        try
        {
            if (string.IsNullOrEmpty(Session["Nombre"].ToString()))
            {
                labelNombre.Text = string.Empty;
            }
            else
            {
                labelNombre.Text = Session["Nombre"].ToString();
            }
            if (string.IsNullOrEmpty(Session["PerfilDesc"].ToString()))
            {
                lblPerfil.Text = string.Empty;
            }
            else
            {
                lblPerfil.Text = " : " + Session["PerfilDesc"].ToString();
            }
        }

        catch (NullReferenceException ex)
        {
            labelNombre.Text = string.Empty;
            lblPerfil.Text = string.Empty;
        }

        catch (Exception ex)
        {

        }
    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        //Session["Perfil"] = null;
        //Session["idUsuario"] = null;
        //Session["PerfilDesc"] = null;
        //Session["GUID"] = null;

        Session.Abandon();

        Response.Redirect("~/LogIn.aspx");
    }

    public Label lblNombre
    {
        get { return this.labelNombre; }
        set { labelNombre = value; }
    }
}
