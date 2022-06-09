using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JuridicoConstantes;
using System.Web.Configuration;

public partial class Login : PaginaBase
{
    private readonly String strConnString = Constantes.CadenaDeConexion;
    private String strConStrEslavon = Constantes.CadenaDeConexionEslavon;

    string myGuid = Guid.NewGuid().ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle("Login");

        Session["GUID"] = myGuid;

        LinkButton btnCerrarSesion = (LinkButton)Master.FindControl("btnCerrarSesion");
        btnCerrarSesion.Visible = false;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //alerta("di click al boton");

        errorMsg.Text = ""; //clear old errors

        if (ConfiguradoParaDesarrollo) {
            abrirSesionDesarrollo(TxtUsuarios.Text, Txtpassword.Text);
        }
        else
        {
            abrirSesionProduccion(TxtUsuarios.Text, Txtpassword.Text);
        }
    }

    //private void alerta(string x)
    //{
    //    //ScriptManager.RegisterClientScriptBlock(this.Page,
    //    //           this.Page.GetType(),
    //    //           "script",
    //    //           "<script type='text/javascript'>alert('" + x + "');</script>", false);

    //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + x + "');", true);
    //}

    private void abrirSesionDesarrollo(string user, string pass)
    {
        try
        {
            SqlConnection connJuridico = new SqlConnection(strConnString);

            //if (TxtUsuarios.Text.Length > 9)
            //{
            //    MostrarMensaje("El usuario tiene " + TxtUsuarios.Text.Length.ToString() + " digitos, debe ser de 9.");
            //    return;
            //}

            //if (ToInt32_0(TxtUsuarios.Text) == 0 && TxtUsuarios.Text != "0")
            //{
            //    MostrarMensaje("El usuario debe ser un número. No se pudo convertir el usuario a tipo numérico.");
            //    return;
            //}

            SqlCommand cmd;

            cmd = new SqlCommand("SCRIPTESLABON", connJuridico);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pEmpleado", TxtUsuarios.Text.Trim());//Convert.ToInt32(TxtUsuarios.Text));
            cmd.Parameters.AddWithValue("@pClave", Txtpassword.Text);
            cmd.Parameters.AddWithValue("@pOpcion", 1);

            SqlCommand cmd2 = new SqlCommand("sp_ExistUser", connJuridico);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@empleado", TxtUsuarios.Text);

            connJuridico.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.FieldCount > 1)
            {
                while (reader.Read())
                {
                    Session["Nombre"] = TxtUsuarios.Text + " " + reader["Nombre"].ToString();
                    Session["NumError"] = reader["Valido"].ToString();
                }

                if (ToInt32_0(Session["NumError"]) == 1)
                {
                    reader.Close();

                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Session["Perfil"] = reader2["id_nperfil"].ToString();
                        Session["idUsuario"] = reader2["id_usuario"].ToString();
                        Session["PerfilDesc"] = reader2["Descripcion"].ToString();
                        Session["email"] = reader2["email"].ToString();
                    }

                    if (ToInt32_0(Session["Perfil"]) == 0)
                    {
                        MostrarMensaje("Usuario no Registrado en el sistema (tbl_Usuario)");
                        return;
                    }

                    switch (ToInt32_0(Session["Perfil"]))
                    {
                        case 1:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "~/Solicitudes/Consultar.aspx"); //Administrador General
                            break;
                        case 2:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "~/Solicitudes/Consultar.aspx"); //Asistente Poderes
                            break;
                        case 3:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "~/Solicitudes/Consultar.aspx"); //Asistente Contratos
                            break;
                        case 4:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "~/Solicitudes/Consultar.aspx"); //Solicitador
                            break;
                        case 5:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "~/Solicitudes/Autorizar.aspx"); //Autorizador
                            break;
                        case 6:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "~/Procesos/Bitacora.aspx"); //Abogado
                            break;

                        default:
                            Response.Redirect("~/Default.aspx");
                            break;
                    }
                }
                else
                {
                    MostrarMensaje("Usuario no Registrado en el sistema");
                }
            }
            else
            {
                MostrarMensaje("El Usuario no es Válido");
            }

            connJuridico.Close();

        }
        catch (Exception ex)
        {
            MostrarMensaje(ex.Message);
            errorMsg.Text += ex.Message;
        }
    }
    
    private void abrirSesionProduccion(string user, string pass)
    {
        try
        {
            //SqlConnection connEslavon = new SqlConnection(strConStrEslavon);
            SqlConnection connEslavon = new SqlConnection(strConnString);
            SqlConnection connJuridico = new SqlConnection(strConnString);

            if (TxtUsuarios.Text.Length > 9)
            {
                MostrarMensaje("El usuario tiene " + TxtUsuarios.Text.Length.ToString() + " digitos, debe ser de 9.");
                return;
            }

            if (ToInt32_0(TxtUsuarios.Text) == 0 && TxtUsuarios.Text != "0")
            {
                MostrarMensaje("El usuario debe ser un número. No se pudo convertir el usuario a tipo numérico.");
                return;
            }

            SqlCommand cmd;

            cmd = new SqlCommand("sp_ConsultaEmpleado_pUP", connEslavon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pEmpleado", Convert.ToInt32(TxtUsuarios.Text));
            cmd.Parameters.AddWithValue("@pClave", Txtpassword.Text);
            cmd.Parameters.AddWithValue("@pOpcion", 1);

            SqlCommand cmd2 = new SqlCommand("sp_ExistUser", connJuridico);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@empleado", TxtUsuarios.Text);

            connEslavon.Open();
                
            SqlDataReader reader = cmd.ExecuteReader();
            
            //nota: el SP sp_ConsultaEmpleado_pUP *siempre* devuelve un row, aun cuando el usuario 
            //no existe o el password es incorrecto. la unica diferencia es en el NumError.
            if (reader.FieldCount > 1)
            {
                while (reader.Read())
                {
                    Session["Nombre"] = TxtUsuarios.Text + " " + reader["Nombre_Completo"].ToString();
                    Session["NumError"] = reader["NumError"].ToString();
                }

                //codigos de estatus devueltos por Intelexion "NumError":
                //NumError == 0 Sin error
                //NumError == 1 La clave no es correcta.
                //NumError == 2 El empleado no existe.

                if (ToInt32_0(Session["NumError"]) == 1)
                {
                    MostrarMensaje("La clave no es correcta.");
                    return;
                }

                if (ToInt32_0(Session["NumError"]) == 2)
                {
                    //fix para usuarios de prueba produccion (quitar despues) *****
                    //List<int> userIdsDePrueba = new List<int> { 990000001, 990000002, 990000003, 990000004, 990000005, 990000006, 990000007 };
                    //if (userIdsDePrueba.Contains(ToInt32_0(TxtUsuarios.Text))) {
                    //    Session["NumError"] = 0;
                    //} else {  


                        MostrarMensaje("El empleado no existe.");
                        return;

                    //}

                }

                if (ToInt32_0(Session["NumError"]) == 0) //sin error
                {
                    reader.Close();

                    connJuridico.Open();

                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        Session["Perfil"] = reader2["id_nperfil"].ToString();
                        Session["idUsuario"] = reader2["id_usuario"].ToString();
                        Session["PerfilDesc"] = reader2["Descripcion"].ToString();
                        Session["email"] = reader2["email"].ToString();
                    }

                    if (ToInt32_0(Session["Perfil"]) == 0)
                    {
                        errorMsg.Text += "reader2.Read() was not read. No row returned from sp";
                        return;
                    }

                    connJuridico.Close();

                    connJuridico.Dispose();


                    switch (ToInt32_0(Session["Perfil"]))
                    {
                        case 1:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Solicitudes/Consultar.aspx"); //Administrador General
                            break;
                        case 2:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Solicitudes/Consultar.aspx"); //Asistente Poderes
                            break;
                        case 3:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Solicitudes/Consultar.aspx"); //Asistente Contratos
                            break;
                        case 4:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Solicitudes/Consultar.aspx"); //Solicitador
                            break;
                        case 5:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Solicitudes/Autorizar.aspx"); //Autorizador
                            break;
                        case 6:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Procesos/Bitacora.aspx"); //Abogado
                            break;

                        default:
                            Response.Redirect(WebConfigurationManager.AppSettings["VD"] + "/Default.aspx");
                            break;
                    }
                }

                else
                {
                    errorMsg.Text += "El usuario no es válido.";
                }
            }

            connEslavon.Close();                
        }
        catch (Exception ex)
        {
            MostrarMensaje(ex.Message);
            errorMsg.Text += ex.Message;
        }
    }
}