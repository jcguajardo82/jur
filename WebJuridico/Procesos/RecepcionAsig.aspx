<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="RecepcionAsig.aspx.cs" Inherits="Procesos_RecepcionAsig" %>

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

    <style type="text/css">
        .auto-style1 {
            width: 35%;
            height: 21px;
        }
        .auto-style2 {
            width: 65%;
            height: 21px;
        }
    </style>

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
                                    <asp:Label runat="server" CssClass="title" ID="lblTitulo">Recepción y Asignación de Solicitudes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="align-content: center">

                                        <asp:Panel ID="PanelRechazar" runat="server" GroupingText="Asignar" Visible="false">
                                            <div style="border: 20px; text-align: center">
                                                Desea rechazar la solicitud?
                                                <br />
                                                <br />
                                                <asp:Button ID="BtnAbogadoRechazar" runat="server" Text="Si" CssClass="Button" OnClick="BtnAbogadoRechazar_Click" /> &nbsp;  &nbsp; 
                                                <asp:Button ID="BtnCancelar" runat="server" Text="No" CssClass="Button" OnClick="BtnCancelar_Click" />
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelAsignarAbogado" runat="server" GroupingText="Asignar" Visible="false">
                                            <br />
                                            Lista de Notarias
                                            <br />
                                            <br />
                                            <asp:RadioButtonList ID="RblAbogado" runat="server"></asp:RadioButtonList>
                                            <br />
                                            <asp:Button ID="BtnAbogadoAsignar" runat="server" Text="Grabar" CssClass="Button" OnClick="BtnAbogadoAsignar_Click" />
                                        </asp:Panel>

                                        <br />

                                        <asp:GridView ID="grvSolicitudes" runat="server"
                                            Width="1300"
                                            Font-Size="12px"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            AutoGenerateColumns="False"
                                            CaptionAlign="Left" CellPadding="4"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            PageSize="13"
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
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatusId" Text='<%# Eval("IdStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFolio" Text='<%# Eval("Folio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Fecha" SortExpression="Fecha" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" />

                                                <asp:TemplateField HeaderStyle-Width="125px" SortExpression="Folio" HeaderText="Folio" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnFolio" runat="server" CommandName="VerSolicitud" CommandArgument="<%# Container.DataItemIndex %>" Text='<%# Eval("Folio") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Tipo" SortExpression="Tipo" ItemStyle-HorizontalAlign="Center" HeaderText="Tipo de plantilla" />
                                                <asp:BoundField DataField="Solicitante" SortExpression="Solicitante" ItemStyle-HorizontalAlign="Center" HeaderText="Nombre solicitador" />
                                                <asp:BoundField DataField="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center" HeaderText="Estatus" />
                                                <asp:BoundField DataField="Clasificacion" SortExpression="Clasificacion" ItemStyle-HorizontalAlign="Center" HeaderText="Clasificación" />
                                                <asp:BoundField DataField="FechaAutorizacion" SortExpression="FechaAutorizacion" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha Autorizada" DataFormatString="{0:yyyy-MM-dd}" />
                                                <asp:BoundField DataField="NombreAutorizador" SortExpression="NombreAutorizador" ItemStyle-HorizontalAlign="Center" HeaderText="Autorizador" />

<%--                                                <asp:CommandField ShowEditButton="true" ItemStyle-HorizontalAlign="Center" EditText="Revisar" HeaderText="Operación" />--%>

                                                <asp:TemplateField HeaderStyle-Width="85px" HeaderText="Operación" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRechazar" runat="server" CommandName="Rechazar" CommandArgument="<%# Container.DataItemIndex %>" Text="Rechazar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="85px" HeaderText="Operación" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAsignar" runat="server" CommandName="Asignar" CommandArgument="<%# Container.DataItemIndex %>" Text="Asignar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                           <%-- <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />--%>
                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                            <PagerSettings PageButtonCount="8" />
                                            <%--<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />--%>
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

            <asp:HiddenField runat="server" ID="hdnSolicitudId" />
            <asp:HiddenField runat="server" ID="hdnFolio" />


            <%-- 
            <table id="tblConsulta" runat="server" style="width: 100%; min-height: 200px; vertical-align: top">
                <tr>
                    <td style="width: 100%; align-content: center; vertical-align: top">
                        <table runat="server" id="Table1" style="width: 800px; align-self: center; margin-left: auto; margin-right: auto">
                            <tr>
                                <td colspan="2">
                                    <asp:Label runat="server" CssClass="title" ID="lblFolio">Detalles de la solicitud Folio: {0}</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <label class="infoLabel">Tipo:</label></td>
                                <td class="auto-style2">
                                    <asp:Label runat="server" ID="lblTipo"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="height: 100px; vertical-align: top">
                                    <label class="infoLabel">Descripcion:</label></td>
                                <td style="height: 100px; vertical-align: top">
                                    <asp:Label runat="server" ID="lblDesc"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="infoLabel">Solicitante:</label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblSolicitante"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="infoLabel">Fecha solicitud:</label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblFecha"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="infoLabel">Status:</label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblEstatus"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    <label class="infoLabel">Etiquetas:</label></td>
                                <td style="vertical-align: top"></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView runat="server"
                                        ID="grvEtiquetas"
                                        AutoGenerateColumns="False"
                                        Width="60%"
                                        ShowHeader="False" CellPadding="2" ForeColor="#000000" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField ControlStyle-Width="40%" ItemStyle-HorizontalAlign="Right" ControlStyle-BorderWidth="0" ControlStyle-BorderColor="White">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblEtiquetaNom" Text='<%# Eval("Etiqueta").ToString().Split((char)60)[0] %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle BorderColor="White" BorderWidth="0px" Width="60%" />
                                            </asp:TemplateField>
                                            <asp:BoundField NullDisplayText=":" />
                                            <asp:TemplateField ControlStyle-Width="0" ItemStyle-HorizontalAlign="Left" Visible="true" ControlStyle-BorderWidth="0" ControlStyle-BorderColor="White">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblEtiqueta" Text='<%# Eval("Valor") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle BorderColor="White" BorderWidth="0px" Width="60%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnRegresar" Text="Regresar" OnClick="btnRegresar_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnReasignar" Text="Asignar" CssClass="rightButton" OnClick="btnAutorizar_Click" />
                                    &nbsp;
                                    <asp:Button runat="server" ID="btnRechazar" Text="Rechazar" CssClass="rightButton" OnClick="btnRechazar_Click" />
                                </td>
                            </tr>
                        </table>

                    <asp:HiddenField runat="server" ID="hdnSolicitudId" OnValueChanged="hdnSolicitudId_ValueChanged" />

                --%>

   <%-- <div>
        <fieldset>
            <label for="name">Elige un motivo para rechazar:</label>
            <asp:DropDownList runat="server" ID="cmbMotivos">
                <asp:ListItem Text="Elige un motivo para rechazar" Value="0"></asp:ListItem>
                <asp:ListItem Text="Falta información" Value="1"></asp:ListItem>
            </asp:DropDownList>
            <input type="text" name="name" id="name" class="text ui-widget-content ui-corner-all" />
            <asp:Button runat="server" ID="btnSubmit" Style="position: absolute; top: -1000px" />
        </fieldset>
    </div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
