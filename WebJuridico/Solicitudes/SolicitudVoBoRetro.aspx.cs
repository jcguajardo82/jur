using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes_SolicitudVoBoRetro : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        PageTitle("Áreas de afectación a involucrar en el proceso");

        int perf = ToInt32_0(Session["perfil"]);

        List<int> PerfilesPermitidos = new List<int> { 1, 4 }; // “AdmGral” y “Solicitador” 

        var solicitudId = ToInt32_0(Request.QueryString["id"]);
        var correo = (Request.QueryString["correo"]).ToString();

        if (!PerfilesPermitidos.Contains(perf))
        {

            //MostrarMensage("El acceso no está permitido para su tipo de perfil.");
            PanelBotonesPrincipales.Visible = false;
            PanelSol.Visible = false;
            return;
        }
        else
        {
            PanelBotonesPrincipales.Visible = true;
        }

        if (!IsPostBack)
        {
            iniciaControles();


            if (!string.IsNullOrEmpty(correo))
            {
                CargarDDLCorreos(solicitudId, correo);
                var si = ddlCorreos.Items.FindByText(correo);
                if (si != null)
                {
                    ddlCorreos.SelectedValue = ddlCorreos.Items.FindByText(correo).Value;
                    ddlCorreos_SelectedIndexChanged(this, null);
                    btnAutorizar.Visible = true;
                    btnRechazar.Visible = true;
                }
                else
                {
                    btnAutorizar.Visible = false;
                    btnRechazar.Visible = false;
                }


            }
            else
            {
                CargarDDLCorreos(solicitudId, "");
            }
        }
    }

    protected void btnAutorizar_Click(object sender, EventArgs e)
    {


        if (txtComentarios.Text.Trim().Length == 0)
        {
            MostrarMensaje("Debe ingresar los comentarios al negocio.");
            txtComentarios.Focus();
            return;
        }

        if (txtRiesgos.Text.Trim().Length == 0)
        {
            MostrarMensaje("Debe ingresar los riesgos destacados.");
            txtComentarios.Focus();
            return;
        }

        bool aut = false;

        if (((Button)sender).CommandArgument.Equals("1"))
        {
            aut = true;
        }


        var solicitud = new tbl_VoBoSolicitudesRetro()
        {
            Id_voBoSolRetro = int.Parse(ddlCorreos.SelectedValue),
            comentariosNegocio = txtComentarios.Text,
            riesgosDestacados = txtRiesgos.Text,
            autorizado = aut,
            correo = ddlCorreos.SelectedItem.Text
        };

        DataAcces.tbl_VoBoSolicitudesRetro_uUp(solicitud);

        if (aut)
        {
            try
            {
                if (!EnvioCorreo.Plantilla5(solicitud.correo, lblFolio.Text, Session["email"].ToString(), txtDesc.Text, solicitud.comentariosNegocio
                    , solicitud.riesgosDestacados, int.Parse(lblIdSolicitud.Text)))
                {
                    MostrarMensaje("Operación realizada con éxito. No se ha podido mandar el  correo de notificación de autorización.");
                }
            }
            catch (Exception)
            {

                MostrarMensaje("Operación realizada con éxito. No se ha podido mandar el  correo de notificación autorización.");
            }
        }
        else
        {
            try
            {
                if (!EnvioCorreo.Plantilla6(solicitud.correo, lblFolio.Text, Session["email"].ToString(), txtDesc.Text, solicitud.comentariosNegocio
                    , solicitud.riesgosDestacados, int.Parse(lblIdSolicitud.Text)))
                {
                    MostrarMensaje("Operación realizada con éxito. No se ha podido mandar el  correo de notificación rechazo.");
                }
            }
            catch (Exception)
            {

                MostrarMensaje("Operación realizada con éxito. No se ha podido mandar el  correo de notificación de rechazo.");
            }

        }


        if (DataAcces.ValidaSolicitudVoboPlantilla_sUp(int.Parse(lblIdSolicitud.Text)).Equals("0"))
        {
            try
            {
                if (!EnvioCorreo.Plantilla7(Session["email"].ToString(), lblFolio.Text, Session["email"].ToString()))
                {
                    MostrarMensaje("Operación realizada con éxito. No se ha podido mandar el  correo de notificación de autorización total.");
                }
            }
            catch (Exception)
            {

                MostrarMensaje("Operación realizada con éxito. No se ha podido mandar el  correo de notificación de autorización total.");
            }

        }


        MostrarMensaje("Operación realizada con éxito.");

        iniciaControles();

        ddlCorreos.SelectedIndex = 0;
    }



    protected void ddlCorreos_SelectedIndexChanged(object sender, EventArgs e)
    {
        var id = int.Parse(ddlCorreos.SelectedValue);

        if (id != 0)
        {
            lblCorreo.Text = ddlCorreos.SelectedItem.Text;

            var solicitud = DataAcces.tbl_VoBoSolicitudesRetroById_sUp(id).FirstOrDefault();

            txtComentarios.Text = solicitud.comentariosNegocio;
            txtRiesgos.Text = solicitud.riesgosDestacados;
            txtDesc.Text = solicitud.Detalle;
            lblFolio.Text = solicitud.FolioCompleto;
            lblIdSolicitud.Text = solicitud.id_Solicitud.ToString();
            //if (solicitud.autorizado == null)
            //{
            //    btnAutorizar.Visible = true;
            //    btnRechazar.Visible = true;
            //    txtComentarios.ReadOnly = false;
            //    txtRiesgos.ReadOnly = false;
            //}
            //else
            //{
            //    btnAutorizar.Visible = false;
            //    btnRechazar.Visible = false;
            //    txtComentarios.ReadOnly = true;
            //    txtRiesgos.ReadOnly = true;
            //}
            PanelSol.Visible = true;
        }
        else
        {
            iniciaControles();
        }
    }

    #region Metodos
    private void CargarDDLCorreos(int Id_voBoSol, string correo)
    {

        List<tbl_VoBoSolicitudesRetro> lst = new List<tbl_VoBoSolicitudesRetro>();

        lst = DataAcces.tbl_VoBoSolicitudesRetro_sUp(Id_voBoSol);

        if (!string.IsNullOrEmpty(correo))
        {
          

            lst = lst.Where(x => x.correo == correo).ToList();


        }

        ddlCorreos.DataSource = lst;
        ddlCorreos.DataTextField = "correo";
        ddlCorreos.DataValueField = "Id_voBoSolRetro";
        ddlCorreos.DataBind();

        ddlCorreos.Items.Insert(0, new ListItem("Seleccione un Correo", "0"));


    }

    private void iniciaControles()
    {
        PanelBotonesPrincipales.Visible = true;
        PanelSol.Visible = false;
        txtComentarios.Text = string.Empty;
        txtDesc.Text = string.Empty;
        txtRiesgos.Text = string.Empty;
        //btnAutorizar.Visible = false;
        //btnRechazar.Visible = false;
    }
    #endregion
}