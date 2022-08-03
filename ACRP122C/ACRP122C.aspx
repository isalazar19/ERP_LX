<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACRP122C.aspx.cs" Inherits="ACRP122C.ACRP122C" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
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
                        Mantenimiento Movimiento Devoluciones - ACRP122C
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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl_almacen"
                ErrorMessage="* Seleccione un Almacen" Font-Size="Smaller" ForeColor="Red" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Label runat="server" ID="lbl_alm" Text="Seleccione Almacen"></asp:Label>
            <asp:DropDownList runat="server" ID="ddl_almacen" AutoPostBack="true" OnSelectedIndexChanged="ddl_almacen_Selection_Change"
                Width="230px" ToolTip="Seleccione el Almacen...">
            </asp:DropDownList>
            &nbsp;&nbsp
            <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" Visible="false" />
            <br />
            <br />
            <div class="centrar_GridView">
                <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="cliente,doc,nc" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                    OnRowEditing="gv_lista_RowEditing" OnRowCancelingEdit="gv_lista_RowCancelingEdit"
                    OnRowUpdating="gv_lista_RowUpdating" OnRowDeleting="gv_lista_RowDeleting" PageSize="20">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Columns>
                        <asp:CommandField HeaderText="" ItemStyle-HorizontalAlign="Center" ButtonType="Image"
                            ShowSelectButton="false" EditImageUrl="~/imagenes/inventario.png" ShowEditButton="True"
                            CancelImageUrl="~/imagenes/x.png" UpdateImageUrl="~/imagenes/save.png">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="Cliente">
                            <EditItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("cliente") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("cliente") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="N° Factura">
                            <EditItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("doc") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("doc") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha">
                            <EditItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendedor">
                            <EditItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("vndr") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("vndr") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Almacen">
                            <EditItemTemplate>
                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("alm") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("alm") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="N/C">
                            <EditItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("nc") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("nc") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <EditItemTemplate>
                                <asp:Label ID="Label16" runat="server" Text='<%# Bind("cant") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("cant") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Placa">
                            <EditItemTemplate>
                                <asp:Label ID="Label17" runat="server" Text='<%# Bind("num_placa") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("num_placa") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Causa">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("cod_causa") %>' Columns="2"
                                    CssClass="centrar_campo_plantilla_GV" Font-Names="Consolas"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("cod_causa") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="N° Pedido">
                            <EditItemTemplate>
                                <asp:Label ID="Label18" runat="server" Text='<%# Bind("pedido") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("pedido") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Localidad">
                            <EditItemTemplate>
                                <asp:Label ID="Label19" runat="server" Text='<%# Bind("local") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("local") %>'></asp:Label>
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
