<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SFCP320C.aspx.cs" Inherits="SFCP320C.SFCP320C" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Paveca.css" rel="stylesheet" type="text/css" />
</head>
<body class="mybody">
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </cc1:ToolkitScriptManager>
    <div>
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                        REPORTE DE SHRINKAGE - SFCP320C
                    </h1>
                </div>
                <div class="loginDisplay">
                    <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                        <AnonymousTemplate>
                            [ <a href="~/Account/Login.aspx" id="HeadLoginStatus" runat="server">Iniciar sesión</a>
                            ]
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <span class="bold">
                                <asp:LoginName ID="HeadLoginName" runat="server" />
                            </span>
                            <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText=""
                                LogoutPageUrl="~/" />
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
                <div class="clear hideSkiplink">
                </div>
            </div>
            &nbsp;
            <div class="Salir">
                <asp:ImageButton runat="server" ID="img_salir" ImageUrl="~/imagenes/back_boton.png"
                    ToolTip="Cerrar" ImageAlign="Right" OnClick="img_salir_Click" />
                <asp:Label runat="server" ID="lbl_salir" Text="Volver"></asp:Label>&nbsp;
            </div>
            &nbsp;
            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; margin-bottom: 10px;">
                <tr>
                    <td align="right" width="20%">
                        PAÍS:
                    </td>
                    <td width="30%">
                        <asp:Label ID="etiquetaParticionSeleccionada" runat="server" Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="20%">
                        Instalación:
                    </td>
                    <td>
                        <asp:DropDownList ID="CampoSeleccionInstalacion" runat="server" AutoPostBack="True"
                            Font-Names="Consolas" OnSelectedIndexChanged="CampoSeleccionInstalacion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Fecha/Período:
                    </td>
                    <td colspan="3" style="padding: 0px">
                        <table border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="campoFechaDesde" runat="server" BorderColor="#ABADB3" BorderWidth="1px"
                                        Columns="8" Font-Names="Consolas" MaxLength="8"></asp:TextBox>
                                    <cc1:CalendarExtender ID="campoFechaDesde_CalendarExtender" runat="server" Animated="False"
                                        DaysModeTitleFormat="MMM yyyy" Enabled="True" Format="yyyyMMdd" TargetControlID="campoFechaDesde"
                                        TodaysDateFormat="d MMM yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="center">
                                    a:
                                </td>
                                <td>
                                    <asp:TextBox ID="campoFechaHasta" runat="server" BorderColor="#ABADB3" BorderWidth="1px"
                                        Columns="8" Font-Names="Consolas" MaxLength="8"></asp:TextBox>
                                    <cc1:CalendarExtender ID="campoFechaHasta_CalendarExtender" runat="server" Animated="False"
                                        DaysModeTitleFormat="MMM yyyy" Enabled="True" Format="yyyyMMdd" TargetControlID="campoFechaHasta"
                                        TodaysDateFormat="d MMM yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="validacionCampoFechaDesde" runat="server" ControlToValidate="campoFechaDesde"
                                        ErrorMessage="(1) Indique una fecha inicial para limitar el reporte" Font-Size="Smaller"
                                        ForeColor="Red" ValidationGroup="Parametros">(1)</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="validacionCampoFechaHasta" runat="server" ControlToValidate="campoFechaHasta"
                                        ErrorMessage="(2) Indique una fecha final para limitar el reporte" Font-Size="Smaller"
                                        ForeColor="Red" ValidationGroup="Parametros">(2)</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Estado de la Orden:
                    </td>
                    <td colspan="3" style="padding: 0px">
                        <table border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="CampoEstadoOrden" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="CampoEstadoOrden_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="SO">Abiertas</asp:ListItem>
                                        <asp:ListItem Value="SZ">Cerradas</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td align="right">
                        Centro de Trabajo:
                    </td>
                    <td colspan="3" style="padding: 0px">
                        <div style="width: auto; float: right; clear: left;">
                            <asp:RadioButtonList ID="CampoRangoCentroDeTrabajo" runat="server" AutoPostBack="True"
                                Font-Size="Smaller" OnSelectedIndexChanged="CampoRangoCentroDeTrabajo_SelectedIndexChanged"
                                RepeatDirection="Horizontal" ToolTip="Rango de Centros de Trabajo" Width="175px">
                                <asp:ListItem Selected="True" Value="D">Defecto</asp:ListItem>
                                <asp:ListItem Value="R">Rango</asp:ListItem>
                                <asp:ListItem Value="T">Todos</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Panel ID="PanelRangoCentroDeTrabajo" runat="server" Visible="False">
                                <table cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td style="font-size: smaller">
                                            Código:
                                        </td>
                                        <td style="font-size: smaller">
                                            <asp:TextBox ID="CampoRangoCentroDeTrabajoDesde" runat="server" AutoPostBack="True"
                                                BorderColor="#ABADB3" BorderWidth="1px" Columns="6" Font-Names="Consolas" MaxLength="6">430000</asp:TextBox>
                                            <%--<cc1:CalendarExtender ID="CampoRangoCentroDeTrabajoDesde_CalendarExtender" runat="server"
                                                Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" Format="yyyyMMdd"
                                                TargetControlID="CampoRangoCentroDeTrabajoDesde" TodaysDateFormat="d MMM yyyy">
                                            </cc1:CalendarExtender>--%>
                                        </td>
                                        <td align="center" style="font-size: smaller">
                                            a:
                                        </td>
                                        <td style="font-size: smaller">
                                            <asp:TextBox ID="CampoRangoCentroDeTrabajoHasta" runat="server" AutoPostBack="True"
                                                BorderColor="#ABADB3" BorderWidth="1px" Columns="6" Font-Names="Consolas" MaxLength="6">439999</asp:TextBox>
                                            <%--<cc1:CalendarExtender ID="CampoRangoCentroDeTrabajoHasta_CalendarExtender" runat="server"
                                                Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" Format="yyyyMMdd"
                                                TargetControlID="CampoRangoCentroDeTrabajoHasta" TodaysDateFormat="d MMM yyyy">
                                            </cc1:CalendarExtender>--%>
                                        </td>
                                        <td style="font-size: smaller">
                                            <asp:RequiredFieldValidator ID="RequeridoCampoRangoCentroDeTrabajoDesde" runat="server"
                                                ControlToValidate="CampoRangoCentroDeTrabajoDesde" ErrorMessage="(3) Indique un código inicial de centro de trabajo para limitar la lista"
                                                Font-Size="Smaller" ForeColor="Red" ValidationGroup="Parametros">(3)</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequeridoCampoRangoCentroDeTrabajoHasta" runat="server"
                                                ControlToValidate="CampoRangoCentroDeTrabajoHasta" ErrorMessage="(4) Indique un código inicial de centro de trabajo para limitar la lista"
                                                Font-Size="Smaller" ForeColor="Red" ValidationGroup="Parametros">(4)</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="ValidacionCampoRangoCentroDeTrabajoDesde" runat="server"
                                                ControlToValidate="CampoRangoCentroDeTrabajoDesde" ErrorMessage="(3.1) Ingrese un número"
                                                Font-Size="Smaller" ForeColor="Red" ValidationExpression="^[0-9]{1,6}$" ValidationGroup="Parametros">(3.1)</asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="ValidacionCampoRangoCentroDeTrabajoHasta" runat="server"
                                                ControlToValidate="CampoRangoCentroDeTrabajoHasta" ErrorMessage="(3.2) Ingrese un número"
                                                Font-Size="Smaller" ForeColor="Red" ValidationExpression="^[0-9]{1,6}$" ValidationGroup="Parametros">(3.2)</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="CampoSeleccionCentroDeTrabajo" runat="server" AutoPostBack="True"
                                        Font-Names="Consolas" OnSelectedIndexChanged="CampoSeleccionCentroDeTrabajo_SelectedIndexChanged"
                                        OnDataBound="CampoSeleccionCentroDeTrabajo_DataBound" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="CampoCentroDeTrabajoDesde" runat="server" />
                        <asp:HiddenField ID="CampoCentroDeTrabajoHasta" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td valign="top">
                        <asp:Button ID="BotonActualizarReporte" runat="server" BackColor="#6699FF" BorderColor="Gray"
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Consolas"
                            ForeColor="White" Height="25px" Text="Ejecutar/Actualizar Reporte" ValidationGroup="Parametros"
                            OnClick="BotonActualizarReporte_Click" />
                    </td>
                    <td colspan="2" valign="top">
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ForeColor="Red" ValidationGroup="Parametros" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="PanelPantalla" runat="server">
                <asp:GridView ID="GridViewShinkrage" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    Width="100%" CellPadding="4" Font-Names="Consolas" Font-Size="Small" ForeColor="#333333"
                    OnDataBound="GridViewShinkrage_DataBound" GridLines="None" OnRowDataBound="GridViewShinkrage_RowDataBound"
                    ShowFooter="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumOrd" HeaderText="ORDEN" SortExpression="NumOrd" DataFormatString="{0:N0}">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdCtroWrk" SortExpression="IdCtroWrk">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NbCtroWrk" HeaderText="CENTRO DE TRABAJO" SortExpression="NbCtroWrk">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdProd" SortExpression="IdProd">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NbProd" HeaderText="ITEM" SortExpression="NbProd">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CONSUMIDO" HeaderText="CONSUMIDO" SortExpression="CONSUMIDO"
                            DataFormatString="{0:N1}">
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCIDO" HeaderText="PRODUCIDO" SortExpression="PRODUCIDO"
                            DataFormatString="{0:N1}">
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RECHAZADO" HeaderText="RECHAZO" SortExpression="RECHAZADO"
                            DataFormatString="{0:N1}">
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Diferencia">
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="SHINKRAGE">
                            <FooterStyle Font-Size="Medium" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="True" Font-Size="Medium" HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        NO SE ENCONTRARON REGISTROS CON LOS PARÁMETROS SELECCIONADOS...
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Top" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
