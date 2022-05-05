<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ConsultarReasignacion.aspx.cs" Inherits="Procesos_ConsultarReasignacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        var abogado;

        $(document).ready(function () {
            $("#txtFechaInicio").datepicker();
            $("#txtFechaFin").datepicker();
            $('#txtFechaInicio').attr('readonly', true);
            $('#txtFechaFin').attr('readonly', true);
        });

        function onSelectAbogado(abogadoId) {
            abogado = abogadoId;
        }

        function doPostBack(eventTarget, eventArgument) {

            form1.__EVENTTARGET.value = eventTarget;
            if (eventArgument == -1) {
                form1.__EVENTARGUMENT.value = eventArgument;
            }
            else {
                form1.__EVENTARGUMENT.value = abogado;
            }

            form1.submit();
        }

        function mostrarAbogados() {

            $("#divAbogados").css('visibility', 'visible');

            $(function () {

                $("#divAbogados").dialog({
                    resizable: false,
                    height: 400,
                    width: 500,
                    modal: true,
                    close: function (e) {
                        doPostBack('btnGrabar', '-1');
                    }
                });
            });
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="Server">
    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>

    <div style="padding: 15px">
        <asp:Label runat="server" CssClass="title" ID="lblTitulo">Consultar y Reasignar Solicitudes</asp:Label>
    </div>

    <asp:Panel ID="PanelBotonesPrincipales" runat="server">
        <div style="padding: 15px; text-align:right; width: 93%">
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnExcel" Text="Excel" OnClick="btnExcel_Click" />
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnGraficaPlantilla" Text="Grafica Plantillas" OnClick="btnGraficaPlantilla_Click" />
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnGraficaStatus" Text="Grafica Estatus" OnClick="btnGraficaStatus_Click" />
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click" />
            <br />
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server" ID="updMain" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tblPrincipal" runat="server" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="width: 100%; align-content: center; vertical-align: top">
                        <table runat="server" id="tblSelSolicitud" style="width: 1200px; align-self: center; margin-left: auto; margin-right: auto">
                            <tr>
                                <td colspan="2">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right;">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 55%">
                                    <label>Seleccione el tipo de clasificación de plantilla</label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" Width="200" ID="cmbClasPlantilla"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Seleccione el nombre del Abogado, Administrador, o Asistente</label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" Width="200" ID="cmbNomAbogado"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Rango de fechas</label>
                                </td>
                                <td>

                                    <table>
                                        <tr>
                                            <td>Fecha inicial</td>
                                            <td>
                                                <asp:TextBox ID="txtFechaInicio" ClientIDMode="Static" runat="server" style="width: 100px"></asp:TextBox>
<%--                                                <input runat="server" type="text" id="txtFechaInicio" style="width: 100px" />--%>
                                            </td>
                                            <td>&nbsp;&nbsp;Fecha final</td>
                                            <td>
                                                <asp:TextBox ID="txtFechaFin" ClientIDMode="Static" runat="server" style="width: 100px"></asp:TextBox>
<%--                                                <input runat="server" type="text" id="txtFechaFin" style="width: 100px" />--%>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="align-content: center">
                                        <asp:GridView ID="grvSolicitudes" runat="server"
                                            Width="1280"
                                            PageSize="12"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            Font-Size="11px"
                                            AutoGenerateColumns="False"
                                            CaptionAlign="Left" CellPadding="4"
                                            ForeColor="#333333"
                                            GridLines="None"
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
                                                        <asp:Label runat="server" ID="lblFolio" Text='<%# Eval("Folio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Fecha" SortExpression="Fecha" HeaderText="Fecha Solicitud" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderStyle-Width="125px" SortExpression="Folio" HeaderText="Folio" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnFolio" runat="server" CommandName="VerSolicitud" CommandArgument="<%# Container.DataItemIndex %>" Text='<%# Eval("Folio") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Tipo" SortExpression="Tipo" HeaderText="Tipo de plantilla" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Solicitante" SortExpression="Solicitante" HeaderText="Solicitador" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Clasificacion" SortExpression="Clasificacion" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="FechaAutorizacion" SortExpression="FechaAutorizacion" HeaderText="Fecha Autorizada" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="NombreAutorizador" SortExpression="NombreAutorizador" HeaderText="Autorizador" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="FechaAsignacion" SortExpression="FechaAsignacion" HeaderText="Fecha Asignación" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="NombreAbogado" SortExpression="NombreAbogado" HeaderText="Abogado" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderStyle-Width="55px" HeaderText="Operación" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnBitacora" runat="server" CommandName="Bitacora" CommandArgument="<%# Container.DataItemIndex %>" Text="Bitácora" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="55px" HeaderText="Operación" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnReasignar" runat="server" CommandName="Reasignar" CommandArgument="<%# Container.DataItemIndex %>" Text="Reasignar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

<%--                                                <asp:CommandField ShowEditButton="true" EditText="Reasignar" ItemStyle-HorizontalAlign="Center" />--%>

                                            </Columns>
                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Black" />
                                            <PagerSettings PageButtonCount="5" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Navy" />
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

<center>

    <asp:Panel runat="server" ID="PanelBitacora" Width="1000px" Visible="false" CssStyle="center">

        <div style="text-align: right">
            <br />
                <asp:Button CssClass="Button" runat="server" Width="130" ID="btnRegresar" Text="Regresar al Grid" OnClick="btnRegresar_Click" />
            <br />
            <br />
        </div>

            <div style="border: 1px solid #000000; background-color: #c0c0c0; font-weight: bold; margin: 3px; padding: 3px; text-align: left;">Historico General</div>

    <%--    AllowPaging="True"
            AllowSorting="True" --%>

            <asp:GridView ID="grvBitacora" runat="server"
            Width="100%" 
            Font-Size="11px"
            AutoGenerateColumns="False"
            CaptionAlign="Left" CellPadding="4"
            ForeColor="#333333"
            GridLines="None"
            HeaderStyle-Font-Bold="true"
            ShowHeaderWhenEmpty="True">

            <AlternatingRowStyle BackColor="White" />
            <Columns>

                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="desc_estatus" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                <asp:BoundField DataField="comentarios" HeaderText="Comentario" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="desc_usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Center" />

            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>

    </asp:Panel>

</center>

            <asp:HiddenField runat="server" ID="hdnSolicitud" Value="0" />
            <asp:HiddenField runat="server" ID="hdnSelSol" Value="" />
            <asp:HiddenField runat="server" ID="hdnFolio" Value="" />


<%--            <asp:UpdatePanel runat="server" ID="PanelAbogados" UpdateMode="Conditional">
                <ContentTemplate>--%>
                <div style="visibility: hidden; width: 500px; height: 400px" id="divAbogados">
                    <table runat="server" style="width: 100%; height: 100%">
                        <tr>
                            <td style="height: 25px">
                                <label class="title">
                                    Lista de Abogados
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%; height: 100%; margin: 0px; border: 1px; border-color: black">
                                    <tr>
                                        <th style="width: 100%; height: 25px; background-color: #cccccc">
                                            <label class="title">Número y nombre del abogado</label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>

                                            <table id="tblAbogados" runat="server" style="width: 100%;">
                                            </table>
                                        
                                            <div style="text-align: center">
                                                <asp:Button runat="server" ID="btnGrabar" Text="Grabar" CssClass="Button" OnClick="btnGrabar_Click" OnClientClick="doPostBack('btnGrabar', '');" CausesValidation="false" />
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
<%--                </ContentTemplate>
            </asp:UpdatePanel>--%>

        <asp:Label ID="errorMsg" runat="server"></asp:Label>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnGrabar" />
        </Triggers>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

