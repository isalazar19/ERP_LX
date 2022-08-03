﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SFCP314C.aspx.cs" Inherits="ERP_LX.SFCP314C" %>

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
            <div class="mypanel_departamento">
                <asp:Panel runat="server" ID="panel" Style="text-align: left; margin-bottom: 0px;">
                    <fieldset class="my_fieldset_departamento">
                        <legend>
                            <asp:Label ID="lbl_panel" runat="server" Font-Bold="true" Text="Selección de Parámetros">
                            </asp:Label>
                        </legend>
                        <asp:Label runat="server" ID="lbl_fecha1" Text="Desde"></asp:Label>
                        <asp:TextBox runat="server" ID="txt_fecha1" Text="" Width="65px" OnTextChanged="txt_fecha1_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                        <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar"
                            OnClick="Image1_Click" />
                        <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txt_fecha1"
                            PopupButtonID="Image1" Format="dd/MM/yyyy" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fecha1"
                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                            ErrorTooltipEnabled="true"></asp:MaskedEditExtender>
                        <asp:Label runat="server" ID="lbl_fecha2" Text="Hasta"></asp:Label>&nbsp
                        <asp:TextBox runat="server" ID="txt_fecha2" Text="" Width="65px" OnTextChanged="txt_fecha2_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                        <asp:ImageButton runat="Server" ID="Image2" ImageUrl="~/imagenes/calendar.png" AlternateText="Click to show calendar"
                            OnClick="Image2_Click" />
                        <asp:CalendarExtender ID="calendarButtonExtender2" runat="server" TargetControlID="txt_fecha2"
                            PopupButtonID="Image2" Format="dd/MM/yyyy" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_fecha2"
                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" InputDirection="RightToLeft"
                            ErrorTooltipEnabled="true"></asp:MaskedEditExtender>
                        <asp:Label runat="server" ID="lbl_almacen" Text="Almacen"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_alm" AutoPostBack="true" OnSelectedIndexChanged="ddl_whs_Selection_Changed">
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lbl_seleccion" Text="Tipo Recepción"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_tipo" AutoPostBack="true" OnSelectedIndexChanged="ddl_tipo_Selection_Change"
                            Width="150px" ToolTip="Seleccione el Tipo de Recepción...">
                            <asp:ListItem Value="  "></asp:ListItem>
                            <asp:ListItem Value="R" Selected="True">Orden de Fabricacion</asp:ListItem>
                            <asp:ListItem Value="R1">Reproceso</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lbl_und" Text="Unidades"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddl_und" AutoPostBack="true" OnSelectedIndexChanged="ddl_und_Selection_Change"
                            Width="100px" ToolTip="Seleccione el Tipo de Unidades...">
                            <asp:ListItem Value="  "></asp:ListItem>
                            <asp:ListItem Selected="True" Value="P">Propias</asp:ListItem>
                            <asp:ListItem Value="S">Standard</asp:ListItem>
                            <asp:ListItem Value="U">Unificadas</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp
                        <asp:ImageButton ID="btn_buscar" runat="server" ImageUrl="~/imagenes/buscar.png"
                            Height="23px" Width="24px" ToolTip="Buscar..." OnClick="btn_buscar_Click" />
                        <br />
                        <%--&nbsp;&nbsp;&nbsp;&nbsp--%>
                        <%--<asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                            ControlToValidate="txt_fecha1" IsValidEmpty="false" MaximumValue="01/01/2099"
                            MinimumValue="01/01/2000" EmptyValueMessage="Enter Date Value" MaximumValueMessage="La Fecha debe ser menor a 01/01/2099"
                            InvalidValueBlurredMessage="Fecha Desde invalida" MinimumValueMessage="La Fecha debe ser mayor a 01/01/2000"
                            EmptyValueBlurredText="* Ingrese una Fecha Desde Valida" ToolTip="La Fecha debe ser entre 01/01/2000 a 01/01/2099"
                            ForeColor="Red">
                        </asp:MaskedEditValidator>--%>
                        <br />
                        <%--<asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                            ControlToValidate="txt_fecha2" IsValidEmpty="false" MaximumValue="01/01/2099"
                            MinimumValue="01/01/2000" EmptyValueMessage="Enter Date Value" MaximumValueMessage="La Fecha debe ser menor a 01/01/2099"
                            InvalidValueBlurredMessage="Fecha Hasta invalida" MinimumValueMessage="La Fecha debe ser mayor a 01/01/2000"
                            EmptyValueBlurredText="* Ingrese una Fecha Hasta Valida" ToolTip="La Fecha debe ser entre 01/01/2000 a 01/01/2099"
                            ForeColor="Red">
                        </asp:MaskedEditValidator>--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rb_resumido" AutoPostBack="True" GroupName="RadioGroup1" Text="Resumido"
                            TextAlign="Right" runat="server" ToolTip="Solo Centro de Fabricacion..." OnCheckedChanged="rb_resumido_CheckedChanged"
                            Checked="true" Visible="true" />
                        <asp:RadioButton ID="rb_detalle" AutoPostBack="True" GroupName="RadioGroup1" Text="Detallado"
                            TextAlign="Right" runat="server" ToolTip="Centro de Fabricacion y Productos..."
                            OnCheckedChanged="rb_detalle_CheckedChanged" Visible="true" />
                    </fieldset>
                </asp:Panel>
            </div>
            &nbsp;&nbsp;&nbsp
            <asp:ImageButton runat="server" ID="img_pdf" ImageUrl="~/imagenes/pdf.jpg" 
                Height="25px" Width="28px" onclick="img_pdf_Click" Visible="false" />
            <div class="centrar_GridView">
                <asp:Label runat="server" ID="lbl_titulo_consulta" BackColor="ButtonHighlight" Font-Bold="true"
                    Font-Size="Small"></asp:Label>
                <asp:GridView ID="gv_lista" runat="server" AllowPaging="True" AllowSorting="false"
                    AutoGenerateColumns="False" DataKeyNames="ctro" CellPadding="4" ForeColor="#333333"
                    GridLines="None" OnPageIndexChanging="gv_lista_Paginar" OnSelectedIndexChanged="gv_lista_SelectedIndexChanged"
                    Width="700px" PageSize="11" ShowFooter="True" OnRowDataBound="gv_lista_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/imagenes/palomita.png" />--%>
                        <%--<asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="DETALLE"
                            ItemStyle-Font-Size="Smaller" />--%>
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
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="No Clasif" DataField="turno0" SortExpression="turno0" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Total" DataField="total" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right">
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
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
            <div class="centrar_GridView">
                <div class="centrar_gridview">
                    <asp:GridView runat="server" ID="gv_detalle" AutoGenerateColumns="False" ForeColor="#333333"
                        GridLines="None" OnRowDataBound="FindTheGridAndBound" 
                        DataKeyNames="THWRKC" Width="700px"
                        PageSize="11" ShowFooter="True" CellPadding="4" CellSpacing="2">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField  HeaderText="Detalle por Centro de Fabricación">
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="lbl_header" Text="Centro de Fabricación: ">--%>
                                    <%#Eval("THWRKC")%> <%#Eval("WDESC")%>
                                    <%--</asp:Label>--%>
                                    <asp:GridView runat="server" ID="GridViewNested" EnableViewState="False" Width="700px"
                                        AutoGenerateColumns="False" GridLines="None" ShowFooter="True" OnRowDataBound="gv_GVN_RowDataBound"
                                        CssClass="EstiloDemo">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Producto" DataField="Producto" HeaderStyle-HorizontalAlign="Justify"
                                                ItemStyle-HorizontalAlign="Justify">
                                                <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" HeaderStyle-HorizontalAlign="Justify"
                                                ItemStyle-HorizontalAlign="Justify">
                                                <HeaderStyle HorizontalAlign="Justify"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Turno 3" DataField="Turno3" 
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" 
                                                DataFormatString="{0:N3}">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Turno 1" DataField="Turno1" 
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" 
                                                DataFormatString="{0:N3}">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Turno 2" DataField="Turno2" 
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" 
                                                DataFormatString="{0:N3}">
                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="No Clasif" DataField="Turno0" 
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" 
                                                DataFormatString="{0:N3}">
                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Total" HeaderStyle-HorizontalAlign="Center" 
                                                ItemStyle-HorizontalAlign="Right" DataField="total" DataFormatString="{0:N3}">
                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
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
                <br />
                <div class="Salir">
                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/imagenes/excel.png"
                        ToolTip="Publicar en Excel" ImageAlign="Right" 
                        OnClick="img_regresar_Click" Height="24px" Width="28px" />
                    <asp:Label runat="server" ID="Label1" Text="Seleccionar otro Ctro Fabricación"></asp:Label>&nbsp;
                </div>
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
