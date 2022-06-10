<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="User_Users" MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">

<div style="text-align: center; padding: 17px">
    <asp:Label ID="pageTitle" runat="server" Text="ADMINISTRACION DE USUARIOS" Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Large"></asp:Label>
</div>

<center>
    <table>
        <tr>
            <td style="text-align: right; background-color: #D52B1E; font-size: 13px;">
                <asp:Label ID="Label2" runat="server" Text="Tipo de usuario:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDLTusuario" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLTusuario_SelectedIndexChanged" Width="314px">
                    <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>

            </td>
            <td>

            </td>
        </tr>
        <tr>
            <td style="text-align: right; background-color: #D52B1E; font-size: 13px;">
                <asp:Label ID="Label1" runat="server" Text="Número de Empleado:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtNoEmpleado" runat="server" Width="310px"></asp:TextBox>
        <%--    <asp:RegularExpressionValidator ID="regex1" Display="None" runat="server" ControlToValidate="TxtNoEmpleado" ValidationExpression="[0-9]*\.?[0-9]*[1-9]" Key="NumbersOnly" ErrorMessage="Escribir solo números en Número Empleado" Font-Size="11px" ForeColor="Red" />--%>
            </td>
            <td>
                <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" Width="90px" OnClick="BtnBuscar_Click" />
            </td>
            <td>

            </td>
        </tr>
        <tr>
            <td style="text-align: right; background-color: #D52B1E; font-size: 13px;">
                <asp:Label ID="Label3" runat="server" Text="Nombre de Empleado:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtNEmpleado" runat="server" Width="310px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="BtnGuardar" runat="server" Text="Grabar" Width="90px" OnClick="BtnGuardar_Click" Enabled="False" />
            </td>
            <td>
                <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" Width="90px" OnClick="BtnLimpiar_Click" />
            </td>
        </tr>
<%--        <tr>
            <td style="text-align: right; background-color: #D52B1E; font-size: 13px;">
                <asp:Label ID="Label4" runat="server" Text="Email de Empleado:"></asp:Label>
            </td>
            <td>--%>
                <asp:TextBox ID="TxtEmailEmpleado" runat="server" Width="310px" Visible="false"></asp:TextBox>
         <%--   </td>
            <td>

            </td>
            <td>

            </td>
        </tr>--%>
    </table>


<div style="text-align: center">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
</div>

</center>

    <hr width="100%" align="center" color="B6BF00">
            
<center>

    <div>LISTA DE USUARIOS</div>

    <asp:GridView ID="grvUsuarios" runat="server"
        CellPadding="4" 
        ForeColor="#333333" 
        GridLines="None"
        Width="650px"
        AutoGenerateColumns="False" 
        ShowHeaderWhenEmpty="True"
        HeaderStyle-Font-Bold="true"
        CaptionAlign="Left" 
        PageSize="11"
        Font-Size="13px"
        OnPageIndexChanging="grvUsuarios_PageIndexChanging" 
        OnRowCommand="grvUsuarios_RowCommand"
        OnRowCancelingEdit="grvUsuarios_RowCancelingEdit" 
        EmptyDataText="No se encontraron registros"
        OnRowDeleting="grvUsuarios_RowDeleting" 
        AllowPaging="True" 
        AllowSorting="True" 
        OnSorting="grvUsuarios_Sorting">
        <AlternatingRowStyle BackColor="White" />
        <Columns>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblIdUsuario" Text='<%# Eval("Id") %>'>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblNombre" Text='<%# Eval("Nombre") %>'>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPerfilId" Text='<%# Eval("PerfilId") %>'>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblEmailEmpleado" Text='<%# Eval("Email") %>'>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="NEmpleado" SortExpression="NEmpleado" HeaderText="No EMPLEADO" ItemStyle-HorizontalAlign="Center" />
            <asp:ButtonField ButtonType="link" CommandName="Nombre" DataTextField="Nombre" HeaderText="NOMBRE DEL EMPLEADO" SortExpression="Nombre" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PerfilDesc" SortExpression="PerfilDesc" HeaderText="TIPO DE USUARIO" />
            <asp:BoundField Visible="false" DataField="PerfilId" SortExpression="PerfilId" />
            <asp:CommandField ShowDeleteButton="true" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#D52B1E" Font-Bold="True" ForeColor="Black" />
        <PagerStyle BackColor="#D52B1E" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>

    <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>

</center>

</asp:Content>

