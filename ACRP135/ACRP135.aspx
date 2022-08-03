<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACRP135.aspx.cs" Inherits="ACRP135.ACRP135" %>

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
                        Reporte de Devoluciones - ACRP135
                    </h1>
                </div>
                <div class="loginDisplay">
                </div>
                <div class="Fecha">
                    <%
                        //fecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        //hora.Text = System.DateTime.Now.ToString("hh:mm");

                        this.fecha.Text = System.DateTime.Now.ToLongDateString();
                    %>
                    <asp:Label runat="server" ID="fecha"></asp:Label>
                    <asp:Label runat="server" ID="hora"></asp:Label>
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
            <br />
            <br />
            &nbsp;&nbsp;&nbsp
            <asp:Label runat="server" ID="lbl_fecha1" Text="Desde "></asp:Label>
            <asp:TextBox runat="server" ID="txt_fecha1" Text="" Width="65px" AutoPostBack="true"></asp:TextBox>
            <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar" />
            <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txt_fecha1"
                PopupButtonID="Image1" Format="dd/MM/yyyy" />
            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fecha1"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                ErrorTooltipEnabled="true">
            </asp:MaskedEditExtender>
            &nbsp
            <asp:Label runat="server" ID="lbl_fecha2" Text="Hasta"></asp:Label>&nbsp
            <asp:TextBox runat="server" ID="txt_fecha2" Text="" Width="65px" AutoPostBack="true"></asp:TextBox>
            <asp:ImageButton runat="Server" ID="Image2" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar" />
            <asp:CalendarExtender ID="calendarButtonExtender2" runat="server" TargetControlID="txt_fecha2"
                PopupButtonID="Image2" Format="dd/MM/yyyy" />
            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_fecha2"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                ErrorTooltipEnabled="true">
            </asp:MaskedEditExtender>
            &nbsp
            <asp:Label runat="server" ID="lbl_pref" Text="Prefijo"></asp:Label>
            <asp:DropDownList ID="ddl_pfx" AutoPostBack="true" OnSelectedIndexChanged="Selection_Change_pfx"
                runat="server" Width="150px" ToolTip="seleccione el Prefijo..." />
            &nbsp
            <asp:Label runat="server" ID="lbl_vend1" Text="Vendedor Desde"></asp:Label>&nbsp
            <asp:TextBox runat="server" ID="txt_vend1" Text="" MaxLength="6" ToolTip="Ingrese Código Vendedor"
                Height="21px" Width="52px"></asp:TextBox>
            <asp:MaskedEditExtender ID="txt_vend1_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                Enabled="True" TargetControlID="txt_vend1" Mask="999999" MaskType="Number" InputDirection="RightToLeft">
            </asp:MaskedEditExtender>
            &nbsp
            <asp:Label runat="server" ID="lbl_vend2" Text="Vendedor Hasta"></asp:Label>&nbsp
            <asp:TextBox runat="server" ID="txt_vend2" Text="" MaxLength="6" ToolTip="Ingrese Código Vendedor"
                Height="21px" Width="52px"></asp:TextBox>
            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                Enabled="True" TargetControlID="txt_vend2" Mask="999999" MaskType="Number" InputDirection="RightToLeft">
            </asp:MaskedEditExtender>
            &nbsp;&nbsp
            <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exportar
            a Excel&nbsp<asp:ImageButton runat="server" ID="img_excel" ImageUrl="~/imagenes/excel.png"
                Height="23px" Width="24px" ToolTip="Exportar a Excel..." OnClick="img_excel_Click" />
            <br />
            <br />
            <%------------------------------------------------------------%>
            <%------------------- GridView -------------------------------%>
            <div class="centrar_GridView">
                <asp:GridView ID="gv_lista" runat="server" AllowPaging="False" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="fecha,nota_cred,factura,clte" CellPadding="4"
                    ForeColor="#333333" GridLines="None" OnPageIndexChanging="gv_lista_Paginar" OnRowDataBound="gv_lista_RowDataBound"
                    OnRowCreated="OnRowCreated" EmptyDataText="No Se Encontraron Registros" Width="1200px"
                    PageSize="13" ShowFooter="True">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField HeaderText="Fecha" DataField="fecha" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nota Credito" DataField="nota_cred" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="N° Factura" DataField="factura" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--SortExpression="clte"--%>
                        <asp:BoundField HeaderText="Cod.Clte" DataField="clte" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nombre Cliente" DataField="nom_clte" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--SortExpression="vend"--%>
                        <asp:BoundField HeaderText="Vendedor" DataField="vend"  HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Localidad" DataField="local" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cantidad" DataField="cant" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N3}">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--SortExpression="placa"--%>
                        <asp:BoundField HeaderText="N° Placa" DataField="placa"  HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--SortExpression="causa"--%>
                        <asp:BoundField HeaderText="Cód. Razón" DataField="causa" 
                            HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Razón" DataField="desc_razon" HeaderStyle-HorizontalAlign="Justify"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
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
    </div>
    <div class="footer">
        <%--<asp:Image ID="img_footer" runat="server" ImageUrl="~/imagenes/logo_blanco.gif" 
            Height="93px" Width="113px"  ImageAlign="Right" />--%>
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
