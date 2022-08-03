<%@ Page Title="Emisión de Documentos a Interfaz Fiscal PAPISA - BILP500PA" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BILP500PA._Default" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .tableVistaPreviaDocumento
        {
            width: 100%;
            font-family: Consolas;
        }
        .tableVistaPreviaDocumento TD
        {
            vertical-align: top;
        }
        .campoDataAlineadaDerecha
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelParametros" runat="server">
        <table border="0" cellpadding="3" cellspacing="0" 
    style="width:100%; margin-bottom: 10px;">
            <tr>
                <td align="right" width="20%">
                    PAÍS:</td>
                <td width="30%">
                    <strong>PANAMÁ</strong></td>
                <td width="20%">
                    &nbsp;</td>
                <td width="30%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    Compañía:</td>
                <td>
                    <asp:DropDownList ID="campoSeleccionDeCompania" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSourceListaCompanias" DataTextField="CMPNAM" 
                DataValueField="CMPNY" Font-Names="Consolas">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceListaCompanias" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
                ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" 
                SelectCommand="SELECT CMPNY, CMPNY||' - '||CMPNAM AS CMPNAM FROM RCOL01">
                    </asp:SqlDataSource>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    Fecha:</td>
                <td colspan="3" style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="campoFechaDesde" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="8" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="65px"></asp:TextBox>
                                <cc1:CalendarExtender ID="campoFechaDesde_CalendarExtender" 
                                    runat="server" Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" 
                                    Format="yyyyMMdd" TargetControlID="campoFechaDesde" 
                                    TodaysDateFormat="d MMM yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="center">
                                a:</td>
                            <td>
                                <asp:TextBox ID="campoFechaHasta" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="8" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="65px"></asp:TextBox>
                                <cc1:CalendarExtender ID="campoFechaHasta_CalendarExtender" 
                                    runat="server" Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" 
                                    Format="yyyyMMdd" TargetControlID="campoFechaHasta" 
                                    TodaysDateFormat="d MMM yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    Prefijos de Documentos:</td>
                <td valign="top" colspan="3">
                    <asp:GridView ID="GridViewPrefijosDeDocumento" runat="server" 
                        AutoGenerateColumns="False" CellPadding="3" 
                        DataSourceID="SqlDataSourcePrefijosDeDocumento" Font-Names="Consolas" 
                        Font-Size="Small" ForeColor="#333333" GridLines="None" Width="100%" 
                        onselectedindexchanged="GridViewPrefijosDeDocumento_SelectedIndexChanged" 
                        OnDataBound="GridViewPrefijosDeDocumento_DataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField SelectText="SELECCIONAR" ShowSelectButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField DataField="DSDPFX" HeaderText="PREFIJO" SortExpression="DSDPFX" 
                                HtmlEncode="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DSDESC" HeaderText="DESCRIPCIÓN" 
                                SortExpression="DSDESC" HtmlEncode="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="False" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="INTERFAZ FISCAL" HtmlEncode="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#ABBCCD" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F8F9" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourcePrefijosDeDocumento" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
                        ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	DSDPFX, DSDESC
FROM
	RDSL01
WHERE
	(DSSSID ='R') AND
	(DSCMPY = ?)
ORDER BY
	DSDPFX">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="campoSeleccionDeCompania" 
                                ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelParametrosSeleccionados" runat="server" Visible="False">
        <table border="0" cellpadding="3" cellspacing="0" 
    style="width:100%; margin-bottom: 10px;">
            <tr>
                <td align="right" width="20%">
                    PAÍS:</td>
                <td width="30%">
                    <strong>PANAMÁ</strong></td>
                <td width="20%">
                    <asp:HiddenField ID="campoPrefijoDocumento" runat="server" />
                    <asp:HiddenField ID="campoTipoDocumentoDesde" runat="server" Value="0" />
                    <asp:HiddenField ID="campoTipoDocumentoHasta" runat="server" Value="9" />
                </td>
                <td width="30%">
                    <asp:Button ID="botonCambiarParametros" runat="server" BackColor="#6699FF" 
                        BorderStyle="None" BorderWidth="1px" Font-Bold="True" ForeColor="White" 
                        onclick="botonCambiarParametros_Click" Text="Cambiar Parámetros" 
                        Width="250px" Font-Names="Consolas" Height="25px" />
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    Compañía:</td>
                <td>
                    <asp:Label ID="etiquetaCompania" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td align="right">
                    Fecha:</td>
                <td style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="etiquetaFechaDesde" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="center">
                                a:</td>
                            <td>
                                <asp:Label ID="etiquetaFechaHasta" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Prefijo de Documento:</td>
                <td>
                    <asp:Label ID="etiquetaPrefijo" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td align="right">
                    Interfaz Fiscal:</td>
                <td style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="etiquetaInterfazFiscal" runat="server" Font-Bold="True" 
                                    ForeColor="Green"></asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="botonActualizarEstadoInterfazFiscal" runat="server" 
                                    ImageUrl="~/images/refresh.gif" 
                                    onclick="botonActualizarEstadoInterfazFiscal_Click" Visible="False" />
                                <asp:HiddenField ID="estadoInterfazFiscal" runat="server" Value="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Tipo de Documento:</td>
                <td colspan="2" style="padding: 0px">
                    <asp:RadioButtonList ID="campoSeleccionTipoDocumento" runat="server" 
                        AutoPostBack="True" CellPadding="3" 
                        onselectedindexchanged="campoSeleccionTipoDocumento_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">1 - Factura</asp:ListItem>
                        <asp:ListItem Value="2">2 - Nota de Débito</asp:ListItem>
                        <asp:ListItem Value="3">3 - Nota de Crédito</asp:ListItem>
                        <asp:ListItem Selected="True" Value="T">Todos los Tipos</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="botonActualizarDocumentos" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Bold="True"
                        Text="Actualizar Lista de Documentos" Width="250px" BorderColor="DimGray" 
                        Font-Names="Consolas" ForeColor="DimGray" Height="25px" 
                        onclick="botonActualizarDocumentos_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Leyenda:</td>
                <td colspan="3" style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0" style="font-size: smaller">
                        <tr>
                            <td align="center" width="20">
                                <asp:Image ID="imagenTTA1" runat="server" BorderWidth="0px" 
                                    ImageUrl="~/images/up.gif" ToolTip="ENVIAR a Interfaz Fiscal" />
                            </td>
                            <td>
                                : ENVIAR a Interfaz Fiscal</td>
                            <td align="center" width="20">
                                <asp:Image ID="imagenTTA2" runat="server" BorderWidth="0px" 
                                    ImageUrl="~/images/dn.gif" ToolTip="RECIBIR ID Fiscal" />
                            </td>
                            <td>
                                : RECIBIR ID Fiscal</td>
                            <td align="center" width="20">
                                <asp:Image ID="imagenTTA3" runat="server" BorderWidth="0px" 
                                    ImageUrl="~/images/edit.gif" ToolTip="REGISTRO MANUAL de ID Fiscal" />
                            </td>
                            <td>
                                : REGISTRO MANUAL de ID Fiscal</td>
                        </tr>
                    </table>
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
                        EnableViewState="False" ForeColor="Green" ViewStateMode="Disabled">
                    </asp:BulletedList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="etiquetaAdvertenciasDelPrograma" runat="server" 
                        Text="Advertencias del programa:" EnableViewState="False" Font-Bold="True" 
                        ForeColor="Red" ViewStateMode="Disabled" Visible="False"></asp:Label>
                </td>
                <td colspan="3" style="padding: 0px">
                    <asp:BulletedList ID="listaAdvertenciasDelPrograma" runat="server" 
                        EnableViewState="False" ForeColor="Red" ViewStateMode="Disabled">
                    </asp:BulletedList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelDocumentos" runat="server" Enabled="False" Visible="False">
        <asp:GridView ID="GridViewDocumentos" runat="server" 
            AutoGenerateColumns="False" CellPadding="4" Font-Names="Consolas" 
            ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridViewDocumentos_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="FECHAE" HeaderText="Emisión" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SFRES" HeaderText="Razón">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="IHDTYP" HtmlEncode="False" ShowHeader="False">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TIPOD" HeaderText="Tipo" >
                <HeaderStyle HorizontalAlign="Left" Font-Size="Smaller" />
                <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="IHDPFX" HeaderText="Pref." HtmlEncode="False">
                <HeaderStyle Font-Size="Smaller" HorizontalAlign="Right" />
                <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="IHDOCN" HeaderText="# Documento LX" 
                    HtmlEncode="False">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Font-Bold="True" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField ConvertEmptyStringToNull="False" DataField="CODCLIENTE" 
                    HeaderText="Cod." HtmlEncode="False">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="NOMCLIENTE" HeaderText="Cliente">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SITOT" DataFormatString="{0:N2}" HeaderText="Monto">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="DOCASO" HeaderText="# Doc. Asoc" HtmlEncode="False" 
                    NullDisplayText="-">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="IDFISCAL" HeaderText="ID Fiscal" 
                    ConvertEmptyStringToNull="False" HtmlEncode="False" NullDisplayText="-" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Comandos" ShowHeader="False">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" 
                            align="center">
                            <tr>
                                <td align="center" width="18">
                                    <asp:ImageButton ID="botonComando1" runat="server" ImageUrl="~/images/up.gif" 
                                        onclick="botonComando1_Click" 
                                        AlternateText="ENVIAR a Interfaz Fiscal" BorderWidth="0px" 
                                        CommandName="Comando1" style="width: 16px" />
                                </td>
                                <td align="center" width="18">
                                    <asp:ImageButton ID="botonComando2" runat="server" 
                                        ImageUrl="~/images/dn-off.gif" onclick="botonComando2_Click" 
                                        AlternateText="RECIBIR ID Fiscal" BorderWidth="0px" 
                                        Enabled="False" CommandName="Comando2" />
                                </td>
                                <td align="center" width="18">
                                    <asp:ImageButton ID="botonComando3" runat="server" ImageUrl="~/images/edit.gif" 
                                        onclick="botonComando3_Click" 
                                        AlternateText="REGISTRO MANUAL de ID Fiscal" BorderWidth="0px" 
                                        CommandName="Comando3" />
                                </td>
                                <td align="center" width="18">
                                    <asp:ImageButton ID="botonComando4" runat="server" ImageUrl="~/images/view.gif" 
                                        onclick="botonComando4_Click" 
                                        AlternateText="VER DOCUMENTO Proforma" BorderWidth="0px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataTemplate>
                <span class="style1"><em>NO HAY DOCUMENTOS QUE CUMPLAN CON LOS PARÁMETROS 
                SELECCIONADOS... </em></span>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#6699FF" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceDocumentos" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	SIHL01.SICOMP, SIHL01.SIID, SIHL01.IHDTYP, SIHL01.SFRES, SIHL01.IHDPFX,
	SIHL01.IHDOCN, SIHL01.SILINS, RCML12.CCUST AS CCUST_CLIENTE, 
	RCML12.CNME AS CNME_CLIENTE, RCML12_1.CCUST AS CCUST_FACTURACION, RCML12_1.CNME AS CNME_FACTURACION, 
	SIHL01.SITOT, RARL05.ARCAIN, RARL05.ARHNDL, RARL05.ARODPX, RARL05.ARODTP, 
	SIHL01.SIINVN, RARL05.RIDTE, RARL05.RDDTE, SIHL01.SIINVD
FROM
	SIHL01, RARL05, RCML12, RCML12 RCML12_1
WHERE
	SIHL01.SIINVN = RARL05.RINVC AND SIHL01.IHDPFX = RARL05.ARDPFX AND 
	SIHL01.IHDTYP = RARL05.ARDTYP AND SIHL01.IHDOCN = RARL05.ARDOCN AND SIHL01.SICUST = RCML12.CCUST AND 
	SIHL01.IHINCU = RCML12_1.CCUST AND SIHL01.SICOMP = RARL05.RCOMP AND
	(SIHL01.SIID = 'IH') AND
	(SIHL01.SICOMP = ?) AND
	(SIHL01.IHDPFX = ?) AND
	(SIHL01.SIINVD BETWEEN ? AND ?) AND
	(SIHL01.IHDTYP BETWEEN ? AND ?)
ORDER BY
	SIHL01.SIINVD DESC, SIHL01.IHDOCN DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="campoSeleccionDeCompania" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="campoPrefijoDocumento" 
                    ConvertEmptyStringToNull="False" DbType="String" Name="?" 
                    PropertyName="Value" />
                <asp:ControlParameter ControlID="campoFechaDesde" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" Name="?" 
                    PropertyName="Text" />
                <asp:ControlParameter ControlID="campoFechaHasta" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" Name="?" 
                    PropertyName="Text" />
                <asp:ControlParameter ControlID="campoTipoDocumentoDesde" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                    PropertyName="Value" />
                <asp:ControlParameter ControlID="campoTipoDocumentoHasta" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="9" Name="?" 
                    PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
    <asp:Panel ID="PanelComando3" runat="server" Enabled="False" Visible="False">
        <table border="0" cellpadding="3" cellspacing="0" 
    style="width:100%; margin-bottom: 10px;">
            <tr>
                <td colspan="4" 
                    style="background-color: #FF6600; color: #FFFFFF; font-weight: bolder">
                    REGISTRO MANUAL de ID Fiscal</td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    Emisión:</td>
                <td width="30%">
                    &nbsp;</td>
                <td align="right" width="20%">
                    Razón:</td>
                <td width="30%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    Tipo:</td>
                <td>
                    &nbsp;</td>
                <td align="right">
                    # Documento LX:</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td style="padding: 0px">
                    &nbsp;</td>
                <td align="right">
                    # Documento Asociado:</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    Cliente:</td>
                <td style="padding: 0px">
                    &nbsp;</td>
                <td align="right">
                    Monto:</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    Fecha de Impresión Fiscal:</td>
                <td style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="campoFechaEmisionFiscal" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="8" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="65px"></asp:TextBox>
                                <cc1:CalendarExtender ID="campoFechaEmisionFiscal_CalendarExtender" 
                                    runat="server" Animated="False" DaysModeTitleFormat="MMM yyyy" Enabled="True" 
                                    Format="yyyyMMdd" TargetControlID="campoFechaEmisionFiscal" 
                                    TodaysDateFormat="d MMM yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    Hora de Impresión Fiscal:</td>
                <td style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0" style="font-family: Consolas">
                        <tr>
                            <td>
                                H</td>
                            <td align="center">
                                <asp:TextBox ID="campoCorrelativoFiscal0" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="2" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="15px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="campoCorrelativoFiscal0_MaskedEditExtender" 
                                    runat="server" AutoCompleteValue="0" ErrorTooltipEnabled="True" 
                                    InputDirection="RightToLeft" Mask="99999999" MaskType="Number" 
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                                    OnInvalidCssClass="MaskedEditError" PromptCharacter="0" 
                                    TargetControlID="campoCorrelativoFiscal0">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td align="center">
                                &nbsp;</td>
                            <td align="center">
                                M</td>
                            <td align="center">
                                <asp:TextBox ID="campoCorrelativoFiscal1" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="2" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="15px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="campoCorrelativoFiscal1_MaskedEditExtender" 
                                    runat="server" AutoCompleteValue="0" ErrorTooltipEnabled="True" 
                                    InputDirection="RightToLeft" Mask="99999999" MaskType="Number" 
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                                    OnInvalidCssClass="MaskedEditError" PromptCharacter="0" 
                                    TargetControlID="campoCorrelativoFiscal1">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td align="center">
                                &nbsp;</td>
                            <td align="center">
                                S</td>
                            <td align="center">
                                <asp:TextBox ID="campoCorrelativoFiscal2" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="2" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="15px">00</asp:TextBox>
                                <cc1:MaskedEditExtender ID="campoCorrelativoFiscal2_MaskedEditExtender" 
                                    runat="server" AutoCompleteValue="0" ErrorTooltipEnabled="True" 
                                    InputDirection="RightToLeft" Mask="99999999" MaskType="Number" 
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                                    OnInvalidCssClass="MaskedEditError" PromptCharacter="0" 
                                    TargetControlID="campoCorrelativoFiscal2">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    # Correlativo Fiscal:</td>
                <td style="padding: 0px">
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                                &nbsp;-</td>
                            <td align="center">
                                <asp:TextBox ID="campoCorrelativoFiscal" runat="server" BorderColor="#ABADB3" 
                                    BorderWidth="1px" Columns="8" Font-Names="Consolas" MaxLength="8" Rows="8" 
                                    Width="65px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="campoCorrelativoFiscal_MaskedEditExtender" 
                                    runat="server" AutoCompleteValue="0" ErrorTooltipEnabled="True" 
                                    InputDirection="RightToLeft" Mask="99999999" MaskType="Number" 
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                                    OnInvalidCssClass="MaskedEditError" PromptCharacter="0" 
                                    TargetControlID="campoCorrelativoFiscal">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td style="padding: 0px">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelDocumentoSeleccionado" runat="server" Enabled="False" 
        Visible="False">
        <table cellpadding="3" cellspacing="0" width="100%" 
            style="margin-bottom: 10px;">
            <tr>
                <td width="50%">
                    &nbsp;</td>
                <td width="20%">
                    &nbsp;</td>
                <td width="30%">
                    <asp:Button ID="botonVerListaDocumentos" runat="server" BorderColor="DimGray" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Consolas" 
                        ForeColor="DimGray" Height="25px"
                        Text="Ver Lista de Documentos" Width="250px" 
                        onclick="botonVerListaDocumentos_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="padding: 0px">
                <div style="padding: 5px; border: 1px dotted #808080; ">
                    <table cellpadding="2" cellspacing="0" class="tableVistaPreviaDocumento">
                        <tr>
                            <td width="50%">
                                <p align="center" 
                                    style="color: #FF0000; font-weight: bolder; font-size: large;">
                                    *** PROFORMA ***
                                    <br />
                                    *** NO FISCAL ***</p>
                            </td>
                            <td width="50%" colspan="2">
                                <p>
                                    <asp:Label ID="LabelDSTipoDocumento" runat="server" Font-Bold="True" 
                                        Font-Size="Medium"></asp:Label>
                                </p>
                                <p>
                                    <strong>Número:
                                    <asp:Label ID="LabelDSNumeroFiscal" runat="server"></asp:Label>
                                    </strong>
                                    <br />
                                    Fecha y Hora:
                                    <asp:Label ID="LabelDSFechaHoraFiscal" runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <p>
                                    PAPELERA ISTMENA, S.A.</p>
                                <p align="left" style="color: #FF0000;">
                                    DIRECCIÓN/RUC ALMACENADO(A) EN LA IMPRESORA FISCAL</p>
                            </td>
                            <td colspan="2" width="50%">
                                <p align="left" style="color: #FF0000;">
                                    TELEFONO/URL WEB ALMACENADO(A) EN LA IMPRESORA FISCAL</p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="LabelEncabezadoL1" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="LabelEncabezadoL2" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="LabelEncabezadoL3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>
                                    <asp:Label ID="LabelNombreCliente" runat="server"></asp:Label>
                                    <br />
                                    RUC/CI:
                                    <asp:Label ID="LabelIDFiscalCliente" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="LabelDireccionCliente" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="LabelTelefonoCliente" runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px">
                                <asp:GridView ID="GridViewDSLineas" runat="server" AutoGenerateColumns="False" 
                                    BorderStyle="None" BorderWidth="0px" CellPadding="3" GridLines="None" 
                                    ShowHeader="False" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="CANTIDAD" DataFormatString="{0:N3}" 
                                            HeaderText="CANTIDAD">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Cod.">
                                            <ItemTemplate>
                                                Cod:
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CODIGO" HeaderText="CODIGO">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BASE" DataFormatString="{0:N4}" HeaderText="BASE">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="IMPUESTO">
                                            <ItemTemplate>
                                                <span class="style1">(*) </span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SUBTOTAL" DataFormatString="{0:N2}" 
                                            HeaderText="SUBTOTAL">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding: 0px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border-top-style: solid; border-top-width: 1px">
                                <b>Subtot.</b></td>
                            <td align="right" colspan="2" 
                                style="border-top-style: solid; border-top-width: 1px">
                                <b>
                                <asp:Label ID="LabelDSSubtotalItems" runat="server"></asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top-style: solid; border-top-width: 1px">
                                Impuesto</td>
                            <td align="right" style="border-top-style: solid; border-top-width: 1px">
                                BI</td>
                            <td align="right" style="border-top-style: solid; border-top-width: 1px">
                                Monto</td>
                        </tr>
                        <tr>
                            <td style="border-color: #696969; border-top-style: solid; border-top-width: 1px; color: #FF0000;">
                                (*) DISTRIBUCIÓN DE IMPUESTOS (GENERADO POR LA IMPRESORA FISCAL)</td>
                            <td align="right" 
                                
                                style="border-color: #696969; border-top-style: solid; border-top-width: 1px; color: #FF0000;">
                                BASE IMPONIBLE CALCULADO</td>
                            <td align="right" 
                                
                                style="border-color: #696969; border-top-style: solid; border-top-width: 1px; color: #FF0000;">
                                IMPUESTO CALCULADO</td>
                        </tr>
                        <tr>
                            <td style="border-top-style: solid; border-top-width: 1px">
                                Subtot.</td>
                            <td align="right" style="border-top-style: solid; border-top-width: 1px">
                                
                                <asp:Label ID="LabelDSSubtotaBaseImponible" runat="server"></asp:Label>
                                
                            </td>
                            <td align="right" style="border-top-style: solid; border-top-width: 1px">
                                <asp:Label ID="LabelDSSubtotaImpuestos" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" 
                                
                                style="border-top-style: solid; border-top-width: 1px; font-size: large; font-weight: bolder;">
                                TOTAL $</td>
                            <td style="border-top-style: solid; border-top-width: 1px" align="right">
                                <asp:Label ID="LabelDSTotal" runat="server" Font-Size="Large" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #FF0000">
                                
                                    FORMA DE PAGO (GENERADO POR LA IMPRESORA FISCAL)
                            </td>
                            <td align="right" style="color: #FF0000">
                                MONTO CALCULADO</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                
                                    SALDO
                            </td>
                            <td align="right" style="color: #FF0000">
                                SALDO CALCULADO</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border-top-style: solid; border-top-width: 1px">
                                <asp:Label ID="LabelPieL1" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="LabelPieL2" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="LabelPieL3" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="docSelTipo" runat="server" Value="0" />
        <asp:HiddenField ID="docSelPrefijo" runat="server" />
        <asp:HiddenField ID="docSelNumero" runat="server" Value="0" />
        <asp:SqlDataSource ID="SqlDataSourceDocSelEncabezado" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	SIHL01.*, RARL05.*
FROM
	SIHL01, RARL05
WHERE
	SIHL01.SIINVN = RARL05.RINVC AND
	SIHL01.IHDPFX = RARL05.ARDPFX AND
	SIHL01.IHDTYP = RARL05.ARDTYP AND
	SIHL01.IHDOCN = RARL05.ARDOCN AND
	(SIHL01.SIID = 'IH') AND	
	(SIHL01.IHDTYP = ?) AND
	(SIHL01.IHDPFX = ?) AND
	(SIHL01.IHDOCN = ?) AND
	(SIHL01.SICOMP = ?)">
            <SelectParameters>
                <asp:ControlParameter ControlID="docSelTipo" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" Size="1" />
                <asp:ControlParameter ControlID="docSelPrefijo" 
                    ConvertEmptyStringToNull="False" DbType="String" Name="?" PropertyName="Value" 
                    Size="2" />
                <asp:ControlParameter ControlID="docSelNumero" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" Size="8" />
                <asp:ControlParameter ControlID="campoSeleccionDeCompania" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                    PropertyName="SelectedValue" Size="2" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceDocSelLineas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	SILL02.*, IIML02.*
FROM
	{ oj SILL02 LEFT OUTER JOIN
	IIML02 ON SILL02.ILPROD = IIML02.IPROD }
WHERE
	(SILL02.ILID = 'IL') AND
	(SILL02.ILDTYP = ?) AND
	(SILL02.ILDPFX = ?) AND
	(SILL02.ILDOCN = ?) AND 
	(SILL02.ILCOMP = ?)
ORDER BY
	SILL02.ILLINE">
            <SelectParameters>
                <asp:ControlParameter ControlID="docSelTipo" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" Size="1" />
                <asp:ControlParameter ControlID="docSelPrefijo" 
                    ConvertEmptyStringToNull="False" DbType="String" Name="?" PropertyName="Value" 
                    Size="2" />
                <asp:ControlParameter ControlID="docSelNumero" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" Size="8" />
                <asp:ControlParameter ControlID="campoSeleccionDeCompania" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                    PropertyName="SelectedValue" Size="2" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceDocSelRARHistoria" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>">
        </asp:SqlDataSource>
        <asp:HiddenField ID="docSelTipoAsociado" runat="server" Value="0" />
        <asp:HiddenField ID="docSelPrefijoAsociado" runat="server" />
        <asp:HiddenField ID="docSelNumeroAsociado" runat="server" Value="0" />
        <asp:SqlDataSource ID="SqlDataSourceDocSelEncabezadoAsociado" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	SIHL01.*, RARL05.*
FROM
	SIHL01, RARL05
WHERE
	SIHL01.SIINVN = RARL05.RINVC AND SIHL01.IHDPFX = RARL05.ARDPFX AND SIHL01.IHDTYP = RARL05.ARDTYP AND
	(SIHL01.SIID = 'IH') AND	
	(SIHL01.IHDTYP = ?) AND
	(SIHL01.IHDPFX = ?) AND
	(SIHL01.IHDOCN = ?) AND
	(SIHL01.SICOMP = ?)">
            <SelectParameters>
                <asp:ControlParameter ControlID="docSelTipoAsociado" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" Size="1" />
                <asp:ControlParameter ControlID="docSelPrefijoAsociado" 
                    ConvertEmptyStringToNull="False" DbType="String" Name="?" PropertyName="Value" 
                    Size="2" />
                <asp:ControlParameter ControlID="docSelNumeroAsociado" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" Size="8" />
                <asp:ControlParameter ControlID="campoSeleccionDeCompania" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                    PropertyName="SelectedValue" Size="2" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:HiddenField ID="docSelCliente" runat="server" />
        <asp:SqlDataSource ID="SqlDataSourceDocSelCliente" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	RCML12.*
FROM
	RCML12
WHERE
	(CCUST = ?)">
            <SelectParameters>
                <asp:ControlParameter ControlID="docSelCliente" 
                    ConvertEmptyStringToNull="False" DbType="Decimal" DefaultValue="0" Name="?" 
                    PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:HiddenField ID="docSelClientePais" runat="server" />
        <asp:SqlDataSource ID="SqlDataSourceDocSelClientePais" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	CCCODE, CCDESC
FROM
	ZCCL03
WHERE
	(CCTABL = 'COUNTRY') AND
	(CCCODE = ?)">
            <SelectParameters>
                <asp:ControlParameter ControlID="docSelClientePais" 
                    ConvertEmptyStringToNull="False" DbType="String" DefaultValue="0" Name="?" 
                    PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceLibroImpuestos" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	ZRCL02.*
FROM
	ZRCL02"></asp:SqlDataSource>
        <asp:HiddenField ID="docSelPedido" runat="server" Value="0" />
        <asp:SqlDataSource ID="SqlDataSourceDocSelNotas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BILP500PAcs %>" 
            ProviderName="<%$ ConnectionStrings:BILP500PAcs.ProviderName %>" SelectCommand="SELECT
	SNSEQ, SNDESC
FROM
	ESNL01
WHERE
	(SNTYPE = 'O') AND
	(SNINV = 'Y') AND
	(SNCUST = ?)
ORDER BY
	SNSEQ">
            <SelectParameters>
                <asp:ControlParameter ControlID="docSelPedido" ConvertEmptyStringToNull="False" 
                    DbType="Decimal" DefaultValue="0" Name="?" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
    </asp:Content>
