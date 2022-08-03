<%@ Page Title="Menú ERP Extra LX" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MENU_ERPLX._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:GridView ID="GridViewGruposDeMenu" runat="server" 
    AutoGenerateColumns="False" CellPadding="4" 
    ForeColor="#333333" GridLines="None" Width="100%" CssClass="tablaUR" 
        Font-Size="9pt">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="MENU" HeaderText="MENÚ">
        <HeaderStyle HorizontalAlign="Left" Width="100px" />
        <ItemStyle HorizontalAlign="Left" Width="100px" Font-Names="Consolas" />
        </asp:BoundField>
        <asp:HyperLinkField DataNavigateUrlFields="URL" 
            DataNavigateUrlFormatString="UR.aspx?id={0}" DataTextField="TITULO" 
            HeaderText="TÍTULO">
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
        </asp:HyperLinkField>
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
            CssClass="titulo" Font-Size="10pt" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F7F7" ForeColor="#333333" CssClass="programa" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#E9E7E2" />
    <SortedAscendingHeaderStyle BackColor="#506C8C" />
    <SortedDescendingCellStyle BackColor="#FFFDF8" />
    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
</asp:Content>
