<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SFCP108.aspx.cs" Inherits="SFCP108.SFCP108" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Aceptar() {
            alert('Ok');


        }


    </script>
</head>
<body class="mybody">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>    
&nbsp;&nbsp;&nbsp; 
    <div>
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        Restaurar Orden de Fabricación en Uso - SFCP108
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
            <asp:ImageButton runat="server" ID="img_ejecutar" ImageUrl="~/imagenes/invisible_boton.png" />
            <%--colocar aqui el pais y cia--%>
            &nbsp;&nbsp;&nbsp
            <asp:Label runat="server" ID="lbl_pais" Text="" Font-Bold="true"></asp:Label>
            <asp:Label runat="server" ID="lbl_cia" Text="" Font-Bold="true"></asp:Label>
        </div>
        <%--hacer la ventana popup para seleccionar el cargo al hacer clic en el boton incluir--%>
        <asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" BorderColor="AliceBlue"
            BorderStyle="Solid" BorderWidth="2px" Width="350px" Height="140px">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label runat="server" ID="lbl_orden" Text="Indicar N° Orden de Fabricación" Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox runat="server" ID="txt_orden" Text="" Width="100px" CssClass="input_mayusculas"
                AutoPostBack="false" MaxLength="8" >
            </asp:TextBox>
            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_orden"
                Mask="99999999" MaskType="Number" InputDirection="RightToLeft"></asp:MaskedEditExtender>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:Button ID="btnOK" runat="server" Text="Restaurar" ToolTip="Actualizar" 
                onclick="btnOK_Click" />
            &nbsp;&nbsp
            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpe" runat="server" TargetControlID="lbl_pais" PopupControlID="pnlModal"
             CancelControlID="btnCancel" DropShadow="true" OnOkScript="Aceptar()">
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
