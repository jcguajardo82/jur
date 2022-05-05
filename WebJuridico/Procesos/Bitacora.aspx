<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bitacora.aspx.cs" Inherits="Procesos_Bitacora" %>

<asp:Content id="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">

         $(function () {
             $("#txtFecha").datepicker();
             $('#txtFecha').attr('readonly', true);
         });

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>
    <asp:Panel runat="server" ID="updMain" UpdateMode="Conditional">

    <div style="padding: 12px;">  
        <asp:Label runat="server" CssClass="title" ID="lblTitulo">Bitácora de Solicitud</asp:Label>
    </div>

<%--    OnRowDataBound="grvSolicitudes_RowDataBound"
    OnSelectedIndexChanged="grvSolicitudes_SelectedIndexChanged"--%>

    <asp:GridView ID="grvSolicitudes" runat="server"
    Width="100%" 
    PageSize="17"
    AllowPaging="True"
    AllowSorting="True"
    Font-Size="11px"
    AutoGenerateColumns="False"
    CaptionAlign="Left" 
    CellPadding="4"
    ForeColor="#333333"
    GridLines="None"
    HeaderStyle-Font-Bold="true"
    ShowHeaderWhenEmpty="True"
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

        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblTipo" Text='<%# Eval("Tipo") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblFolioProceso" Text='<%# Eval("FolioProceso") %>'></asp:Label>
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
    <%--<asp:BoundField DataField="NombreAbogado" SortExpression="NombreAbogado" HeaderText="Abogado" ItemStyle-HorizontalAlign="Center" />--%>
        <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Estatus" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="FolioProceso" SortExpression="FolioProceso" HeaderText="Folio Proceso" ItemStyle-HorizontalAlign="Center" />

        <asp:TemplateField HeaderStyle-Width="55px" HeaderText="Operación" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:LinkButton ID="btnVer" runat="server" CommandName="Ver" CommandArgument="<%# Container.DataItemIndex %>" Text="Ver" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-Width="55px" HeaderText="Operación" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:LinkButton ID="btnBitacora" runat="server" CommandName="Bitacora" CommandArgument="<%# Container.DataItemIndex %>" Text="Bitácora" />
            </ItemTemplate>
        </asp:TemplateField>


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

    </asp:Panel>

    <center>

    <asp:Panel runat="server" ID="PanelBitacora" Width="800px" Visible="false" CssStyle="center">
        <div style="padding: 12px;">
            <asp:Label runat="server" CssClass="title" ID="lblTitulo2">Bitácora de Solicitud</asp:Label>
        </div>

            <div style="text-align: right">

              <asp:Button CssClass="Button" runat="server" Width="130" ID="btnRegresar" Text="Regresar" OnClick="btnRegresar_Click" />
              <asp:Button CssClass="Button" runat="server" Width="130" ID="btnLimpiar" Text="Limpiar" OnClick="btnLimpiar_Click" />
              <asp:Button CssClass="Button" runat="server" Width="130" ID="btnGuardar" Text="Grabar" OnClick="btnGuardar_Click" />

            </div>

            <br />
            <br />

            <table style="width: 825px">
                <tr>
                    <td>
                        <asp:Label runat="server" CssClass="infoLabel" ID="lblFolioTitle" Text="Folio Solicitud" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFolio" runat="server" Width="230" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" CssClass="infoLabel" ID="Label2" Text="Folio Proceso" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFolioProceso" runat="server" Width="230" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" CssClass="infoLabel" ID="Label3" Text="Fecha" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFecha" runat="server" Width="230" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" CssClass="infoLabel" ID="Label4" Text="Tipo De Plantilla" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtTipoPlantilla" runat="server" Width="230" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:Label runat="server" CssClass="infoLabel" ID="Label5" Text="Tipo de Estatus" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" Width="234" ID="ddlEstatus"></asp:DropDownList>
                    </td>
                </tr>
            </table>

                <br />
                <br />

            <table style="width: 825px">
                <tr>
                    <td>
                        <asp:Label runat="server" CssClass="infoLabel" ID="Label6" Text="Comentario" />
                    </td>
                    <td>
     
                        <asp:TextBox ID="TxtComentario" runat="server" Height="75px" TextMode="MultiLine" Style="width: 700px" onchange="checkMaxLength(this,400)" onkeydown="checkMaxLength(this,400)" onkeyup="checkMaxLength(this,400)"></asp:TextBox>

                    </td>
                </tr>

            </table>

    <asp:Panel runat="server" ID="PanelBotonEtiquetasOtorgantes" Width="800px" Visible="false">

        <div style=" padding: 11px;">
            Oprimir Botón para agregar los otorgantes y testigos
            <asp:Button CssClass="Button" runat="server" Width="330" ID="btnEtiquetasOtorgantes" Text="Etiquetas Otorgantes y Testigos" OnClick="btnEtiquetasOtorgantes_Click" />
        </div>

    </asp:Panel>

    <asp:Panel runat="server" ID="PanelOtorganteEtiqueta" Width="800px" Visible="false">

        <div style="padding: 12px; border: 1px solid #c0c0c0;">

            <div style="text-align: right">
                <div style="float: left; vertical-align: bottom;">
                    Etiquetas de versión
                </div>

    <%--              <asp:Button CssClass="Button" runat="server" Width="130" ID="btnLimpiarEtiquetas" Text="Limpiar" />--%>
                <asp:Button CssClass="Button" runat="server" Width="130" ID="btnGuardarEtiquetas" Text="Grabar" OnClick="btnGuardarEtiquetas_Click" />

            </div>

            <br />

            <asp:GridView ID="grvEtiquetasJuridicas" runat="server"
            Width="100%" 
            AllowSorting="True"
            Font-Size="14px"
            AutoGenerateColumns="False"
            CaptionAlign="Left" 
            CellPadding="4"
            ForeColor="#333333"
            GridLines="None"
            HeaderStyle-Font-Bold="true"
            OnRowCommand="grvEtiquetasJuridicas_RowCommand"
            OnRowDataBound="grvEtiquetasJuridicas_RowDataBound"
            ShowHeaderWhenEmpty="True">

            <AlternatingRowStyle BackColor="White" />
            <Columns>

                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblIdSolicitudEtiqueta" Text='<%# Eval("IdSolicitudEtiqueta") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_id_usuarioLigado" Text='<%# Eval("id_usuarioLigado") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Pregunta" HeaderText="Nombre del tipo de identificación de campo" ItemStyle-HorizontalAlign="Center" />

                <asp:TemplateField HeaderText="Nombre del Otorgante ó Testigo">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlOtorganteTestigo" runat="server" Width="280" Enabled="false"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sel.">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="SeleccionarCampo" CommandArgument="<%# Container.DataItemIndex %>" Text="Activar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <%--<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />--%>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#D52B1E" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#edece6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>

        </div>

    </asp:Panel>

        <br />
        <br />

            <div style="border: 1px solid #000000; background-color: #c0c0c0; font-weight: bold; margin: 3px; padding: 3px; text-align: left;">Historico General</div>

    <%--    AllowPaging="True"
            AllowSorting="True" --%>

            <asp:GridView ID="grvBitacora" runat="server"
            Width="100%" 
            AllowSorting="True"
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
            <%--<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />--%>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#edece6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>

    </asp:Panel>


        <asp:HiddenField runat="server" ID="hdnSolicitud" Value="0" />
        <asp:HiddenField runat="server" ID="hdnPlantillaId" Value="0" />
        <asp:Label ID="errorMsg" runat="server"></asp:Label>

    </center>

</asp:Content>