<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SYSPC199.aspx.cs" Inherits="SYSPC199.SYSPC199" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CancelClick() {
        }
        function CancelClick() {
        }

        var est;
        //        function onOK() {
        //        document.getElementById('parrafo1').classname=est;
        //        }

        function toUpper(txt_user) {
            if (/[a-z]/.test(txt_user.value)) {
                control.value = txt_user.value.toUpperCase();
            }
        }

    </script>
</head>
<body class="mybody">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>
    <div>
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        Análisis de Movimiento (Inv/Fac/CxC)- SYSPC199
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
            <br /><br /><br /><br />
            <div class="centrar_GridView">
            <asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" BorderColor="AliceBlue"
                BorderStyle="Solid" BorderWidth="2px" Width="350px" Height="140px">
                <asp:Label runat="server" ID="lbl_fecha1" Text="Fecha Desde" Font-Size="Small"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lbl_fecha2" Text="Fecha Hasta" Font-Size="Small"></asp:Label>
                <br />
                &nbsp
                <asp:TextBox runat="server" ID="txt_fecha1" Text="" Width="65px" AutoPostBack="true"
                    OnTextChanged="txt_fecha1_TextChanged"></asp:TextBox>
                <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CalendarExtender ID="calendarButtonExtender" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="Image1" TargetControlID="txt_fecha1" />
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ErrorTooltipEnabled="true"
                    InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                    TargetControlID="txt_fecha1">
                </asp:MaskedEditExtender>
                <asp:TextBox ID="txt_fecha2" runat="server" AutoPostBack="true" OnTextChanged="txt_fecha2_TextChanged"
                    Text="" Width="65px"></asp:TextBox>
                <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/imagenes/calendar.png"
                    AlternateText="Click to show calendar" />
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image2"
                    TargetControlID="txt_fecha2" />
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ErrorTooltipEnabled="true"
                    InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                    TargetControlID="txt_fecha2">
                </asp:MaskedEditExtender>
                <br />
                <asp:Label ID="lbl_user" runat="server" Font-Size="Small" Text="Ingrese el Usuario de LX"></asp:Label>
                <br />
                &nbsp;
                <asp:TextBox ID="txt_user" runat="server" AutoPostBack="false" CssClass="input_mayusculas"
                    Text="" Width="300px"></asp:TextBox>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnOK" runat="server" Text="Enviar" 
                    ToolTip="Acepta los Datos para luego Enviar al Spool el Análisis" 
                    onclick="btnOK_Click" />&nbsp;&nbsp
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click" />
            </asp:Panel>
<%--            <asp:ModalPopupExtender ID="mpe" runat="server" TargetControlID="lbl_fecha1" PopupControlID="pnlModal"
                OkControlID="btnOK" CancelControlID="btnCancel" DropShadow="true">
            </asp:ModalPopupExtender>--%>
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
