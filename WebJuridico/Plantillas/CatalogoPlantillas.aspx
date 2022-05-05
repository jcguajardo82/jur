<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CatalogoPlantillas.aspx.cs" Inherits="Plantillas_CatalogoPlantillas" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 26px;
        }

        .auto-style3 {
            height: 23px;
        }

        .auto-style4 {
            height: 23px;
            width: 415px;
        }

        .auto-style5 {
            width: 375px;
        }

        .auto-style6 {
            height: 26px;
            width: 415px;
        }

        .auto-style7 {
            height: 23px;
            width: 174px;
        }

        .auto-style9 {
            height: 26px;
            width: 174px;
        }

        .auto-style11 {
            width: 174px;
        }

        .auto-style13 {
            width: 415px;
        }

        </style>

    <script type="text/javascript">
        function confirmDeleteClasificacion() {
            return confirm("¿Desea elminar la clasificación seleccionada?");
        }

        function confirmDeleteTipo() {
            return confirm("¿Desea elminar el tipo de plantilla seleccionado?");
        }
    </script>



</asp:Content>

<asp:Content ID="Contenido" ContentPlaceHolderID="Contenido" runat="server">
    <div>
        <table width="80%" align="center">
            <tr>
                <td><asp:Label ID="lblTitulo" runat="server" Text="Administración de Catálogos de Plantillas"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Button Text="Clásificación Plantilla" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server" OnClick="Tab1_Click" />
                    <asp:Button Text="Tipo de Plantilla" BorderStyle="None" ID="Tab2" BackColor="Red" CssClass="Initial" runat="server" OnClick="Tab2_Click" />
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID ="VwClasPlan" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid;">
                                <tr>
                                    <td>
                                        <table class="auto-style1">
                                            <tr>
                                                <td><asp:Label ID="lblTipoPlan" runat="server" Text="Ingrese Nombre Clasificación de Plantilla"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtClasPlantilla" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSaveClasiPlantilla" runat="server" Text="agregar" OnClick="btnSaveClasiPlantilla_Click"/>
                                                    <asp:Button ID="btnCancelClasiPlantilla" runat="server" Text="Cancelar Edición" Visible="false" OnClick="btnCancelClasiPlantilla_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
                                        <p>Tipos de Clasificación Actuales</p>
                                        <asp:GridView ID="gvClasificacionesPlan" runat="server"
                                            Width="990"
                                            AutoGenerateColumns="false"
                                            CaptionAlign="Left"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            Font-Size="13px"
                                            EmptyDataText="No se encontraron registros"
                                            HeaderStyle-Font-Bold="true"
                                            OnRowCommand="gvClasificacionesPlan_RowCommand" >

                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblClasPlan" Text =' <%# Eval("id_tipoplantilla") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderStyle-Width="100px" DataField="Descripcion" SortExpression="Descripcion" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderStyle-Width="135px" SortExpression="Descripcion" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditClas" runat="server" CommandName="EditaClas" CommandArgument=' <%# Eval("id_tipoplantilla") %>' Text="Editar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="135px" SortExpression="Descripcion" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEliminaClas" runat="server" CommandName="EliminaClas" CommandArgument=' <%# Eval("id_tipoplantilla") %>' Text="Eliminar" OnClientClick="return confirmDeleteClasificacion()"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#D52B1E" ForeColor="White" HorizontalAlign="Center" />

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
                        </asp:View>
                        <asp:View ID ="VwTipoPlan" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid; min-height: 300px">
                                <tr>
                                    <td>
                                        <table class="auto-style1">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClasif" runat="server" Text="Ingrese el Nombre Tipo de Plantilla"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Clasificación de Plantilla: <asp:DropDownList ID="ddlClasPlantilla" runat="server" ToolTip="Seleccione una opción" OnSelectedIndexChanged="ddlClasPlantilla_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Nombre del Tipo de Plantilla: <asp:TextBox ID="txtTipoPlantilla" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                
                                                <td>
                                                    <asp:Button ID="btnSaveTipoPlantilla" runat="server" Text="Agregar" OnClick="btnSaveTipoPlantilla_Click"/>
                                                    <asp:Button ID="btnCancelTipoPlantilla" runat="server" Text="Cancelar Edición" Visible="false" OnClick="btnCancelTipoPlantilla_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
                                        <p>Tipos de Plantillas Actuales</p>
                                        <asp:GridView ID="gvTiposPlan" runat="server"
                                            Width="990"
                                            AutoGenerateColumns="false"
                                            CaptionAlign="Left"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            Font-Size="13px"
                                            EmptyDataText="No se encontraron registros"
                                            HeaderStyle-Font-Bold="true"
                                            OnRowCommand="gvTiposPlan_RowCommand" >

                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblClasPlan" Text =' <%# Eval("IDClasificacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderStyle-Width="100px" DataField="TipoPlantilla" SortExpression="TipoPlantilla" HeaderText="Clásificación" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderStyle-Width="100px" DataField="ClasificacionPlantilla" SortExpression="ClasificacionPlantilla" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderStyle-Width="135px" SortExpression="Descripcion" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditClas" runat="server" CommandName="EditaTipo" CommandArgument=' <%# Eval("IDClasificacion") %>' Text="Editar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="135px" SortExpression="Descripcion" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEliminaClas" runat="server" CommandName="EliminaTipo" CommandArgument=' <%# Eval("IDClasificacion") %>' Text="Eliminar" OnClientClick="return confirmDeleteTipo()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#D52B1E" ForeColor="White" HorizontalAlign="Center" />

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
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </div>

    <asp:HiddenField ID="hdfEdita" runat="server" Value="0" />

</asp:Content>