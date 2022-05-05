<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ConsultarSolicitud.aspx.cs" Inherits="Solicitudes_ConsultarSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">

    <asp:ScriptManager runat="server" ID="scrMan"></asp:ScriptManager>

    <asp:Panel runat="server" ID="updMain" UpdateMode="Conditional">
<%--        <ContentTemplate>--%>
                       
            <div style="text-align: center; padding: 5px;">     
                <asp:Label runat="server" ID="lblPageTitle" CssClass="title"></asp:Label>
            </div>

            <table runat="server" id="tblDetalleSolicitud" style="width: 700px; align-self: center; margin-left: auto; margin-right: auto;">
                <tr>
                    <td>

                        <asp:Panel runat="server" ID="PanelDatosPrincipales" Width="100%" GroupingText="Datos">

                            <table style="width: 100%; min-height: 200px; align-self: center; margin-left: auto; margin-right: auto;">
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label runat="server" CssClass="infoLabel" ID="lblTipoTitle">Clasificación:</asp:Label>
                                    </td>
                                    <td style="width: 75%">
                                        <asp:Label runat="server" ID="lblTipo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label runat="server" CssClass="infoLabel" ID="lblNombrePlantillaTitle">Tipo de plantilla:</asp:Label>
                                    </td>
                                    <td style="width: 75%">
                                        <asp:Label runat="server" ID="lblNombrePlantilla"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 70px; vertical-align: top">
                                        <asp:Label runat="server" CssClass="infoLabel" ID="lblDescTitle">Descripción:</asp:Label>
                                    </td>
                                    <td style="height: 70px; vertical-align: top">
                                        <asp:Label runat="server" ID="lblDesc"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">

                                    <asp:Panel runat="server" ID="PanelMasInfo" Width="100%">

                                        <table style="width: 100%;; align-self: center; margin-left: auto; margin-right: auto;">
                                            <tr>
                                                <td style="width: 25%">
                                                    <asp:Label runat="server" CssClass="infoLabel" ID="lblSolicitanteTitle">Solicitante:</asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label runat="server" ID="lblSolicitante"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    <asp:Label runat="server" CssClass="infoLabel" ID="lblFechaSolicitudTitle">Fecha Solicitud:</asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label runat="server" ID="lblFechaSolicitud"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%">
                                                    <asp:Label runat="server" CssClass="infoLabel" ID="lblEstatusTitle">Estatus:</asp:Label>
                                                </td>
                                                <td style="width: 75%">
                                                    <asp:Label runat="server" ID="lblEstatus"></asp:Label>
                                                    <asp:Label runat="server" ID="lblEstatusId" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            </table>

                                        <br />

                                    </asp:Panel>


                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100px; vertical-align: top">
                                        <asp:Label runat="server" CssClass="infoLabel" ID="lblEtiquetasTitle">Etiquetas:</asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:GridView runat="server" ID="grvEtiquetas" 
                                            AutoGenerateColumns="False" Width="99%"
                                            ShowHeader="False" 
                                            CellPadding="2" 
                                            Font-Size="12px"
                                            ForeColor="#000000" 
                                            GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEtiquetaId" Text='<%# Eval("IdEtiqueta") %>' />             
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEtiqueta" Text='<%# Eval("Etiqueta") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ControlStyle-Width="215px">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEtiquetaNom" Text='<%# Eval("Etiqueta").ToString().Split((char)60)[0] %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ControlStyle-Width="215px">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" CssClass="textboxGeneral" ID="txtValorEtiqueta" Height="80px" Width="100%" Font-Size="12px" Text='<%# Eval("Valor") %>' TextMode="MultiLine" onchange="checkMaxLength(this,1500)" onkeydown="checkMaxLength(this,1500)" onkeyup="checkMaxLength(this,1500)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblValorEtiqueta" Text='<%# Eval("Valor") %>'></asp:Label>
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

                        <asp:Panel ID="PanelMotivoRechazo" runat="server" GroupingText="Rechazar" Visible="false">
                            <br />
                            Para rechazar la solicitud elige un motivo y complementalo con el campo de observaciones:
                            <br />
                            <br />
                            <asp:DropDownList runat="server" ID="ddlMotivo">
                                <asp:ListItem Text="Elige un motivo para rechazar" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Falta información" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:TextBox ID="TxtMotivo" runat="server" Height="68px" TextMode="MultiLine" Style="width: 380px" onchange="checkMaxLength(this,200)" onkeydown="checkMaxLength(this,200)" onkeyup="checkMaxLength(this,200)"></asp:TextBox>
                            <br />
<%--                        <asp:Button runat="server" ID="btnRechazar2" Text="Rechazar" CssClass="Button" OnClick="btnRechazar2_Click" />--%>
                        </asp:Panel>

<%--                        <asp:Panel ID="PanelAsignarAbogado" runat="server" GroupingText="Asignar" Visible="false">
                            <br />
                            Lista de Abogados
                            <br />
                            <br />
                            <asp:RadioButtonList ID="RblAbogado" runat="server"></asp:RadioButtonList>
                            <br />
                            <asp:Button ID="BtnAsignarAbogado2" runat="server" Text="Asignar" />
                        </asp:Panel>--%>

                        <br />

                    </td>
                </tr>
            </table>

            <table runat="server" id="tblAdjuntarDocumentos" Visible="false" style="width: 700px; align-self: center; margin-left: auto; margin-right: auto;">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblAdjuntarTitle">Adjuntar documentos:</asp:Label>
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
                        </table>

                    </td>
                </tr>
            </table>

            <table style="width: 700px; align-self: center; margin-left: auto; margin-right: auto;">
                <tr>
                    <td colspan="2" style="background-color: gray">
                        <label>
                            Listado de documentos
                        </label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <%-- OnSelectedIndexChanged="grvDocumentos_SelectedIndexChanged" --%>
                        <asp:GridView runat="server" ID="grvDocumentos" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grvDocumentos_RowDataBound" OnRowCommand="grvDocumentos_RowCommand" OnRowDeleting="grvDocumentos_RowDeleting" OnSelectedIndexChanged="grvDocumentos_SelectedIndexChanged">
                            <Columns>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEsNuevo" Text='<%# Eval("EsNuevo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

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
                                    <HeaderTemplate>
                                        <label></label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" Visible="false" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" Text="Eliminar" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle Font-Bold="false"  />
                        </asp:GridView>

                        <br />

                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                    <div style="text-align: center;">

                        <asp:Panel ID="PanelModificarComplementar" runat="server">
                            <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" CssClass="Button" OnClick="btnEliminar_Click" />
                            <asp:Button runat="server" ID="btnModificar" Text="Modificar" CssClass="Button" OnClick="btnModificar_Click" />
                            <asp:Button runat="server" ID="btnGuardar" Text="Guardar Cambios" CssClass="Button" OnClick="btnGuardar_Click" />
                        </asp:Panel>

                        <asp:Panel ID="PanelBotonesAutorizar" runat="server" Visible="false">
                            <asp:Button runat="server" ID="btnRechazar" Text="Rechazar" CssClass="Button" OnClick="btnRechazar_Click" />
                            <asp:Button runat="server" ID="btnAutorizar" Text="Visto bueno" CssClass="Button" OnClick="btnAutorizar_Click" />
                        </asp:Panel>

<%--                        <asp:Panel ID="PanelBotonesAbogado" runat="server" Visible="false">
                            <asp:Button runat="server" ID="BtnAbogadoRechazar" Text="Rechazar" CssClass="Button" OnClick="BtnAbogadoRechazar_Click" />
                            <asp:Button runat="server" ID="BtnAbogadoAsignar" Text="Asignar" CssClass="Button" OnClick="BtnAbogadoAsignar_Click" />
                        </asp:Panel>--%>

                    </div>

                    </td>
                </tr>
            </table>

            </div>

<%--        </ContentTemplate>--%>

        <asp:HiddenField ID="hdnPlantillaId" runat="server" />
        <asp:HiddenField ID="hdnFolio" runat="server" />

    <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnCargar" />
        </Triggers>

    </asp:Panel>
</asp:Content>

