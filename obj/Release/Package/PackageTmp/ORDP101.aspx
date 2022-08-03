<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ORDP101.aspx.cs" Inherits="ERP_LX.ORDP101" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CancelClick() {
        }

    </script>
</head>
<body class="mybody">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        Mantenimiento Peso Caja Conversión - ORDP101
                    </h1>
                </div>
                <div class="loginDisplay">
                    <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                        <AnonymousTemplate>
                            <%--[ <a href="~/Account/Login.aspx" id="HeadLoginStatus" runat="server">Iniciar sesión</a>
                        ]--%>
                            &nbsp;&nbsp
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            Pantalla de bienvenida <span class="bold">
                                <asp:LoginName ID="HeadLoginName" runat="server" />
                            </span>! [
                            <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Cerrar sesión"
                                LogoutPageUrl="~/" />
                            ]
                        </LoggedInTemplate>
                    </asp:LoginView>
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
            </div>
            &nbsp;
            <div class="Salir">
                <asp:ImageButton runat="server" ID="img_salir" ImageUrl="~/imagenes/x.png" ToolTip="Cerrar"
                    ImageAlign="Right" OnClick="img_salir_Click" />
                <asp:Label runat="server" ID="lbl_salir" Text="Cerrar"></asp:Label>&nbsp;
            </div>
            <div class="panel">
                <asp:Panel runat="server" ID="panel2" Width="515px" Style="text-align: left; margin-bottom: 0px;">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbl_panel" runat="server" Font-Bold="true">
                            </asp:Label>
                        </legend>
                        <asp:Label runat="server" ID="lbl_tipo" Text="Seleccione su opción"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_tipo" AutoPostBack="true" OnSelectedIndexChanged="ddl_tipo_Selection_Change"
                            Width="120px" ToolTip="Seleccione el Tipo...">
                            <asp:ListItem Value="  "></asp:ListItem>
                            <asp:ListItem Value="IN">Insumo</asp:ListItem>
                            <asp:ListItem Value="PT">Paleta</asp:ListItem>
                            <asp:ListItem Value="ST">Stretch</asp:ListItem>
                            <asp:ListItem Value="CA">Capacidad</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                            Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
                    </fieldset>
                </asp:Panel>
            </div>
            <table>
                <tr>
                    <td valign="top">
                        <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="codigo" CellPadding="4" ForeColor="#333333"
                            GridLines="None" OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                            Width="550px" PageSize="15">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/imagenes/palomita.png" />
                                <asp:BoundField HeaderText="Insumo" DataField="codigo" SortExpression="codigo" HeaderStyle-HorizontalAlign="Justify"
                                    ItemStyle-HorizontalAlign="Justify" />
                                <asp:BoundField HeaderText="Clase/Articulo" DataField="articulo" SortExpression="articulo"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Descripción" DataField="nombre" SortExpression="nombre"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Centro" DataField="ctro" SortExpression="ctro" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Peso Estandar" DataField="peso" SortExpression="peso"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Unidad" DataField="und" ReadOnly="true" Visible="false" />
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                        <asp:Button runat="server" ID="b_incluir" Text="Incluir" Width="68px" OnClick="b_incluir_Click"
                            Visible="false" />
                        <asp:Button runat="server" ID="b_eliminar" Text="Borrar" Width="68px" Enabled="false"
                            OnClick="b_eliminar_Click" Visible="false" />
                        <asp:Button runat="server" ID="b_modificar" Text="Modificar" Width="68px" OnClick="b_modificar_Click"
                            Enabled="false" Visible="false" />
                        <asp:ConfirmButtonExtender ID="b_eliminar_ConfirmButtonExtender" runat="server" ConfirmText="Desea Eliminar este Registro?"
                            Enabled="True" TargetControlID="b_eliminar" OnClientCancel="CancelClick">
                        </asp:ConfirmButtonExtender>
                        <asp:Button runat="server" ID="b_guardar" Text="Guardar" Width="68px" Enabled="false"
                            OnClick="b_guardar_Click" Visible="false" />
                    </td>
                    <td valign="top">
                        <div class="panel">
                            <asp:Panel runat="server" ID="panel1" Width="520px" Style="text-align: left; margin-bottom: 0px;"
                                Visible="false">
                                <fieldset class="my_fieldset_small">
                                    <legend>
                                        <asp:Label ID="lbl_panel_datos" runat="server" Font-Bold="true">
                                        </asp:Label>
                                    </legend>
                                    <asp:Label runat="server" ID="lbl_codigo" Text="Código Insumo"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_codigo" Text="" Width="40px" Enabled="false"
                                        AutoPostBack="true" OnTextChanged="txt_codigo_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredtxtcodigo" runat="server" Display="Dynamic"
                                        ControlToValidate="txt_codigo" Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:Label runat="server" ID="lbl_descripcion" Text="Descripción"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_descripcion" Text="" Width="260px" Enabled="false"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_und" Text="Unidad de Medida"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddl_und" AutoPostBack="true" OnSelectedIndexChanged="ddl_und_Selection_Change"
                                        Width="240px" ToolTip="Seleccione la Unidad de Medida..." Enabled="false">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_articulo" Text="Clase o Artículo"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddl_art" AutoPostBack="true" OnSelectedIndexChanged="ddl_articulo_Selection_Change"
                                        Width="370px" ToolTip="Seleccione el Artículo..." Enabled="false">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_ctro" Text="Centro Fabricación"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddl_ctro" AutoPostBack="true" OnSelectedIndexChanged="ddl_ctro_Selection_Change"
                                        Width="240px" ToolTip="Seleccione el Centro de Fabricación..." Enabled="false">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_peso_std" Text="Peso Estandar"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_peso_std" Text="" Width="50px" Enabled="false"></asp:TextBox>
                                    <asp:Label runat="server" ID="lbl_peso_min" Text="Peso Mínimo"></asp:Label>&nbsp
                                    <asp:TextBox runat="server" ID="txt_peso_min" Text="" Width="50px" Enabled="false"></asp:TextBox>
                                    <asp:Label runat="server" ID="lbl_peso_max" Text="Peso Máximo"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_peso_max" Text="" Width="50px" Enabled="false"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_peso_prom" Text="Peso Promedio"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_peso_prom" Text="" Width="50px" Enabled="false"></asp:TextBox>
                                    <asp:Label runat="server" ID="lbl_prod_teor" Text="Producción Teórica"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_prod_teor" Text="" Width="50px" Enabled="false"></asp:TextBox>
                                    <asp:Label runat="server" ID="lbl_param" Text="" Visible="false"></asp:Label>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <%--hacer la ventana popup para seleccionar el cargo al hacer clic en el boton incluir--%>
        <%--<asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" BorderColor="AliceBlue"
            BorderStyle="Solid" BorderWidth="2px" Width="350px" Height="100px">
            <asp:Label runat="server" ID="lbl_tipo" Text="Seleccione su Opción..." Font-Size="Small"></asp:Label>
            <br />
            &nbsp
            <asp:DropDownList runat="server" ID="ddl_tipo" AutoPostBack="true" OnSelectedIndexChanged="ddl_tipo_Selection_Change"
                Width="300px" ToolTip="Seleccione el Tipo...">
                <asp:ListItem Value="IN">Insumo</asp:ListItem>
                <asp:ListItem Value="PT">Paleta</asp:ListItem>
                <asp:ListItem Value="ST">Stretch</asp:ListItem>
                <asp:ListItem Value="CA">Capacidad</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:Button ID="btnOK" runat="server" Text="OK" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpe" runat="server" TargetControlID="b_incluir" PopupControlID="pnlModal"
            OkControlID="btnOK" CancelControlID="btnCancel" DropShadow="true"></asp:ModalPopupExtender>--%>
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
