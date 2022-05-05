using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reportes_Reportes : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        verificarSesionAbierta();

        if (!Page.IsPostBack)
        {
            GetReportData();
        }
    }

    private void GetReportData()
    {
        rpvMain.ProcessingMode = ProcessingMode.Local;

        ReportDataSource ds = null;

        if (string.IsNullOrEmpty(Request.QueryString["tipo"]))
        {
            rpvMain.LocalReport.ReportPath = "Reportes\\CartasPoder.rdlc";
            ds = new ReportDataSource("DsData", DataAcces.GetReporteCartasPoder());
        }

        else
        {
            if (Request.QueryString["tipo"] == "1")
            {
                rpvMain.LocalReport.ReportPath = "Reportes\\CartasPoder.rdlc";
                ds = new ReportDataSource("DsData", DataAcces.GetReporteCartasPoder());
            }

            if (Request.QueryString["tipo"] == "2")
            {
                rpvMain.LocalReport.ReportPath = "Reportes\\Contratos.rdlc"; //"Reportes\\GeneralContratos.rdlc";
                ds = new ReportDataSource("DsData", DataAcces.GetReporteContratos());
            }

            if (Request.QueryString["tipo"] == "3")
            {
                rpvMain.LocalReport.ReportPath = "Reportes\\Cancelados.rdlc";
                ds = new ReportDataSource("DsData", DataAcces.GetReportePoderesStatus());
            }

            if (Request.QueryString["tipo"] == "4")
            {
                rpvMain.LocalReport.ReportPath = "Reportes\\Cancelados.rdlc";
                ds = new ReportDataSource("DsData", DataAcces.GetReporteContratosStatus());
            }

        }

        rpvMain.LocalReport.DataSources.Clear();

        rpvMain.LocalReport.DataSources.Add(ds);
    }
}