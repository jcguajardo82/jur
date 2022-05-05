using DACJuridico;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Configuration;

/// <summary>
/// Summary description for PaginaBase
/// </summary>
public abstract class PaginaBase : System.Web.UI.Page
{

    #region Campos

    private OfficeDataAcces oDataAcces;
    private static DataAcces dataAcces;
    public string sistemaURL = WebConfigurationManager.AppSettings["ServerURL"];
    private static readonly string SystemName = "Administrador De Servicios Jurídicos Corporativos";
    private bool invalidEmailAddress = false;
    public bool ConfiguradoParaDesarrollo = true;

    #endregion

    #region Constructor

    public PaginaBase()
        : base()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Propiedades

    protected static DataAcces DataAcces
    {
        get
        {
            if (dataAcces == null)
            {
                dataAcces = new DataAcces();
            }

            return dataAcces;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns a formatted Page Title, with the System's name and a separating dash.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public void PageTitle(string input)
    {
        var page = (Page)HttpContext.Current.Handler;
        page.Title = input + " - " + SystemName;
    }

    protected void MostrarMensaje(string mensaje)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page,
                this.Page.GetType(),
                "script",
                "<script type='text/javascript'>validarPlantilla('" + mensaje + "');</script>", false);
    }

    /// <summary>
    /// Returns a value converted to Int32 format. If conversion fails, 0 will be returned instead.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public int ToInt32_0(object input)
    {
        try
        {
            if (input == null)
                return 0;
            else
                return Convert.ToInt32(input);
        }

        catch (Exception ex)
        {
            return 0;
        }
    }

    /// <summary>
    /// Converts to DateTime, avoiding an Exception if a null value is encountered.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public DateTime? ToDateTime(object input)
    {
        try
        {
            if (input == null)
                return null;
            else

                //CultureInfo MX = new CultureInfo("es-MX");
                //CultureInfo US = new CultureInfo("en-US");
                //return Convert.ToDateTime(input, US.DateTimeFormat).ToString("dd/MM/yyyy"); //"dd/MM/yyyy HH:mm:ss tt"

                return Convert.ToDateTime(input);
        }

        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Send Email to the specified recipient.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    public void EnviarCorreo(string email, string subject, string body)
    {
        SmtpClient server = new SmtpClient("smtp-mail.outlook.com", 587);
        server.Credentials = new System.Net.NetworkCredential("juridicopruebasor@outlook.com", "Allware2015");
        server.EnableSsl = true;

        MailMessage mmsg = new MailMessage();
        //llenado de correo
        mmsg.To.Add(email);
        mmsg.Subject = subject;
        //mmsg.Bcc.Add("destinatariocopia@servidordominio.com"); //Preguntar si quiere agregar alguien a la copia
        mmsg.Body = body;
        mmsg.BodyEncoding = Encoding.UTF8;
        mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML
        mmsg.From = new System.Net.Mail.MailAddress("juridicopruebasor@outlook.com"); //Cuenta del cual saldra el correo

        // Configuracion de SMTP


        //envio de Correo
        try
        {
            server.Send(mmsg);
            //Enviamos el mensaje      

        }
        catch (System.Net.Mail.SmtpException ex)
        {
            MostrarMensaje("Ocurrió un problema al enviar el correo.");
        }
    }

    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        if (data == null)
        {
            return new DataTable();
        }

        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;
    }

    public void DescargarDocumentos(int tipo, int id)
    {
        string url = null;

        if (tipo == 1)//1-Plantillas (DOCX)
        {
            url = "../download.aspx/?tipo=1&id=";
        }

        else if (tipo == 2)//2-Solicitudes (DOCX editados)
        {
            url = "../download.aspx/?tipo=2&id=";
        }

        else if (tipo == 3)//3-PDFs de la solicitud
        {
            url = "../download.aspx/?tipo=3&id=";
        }

        url = url + id.ToString();

        string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    public void DescargarDocumentosArchivoTemporal(byte[] archivo, string nombre)
    {
        Session["ArchivoTemporal"] = null;
        Session["ArchivoTemporal"] = archivo;

        string url = "../download.aspx/?tipo=4&nombre=" + nombre;

        string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    public bool IsValidEmail(string strIn)
    {
        invalidEmailAddress = false;
        if (String.IsNullOrEmpty(strIn))
            return false;

        // Use IdnMapping class to convert Unicode domain names.
        strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
        if (invalidEmailAddress)
            return false;

        // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(strIn,
               @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
               RegexOptions.IgnoreCase);
    }

    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalidEmailAddress = true;
        }
        return match.Groups[1].Value + domainName;
    }

    public string getSortDirectionString(string sortDirection)
    {
        string newSortDirection = String.Empty;

        if (sortDirection == "ASC")
        {
            newSortDirection = "DESC";
        }

        else
        {
            newSortDirection = "ASC";
        }

        return newSortDirection;
    }

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

    public OfficeDataAcces ODataAcces
    {
        get
        {
            if (oDataAcces == null)
            {
                oDataAcces = new OfficeDataAcces();
            }

            return oDataAcces;
        }
    }

    #endregion

}