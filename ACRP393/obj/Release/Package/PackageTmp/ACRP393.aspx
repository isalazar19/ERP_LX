<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACRP393.aspx.cs" Inherits="ACRP393.ACRP393" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>
    <div>
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        Análisis de Vencimiento de Cuentas por Cobrar - ACRP393
                    </h1>
                </div>
                <div class="loginDisplay">
                </div>
                <div class="Fecha">
                </div>
                <div class="clear hideSkiplink">
                </div>
            </div>
            &nbsp;
            <div class="Salir">
                <asp:ImageButton runat="server" ID="img_salir" ImageUrl="~/imagenes/back_boton.png"
                    ToolTip="Cerrar" ImageAlign="Right" OnClick="img_salir_Click" />
                <asp:Label runat="server" ID="lbl_salir" Text="Volver"></asp:Label>&nbsp;
            </div>
            <%--colocar aqui el pais y cia--%>
            &nbsp;&nbsp;&nbsp
            <asp:Label runat="server" ID="lbl_pais" Text="" Font-Bold="true"></asp:Label>
            <asp:Label runat="server" ID="lbl_cia" Text="" Font-Bold="true"></asp:Label>
            &nbsp;&nbsp;&nbsp
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td bgcolor="#003366">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            runat="server" ID="lbl_moneda" Text="Moneda" ForeColor="White"></asp:Label>
                    </td>
                    <td bgcolor="#003366">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbl_recordid" Text="Mostrar" ForeColor="White"></asp:Label>
                    </td>
                    <td bgcolor="#003366">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lbl_cliente" Text="Por Cliente" ForeColor="White"></asp:Label>
                    </td>
                    <td bgcolor="#003366">
                        &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="Label1" Text="Por Vendedor" ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl_fecha1" Text="Fecha "></asp:Label>
                        <asp:TextBox runat="server" ID="txt_fecha1" Text="" Width="65px" AutoPostBack="true"
                            OnTextChanged="txt_fecha1_TextChanged"></asp:TextBox>
                        <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar" />
                        <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txt_fecha1"
                            PopupButtonID="Image1" Format="dd/MM/yyyy" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fecha1"
                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                            ErrorTooltipEnabled="true">
                        </asp:MaskedEditExtender>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButton ID="rb_base" AutoPostBack="True" GroupName="RadioGroup1" Text="Base"
                            TextAlign="Right" runat="server" ToolTip="Emite Edo Cta en moneda local..." Checked="true"
                            Visible="true" />
                        <asp:RadioButton ID="rb_transaccion" AutoPostBack="True" GroupName="RadioGroup1"
                            Text="Transacción" TextAlign="Right" runat="server" ToolTip="Emite Edo Cta en moneda extrajera..."
                            Visible="true" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rb_detalle" AutoPostBack="true" GroupName="RadioGroup2" Text="Detallado"
                            TextAlign="Right" runat="server" ToolTip="Muestra el Edo Cta Detallado..." Checked="false"
                            OnCheckedChanged="rb_detalle_CheckedChanged" />
                        <asp:RadioButton ID="rb_resumida" AutoPostBack="true" GroupName="RadioGroup2" Text="Resumido"
                            TextAlign="Right" runat="server" ToolTip="Muestra el Edo Cta Resumido..." OnCheckedChanged="rb_resumida_CheckedChanged"
                            Checked="true" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rb_natural" AutoPostBack="true" GroupName="RadioGroup3" Text="Natural"
                            TextAlign="Right" runat="server" ToolTip="Muestra el Edo Cta Por Cliente Natural..."
                            Checked="false" OnCheckedChanged="rb_natural_CheckedChanged" />
                        <asp:RadioButton ID="rb_corporativo" AutoPostBack="true" GroupName="RadioGroup3"
                            Text="Corporativo" TextAlign="Right" runat="server" ToolTip="Muestra el Edo por Cliente Corporativo..."
                            OnCheckedChanged="rb_corporativo_CheckedChanged" Checked="true" />
                    </td>
                    <td>
                        <asp:RadioButton runat="server" ID="rb_vndSI" AutoPostBack="true" GroupName="RadioGroup4"
                            Text="Si" TextAlign="Right" ToolTip="Muestra el Reporte por Vendedor" Checked="false"
                            OnCheckedChanged="rb_vndSI_CheckedChanged" />
                        <asp:RadioButton runat="server" ID="rb_vndNO" AutoPostBack="true" GroupName="RadioGroup4"
                            Text="No" TextAlign="Right" ToolTip="" Checked="true" OnCheckedChanged="rb_vndNO_CheckedChanged" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lbl_vndr" Text="Seleccione el Vendedor" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddl_vndr" AutoPostBack="true" OnSelectedIndexChanged="Selection_Change_vndr"
                            runat="server" Width="150px" ToolTip="seleccione el Vendedor..."  Visible="false"/>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                            Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" Visible="true" />
                    </td>
                    <td>
                        <%--<input type="image" runat="server" id="btnPrint" src="~/imagenes/imprimir.gif" onclick="javascript:CallPrint('divPrint')" />--%>
                        Exportar a Excel&nbsp
                        <asp:ImageButton runat="server" ID="img_excel" ImageUrl="~/imagenes/excel.png" Height="23px"
                            Width="24px" ToolTip="Exportar a Excel..." OnClick="img_excel_Click" />
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp &nbsp;&nbsp &nbsp;&nbsp
            <%------------------------------------------------------------%>
            <%------------------- GridView -------------------------------%>
            <%--<div class="centrar_GridView">--%>
            <asp:GridView ID="gv_lista" runat="server" AllowPaging="False" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="clte" CellPadding="4" ForeColor="#333333"
                GridLines="None" OnPageIndexChanging="gv_lista_Paginar" EmptyDataText="" PageSize="13"
                ShowFooter="True" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged" OnRowDataBound="gv_lista_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <%--<asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/imagenes/inventario.png" />--%>
                    <asp:BoundField HeaderText="Cod.Clte" DataField="clte" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Nombre Cliente" DataField="nom_clte" HeaderStyle-HorizontalAlign="Justify"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Saldo a la Fecha" DataField="saldo" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="En el Mes" DataField="saldomes" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Mas 30 días" DataField="saldo30dia" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="1-30 días" DataField="saldo1_30" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="31-60 días" DataField="saldo31_60" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="61-90 días" DataField="saldo61_90" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="> 90 días" DataField="saldo91" DataFormatString="{0:#,#.00}"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <%--</div>--%>
            <%------------------------------------------------------------%>
            <%-------------- GridView Detalle x Vendedor -----------------%>
            <div class="centrar_GridView">
                <asp:GridView ID="gv_detalle" runat="server" AllowSorting="True" DataKeyNames="clte"
                    CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="" Width="900px"
                    PageSize="13" OnSorting="gv_detalle_sort" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField AccessibleHeaderText="Vendedor" HeaderText="Vendedor" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("vndr") %>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("clte") %>'></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("num_doc") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("vndr") %>)'></asp:TextBox>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("clte") %>'></asp:TextBox>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("num_doc") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Vendedor" DataField="vndr" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo Doc" DataField="tipo_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="N° Doc" DataField="num_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Prefijo" DataField="pref_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha" DataField="fecha_doc" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Vencimiento" DataField="fecha_vcto" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cliente" DataField="clte" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Saldo a la Fecha" DataField="saldo" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="En el Mes" DataField="saldomes" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Mas 30 días" DataField="saldo30dia" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="1-30 días" DataField="saldo1_30" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="31-60 días" DataField="saldo31_60" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="61-90 días" DataField="saldo61_90" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="> 90 días" DataField="saldo91" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <%------------------------------------------------------------%>
            <%-------------- GridView Detalle x Cliente  -----------------%>
            <div class="centrar_GridView">
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" DataKeyNames="clte"
                    CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="" Width="900px"
                    PageSize="13" OnSorting="gv_detalle_sort" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField AccessibleHeaderText="Cliente" HeaderText="Cliente" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("clte") %>'></asp:Label>
                                <%--<asp:Label ID="Label4" runat="server" Text='<%# Bind("tpo") %>'></asp:Label>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("clte") %>'></asp:TextBox>
                                <%--<asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("tpo") %>'></asp:TextBox>--%>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Vendedor" DataField="vndr" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo Doc" DataField="tipo_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="N° Doc" DataField="num_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Prefijo" DataField="pref_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha" DataField="fecha_doc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Vencimiento" DataField="fecha_vcto" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cliente" DataField="clte" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Saldo a la Fecha" DataField="saldo" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="En el Mes" DataField="saldomes" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Mas 30 días" DataField="saldo30dia" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="1-30 días" DataField="saldo1_30" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="31-60 días" DataField="saldo31_60" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="61-90 días" DataField="saldo61_90" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="> 90 días" DataField="saldo91" DataFormatString="{0:#,#.00}"
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <%------------------------------------------------------------%>
            <asp:GridView ID="gv_saldo" runat="server" AllowPaging="False" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="clte" CellPadding="4" ForeColor="#333333"
                GridLines="None" PageSize="13" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField HeaderText="Cod.Clte" DataField="clte" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Clte Nat" DataField="clten" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Monto" DataField="monto" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha Doc" DataField="fechadoc" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha Vcto" DataField="fechavcto" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
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
    <div class="footer">
        <asp:Label runat="server" ID="mensaje1">&copy 2012 Copyright Papeles Venezolanos, C.A.</asp:Label>
        <br />
        <asp:Label runat="server" ID="mensaje2">Todos los derechos reservados</asp:Label>
        <br />
        <asp:Label runat="server" ID="mensaje3">Gerencia Tecnología de Información </asp:Label>
        <br />
        <asp:Label runat="server" ID="mensaje4">Versión: V.1.0</asp:Label>
    </div>
    </form>
</body>
</html>
