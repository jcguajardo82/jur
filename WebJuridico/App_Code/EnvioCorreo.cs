using DACJuridico;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for EnvioCorreo
/// </summary>
public static class EnvioCorreo
{

    public class CustomMailMessageDto
    {
        public string Subject { get; set; }
        public string HmlContent { get; set; }
        public string MailTo { get; set; }
        public string Sender { get; set; }
    }
    private static void EnviarCorreo(CustomMailMessageDto correo)
    {
        string log;

        ServicePointManager.Expect100Continue = true;
        log = " ServicePointManager.Expect100Continue = true; <br>";

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        log =log + " ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; <br>";
        Uri apiUrl = new Uri("https://sorianajuridicosendmailqa.azurewebsites.net/api/Sendmail/");

        string inputJson = (new JavaScriptSerializer()).Serialize(correo);
        WebClient client = new WebClient();
        client.Headers["Content-type"] = "application/json";
        client.Encoding = Encoding.UTF8;
        //string json = client.UploadString(apiUrl + "/GetCustomers", inputJson);
        string HtmlResult = client.UploadString(apiUrl, "POST", inputJson);

        Console.WriteLine(HtmlResult);


    }


    /// <summary>
    /// Notificación de Autorización total de la solicitud- Proceso de Contrato CSOR @folio
    /// </summary>
    /// <param name="MailTo"></param>
    /// <param name="folio"></param>
    /// <param name="senderMail"></param>
    /// <returns></returns>
    public static bool Plantilla7(string MailTo, string folio, string senderMail)
    {
        bool success = false;
        try
        {

            DataAcces obj = new DataAcces();

            Entidades.tbl_PlantillasCorreo plantilla = obj.GetPlantillaCorreo(7);
            CustomMailMessageDto correo = new CustomMailMessageDto
            {
                Sender = senderMail,
                Subject = plantilla.Subject.Replace("@folio", folio),
                HmlContent = plantilla.Body.Replace("@folio", folio)
            };


            correo.HmlContent = ReplacePieEncabezado(correo.HmlContent);


            correo.MailTo = MailTo;

            EnviarCorreo(correo);


            success = true;
        }
        catch (Exception)
        {

            success = false;
        }
        return success;
    }

    /// <summary>
    /// Notificación de Rechazo de solicitud- Proceso de Contrato CSOR @folio
    /// </summary>
    /// <param name="correos"></param>
    /// <param name="folio"></param>
    /// <param name="senderMail"></param>
    /// <returns></returns>
    public static bool Plantilla6(string MailTo, string folio, string senderMail, string detalle1, string detalle2, string detalle3)
    {
        bool success = false;
        try
        {

            DataAcces obj = new DataAcces();

            Entidades.tbl_PlantillasCorreo plantilla = obj.GetPlantillaCorreo(6);
            CustomMailMessageDto correo = new CustomMailMessageDto
            {
                Sender = senderMail,
                Subject = plantilla.Subject.Replace("@folio", folio),
                HmlContent = plantilla.Body.Replace("@folio", folio)
            };
            correo.HmlContent = correo.HmlContent.Replace("@detalle1", detalle1.Replace(Environment.NewLine, "<br />"));
            correo.HmlContent = correo.HmlContent.Replace("@detalle2", detalle2.Replace(Environment.NewLine, "<br />"));
            correo.HmlContent = correo.HmlContent.Replace("@detalle3", detalle3.Replace(Environment.NewLine, "<br />"));

            correo.HmlContent = ReplacePieEncabezado(correo.HmlContent);


            correo.MailTo = MailTo;

            EnviarCorreo(correo);


            success = true;
        }
        catch (Exception)
        {

            success = false;
        }
        return success;
    }


    /// <summary>
    /// Notificación de Autorización de solicitud- Proceso de Contrato CSOR @folio
    /// </summary>
    /// <param name="correos"></param>
    /// <param name="folio"></param>
    /// <param name="senderMail"></param>
    /// <returns></returns>
    public static bool Plantilla5(string MailTo, string folio, string senderMail, string detalle1, string detalle2, string detalle3)
    {
        bool success = false;
        try
        {

            DataAcces obj = new DataAcces();

            Entidades.tbl_PlantillasCorreo plantilla = obj.GetPlantillaCorreo(5);
            CustomMailMessageDto correo = new CustomMailMessageDto
            {
                Sender = senderMail,
                Subject = plantilla.Subject.Replace("@folio", folio),
                HmlContent = plantilla.Body.Replace("@folio", folio)
            };
            correo.HmlContent = correo.HmlContent.Replace("@detalle1", detalle1.Replace(Environment.NewLine, "<br />"));
            correo.HmlContent = correo.HmlContent.Replace("@detalle2", detalle2.Replace(Environment.NewLine, "<br />"));
            correo.HmlContent = correo.HmlContent.Replace("@detalle3", detalle3.Replace(Environment.NewLine, "<br />"));

            correo.HmlContent = ReplacePieEncabezado(correo.HmlContent);


            correo.MailTo = MailTo;

            EnviarCorreo(correo);


            success = true;
        }
        catch (Exception)
        {

            success = false;
        }
        return success;
    }

    /// <summary>
    /// Solicitud de revisión de Requerimiento - Proceso de Contrato CSOR  @folio
    /// </summary>
    /// <param name="correos"></param>
    /// <param name="folio"></param>
    /// <param name="senderMail"></param>
    /// <param name="detalle"></param>
    /// <returns></returns>
    /// 


    public static bool Plantilla3(string MailTo, string folio, string senderMail)
    {
        bool success = false;
        try
        {

            DataAcces obj = new DataAcces();

            Entidades.tbl_PlantillasCorreo plantilla = obj.GetPlantillaCorreo(3);
            CustomMailMessageDto correo = new CustomMailMessageDto
            {
                Sender = senderMail,
                Subject = plantilla.Subject.Replace("@folio", folio)
            };


            correo.HmlContent = ReplacePieEncabezado(plantilla.Body);


            correo.MailTo = MailTo;

            EnviarCorreo(correo);


            success = true;
        }
        catch (Exception)
        {

            success = false;
        }
        return success;
    }
    public static bool Plantilla4(List<string> correos, string folio, string senderMail, string detalle)
    {
        bool success = true;
        try
        {

            DataAcces obj = new DataAcces();

            Entidades.tbl_PlantillasCorreo plantilla = obj.GetPlantillaCorreo(4);
            CustomMailMessageDto correo = new CustomMailMessageDto
            {
                Sender = senderMail,
                Subject = plantilla.Subject.Replace("@folio", folio),
                HmlContent = plantilla.Body.Replace("@detalle", detalle.Replace(Environment.NewLine, "<br />"))
            };
            correo.HmlContent = correo.HmlContent.Replace("@folio", folio);

            correo.HmlContent = ReplacePieEncabezado(correo.HmlContent);

            foreach (string item in correos)
            {
                if (!string.IsNullOrEmpty(item.Trim()))
                {
                    correo.MailTo = item;

                    EnviarCorreo(correo);
                }
            }


            //success = true;
        }
        catch (Exception ex)
        {

            success = false;
        }
        return success;
    }
    private static string ReplacePieEncabezado(string body)
    {
        string result = string.Empty;
        DataAcces obj = new DataAcces();

        string header = obj.GetPlantillaCorreo(1).Body;
        string footer = obj.GetPlantillaCorreo(2).Body;

        result = body.Replace("@header", header);
        result = result.Replace("@footer", footer);


        return result;

    }
}