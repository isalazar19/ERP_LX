<%@ Page Title="UR Extra LX" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UR.aspx.cs" Inherits="MENU_ERPLX.UR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewMenu" runat="server" 
    AutoGenerateColumns="False" CellPadding="4" 
    ForeColor="#333333" GridLines="None" Width="100%" CssClass="tablaUR" 
        OnDataBound="GridViewMenu_DataBound" Font-Size="9pt">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="NUMERO">
        <HeaderStyle HorizontalAlign="Right" Width="100px" />
        <ItemStyle HorizontalAlign="Right" Width="100px" />
        </asp:BoundField>
        <asp:HyperLinkField DataNavigateUrlFields="URL" DataTextField="TITULO" 
            Target="_blank">
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
        </asp:HyperLinkField>
        <asp:BoundField DataField="PROGRAMA" HeaderText="PROGRAMA">
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" Font-Names="Consolas" />
        </asp:BoundField>
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
