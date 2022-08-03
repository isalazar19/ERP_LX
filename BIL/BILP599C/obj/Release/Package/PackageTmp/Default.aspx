<%@ Page Title="Relación de Documentos Recibidos del Transporte - BILP599C" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BILP599C._Default" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:Panel ID="panelSeleccionDeParticion" runat="server">
        <div>
            <table align="right" border="0" cellpadding="3" cellspacing="0" 
                style="width: 33%">
                <tr>
                    <td align="right" valign="middle">
                        PAÍS:</td>
                    <td valign="middle">
                        <asp:DropDownList ID="campoSeleccionDeParticion" runat="server" 
                            AutoPostBack="True" Font-Names="Consolas" 
                            onselectedindexchanged="campoSeleccionDeParticion_SelectedIndexChanged">
                            <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                            <asp:ListItem Enabled="False" Value="DNetDesarrollErpDE">DESARROLLO</asp:ListItem>
                            <asp:ListItem Value="DNetPrototipoErpPA">PANAMÁ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
</asp:Panel>
<asp:Panel ID="panelParametros" runat="server" Enabled="False" Visible="False">
    <table border="0" cellpadding="3" cellspacing="0" 
        style="width:100%; margin-bottom: 10px;">
        <tr>
            <td align="right" width="20%">
                PAÍS:</td>
            <td width="30%">
                <asp:Label ID="etiquetaParticionSeleccionada" runat="server" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td width="20%">
                <asp:Button ID="botonCambiarParticion" runat="server" BorderColor="DimGray" 
                    BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" Font-Bold="True" 
                    Font-Names="Consolas" Height="25px" onclick="botonCambiarParticion_Click" 
                    Text="Cambiar PAÍS" UseSubmitBehavior="False" Width="150px" />
            </td>
            <td width="30%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" width="20%">
                Compañía:</td>
            <td style="padding: 0px">
                <table border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            <asp:DropDownList ID="campoSeleccionDeCompania" runat="server" 
                                AutoPostBack="True" DataSourceID="SqlDataSourceListaCompanias" 
                                DataTextField="CMPNAM" DataValueField="CMPNY" Font-Names="Consolas" 
                                OnDataBound="campoSeleccionDeCompania_DataBound">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSourceListaCompanias" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOTNETAPPNDESARROL %>" 
                    ProviderName="<%$ ConnectionStrings:DOTNETAPPNDESARROL.ProviderName %>" SelectCommand="SELECT
	DISTINCT RCOL01.CMPNY, RCOL01.CMPNAM
FROM
	RCOL01
ORDER BY
	RCOL01.CMPNY"></asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                Período:</td>
            <td colspan="3" style="padding: 0px">
                <table border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="campoPeriodoDesde" runat="server" BorderColor="#ABADB3" 
                                BorderWidth="1px" Columns="10" Font-Names="Consolas" MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="campoPeriodoDesde_CalendarExtender" runat="server" 
                                Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" 
                                Format="dd/MM/yyyy" TargetControlID="campoPeriodoDesde" 
                                TodaysDateFormat="d MMM yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="center">
                            a:</td>
                        <td>
                            <asp:TextBox ID="campoPeriodoHasta" runat="server" BorderColor="#ABADB3" 
                                BorderWidth="1px" Columns="10" Font-Names="Consolas" MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="campoPeriodoHasta_CalendarExtender" runat="server" 
                                Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" 
                                Format="dd/MM/yyyy" TargetControlID="campoPeriodoHasta" 
                                TodaysDateFormat="d MMM yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="requeridoPeriodoDesde" runat="server" 
                                ControlToValidate="campoPeriodoDesde" 
                                ErrorMessage="(1) Seleccione el inicio del período" Font-Size="Smaller" 
                                ForeColor="Red" ValidationGroup="Parametros">(1)</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="requeridoPeriodoHasta" runat="server" 
                                ControlToValidate="campoPeriodoDesde" EnableTheming="True" 
                                ErrorMessage="(2) Seleccione el fin del período" Font-Size="Smaller" 
                                ForeColor="Red" ValidationGroup="Parametros">(2)</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                Almacén:</td>
            <td colspan="3">
                <asp:DropDownList ID="campoSeleccionAlmacen" runat="server" 
                    DataSourceID="SqlDataSourceListaAlmacenes" DataTextField="LDESC" 
                    DataValueField="LWHS" Font-Names="Consolas">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceListaAlmacenes" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOTNETAPPNDESARROL %>" 
                    ProviderName="<%$ ConnectionStrings:DOTNETAPPNDESARROL.ProviderName %>" 
                    SelectCommand="SELECT LWHS, LWHS||' - '||LDESC AS LDESC FROM IWML05 WHERE WMCOMP = ? ORDER BY LWHS">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="campoSeleccionDeCompania" DbType="Decimal" 
                            DefaultValue="-1" Name="?" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td align="right">
                Reporte a generar:</td>
            <td colspan="3">
                <asp:RadioButtonList ID="CampoReporteAGenerar" runat="server">
                    <asp:ListItem Selected="True" Value="1">RECIBIDOS</asp:ListItem>
                    <asp:ListItem Value="2">NO RECIBIDOS</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Usuario Cola Spool:</td>
            <td colspan="3" style="padding: 0px;">
                <table border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="campoSpoolDestino" runat="server" BorderColor="#ABADB3" 
                                BorderWidth="1px" Columns="10" Font-Names="Consolas" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="requeridoSpoolDestino" runat="server" 
                                ControlToValidate="campoSpoolDestino" 
                                ErrorMessage="(12) Ingrese el Usuario del Sistema que recibirá en su Spool el resultado del proceso" 
                                Font-Size="Smaller" ForeColor="Red" ValidationGroup="Parametros">(12)</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="validacionSpoolDestino" runat="server" 
                                ControlToValidate="campoSpoolDestino" 
                                ErrorMessage="(12.1) Solo permite Alfanumérico sin espacios" 
                                Font-Size="Smaller" ForeColor="Red" ValidationExpression="^[0-9a-zA-Z]{1,10}$" 
                                ValidationGroup="Parametros">(12.1)</asp:RegularExpressionValidator>
                        </td>
                        <td>
                            Usuario del Sistema que recibirá en su Spool el resultado del proceso</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td valign="top">
                <asp:Button ID="botonEjecutarProceso" runat="server" BackColor="#6699FF" 
                    BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Names="Consolas" 
                    ForeColor="White" Height="25px" onclick="botonEjecutarProceso_Click" 
                    Text="Ejecutar Proceso" ValidationGroup="Parametros" Width="180px" />
                <asp:Button ID="botonEjecutarOtro" runat="server" BorderColor="DimGray" 
                    BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" Enabled="False" 
                    Font-Bold="True" Font-Names="Consolas" Height="25px" 
                    onclick="botonCambiarParticion_Click" Text="Ejecutar Otro" Visible="False" 
                    Width="180px" />
            </td>
            <td colspan="2" valign="top">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" ForeColor="Red" 
                    ValidationGroup="Parametros" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="etiquetaMensajesDelPrograma" runat="server" 
                    EnableViewState="False" Font-Bold="True" ForeColor="Green" 
                    Text="Mensajes del programa:" ViewStateMode="Disabled" Visible="False"></asp:Label>
            </td>
            <td colspan="3" style="padding: 0px">
                <asp:BulletedList ID="listaMensajesDelPrograma" runat="server" 
                    EnableViewState="False" Font-Bold="True" ForeColor="Green" 
                    ViewStateMode="Disabled">
                </asp:BulletedList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="etiquetaAdvertenciasDelPrograma" runat="server" 
                    EnableViewState="False" Font-Bold="True" ForeColor="Red" 
                    Text="Advertencias del programa:" ViewStateMode="Disabled" Visible="False"></asp:Label>
            </td>
            <td colspan="3" style="padding: 0px">
                <asp:BulletedList ID="listaAdvertenciasDelPrograma" runat="server" 
                    EnableViewState="False" ForeColor="Red" ViewStateMode="Disabled">
                </asp:BulletedList>
            </td>
        </tr>
    </table>
</asp:Panel>
    </asp:Content>
