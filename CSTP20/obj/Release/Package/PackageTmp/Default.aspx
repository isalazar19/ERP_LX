<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CSTP20._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="Salir">
        <asp:ImageButton runat="server" ID="img_salir" ImageUrl="~/imagenes/x.png" ToolTip="Cerrar"
            ImageAlign="Right" OnClick="img_salir_Click" />
        <asp:Label runat="server" ID="lbl_salir" Text="Cerrar"></asp:Label>&nbsp;
    </div>
    <%--<div style="float: left; width: 452px;">--%>
    <br />
    <div class="seleccion_pais">
        <asp:Label ID="Label1" runat="server" Text="País" Font-Size="Small"></asp:Label>
        <asp:DropDownList ID="ddl_pais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change"
            ToolTip="seleccione la region...">
            <asp:ListItem Value="               ">Seleccione</asp:ListItem>
            <%--<asp:ListItem Value="               ">             </asp:ListItem>--%>
            <asp:ListItem Enabled="false" Value="1">Colombia</asp:ListItem>
            <asp:ListItem Enabled="false" Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Panamá</asp:ListItem>
            <asp:ListItem Enabled="false" Value="4">Perú</asp:ListItem>
            <asp:ListItem Enabled="false" Value="5">Trinidad &amp; Tobago</asp:ListItem>
            <asp:ListItem Enabled="false" Value="6">Venezuela</asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lbl_cia" Text="Compañia" Visible="false"></asp:Label>
        <asp:DropDownList runat="server" ID="ddl_cia" AutoPostBack="true" OnSelectedIndexChanged="ddl_cia_Selection_Change"
            Width="240px" ToolTip="Seleccione la compañia..." Visible="false">
        </asp:DropDownList>
        <br />
    </div>
</asp:Content>
