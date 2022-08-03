<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SFCP314C.aspx.cs" Inherits="ERP_LX.SFCP314C" %>

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
                        Consulta de Producción por Centro y Turno - SFCP314C
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
            <div class="mypanel_cia">
                <asp:Panel runat="server" ID="panel" Style="text-align: left; margin-bottom: 0px;">
                    <fieldset class="my_fieldset_cia">
                        <legend>
                            <asp:Label ID="lbl_panel" runat="server" Font-Bold="true" Text="Selección de Parámetros">
                            </asp:Label>
                        </legend>
                        <asp:Label runat="server" ID="lbl_fecha1" Text="Desde"></asp:Label>
                        <asp:TextBox runat="server" ID="txt_fecha1" Text="" Width="65px"></asp:TextBox>
                        <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar"
                            OnClick="Image1_Click" />
                        <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txt_fecha1"
                            PopupButtonID="Image1" Format="dd/MM/yyyy" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fecha1"
                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                            ErrorTooltipEnabled="true">
                        </asp:MaskedEditExtender>
                        <asp:Label runat="server" ID="lbl_almacen" Text="Almacen"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_alm" AutoPostBack="true" OnSelectedIndexChanged="ddl_whs_Selection_Changed">
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lbl_seleccion" Text="Tipo Recepción"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_tipo" AutoPostBack="true" OnSelectedIndexChanged="ddl_tipo_Selection_Change"
                            Width="150px" ToolTip="Seleccione el Tipo de Recepción...">
                            <asp:ListItem Value="  "></asp:ListItem>
                            <asp:ListItem Value="R">Orden de Fabricacion</asp:ListItem>
                            <asp:ListItem Value="R1">Reproceso</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lbl_und" Text="Unidades"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_und" AutoPostBack="true" OnSelectedIndexChanged="ddl_und_Selection_Change"
                            Width="100px" ToolTip="Seleccione el Tipo de Unidades...">
                            <asp:ListItem Value="  "></asp:ListItem>
                            <asp:ListItem Value="S">Standard</asp:ListItem>
                            <asp:ListItem Value="P">Propias</asp:ListItem>
                            <asp:ListItem Value="U">Unificadas</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                            Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
                        <br />
                        <asp:Label runat="server" ID="lbl_fecha2" Text="Hasta"></asp:Label>&nbsp
                        <asp:TextBox runat="server" ID="txt_fecha2" Text="" Width="65px"></asp:TextBox>
                        <asp:ImageButton runat="Server" ID="Image2" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar" />
                        <asp:CalendarExtender ID="calendarButtonExtender2" runat="server" TargetControlID="txt_fecha2"
                            PopupButtonID="Image2" Format="dd/MM/yyyy" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_fecha2"
                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                            ErrorTooltipEnabled="true">
                        </asp:MaskedEditExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp
                        <asp:RadioButton ID="rb_resumido" AutoPostBack="True" GroupName="RadioGroup1" Text="Resumido"
                            TextAlign="Left" runat="server" ToolTip="Solo Centro de Fabricacion..." OnCheckedChanged="rb_resumido_CheckedChanged" />
                        <asp:RadioButton ID="rb_detalle" AutoPostBack="True" GroupName="RadioGroup1" Text="Detallado"
                            TextAlign="Left" runat="server" ToolTip="Centro de Fabricacion y Productos..."
                            OnCheckedChanged="rb_detalle_CheckedChanged" />
                    </fieldset>
                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                        ControlToValidate="txt_fecha1" IsValidEmpty="false" MaximumValue="01/01/2099"
                        MinimumValue="01/01/2000" EmptyValueMessage="Enter Date Value" MaximumValueMessage="La Fecha debe ser menor a 01/01/2099"
                        InvalidValueBlurredMessage="Fecha Desde invalida" MinimumValueMessage="La Fecha debe ser mayor a 01/01/2000"
                        EmptyValueBlurredText="* Ingrese una Fecha Desde Valida" ToolTip="La Fecha debe ser entre 01/01/2000 a 01/01/2099"
                        ForeColor="Red">
                    </asp:MaskedEditValidator>
                    <br />
                    <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                        ControlToValidate="txt_fecha2" IsValidEmpty="false" MaximumValue="01/01/2099"
                        MinimumValue="01/01/2000" EmptyValueMessage="Enter Date Value" MaximumValueMessage="La Fecha debe ser menor a 01/01/2099"
                        InvalidValueBlurredMessage="Fecha Hasta invalida" MinimumValueMessage="La Fecha debe ser mayor a 01/01/2000"
                        EmptyValueBlurredText="* Ingrese una Fecha Hasta Valida" ToolTip="La Fecha debe ser entre 01/01/2000 a 01/01/2099"
                        ForeColor="Red">
                    </asp:MaskedEditValidator>
                </asp:Panel>
            </div>&nbsp;&nbsp;&nbsp
            <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="ctro" CellPadding="4" ForeColor="#333333"
                GridLines="None" OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                Width="550px" PageSize="17">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/imagenes/palomita.png" />
                    <asp:BoundField HeaderText="Centro Fabricación" DataField="ctro" SortExpression="ctro"
                        HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify">
                        <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Turno 3" DataField="turno3" SortExpression="turno3" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Turno 1" DataField="turno1" SortExpression="turno1" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Turno 2" DataField="turno2" SortExpression="turno2" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Total" DataField="total" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
