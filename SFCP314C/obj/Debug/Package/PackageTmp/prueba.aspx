<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="SFCP314C.prueba" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
</head>
<body class="mybody">
    <form id="form1" runat="server">
    <div>
        <div class="centrar_gridview">
            <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" ForeColor="#333333"
                GridLines="None" OnRowDataBound="FindTheGridAndBound" 
                DataKeyNames="THWRKC" Width="700px"
                PageSize="11" ShowFooter="True" CellPadding="4" CellSpacing="2">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Detalle por Centro de Fabricación">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbl_header" Text="Centro de Fabricación: ">
                        <%#Eval("THWRKC")%>-<%#Eval("WDESC")%>
                            </asp:Label>
                            <asp:GridView runat="server" ID="GridViewNested" EnableViewState="false" Width="700"
                                GridLines="None" ShowFooter="true" OnRowDataBound="gv_GVN_RowDataBound" 
                                CssClass="EstiloDemo">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
