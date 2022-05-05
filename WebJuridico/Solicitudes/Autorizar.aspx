<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Autorizar.aspx.cs" Inherits="Solicitudes_Autorizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function MostrarConfirmacion() {
            $("#dialog-form").dialog(
                {
                    autoOpen: true,
                    height: 300,
                    width: 350,
                    modal: true,
                });
        }
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">

    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updMain" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="height: 20px"></div>
            <table id="tblPrincipal" runat="server" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="width: 100%; align-content: center; vertical-align: top">
                        <table runat="server" id="tblSelSolicitud" style="width: 800px; align-self: center; margin-left: auto; margin-right: auto">
                            <tr>
                                <td colspan="2">
                                    <asp:Label runat="server" CssClass="title" ID="lblTitulo">Autorizar Solicitudes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="align-content: center">
                                        <asp:GridView ID="grvSolicitudes" runat="server"
                                            Width="1155px"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            AutoGenerateColumns="False"
                                            CaptionAlign="Left" CellPadding="4"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            PageSize="13"
                                            Font-Size="12px"
                                            HeaderStyle-Font-Bold="true"
                                            ShowHeaderWhenEmpty="True"
                                            OnRowDataBound="grvSolicitudes_RowDataBound" 
                                            OnPageIndexChanging="grvSolicitudes_PageIndexChanging"
                                            OnSorting="grvSolicitudes_Sorting"
                                            OnRowCommand="grvSolicitudes_RowCommand">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSolicitudId" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPlantilla" Text='<%# Eval("IdPlantilla") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Fecha" SortExpression="Fecha" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha" ItemStyle-Width="95px" DataFormatString="{0:yyyy-MM-dd}" />

                                                <asp:TemplateField HeaderStyle-Width="125px" SortExpression="Folio" HeaderText="Folio" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnFolio" runat="server" CommandName="VerSolicitud" CommandArgument='<%# Eval("Id") %>' Text='<%# Eval("Folio") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatusId" Text='<%# Eval("IdStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

<%--                                                <asp:BoundField DataField="Folio" SortExpression="Folio" ItemStyle-HorizontalAlign="Center" HeaderText="Folio" ItemStyle-Width="125px" />--%>

                                                <asp:BoundField DataField="Tipo" SortExpression="Tipo" ItemStyle-HorizontalAlign="Center" HeaderText="Tipo de plantilla" ItemStyle-Width="330px" />
                                                <asp:BoundField DataField="Solicitante" SortExpression="Solicitante" ItemStyle-HorizontalAlign="Center" HeaderText="Nombre solicitador" ItemStyle-Width="235px" />
                                                <asp:BoundField DataField="Clasificacion" SortExpression="Clasificacion" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                                                <asp:BoundField DataField="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center" HeaderText="Estatus" ItemStyle-Width="180px" />

<%--                                                <asp:CommandField ShowEditButton="true" ItemStyle-HorizontalAlign="Center" EditText="Revisar" HeaderText="Operación" ItemStyle-Width="90px" />--%>

                                                <asp:TemplateField HeaderStyle-Width="90px" HeaderText="Operación">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRevisar" runat="server" CommandName="Revisar" CommandArgument='<%# Eval("Id") %>' Text="Revisar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <%--<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />--%>
                                            <PagerSettings PageButtonCount="8" />
                                            <%--<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />--%>
                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#edece6" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                            <SortedDescendingHeaderStyle BackColor="#820000" />
                                        </asp:GridView>
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

