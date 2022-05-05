<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reportes.aspx.cs" Inherits="Reportes_Reportes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="Server">
    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updReport">
        <ContentTemplate>
            <rsweb:ReportViewer ID="rpvMain" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <%--<LocalReport ReportPath="Reportes\CartasPoder.rdlc">
                </LocalReport>--%>
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

