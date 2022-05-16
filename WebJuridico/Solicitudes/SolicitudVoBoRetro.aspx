<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SolicitudVoBoRetro.aspx.cs" Inherits="Solicitudes_SolicitudVoBoRetro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/MaxLength.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //Normal Configuration
            //$("[id*=txtDesc]").MaxLength({ MaxLength: 200 });

            //Specifying the Character Count control explicitly
            $("[id*=txtComentarios]").MaxLength(
                {
                    MaxLength: 200,
                    CharacterCountControl: $('#counter')
                });

            $("[id*=txtRiesgos]").MaxLength(
                {
                    MaxLength: 200,
                    CharacterCountControl: $('#counter2')
                });

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
        <asp:Label runat="server" CssClass="title" ID="lblTitulo">Retroalimentación del negocio</asp:Label>
    </div>

    <asp:Panel ID="PanelBotonesPrincipales" runat="server">
        <div style="padding: 15px; text-align: left; width: 93%">
            <asp:DropDownList runat="server" ID="ddlCorreos" CssClass="textboxGeneral" AutoPostBack="true" OnSelectedIndexChanged="ddlCorreos_SelectedIndexChanged"></asp:DropDownList>
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnAutorizar" Text="Autorizar" OnClick="btnAutorizar_Click" CommandArgument="1" />
            <asp:Button CssClass="Button" runat="server" Width="130" ID="btnRechazar" Text="Rechazar" OnClick="btnAutorizar_Click"  CommandArgument="0"/>
            <br />
        </div>
    </asp:Panel>
    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>
            <asp:Panel ID="PanelSol" runat="server" Style="padding: 15px; text-align: center; width: 93%;">

                <table style="width: 100%;" border="0">
                    <thead>
                        <tr>
                            <td colspan="5" style="text-align: center; background-color: #B0A8CF; font-weight: bold;">RETROALIMENTACION AL NEGOCIO
                            </td>
                        </tr>
                    </thead>
                    <tr>
                        <td colspan="5" style="text-align: left; font-weight: bold;">
                            <hr />
                         <p>  Estimado <b style="color:red;"> <asp:Label  ID="lblCorreo" runat="server"></asp:Label> </b> , como parte del proceso de contratos, se solicita de su apoyo para retroalimentar
                             desde su área de experiencia,el siguiente negocio.
                             </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="text-align: center; font-weight: bold;">
                            <hr />
                            Detalle y descripción del negocio a revisar
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center;">

                            <asp:TextBox Style="width: 100%" runat="server" ClientIDMode="Static" ID="txtDesc" ReadOnly="true" MaxLength="200" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="text-align: center; font-weight: bold;">
                            <hr />
                            Comentarios al negocio
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center;">
                          
                            <asp:TextBox Style="width: 100%" runat="server" ClientIDMode="Static" ID="txtComentarios" MaxLength="200" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center; font-weight: bold;">
                            <hr />
                            Riesgos detectados
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: center;">

                            <asp:TextBox Style="width: 100%" runat="server" ClientIDMode="Static" ID="txtRiesgos" MaxLength="200" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

