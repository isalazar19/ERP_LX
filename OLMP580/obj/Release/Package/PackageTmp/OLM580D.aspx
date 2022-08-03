<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OLM580D.aspx.cs" Inherits="OLMP580.OLM580D" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
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
                        Llegadas De Envio - OLM580D
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
            &nbsp;&nbsp;&nbsp
            <asp:Label runat="server" ID="lbl_notaentrega" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lbl_factura" Text="" Visible="false"></asp:Label>
            <div class="seleccion_pedido">
                <asp:Label runat="server" ID="Label1" Text="Pedido N°" Font-Size="Small" Font-Bold="true"></asp:Label>
                <asp:TextBox runat="server" ID="txt_pedido" Text="" Width="100px" CssClass="input_mayusculas"
                    AutoPostBack="false">
                </asp:TextBox>
                &nbsp;&nbsp
                <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                    Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
            </div>
            <br />
            <div class="centrar_GridView">
                <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="nota_entrega" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                    OnRowEditing="gv_lista_RowEditing" OnRowCancelingEdit="gv_lista_RowCancelingEdit"
                    OnRowUpdating="gv_lista_RowUpdating" Width="700px" PageSize="11">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Columns>
                        <%--<asp:CommandField HeaderText="" ItemStyle-HorizontalAlign="Center" ButtonType="Image"
                            SelectImageUrl="~/imagenes/palomita.png" ShowSelectButton="true">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>--%>
                        <asp:CommandField HeaderText="" ItemStyle-HorizontalAlign="Center" ButtonType="Image"
                            ShowSelectButton="false" EditImageUrl="~/imagenes/inventario.png" ShowEditButton="True"
                            CancelImageUrl="~/imagenes/x.png" UpdateImageUrl="~/imagenes/save.png">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="Fecha de Recep" SortExpression="fecha">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fecha") %>' Height="20px"
                                    Width="85px" ToolTip="Ingrese la Fecha en formato dd/mm/aaaa" CssClass="centrar_campo_plantilla_GV"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/calendar.png"
                                    AlternateText="Click to show calendar" />
                                <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="TextBox1"
                                    PopupButtonID="ImageButton1" Format="dd/MM/yyyy" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox1"
                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                                    ErrorTooltipEnabled="true">
                                </asp:MaskedEditExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="N° Placa" SortExpression="placa">
                            <EditItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("placa") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("placa") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nota de Entrega" SortExpression="nota_entrega">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("nota_entrega") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("nota_entrega") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Factura" SortExpression="doc">
                            <EditItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("doc") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("doc") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Fact">
                            <EditItemTemplate>
                                <asp:Label ID="Label7" runat="server" Height="20px" Text='<%# Bind("fecfac") %>'
                                    Width="85px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("fecfac") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cliente" SortExpression="clte">
                            <EditItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("clte") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("clte") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Justify" />
                            <ItemStyle HorizontalAlign="Center" />
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
            <br />
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
