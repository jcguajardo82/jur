<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">

    <center>

<br />

<asp:Label ID="lblTitulo" runat="server" Text="LOGIN"></asp:Label>

<br />
<br />

    <table>
        <tr>
            <td style="width: 110px; text-align: right; background-color: #D52B1E;color:white ">Usuario:</td>
            <td>
                <asp:TextBox ID="TxtUsuarios" runat="server" Width="340px" CssClass="loginTextbox"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="TxtUsuarios" ErrorMessage="Necesita Teclear su Usuario" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 110px; text-align: right; background-color: #D52B1E;color:white ">Password:</td>
            <td>
                <asp:TextBox ID="Txtpassword" runat="server" Width="340px" CssClass="loginTextbox" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="Txtpassword" ErrorMessage="Necesita teclear su Password" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: right">
                <br />
                <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="Button" OnClick="btnLogin_Click" BackColor="#B6BF00" ForeColor="White" />
            </td>
        </tr>
    </table>

    <br />
    <asp:Label ID="errorMsg" runat="server"></asp:Label>

</center>

 </asp:Content>
