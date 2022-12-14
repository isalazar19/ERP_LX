<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSTP20.aspx.cs" Inherits="CSTP20.CSTP20" %>

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
<body class="my_body">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        Mantenimiento Tabla De Tipo Por Clase - CSTP20
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
            <div class="mypanel_cia">
                <asp:Panel runat="server" ID="panel2" Width="1015px" Style="text-align: left; margin-bottom: 0px;">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbl_panel" runat="server" Font-Bold="true">
                            </asp:Label>
                        </legend>
                        <asp:Label runat="server" ID="lbl_tipo" Text="Seleccione Tipo de Producto"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_tipo" AutoPostBack="True" OnSelectedIndexChanged="ddl_tipo_Selection_Change"
                            Width="190px" ToolTip="Seleccione el Tipo de Producto...">
                            <asp:ListItem Value="  "></asp:ListItem>
                            <asp:ListItem Value="PT">Prod. Terminado</asp:ListItem>
                            <asp:ListItem Value="SE">Semielaborados</asp:ListItem>
                            <asp:ListItem Value="EM">Empaques</asp:ListItem>
                            <asp:ListItem Value="CO">Cores</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lbl_almacen" Text="Almacen"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_almacen" AutoPostBack="true" OnSelectedIndexChanged="ddl_almacen_Selection_Change"
                            Width="230px" ToolTip="Seleccione el Almacen...">
                        </asp:DropDownList>
                        &nbsp;&nbsp
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                            Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_tipo"
                            ErrorMessage="* Seleccione un Tipo de Producto" ForeColor="Red" Font-Size="Smaller"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_almacen"
                            ErrorMessage="* Seleccione un Almacen" Font-Size="Smaller" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <%--                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl_clase"
                            ErrorMessage="* Seleccione una Clase" ForeColor="Red" Font-Size="Smaller" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>--%>
                    </fieldset>
                </asp:Panel>
            </div>
            <table>
                <tr>
                    <td valign="top">
                        <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" DataKeyNames="tipo,clase" CellPadding="4" ForeColor="#333333"
                            GridLines="None" OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                            Width="550px" PageSize="13">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/imagenes/palomita.png" />
                                <asp:BoundField HeaderText="Tipo Prod" DataField="tipo" SortExpression="tipo" HeaderStyle-HorizontalAlign="Justify"
                                    ItemStyle-HorizontalAlign="Justify" />
                                <asp:BoundField HeaderText="Clase" DataField="clase" SortExpression="clase" HeaderStyle-HorizontalAlign="Justify"
                                    ItemStyle-HorizontalAlign="Justify" />
                                <asp:BoundField HeaderText="Descripcion de la Clase" DataField="descripcion" HeaderStyle-HorizontalAlign="Justify"
                                    ItemStyle-HorizontalAlign="Justify" />
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
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
                                    <asp:Label runat="server" ID="lbl_codigo" Text="Codigo de la Tabla"></asp:Label>
                                    <asp:TextBox runat="server" ID="txt_codigo" Text="" Width="40px" Enabled="false"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_tipo2" Text="Tipo Producto"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                    <asp:TextBox runat="server" ID="txt_tipo" Text="" Width="40px" Enabled="false"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" ID="lbl_clase" Text="Clase"></asp:Label><br />
                                    <asp:DropDownList runat="server" ID="ddl_clase" AutoPostBack="true" OnSelectedIndexChanged="ddl_clase_Selection_Change"
                                        Width="450px" ToolTip="Seleccione la Clase de Articulo..." Enabled="false">
                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="lbl_param" Text="" Visible="false"></asp:Label>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
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
