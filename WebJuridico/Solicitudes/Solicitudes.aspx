<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Solicitudes.aspx.cs" Inherits="Solicitudes_Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>
    <asp:Panel runat="server" ID="updMain" UpdateMode="Conditional">
            <div style="height: 20px"></div>
            <table style="width: 100%; min-height: 200px; vertical-align: top">
                <tr>
                    <td style="width: 100%; align-content: center; vertical-align: top">
                        <table runat="server" id="tblSelSolicitud" style="width: 500px; align-self: center; margin-left: auto; margin-right: auto">
                            <tr>
                                <td colspan="2">
                                    <asp:Label runat="server" CssClass="title" ID="lblTitulo"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="width: 200px;">
                                    <%--<asp:RadioButton runat="server" ID="rdbPoder" CssClass="infoLabel" GroupName="grpTipoPlantilla" Text="Poderes" OnCheckedChanged="rdbPoder_CheckedChanged" AutoPostBack="true" />--%>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlPoder" Width="300" Visible="false"></asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlContrato" Width="300" Visible="false"></asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlServiciosNot" Width="300" Visible="false"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="auto-style1"></td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<asp:RadioButton runat="server" ID="rdbContrato" CssClass="infoLabel" GroupName="grpTipoPlantilla" Text="Contratos" AutoPostBack="true" OnCheckedChanged="rdbContrato_CheckedChanged" />--%>
                                </td>
                                <td>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: right">
                                    <asp:Button runat="server" ID="btnElegir" CssClass="Button" Text="Elegir" OnClick="btnElegir_Click" />
                                </td>
                            </tr>
                        </table>

                        <table runat="server" id="tblDetalleSolicitud" style="width: 700px; align-self: center; margin-left: auto; margin-right: auto;" visible="false">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="pnlDatosPrincipales" Width="100%" GroupingText="Solicitud de Servicio">
                                        <table style="width: 100%; min-height: 200px; align-self: center; margin-left: auto; margin-right: auto;">
                                            <tr>
                                                <td colspan="2" style="height: 25px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    <asp:Label runat="server" CssClass="infoLabel" ID="lblNomPlantilla">Nombre de la plantilla:</asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label runat="server" ID="lblNombrePlantillaValue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100px; vertical-align: top">
                                                    <asp:Label runat="server" CssClass="infoLabel" ID="lblDesc">Descripción:</asp:Label>
                                                </td>
                                                <td style="height: 100px; vertical-align: top">
                                                    <asp:Label runat="server" ID="lblDescValue"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100px; vertical-align: top">
                                                    <asp:Label runat="server" CssClass="infoLabel" ID="lblEtiquetas">Etiquetas:</asp:Label>
                                                </td>
                                                <td style="vertical-align: top">

                                                    <asp:GridView runat="server"
                                                        ID="grvEtiquetas"
                                                        AutoGenerateColumns="False"
                                                        Width="484px"
                                                        Font-Size="12px"
                                                        GridLines="None"
                                                        ShowHeader="False" 
                                                        CellPadding="2" 
                                                        ForeColor="#000000"
                                                        OnRowDataBound="grvEtiquetas_RowDataBound">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>

                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblEtiquetaId" Text='<%# Eval("id_etiquetas") %>' />                                                                      
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblEtiqueta" Text='<%# Eval("Pregunta") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ControlStyle-Width="215px">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblEtiquetaNom" Text='<%# Eval("Pregunta").ToString().Split((char)60)[0] %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ControlStyle-Width="215px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" CssClass="textboxGeneral"   OnTextChanged="txtValorEtiqueta_TextChanged" AutoPostBack="true" ID="txtValorEtiqueta" Enabled='<%# Eval("Pregunta").ToString().Trim().StartsWith("Nota")? false : true %>' Font-Size="12px" Height="80px" Width="240px" TextMode="MultiLine" onchange="checkMaxLength(this,1500)" onkeydown="checkMaxLength(this,1500)" onkeyup="checkMaxLength(this,1500)"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="White" />
                                                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                                        
                                                    </asp:GridView>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAdjuntarTitl">Adjuntar documentos:</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%">
                                                <asp:Label runat="server" CssClass="infoLabel" ID="lblTipoDocumento">Tipo de documento:</asp:Label>
                                            </td>
                                            <td style="width: 75%">
                                                <asp:DropDownList runat="server" ID="ddlTipoDocumento" Width="75%"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" CssClass="infoLabel" ID="lblCargaDocumento">Carga de documento:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:FileUpload runat="server" ID="fluDocumento" Width="75%" />
                                                <asp:Button runat="server" ID="btnCargar" Text="Cargar" Width="20%" CssClass="rightButton" OnClick="btnCargar_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 15px"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="background-color: gray">
                                                <label>
                                                    Listado de documentos
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView runat="server" ID="grvDocumentos" 
                                                    AutoGenerateColumns="false" Width="100%" 
                                                    OnRowCommand="grvDocumentos_RowCommand" 
                                                    OnRowDataBound="grvDocumentos_RowDataBound" 
                                                    OnRowDeleting="grvDocumentos_RowDeleting">
                                                    <Columns>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDocumentoId" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <label>Nombre del documento</label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDocumentoNombre" Text='<%# Eval("TipoDocumento") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <label>Archivo</label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblArchivoPath" Visible="false" Text='<%# Eval("Nombre") %>'></asp:Label>
                                                                <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>" Text='<%# Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" Text="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle Font-Bold="false"  />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="min-height:5px"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: right">
                                                <asp:Button runat="server" CssClass="Button" ID="btnSolicitar" Text="Solicitar" OnClick="btnSolicitar_Click" />
                                                <asp:Button runat="server" CssClass="Button" ID="btnNueva" Text="Nueva solicitud" Visible="false" OnClick="btnNuevaSolicitud_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

    <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnCargar" />
        </Triggers>

    </asp:Panel>

</asp:Content>
