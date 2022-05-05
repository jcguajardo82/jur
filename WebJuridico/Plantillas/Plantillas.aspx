<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Plantillas.aspx.cs" Inherits="Plantillas_Plantillas" %>

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
    
    

</asp:Content>

<asp:Content ID="Contenido" ContentPlaceHolderID="Contenido" runat="server">
    <div>
        <table width="80%" align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblTitulo" runat="server" Text="Administración De Plantillas"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Button Text="Plantillas" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                        OnClick="Tab1_Click" />
                    <asp:Button Text="Autorizadores" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                        OnClick="Tab2_Click" />
                    <asp:Button Text="Etiquetas" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                        OnClick="Tab3_Click" />
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid; min-height: 300px">
                                <tr>
                                    <td>
                                        <table class="auto-style1">
                                            <tr>
                                                <td class="auto-style3"></td>
                                                <td class="auto-style3"></td>
                                                <td class="auto-style7"></td>
                                                <td class="auto-style4"></td>
                                                <td class="auto-style3"></td>
                                                <td class="auto-style3"></td>
                                                <td class="auto-style3"></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">&nbsp;</td>
                                                <td class="auto-style13">&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">
                                                    <asp:Label ID="Label1" runat="server" Text="Clasificación de Plantilla:"></asp:Label>
                                                </td>
                                                <td class="auto-style13">
                                                    <asp:DropDownList Style="width: 100%" ID="DDLClaPlantillas" runat="server" OnSelectedIndexChanged="DDLClaPlantillas_SelectedIndexChanged" AutoPostBack="true">
                                                        <%-- OnSelectedIndexChanged="DDLClaPlantillas_SelectedIndexChanged"--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">
                                                    <asp:Label ID="Label2" runat="server" Text="Nombre de Plantilla:"></asp:Label>
                                                </td>
                                                <td class="auto-style13">
                                                    <asp:TextBox Style="width: 100%" ID="TxtNomPlantilla" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">
                                                    <asp:Label ID="Label10" runat="server" Text=" "></asp:Label>
                                                    <asp:Label ID="Label11" runat="server" Text="Descripción:"></asp:Label>
                                                </td>
                                                <td class="auto-style13">
                                                    <asp:TextBox ID="TxtDescripcion" runat="server" Height="75px" TextMode="MultiLine" Style="width: 100%" onchange="checkMaxLength(this,600)" onkeydown="checkMaxLength(this,600)" onkeyup="checkMaxLength(this,600)"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2"></td>
                                                <td class="auto-style2"></td>
                                                <td class="auto-style9">
                                                    <asp:Label ID="lblTipoPlantilla" runat="server" Text="Tipo de Plantilla:"></asp:Label>
                                                </td>
                                                <td class="auto-style6">
                                                    <asp:DropDownList ID="DDLTipoPlantilla" runat="server" OnSelectedIndexChanged="DDLTipoPlantilla_SelectedIndexChanged" Width="150px">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="auto-style2"></td>
                                                <td class="auto-style2"></td>
                                                <td class="auto-style2"></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">
                                                    <asp:Label ID="Label13" runat="server" Text="Carga Plantilla:"></asp:Label>
                                                </td>
                                                <td class="auto-style13">

                                                    <asp:FileUpload ID="Fileload" runat="server" Width="280px" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnUp" runat="server" Text="Guardar" ValidationGroup="up" OnClick="btnUp_Click" />
                                                    <asp:Label ID="lblCargadePlantilla" runat="server" Visible="false"></asp:Label>

                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_eliminar" runat="server" OnClick="btn_eliminar_Click" Text="Eliminar" />

                                                    <br />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">
                                                    <asp:Label ID="lblvigencia" runat="server" Text="Vigencia:"></asp:Label>
                                                </td>
                                                <td class="auto-style13">
                                                    <asp:DropDownList ID="ddlvigencia" runat="server" Style="width: 100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">&nbsp;</td>
                                                <td class="auto-style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="auto-style11">&nbsp;</td>
                                                <td class="auto-style13">&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid; min-height: 300px">
                                <tr>
                                    <td>

                                        <asp:Panel ID="Panel2" runat="server" GroupingText="Lista de usuarios Autorizadores">
                                            <br />
                                            &nbsp;&nbsp;<table class="auto-style1" width="682px">
                                                <tr>
                                                    <td class="auto-style26" align="center">
                                                        <asp:Label ID="Label4" runat="server" Text="Autorizadores Disponibles"></asp:Label></td>
                                                    <td class="auto-style24" align="center">
                                                        <asp:Label ID="Label5" runat="server" Text="Autorizadores Seleccionados"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style26" align="center">
                                                        <asp:ListBox ID="lsbAutDisp" runat="server" Height="185px" Width="370px">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:ListBox>

                                                    </td>
                                                    <td class="auto-style5" align="center">
                                                        <asp:ListBox ID="lsbAutorizadores" runat="server" Height="185px" Width="370px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="BtnAgregar" CssClass="Button" runat="server" OnClick="BtnAgregar_Click" Text="Agregar" /></td>
                                                    <td class="auto-style5" align="center">
                                                        <asp:Button ID="BtnEliminar" CssClass="Button" runat="server" OnClick="BtnEliminar_Click" Text="Eliminar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style26" align="right">&nbsp;</td>
                                                </tr>
                                            </table>
                                            <br />
                                        </asp:Panel>

                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid; min-height: 300px">
                                <tr>
                                    <td>
                                        <div align="center">
                                            <asp:GridView ID="grvEtiquetas"
                                                runat="server" Width="550px"
                                                AutoGenerateColumns="False"
                                                AlternatingRowStyle-BackColor="#FFCC66"
                                                HeaderStyle-BackColor="#DD4433"
                                                ShowFooter="True"
                                                OnRowCommand="grvEtiquetas_RowCommand"
                                                OnRowDeleting="grvEtiquetas_RowDeleting"
                                                CellPadding="4" ForeColor="#333333"
                                                GridLines="None" PageSize="5">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Identificador de Campo">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblEtiqueta" Text='<%# Eval("Pregunta") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtIdentificador" runat="server" Width="100%" MaxLength="5000" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cambia solo Juridico" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="chkSoloJuridico" Checked='<%# Eval("Juridica") %>' Enabled="false" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:CheckBox ID="CkbJuridico" runat="server" Text="Solo Juridico" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" Text="Eliminar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAdd" runat="server" CommandName="Footer" OnClick="Add" Text="Agregar" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:CommandField ShowDeleteButton="true" />--%>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#D52B1E" ForeColor="#333333" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Navy" />
                                                <SortedAscendingCellStyle BackColor="#D52B1E" />
                                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                <SortedDescendingCellStyle BackColor="#D52B1E" />
                                                <SortedDescendingHeaderStyle BackColor="#820000" />
                                            </asp:GridView>
                                            <table id="tblEtiquetas" runat="server" visible="false" width="550px">
                                                <tr>
                                                    <th scope="col">Identificador de campo
                                                    </th>
                                                    <th scope="col"><asp:Label ID="lblChkSoloJuridico" runat="server" Text="Solo Juridico"></asp:Label>
                                                    </th>

                                                    <th scope="col"></th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtIdentificador" runat="server" MaxLength="5000" Style="width: 100%" />
                                                    </td>
                                                    <td style="align-content: center">
                                                        <asp:CheckBox ID="chkJuridico" Text="Solo Juridico" runat="server" />
                                                    </td>

                                                    <td style="align-content: center">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Agregar" OnClick="AgregarEtiqueta" CommandName="EmptyDataTemplate" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <p>
                                                &nbsp;
                                            </p>
                                        </div>
                                    </td>
                                </tr>

                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
                <tr>
                    <td>&nbsp;
                        <asp:Button ID="BtnGuardarPlantilla" CssClass="Button" Width="150" runat="server" Text="Guardar Plantilla" OnClick="BtnGuardarPlantilla_Click" ValidationGroup="1" />
                        <%-- OnSelectedIndexChanged="DDLClaPlantillas_SelectedIndexChanged"--%>
<%--                        <asp:Button ID="BtnNueva" CssClass="Button" Width="150" runat="server" Text="Nueva Plantilla" OnClick="BtnNueva_Click" />--%>
                        <asp:Button ID="BtnUpdate" CssClass="Button" Width="150" runat="server" OnClick="BtnUpdate_Click" Text="Actualizar Plantilla" />
                        <asp:Button ID="BtnLimpiar" CssClass="Button" Width="150" runat="server" Text="Limpiar" OnClick="BtnLimpiar_Click" />
                    </td>
                </tr>
            </tr>
        </table>
    </div>

    <hr width="100%" align="center" color="B6BF00" />

    <table width="100%">
        <tr>
            <td>
                <div align="center">

                    <div>LISTA DE PLANTILLAS</div>

                    <asp:GridView ID="GDVPlantillas" runat="server" 
                        Width="990"
                        AllowPaging="True" 
                        AllowSorting="True" 
                        AutoGenerateColumns="False" 
                        CaptionAlign="Left" 
                        CellPadding="4" 
                        ForeColor="#333333" 
                        GridLines="None" 
                        Font-Size="13px"
                        EmptyDataText="No se encontraron registros"
                        HeaderStyle-Font-Bold="true" 
                        OnPageIndexChanging="GDVPlantillas_PageIndexChanging" 
                        OnRowCancelingEdit="GDVPlantillas_RowCancelingEdit" 
                        OnRowDeleting="GDVPlantillas_RowDeleting" 
                        OnRowDataBound="GDVPlantillas_RowDataBound" 
                        OnRowCommand="GDVPlantillas_RowCommand" 
                        OnSelectedIndexChanged="GDVPlantillas_SelectedIndexChanged"
                        OnSorting="GDVPlantillas_Sorting" 
                        ShowHeaderWhenEmpty="True" 
                        PageSize="5">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblIdPlantilla" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Nombre" SortExpression="Nombre" HeaderText="Nombre de Plantilla" />
                            <asp:BoundField DataField="Descripcion" SortExpression="Descripcion" HeaderText="Tipo de plantilla" />
                            <asp:BoundField DataField="NombreArchivo" SortExpression="Archivo" HeaderText="Plantilla" />
                            <asp:BoundField DataField="VersionDoc" SortExpression="Archivo" HeaderText="Versión" ItemStyle-HorizontalAlign="Center" />

                            <asp:TemplateField HeaderStyle-Width="85px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDescargar" runat="server" CommandName="Descargar" ForeColor="#333333" CommandArgument='<%# Eval("Id") %>' Text="Descargar" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField ShowSelectButton="True" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Black" />
                        <PagerSettings PageButtonCount="5" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
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

        <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
