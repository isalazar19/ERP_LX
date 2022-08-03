<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ORDP58.aspx.cs" Inherits="ORDP58.ORDP58" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
    <script>
        function Aceptar() {
            //            alert('Ok');
            //            __doPostBack(sender, e);
            $get('btnGrid').click();
        }
        function Cancelar() {
            $get('btnCncl').click();
        }
    </script>
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
                        Pedidos Pendientes Entre Almacenes - ORDP584BC
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
            <asp:Label runat="server" ID="lbl_parametros" Text=""></asp:Label>
            <asp:Button runat="server" ID="btnGrid" Visible="true" OnClick="btnGrid_Click" />
            <asp:Button runat="server" ID="btnCncl" Visible="true" OnClick="btnCncl_Click" />
        </div>
        <%--hacer la ventana popup para seleccionar el cargo al hacer clic en el boton incluir--%>
        <asp:HiddenField runat="server" ID="CampoOculto" />
        <asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" BorderColor="AliceBlue"
            BorderStyle="Solid" BorderWidth="2px" Width="800px" Height="370px">
            <asp:Label runat="server" ID="lbl_fecha1" Text="Fecha Desde"></asp:Label>
            <asp:TextBox runat="server" ID="txt_fecha1" Text="" Width="65px"></asp:TextBox>
            <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar"
                OnClick="Image1_Click" />
            <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txt_fecha1"
                PopupButtonID="Image1" Format="dd/MM/yyyy" />
            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fecha1"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                ErrorTooltipEnabled="true">
            </asp:MaskedEditExtender>
            <asp:Label runat="server" ID="lbl_fecha2" Text="Fecha Hasta"></asp:Label>&nbsp
            <asp:TextBox runat="server" ID="txt_fecha2" Text="" Width="65px"></asp:TextBox>
            <asp:ImageButton runat="Server" ID="Image2" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar" />
            <asp:CalendarExtender ID="calendarButtonExtender2" runat="server" TargetControlID="txt_fecha2"
                PopupButtonID="Image2" Format="dd/MM/yyyy" />
            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_fecha2"
                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                ErrorTooltipEnabled="true">
            </asp:MaskedEditExtender>
            <asp:Label runat="server" ID="lbl_deposito" Text="Código del Deposito" Font-Size="Small"></asp:Label>
            <asp:DropDownList runat="server" ID="ddl_dep" AutoPostBack="false" OnSelectedIndexChanged="ddl_dep_Selection_Change"
                ToolTip="Seleccione el Depósito...">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label runat="server" ID="lbl_almacen" Text="Almacen del Cliente" Font-Size="Small"></asp:Label>
            <asp:DropDownList runat="server" ID="ddl_alm" AutoPostBack="false" OnSelectedIndexChanged="ddl_alm_Selection_Change"
                ToolTip="Seleccione el Almacen del Cliente...">
            </asp:DropDownList>
            <asp:Label runat="server" ID="lbl_tipoClte" Text="Tipo de cliente" Font-Size="Small"></asp:Label>
            <asp:DropDownList runat="server" ID="ddl_tipoClte" AutoPostBack="false" OnSelectedIndexChanged="ddl_tipo_Clte_Selection_Change"
                ToolTip="Seleccione el Tipo de cliente...">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label runat="server" ID="lbl_vendedor" Text="Vendedor"></asp:Label>
            <asp:DropDownList runat="server" ID="ddl_vend" AutoPostBack="false" OnSelectedIndexChanged="ddl_vend_Selection_Change"
                ToolTip="Seleccione el Vendedor...">
            </asp:DropDownList>
            <br />
            <table>
                <tr>
                    <td rowspan="3">
                        <div class="panel_filtro">
                            <asp:Panel runat="server" ID="panel" Style="text-align: left; margin-bottom: 0px;">
                                <fieldset class="myfieldset_filtro">
                                    <legend>
                                        <asp:Label ID="lbl_panel" runat="server" Font-Bold="true" Text="Tipo de Selección">
                                        </asp:Label>
                                    </legend>
                                    <asp:RadioButton ID="rb_pendiente" AutoPostBack="false" GroupName="RadioGroup1" Text="Pendiente"
                                        TextAlign="Right" runat="server" ToolTip="Pedidos Pendientes..." />
                                    <br />
                                    <asp:RadioButton ID="rb_backlog" AutoPostBack="false" GroupName="RadioGroup1" Text="BackLog"
                                        TextAlign="Right" runat="server" ToolTip="Pedidos en BackLog..." Checked="true" />
                                    <br />
                                    <asp:RadioButton ID="rb_remanentes" AutoPostBack="false" GroupName="RadioGroup1"
                                        Text="Remanentes" TextAlign="Right" runat="server" ToolTip="Pedidos Remanentes..." />
                                    <br />
                                    <asp:RadioButton ID="rb_backorder" AutoPostBack="false" GroupName="RadioGroup1" Text="BackOrder"
                                        TextAlign="Right" runat="server" ToolTip="Pedidos en BackOrder..." />
                                    <br />
                                    <asp:RadioButton ID="rb_reabastec" AutoPostBack="False" GroupName="RadioGroup1" Text="Reabastecimientos"
                                        TextAlign="Right" runat="server" ToolTip="Pedidos en Reabastecimientos..." />
                                </fieldset>
                            </asp:Panel>
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
                        <div class="panel">
                            <asp:Panel runat="server" ID="panel1" Style="text-align: left; margin-bottom: 0px;">
                                <fieldset class="my_fieldset_smaller">
                                    <legend>
                                        <asp:Label ID="lbl_panel2" runat="server" Font-Bold="true" Text="Detalle del Reporte"></asp:Label>
                                    </legend>
                                    <asp:RadioButton ID="rb_normal" AutoPostBack="false" GroupName="RadioGroup2" Text="Normal"
                                        TextAlign="Right" runat="server" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                    <asp:RadioButton ID="rb_pedido" AutoPostBack="false" GroupName="RadioGroup2" Text="Por Pedido"
                                        TextAlign="Right" runat="server" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                    <asp:RadioButton ID="rb_producto" AutoPostBack="false" GroupName="RadioGroup2" Text="Por Producto"
                                        TextAlign="Right" runat="server" />
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <div class="panel">
                            <asp:Panel runat="server" ID="panel2" Style="text-align: left; margin-bottom: 0px;">
                                <fieldset class="fieldset">
                                    <legend>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Filtro por Producto"></asp:Label>
                                    </legend>
                                    <asp:Label runat="server" ID="lbl_prod1" Text="Desde"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddl_prod1" AutoPostBack="false" OnSelectedIndexChanged="ddl_prod1_Selection_Change"
                                        ToolTip="Seleccione Producto Desde..." Width="200px">
                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="lbl_prod2" Text="Hasta"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddl_prod2" AutoPostBack="false" OnSelectedIndexChanged="ddl_prod2_SelectedIndexChanged"
                                        ToolTip="Seleccione Producto Hasta..." Width="200px">
                                    </asp:DropDownList>
                                    <br />
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <%--<asp:Button ID="btnOK" runat="server" Text="Aceptar" />&nbsp;&nbsp--%>
            <asp:ImageButton runat="server" ID="imgOK" ImageUrl="~/imagenes/palomita.png" />
            <asp:Label runat="server" ID="lbl_Aceptar" Text="Aceptar"></asp:Label>
            <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" />--%>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:ImageButton runat="server" ID="imgCancel" ImageUrl="~/imagenes/x.png" />
            <asp:Label runat="server" ID="lbl_Cancelar" Text="Cancelar"></asp:Label>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpe" runat="server" TargetControlID="CampoOculto" PopupControlID="pnlModal"
            OkControlID="imgOK" CancelControlID="imgCancel" DropShadow="true" OnOkScript="Aceptar()" OnCancelScript="Cancelar()">
        </asp:ModalPopupExtender>
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
