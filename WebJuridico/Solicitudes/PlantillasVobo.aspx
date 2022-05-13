<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="PlantillasVobo.aspx.cs" Inherits="Solicitudes_PlantillasVobo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">

    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updMain" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="height: 20px"></div>
            <table id="tblPrincipal" runat="server" style="width: 100%; vertical-align: top">
                <tr>
                    <td style="width: 100%; align-content: center; vertical-align: top">
                        <table runat="server" id="tblGridSolicitudes" style="width: 800px; align-self: center; margin-left: auto; margin-right: auto">
                            <tr>
                                <td colspan="2">
                                    <asp:Label runat="server" CssClass="title" ID="lblTitulo">Configuracion VoBo Plantillas</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="align-content: center">
<%--                                    OnRowEditing="grvSolicitudes_RowEditing"
                                        OnSelectedIndexChanged="grvSolicitudes_SelectedIndexChanged"--%>
                                        <asp:GridView ID="grvSolicitudes" runat="server"
                                            Width="1000px"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            AutoGenerateColumns="False"
                                            CaptionAlign="Left" 
                                            CellPadding="4"
                                            ForeColor="#333333"
                                            GridLines="None"
                                            PageSize="13"
                                            Font-Size="12px"
                                            HeaderStyle-Font-Bold="true"
                                            ShowHeaderWhenEmpty="True"
                                            OnPageIndexChanging="grvSolicitudes_PageIndexChanging"
                                        
                                            OnSorting="grvSolicitudes_Sorting"
                                         >
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSolicitudId" Text='<%# Eval("Id_PlantillaJuridica") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     

                                                <asp:BoundField HeaderStyle-Width="100px" DataField="DescClas" HeaderStyle-HorizontalAlign="Center" SortExpression="DescClas" HeaderText="Clasificación" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderStyle-Width="300px" DataField="Nombre" HeaderStyle-HorizontalAlign="Center" SortExpression="Nombre" HeaderText="Tipo de plantilla" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderStyle-Width="115px" HeaderText="Requiere Vo.Bo.">
                                                    <ItemTemplate>
                                                       <asp:CheckBox runat="server" ID="chbVobo" OnCheckedChanged="chbVobo_CheckedChanged" Checked='<%#Eval("voBo") %>'   AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

<%--                                                <asp:CommandField ShowEditButton="true" EditText="Complementar" ItemStyle-HorizontalAlign="Center" />--%>

<%--            				                    <asp:HyperLinkField DataNavigateUrlFields="IdPlantilla" DataNavigateUrlFormatString="~/Solicitudes/Solicitudes.aspx?plantilla={0}" HeaderText="" Text="Complementar">
				                                </asp:HyperLinkField>--%>

                                            </Columns>
                                            <%--<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#DD4433" Font-Bold="True" ForeColor="Black" />--%>
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
