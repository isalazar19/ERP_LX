<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACRP99.aspx.cs" Inherits="ACRP99.ACRP99" %>

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
                        Lanzamiento de Despacho Nota de Carga - ACRP99
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
            <asp:Label runat="server" ID="lbl_almacen" Text="Almacen"></asp:Label>
            <asp:DropDownList runat="server" ID="ddl_alm" AutoPostBack="true" OnSelectedIndexChanged="ddl_whs_Selection_Changed">
            </asp:DropDownList>
            &nbsp;&nbsp
            <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
            <br />
            <br />
            <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="alm,ped,nota_carga" CellPadding="4"
                ForeColor="#333333" GridLines="None" OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                Width="550px" PageSize="13" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField HeaderText="Almacen" DataField="alm" SortExpression="ctro" HeaderStyle-HorizontalAlign="Justify"
                        ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Pedido" DataField="ped" SortExpression="ctro" HeaderStyle-HorizontalAlign="Justify"
                        ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Nota Carga" DataField="nota_carga" SortExpression="ctro"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cantidad" DataField="cant" SortExpression="ctro" HeaderStyle-HorizontalAlign="Justify"
                        ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="N°Placa" DataField="placa" SortExpression="ctro" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Justify">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:CommandField HeaderText="Asignar" ItemStyle-HorizontalAlign="Center" ShowSelectButton="true"
                        ButtonType="Image" SelectImageUrl="~/imagenes/palomita.png" />
                    <%--<asp:CommandField HeaderText="" ItemStyle-HorizontalAlign="Center" ShowSelectButton="true" ButtonType="Link" SelectText="Grabar" />--%>
                </Columns>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:Label runat="server" ID="lbl_parametros" Text=""></asp:Label>
            <br />
            <asp:Label runat="server" ID="lbl_nota" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lbl_pedi" Text="" Visible="false"></asp:Label>
        </div>
        <%--hacer la ventana popup para seleccionar el cargo al hacer clic en el boton incluir--%>
        <asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" BorderColor="AliceBlue"
            BorderStyle="Solid" BorderWidth="2px" Width="350px" Height="140px">
            <asp:Label runat="server" ID="lbl_cargo" Text="Seleccione la Placa..." Font-Size="Small"></asp:Label>
            <br />
            &nbsp
            <asp:DropDownList runat="server" ID="ddl_placa" AutoPostBack="false" OnSelectedIndexChanged="ddl_placa_Selection_Change"
                Width="300px" ToolTip="Seleccione la Placa...">
            </asp:DropDownList>
            <br />
            <asp:Label runat="server" ID="lbl_user" Text="Ingrese el Usuario de LX" Font-Size="Small"></asp:Label>
            <br />
            &nbsp
            <asp:TextBox runat="server" ID="txt_user" Text="" Width="300px" CssClass="input_mayusculas"
                AutoPostBack="true"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:Button ID="btnOK" runat="server" Text="Enviar" ToolTip="Acepta los Datos para luego Enviar al Spool La Nota de Carga" />&nbsp;&nbsp
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpe" runat="server" TargetControlID="lbl_almacen" PopupControlID="pnlModal"
            OkControlID="btnOK" CancelControlID="btnCancel" DropShadow="true">
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
