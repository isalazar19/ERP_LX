<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdoCtaClte.aspx.cs" Inherits="ACRP597.EdoCtaClte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=999,height=700,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPrint">
        <div class="page">
            <table>
                <tr>
                    <td>
                        <div class="encabezado_impresion">
                            <asp:Label runat="server" ID="lbl_1" Text="TELEFONOS:"></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="lbl_2" Text="(507) 236-16-11"></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="Label1" Text="(507) 236-18-11"></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="Label2" Text="FAX: (507) 236-14-79"></asp:Label>
                        </div>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <div class="titulo_empresa">
                            <asp:Label runat="server" ID="lbl_emp" Text="Papelera Itsmeña, S.A."></asp:Label>
                        </div>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <div class="titulo_adicional">
                            <asp:Label runat="server" ID="lbl_titulo" Text="ESTADO DE CUENTA" Style="font-weight: 700"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="encabezado_impresion">
                            <asp:ImageButton ID="img_papisa" runat="server" ImageUrl="http://recursos.ti.regional.app.papeleslatinos.com/multimedia/logos/fondo-blanco/full-color/simbolo-alt70px/papisa.gif"
                                Height="87px" Width="72px" />
                        </div>
                    </td>
        </div>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td valign="top">
            <asp:Label runat="server" ID="lbl_ncliente" Text=""></asp:Label>
            <br />
            <asp:Label runat="server" ID="lbl_dir" Text=""></asp:Label>
            <br />
            <asp:Label runat="server" ID="lbl_tel" Text=""></asp:Label>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
        <td valign="top">
            <asp:Label runat="server" ID="lbl_fecha" Text=""></asp:Label>
            <br />
            <asp:Label runat="server" ID="lbl_clte" Text=""></asp:Label>
            <br />
            <asp:Label runat="server" ID="lbl_ruc" Text=""></asp:Label>
        </td>
        </tr> </table>
        <%------------------------------------------------------------%>
        <%------------------- GridView -------------------------------%>
        <div class="centrar_GridView">
            <asp:GridView ID="gv_lista" runat="server" AllowSorting="True" DataKeyNames="sistema"
                CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No Se Encontraron Registros"
                Width="900px" PageSize="13" OnSorting="gv_lista_sort" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField HeaderText="Fecha" DataField="fecha" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Detalle" DataField="detall" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Doc Fiscal" DataField="fiscal" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField AccessibleHeaderText="Doc Sistema" HeaderText="Doc Sistema" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("sistema") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sistema") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Doc Sistema" DataField="sistema" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tipo Doc" DataField="tpd" HeaderStyle-HorizontalAlign="Justify"
                        ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Monto" DataField="monto" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:BoundField HeaderText="Saldo" DataField="remanente" HeaderStyle-HorizontalAlign="Justify"
                        ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>--%>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <br />
        <input type="image" runat="server" id="btnPrint" src="~/imagenes/imprimir.gif" onclick="javascript:CallPrint('divPrint')" />
    </div>
    </form>
</body>
</html>
