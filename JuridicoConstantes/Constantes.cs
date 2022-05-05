using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuridicoConstantes
{
    public class Constantes
    {
        #region Campos

        //public static readonly string CadenaDeConexionEslavon = "Data Source=DESARROOPER;initial catalog=AdminJurProdDB;user id=dbotienda; password=grabar";//"Data Source=SRVNOMINA;Database=Eslabon; user id=cahorro; password=cahorro;";
        //public static readonly string CadenaDeConexionEslavon = "Server=localhost;Database=AdminJurProdDB;Trusted_Connection=True;"; //Usado para pruebas en localhost con TRUSTED_CONNECTION
        //public static readonly string CadenaDeConexion =        "Data Source=SRVADMIN01;initial catalog=AdmJurProdDB;user id=Juridico; password=Inst.2020";
        //public static readonly string CadenaDeConexion = "Data Source=allwareserver\\allwaredb;initial catalog=juridico; user id=juridico; password=juridico";
        //public static readonly string CadenaDeConexion = "Data Source=DESARROOPER;initial catalog=AdminJurProdDB;user id=dbotienda; password=grabar";
        //public static readonly string CadenaDeConexion = "Server=localhost;Database=AdminJurProdDB;Trusted_Connection=True;"; //Usuado para pruebas en localhost con TRUSTED_CONNECTION
        //public static readonly string CadenaDeConexion =   "Data Source=AdmJurProdDB.mssql.somee.com;initial catalog=AdmJurProdDB;user id=jcguajardo_SQLLogin_2; password=emwlm125kc";

        //public static readonly string CadenaDeConexionEslavon = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["flagWebConection"]) ? System.Configuration.ConfigurationSettings.AppSettings["WebConection"] : "Server=localhost;Database=AdminJurProdDB;Trusted_Connection=True;"; //Usado para pruebas en localhost con TRUSTED_CONNECTION
        //public static readonly string CadenaDeConexion = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["flagWebConection"]) ? System.Configuration.ConfigurationSettings.AppSettings["WebConection"] : "Server=localhost;Database=AdminJurProdDB;Trusted_Connection=True;"; //Usuado para pruebas en localhost con TRUSTED_CONNECTION


        public static readonly string CadenaDeConexionEslavon = System.Configuration.ConfigurationManager.ConnectionStrings["WebConection"].ConnectionString;
        public static readonly string CadenaDeConexion = System.Configuration.ConfigurationManager.ConnectionStrings["WebConection"].ConnectionString;



        //nota: para produccion, seleccionar servidor DB SRVADMIN01                         *****
        //nota: para produccion, en PaginaBase.cs poner ConfiguradoParaDesarrollo = false   *****
        //nota: para produccion, en Web.Config quitar etiqueta <customErrors mode="Off" />  *****
        //nota: para produccion, en Web.Config poner compilation debug="false"              *****
        //nota: para produccion, falta configurar servidor email en PaginaBase.cs           *****
        //nota: para produccion, Create a Transact-SQL Job Step, habilitar sp_send_dbmail   *****

        #endregion
    }
}
