<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SolicitudVobo.aspx.cs" Inherits="Solicitudes_SolicitudVobo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/MaxLength.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //Normal Configuration
            $("[id*=txtDesc]").MaxLength({ MaxLength: 200 });

            //Specifying the Character Count control explicitly
            //$("[id*=TextBox2]").MaxLength(
            //{
            //    MaxLength: 15,
            //    CharacterCountControl: $('#counter')
            //});

            ////Disable Character Count
            //$("[id*=TextBox3]").MaxLength(
            //{
            //    MaxLength: 20,
            //    DisplayCharacterCount: false
            //});
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="Server">
    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>

    <div style="padding: 15px">
        <asp:Label runat="server" CssClass="title" ID="lblTitulo">Áreas de afectación a involucrar en el proceso</asp:Label>
    </div>

    <asp:Panel ID="PanelBotonesPrincipales" runat="server">
        <div style="padding: 15px; text-align: left; width: 93%">
            <asp:TextBox runat="server" ID="txtBusqueda" CssClass="textboxGeneral"></asp:TextBox>
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnGenerarSol" Text="Generar Solicitud" OnClick="btnGenerarSol_Click" />
            <br />
        </div>
    </asp:Panel>
    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>
            <asp:Panel ID="PanelSol" runat="server" Style="padding: 15px; text-align: center; width: 93%;">
                <input type="hidden" runat="server" id="hddIdSol" />
                <table style="width: 100%;" border="0">
                    <thead>
                        <tr>
                            <td colspan="5" style="text-align: center; background-color: #B0A8CF; font-weight: bold;">Áreas de afectación a involucrar en el proceso
                            </td>
                        </tr>
                    </thead>
                    <tr>
                        <td colspan="5" style="text-align: left; font-weight: bold;">
                            <hr />
                            Favor de incluir los correos de hasta 5 subdirectores o directores a los que se les compartirá el requerimiento para su revisión.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtCorreo1" placeholder="correo1@soriana.com"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCorreo2" placeholder="correo2@soriana.com"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCorreo3" placeholder="correo3@soriana.com"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCorreo4" placeholder="correo4@soriana.com"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCorreo5" placeholder="correo5@soriana.com"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center; font-weight: bold;">
                            <hr />
                            Detalle y descripción del negocio a revisar
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center;">
                            <div id="counter"></div>
                            <asp:TextBox Style="width: 100%" runat="server" ClientIDMode="Static" ID="txtDesc" MaxLength="200" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center; background-color: #B0A8CF; font-weight: bold;">Documentos de Soporte
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                        </td>
                        <td colspan="3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No se han subido archivos"
                                Width="100%"
                                AllowPaging="false"
                                AllowSorting="false"
                                CaptionAlign="Left"
                                CellPadding="4"
                                ForeColor="#333333"
                                GridLines="None"
                                PageSize="13"
                                Font-Size="12px"
                                HeaderStyle-Font-Bold="true"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre del Archivo" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" Text="Eliminar" CommandArgument='<%# Eval("Nombre") %>' runat="server" OnClick="lnkDelete_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <PagerSettings PageButtonCount="5" />
                                <%--<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />--%>
                                <RowStyle BackColor="#edece6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

