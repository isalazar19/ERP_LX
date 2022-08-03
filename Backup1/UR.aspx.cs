using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace MENU_ERPLX
{
    public partial class UR : System.Web.UI.Page
    {

        // ACCESO A LOS PROGRAMAS
        // Función de construcción de URL para INVOCACIÓN DE UN PROGRAMA
        private string urlDeAcceso(string programa)
        {
            string urlBaseApp = "http://app.extra.erp.regional.app.papeleslatinos.com/";
            return String.Format(urlBaseApp + "{0}", programa);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // control de la etiqueta del título de la página en sitemaster
            Label LabelMenuUR = (Label)Master.FindControl("LabelMenuUR");


            // MENÚ ERP EXTRA LX

            // definición de tabla
            DataTable dt = new DataTable();

            // definición de columnas
            DataColumn dcNUMERO = new DataColumn("NUMERO", typeof(string));
            DataColumn dcTITULO = new DataColumn("TITULO", typeof(string));
            DataColumn dcURL = new DataColumn("URL", typeof(string));
            DataColumn dcPROGRAMA = new DataColumn("PROGRAMA", typeof(string));

            dt.Columns.AddRange(new DataColumn[] { dcNUMERO, dcTITULO, dcURL, dcPROGRAMA });


            // UR Invocado con variable GET
            string ur = Request.QueryString["id"];


            //dt.Rows.Add(new object[] { "", "", "", "D1"});

            //dt.Rows.Add(new object[] { "", "", "", ""});

            //urlDeAcceso("")

            switch (ur)
            {
                // UR1 GERENCIA
                case "1":
                    this.Title = "Menú UR1 GERENCIA EXTRA LX";
                    LabelMenuUR.Text = "UR1 GERENCIA Extra LX";


                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Estadísticas de pedidos", "", "ORDP801" });
                    dt.Rows.Add(new object[] { "", "Inventario de productos", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Inventario de Prod.Terminados p/Despacho", "", "INVP391C" });
                    dt.Rows.Add(new object[] { "", "Inventario de Prod.Term.Almacenes Externos Zona GC.", "", "INVP392C" });
                    dt.Rows.Add(new object[] { "", "Inventario de Prod.Terminados Consolidado", "", "INVP393C" });
                    dt.Rows.Add(new object[] { "", "Analisis de Vencimiento de Cuentas por Cobrar", urlDeAcceso("ACRP393"), "ACRP393" });
                    dt.Rows.Add(new object[] { "", "Consulta de producción por Turno y Centro", urlDeAcceso("SFCP314C"), "SFCP314C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reporte Consolidado de Ventas", "", "INVP330C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Ventas Diarias ON-LINE", urlDeAcceso("BILP801"), "BILP801" });
                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });


                    break;
                // UR2 DISTRIBUCION Y VENTAS
                case "2":
                    this.Title = "Menú UR2 DISTRIBUCION Y VENTAS EXTRA LX";
                    LabelMenuUR.Text = "UR2 DISTRIBUCION Y VENTAS Extra LX";


                    dt.Rows.Add(new object[] { "", "MODULO GESTION DE PEDIDOS (ORD)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Impresión Nota de Carga", urlDeAcceso("ACRP99"), "ACRP99" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Movimiento Devoluciones", urlDeAcceso("ACRP122C"), "ACRP122C" });
                    dt.Rows.Add(new object[] { "", "Llegadas de Envio", urlDeAcceso("OLMP580"), "OLMP580" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Pedidos Depurados Automaticos", "", "ORDP053" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte Comparativo de BackOrder", "", "ORDP600BC" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Resumen de Pedidos Pendientes", urlDeAcceso("ORDP584BC"), "ORDP584BC" });
                    dt.Rows.Add(new object[] { "", "Reporte De Gestión De Pedidos", "", "BILP599CC" });
                    dt.Rows.Add(new object[] { "", "Reporte De Devoluciones", urlDeAcceso("ACRP135"), "ACRP135" });

                    dt.Rows.Add(new object[] { "", "MODULO DE FACTURACION Y VENTAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "EMISION DE DOCUMENTOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reimpresion de notas de entrega", "", "ACRP95C" });
                    dt.Rows.Add(new object[] { "", "Emisión de Documentos a Interfaz Fiscal", urlDeAcceso("BILP500PA"), "BILP500PA" });
                    dt.Rows.Add(new object[] { "", "Re-Impresion de Documentos Financieros (no afectan Inventarios) - PAPISA", urlDeAcceso("AACRP65PA"), "AACRP65PA" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Selección De Documentos De Ventas", urlDeAcceso("INVP597C"), "INVP597C" });
                    dt.Rows.Add(new object[] { "", "Selección De Facturas Por Clientes (Pago Bonific)", "", "BILP598C" });
                    dt.Rows.Add(new object[] { "", "Relación De Documentos Recibidos/No Recibidos Del Transporte", urlDeAcceso("BILP599C"), "BILP599C" });
                    //dt.Rows.Add(new object[] { "", "Relación De Documentos No Recibidos Del Transporte", urlDeAcceso("BILP599C"), "BILP599CB" });
                    dt.Rows.Add(new object[] { "", "Reporte Pedidos/Ventas & Facturacion Por Dep/Vend", urlDeAcceso("SILP301C"), "SILP301C" });
                    dt.Rows.Add(new object[] { "", "Reporte Despachos Realizados Por Planta", "", "INVP600C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE CUENTAS POR COBRAR (ACR)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte de Cobranzas", urlDeAcceso("ACRP609"), "ACRP609" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Maestra De Clientes Por Punto De Envío", urlDeAcceso("ACRP593C"), "ACRP600" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte De Clientes Activos", "", "ACRP580C" });
                    dt.Rows.Add(new object[] { "", "Relación De Documentos Al Cobro (Selec./Reporte)", "", "ACRP250C" });
                    dt.Rows.Add(new object[] { "", "Facturas Con Saldos y han Tenidos Movimientos", "", "SYSPC210" });
                    dt.Rows.Add(new object[] { "", "Analisis De Ventas vs Ctas. x Cobrar", "", "ACRP201C" });
                    dt.Rows.Add(new object[] { "", "Reporte NC y ND PP Procedente o No", "", "SYSPC155" });
                    dt.Rows.Add(new object[] { "", "Reporte de Puntos de Envio por Clte/Vend/Alm", urlDeAcceso("ACRP593C"), "ORDP100C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE INVENTARIO", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Menu de cierre de operaciones", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados resumen", "", "INV598BC" });
                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados detalle", "", "INV598DC" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento y Reporte de Cheques Devueltos Temporales", "", "CDPC100" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de carga vs confirmacion de despacho", "", "ORDP583C" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de entrega (Solo Planta)", "", "INVP596C" });
                    dt.Rows.Add(new object[] { "", "Reporte de transacciones de inventario", "", "CIEP906C" });
                    dt.Rows.Add(new object[] { "", "Reporte de devoluciones", urlDeAcceso("ACRP135"), "ACRP135" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta Existencia de Productos", urlDeAcceso("INVP390C"), "INVP390C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Impresión del Comprobante de Recepción de Material", urlDeAcceso("PURP312C"), "PURP312C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE PROMOCIONES(PRO)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "MODULO DE CAPTURA INFORMACION DEL MERCADO", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento Codigos de Fabricantes", "", "INVP108" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Tabla Articulos del Mercado", "", "INVP107" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Tabla Comentarios del Mercado", "", "INVP109" });
                    dt.Rows.Add(new object[] { "", "Actualizacion Nuevos Productos del Mercado", "", "INVP104" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento B.D Estadistica de Precios", "", "INVP112C" });

                    dt.Rows.Add(new object[] { "", "OPCIONES ADICIONALES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Formato Encuesta Paveca Ag.de retencion", "", "ORDP511C" });
                    dt.Rows.Add(new object[] { "", "Mantemiento Masivo Del Nro. De Control", "", "BILP110C" });


                    break;
                // UR3 PLANIFICACION DE PRODUCCION
                case "3":
                    this.Title = "Menú UR3 PLANIFICACION DE PRODUCCION EXTRA LX";
                    LabelMenuUR.Text = "UR3 PLANIFICACION DE PRODUCCION Extra LX";


                    dt.Rows.Add(new object[] { "", "MODULO DE INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "MANTENIMIENTOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte Factores para Consumo Químicos", "", "INV577CP" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Valores Consumo Peroxido", "", "INV532BP" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consumo para la elaboración de obsequios", "", "INV523CP" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte/Consulta Existencia Material por Almacén", "", "INVP305C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Movimiento de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });
                    dt.Rows.Add(new object[] { "", "Resumen de Inventario (Rango=Fechas y Productos)", urlDeAcceso("INVP310H"), "INVP310CP" });
                    dt.Rows.Add(new object[] { "", "Detalle de Movimiento de Inventario", urlDeAcceso("INVP318C"), "INVP312C" });
                    dt.Rows.Add(new object[] { "", "Reporte de transacciones(mvto) para costos", "", "CIEP905C" });
                    dt.Rows.Add(new object[] { "", "Detalle de Fallas de Transaciones", urlDeAcceso("INVP316C"), "INVP316C" });
                    dt.Rows.Add(new object[] { "", "Reporte Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte de mediciones tanques de químicos", "", "INV590CP" });
                    dt.Rows.Add(new object[] { "", "Inventario de Material de Empaque e Insumos", "", "INVP211C" });
                    dt.Rows.Add(new object[] { "", "Reporte consolidado de control de lotes", urlDeAcceso("SFCP305C"), "SFCP308C" });
                    dt.Rows.Add(new object[] { "", "Movimiento de inventario por centro de costo", urlDeAcceso("INVP318C"), "INVP314C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE COMPRAS(PUR)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Imprime Comprobantes Recepción de Materiales", urlDeAcceso("PURP312C"), "PURP312C" });
                    // dt.Rows.Add(new object[] { "", "Activa O/C cerrada/lines(s) totalmente recibida(s)", urlDeAcceso("PURP902C"), "PURP902C" });
                    // dt.Rows.Add(new object[] { "", "Cierre manual de la orden de compra", urlDeAcceso("PURP901C"), "PURP901C" });
                    dt.Rows.Add(new object[] { "", "Lista Alfabética de Proveedores", "", "PURP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes de Compra", urlDeAcceso("PURP300C"), "PURP300C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE SFC Y BOM", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Registro de Transaciones por Orden de Fabricación", "", "SFCP102C" });
                    dt.Rows.Add(new object[] { "", "Restaurar Orden de fabricación en uso", urlDeAcceso("SFCP108"), "SFCP108" });
                    dt.Rows.Add(new object[] { "", "Activa Orden de fabricación Inactiva", "", "SFCP101C" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de Producción por Turno y Centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Consulta de Broke en Linea y Peso Real", "", "SFCP337" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Listado de Materiales", "", "BOMP200C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Requerimientos de Materiales", "", "MRPP300C" });
                    dt.Rows.Add(new object[] { "", "Listado de ordenes de fabricacion valoradas", urlDeAcceso("CSTP305C"), "CSTP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes de Fabricación", "", "SFCP300C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes Abiertas para Planificación", "", "SFCP310C" });
                    dt.Rows.Add(new object[] { "", "Resumen de desviacion de costo de orden/Fabricac.", urlDeAcceso("CSTP310C"), "CSTP310C" });
                    dt.Rows.Add(new object[] { "", "Reporte semanal de cierre de ODF", urlDeAcceso("CSTP312C"), "CSTP312C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Consumo Real Vs. Standard", "", "SFCP317C" });
                    dt.Rows.Add(new object[] { "", "Reporte Horas x centro de trabajo y ODF", "", "SFCP303C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Producción diaria por producto", "", "BILP502CA" });
                    dt.Rows.Add(new object[] { "", "Reporte de Shinkrage en Molinos", urlDeAcceso("SFCP320C"), "SFCP320C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Producción Total Anual", "", "INVP308C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE VENTAS Y PEDIDOS (BIL/ORD)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Consulta estadística de Pedidos y Back Log", "", "ORDP801" });


                    break;
                // UR4 LOGISTICA DE DESPACHO
                case "4":
                    this.Title = "Menú UR4 LOGISTICA DE DESPACHO EXTRA LX";
                    LabelMenuUR.Text = "UR4 LOGISTICA DE DESPACHO Extra LX";


                    dt.Rows.Add(new object[] { "", "MODULO DE GESTION DE PEDIDOS (ORD)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta estadistica de pedidos", "", "ORDP801" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Resumen de Pedidos Pendientes", urlDeAcceso("ORDP584BC"), "ORDP584BC" });
                    dt.Rows.Add(new object[] { "", "Reporte Consolidado Notas de Entrega por Nro.Placa", "", "ORDP110C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Productos Dejados de Despachar", "", "ORDP587C" });
                    dt.Rows.Add(new object[] { "", "Analisis de movimiento ( Inv / Fac / CxC)", urlDeAcceso("SYSPC199"), "SYSPC199" });

                    dt.Rows.Add(new object[] { "", "MODULO DE FACTURACION", "", "D1" });

                    dt.Rows.Add(new object[] { "", "EMISION DE DOCUMENTOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reimpresion de notas de entrega", "", "ACRP95C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Relación De Documentos Recibidos/No Recibidos Del Transporte", urlDeAcceso("BILP599C"), "BILP599C" });
                    //dt.Rows.Add(new object[] { "", "Relacion de documentos no recibidos del transporte", urlDeAcceso("BILP599C"), "BILP599CB" });
                    dt.Rows.Add(new object[] { "", "Reporte Consolidado De Ventas", "", "INVP330C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });
	
                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados resumen", "", "INV598BC" });
                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados detalle", "", "INV598DC" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento y Reporte de Cheques Devueltos Temporales", "", "CDPC100" });
                    dt.Rows.Add(new object[] { "", "Relacion de documentos", "", "INV596FC" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de carga vs confirmacion de despacho", "", "ORDP583C" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de entrega (Solo Planta)", "", "INVP596C" });
                    dt.Rows.Add(new object[] { "", "Reporte de transacciones de inventario", "", "CIEP906C" });
                    dt.Rows.Add(new object[] { "", "Reporte de devoluciones", urlDeAcceso("ACRP135"), "ACRP135" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de Existencias de productos", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados p/Despacho", "", "INVP391C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Term. ALM EXT. Z-GC", "", "INVP392C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Term. Consolidado", "", "INVP393C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Resumen de Movimiento de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });

                    dt.Rows.Add(new object[] { "", "MODULO DE CONTROL DE ACCESO DE VEHICULOS A PLANTA", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Sistema Control Acceso Vehiculo a Planta", "", "CAVP900C" });

                    dt.Rows.Add(new object[] { "", "OTRAS OPCIONES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento Pedidos Depurados En Planta", "", "ORDP053" });

                    dt.Rows.Add(new object[] { "", "MODULO DE COMPRAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Imprime Notas De Recepción De Materia Prima", "", "PURP316C" });
                    dt.Rows.Add(new object[] { "", "Imprime comprobante de recepcion de materiales", urlDeAcceso("PURP312C"), "PURP312C" });
                    // dt.Rows.Add(new object[] { "", "Cierre Manual De La Orden De Compra", urlDeAcceso("PURP901C"), "PURP901C" });
                    // dt.Rows.Add(new object[] { "", "Activa O/C Cerrada/Lines(s)Totalmente Recibida(s)", urlDeAcceso("PURP902C"), "PURP902C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte De Recepciones Por Producto", "", "PURP318C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Recepciones Por Proveedor", "", "PURP319C" });
                    dt.Rows.Add(new object[] { "", "Reporte Detallado Notas De Entrega Por Proveedor", "", "PURP320C" });
                    dt.Rows.Add(new object[] { "", "Cuadre De Recepción vs Conciliación", "", "CSTP632C" });
                    dt.Rows.Add(new object[] { "", "Reporte O/Compra Por Tipo Moneda", "", "PURP302C" });
                    dt.Rows.Add(new object[] { "", "Reporte Ordenes De Compra", urlDeAcceso("PURP300C"), "PURP300C" });
                    dt.Rows.Add(new object[] { "", "Imprime informe de contrato", "", "PURP310C" });
                    dt.Rows.Add(new object[] { "", "Informe De Recepciones", urlDeAcceso("PURP314C"), "PURP314C" });

                    dt.Rows.Add(new object[] { "", "FACTURACIÓN 2 (BIL)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento Masivo Del Número De Control", "", "BILP110C" });


                    break;
                // UR5 COSTOS
                case "5":
                    this.Title = "Menú UR5 COSTOS EXTRA LX";
                    LabelMenuUR.Text = "UR5 COSTOS Extra LX";


                    dt.Rows.Add(new object[] { "", "MANTENIMIENTO INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento tabla de equivalencia transacciones costos-bpcs", urlDeAcceso("CIEPR5"), "CIEPR5" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento tabla de tipo por clase", urlDeAcceso("CSTP20"), "CSTP20" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento de Agrupación de almacenes", "", "CSTP25" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Registro de Transaciones por Orden de Fabricación", "", "SFCP102C" });
                    dt.Rows.Add(new object[] { "", "Consumo para elaboración de obsequios", "", "INV523CP" });
                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados resumen", "", "INV598BC" });
                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados detalle", "", "INV598DC" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento y Reporte de Cheques Devueltos Temporales", "", "CDPC100" });
                    dt.Rows.Add(new object[] { "", "Relacion de documentos", "", "INV596FC" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de carga vs confirmacion de despacho", "", "ORDP583C" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de entrega (Solo Planta)", "", "INVP596C" });
                    dt.Rows.Add(new object[] { "", "Reporte de transacciones de inventario", "", "CIEP906C" });
                    dt.Rows.Add(new object[] { "", "Reporte de devoluciones", urlDeAcceso("ACRP135"), "ACRP135" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de Inventario de Prod.Terminados p/Despacho", "", "INVP391C" });
                    dt.Rows.Add(new object[] { "", "Consulta de Inventario Prod.Term.Almacenes Externos Zona GC", "", "INVP392C" });
                    dt.Rows.Add(new object[] { "", "Consulta de Inventario Prod.Terminados Consolidado", "", "INVP393C" });
                    dt.Rows.Add(new object[] { "", "Consulta estadistica de pedidos y back log", "", "ORDP801" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Resumen de Movimiento de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });
                    dt.Rows.Add(new object[] { "", "Resumen de Inventario (Rango= fechas y productos)", urlDeAcceso("INVP310H"), "INVP310CP" });
                    dt.Rows.Add(new object[] { "", "Reporte de transacciones (mvmto.) para COSTOS", "", "CIEP905C" });
                    dt.Rows.Add(new object[] { "", "Detalle de movimiento de inventario", urlDeAcceso("INVP318C"), "INVP312C" });
                    dt.Rows.Add(new object[] { "", "Detalle de Fallas de Transacciones", urlDeAcceso("INVP316C"), "INVP316C" });
                    dt.Rows.Add(new object[] { "", "Reporte Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte consolidado de control de lotes", urlDeAcceso("SFCP305C"), "SFCP308C" });
                    dt.Rows.Add(new object[] { "", "Analisis de existencia de inventario de MP por antigüedad", "", "INVP335C" });
                    dt.Rows.Add(new object[] { "", "Inventarios Valorados", urlDeAcceso("INVP318C"), "INVPC325" });
                    dt.Rows.Add(new object[] { "", "Cuadre de Recepción Vs Conciliación", urlDeAcceso("CSTP630C"), "CSTP630C" });
                    dt.Rows.Add(new object[] { "", "Mant. Tabla de Descripcion de Grupos", "", "CSTP38" });
                    dt.Rows.Add(new object[] { "", "Mant. Tabla de Grupos de Productos", "", "CSTP40" });
                    dt.Rows.Add(new object[] { "", "Mant. Tabla de Textos de Lineas", "", "CSTP30" });
                    dt.Rows.Add(new object[] { "", "Mant. Tabla de Contenidos en Lineas", "", "CSTP35" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento tabla de grupo para individuales", "", "CSTP4A" });

                    dt.Rows.Add(new object[] { "", "MODULO DE MANUFACTURING (MFG)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Tabla de Cuentas Asociadas a Columnas", "", "CSTP701" });
                    dt.Rows.Add(new object[] { "", "Tabla de Valores Asociadas a Columnas", "", "CSTP703" });
                    dt.Rows.Add(new object[] { "", "Tabla de Clases  Asociadas a Columnas", "", "CSTP705" });
                    dt.Rows.Add(new object[] { "", "Tabla de Programas y Columnas", "", "CSTP707" });
                    dt.Rows.Add(new object[] { "", "Tabla de Categorias y Clase/Seg. Costo Asociado", "", "CSTP712" });
                    dt.Rows.Add(new object[] { "", "Tabla de Codigo Causa Asociados al Reporte", "", "CSTP714" });
                    dt.Rows.Add(new object[] { "", "Tabla de Reportes de Costo a Generar", "", "CSTP716" });
                    dt.Rows.Add(new object[] { "", "Generacion de Reportes (Todos)", "", "CSTP751C" });
                    dt.Rows.Add(new object[] { "", "Mant. Statement Of Cost Of Mills Production", "", "CSTP718" });
                    dt.Rows.Add(new object[] { "", "Mant. Statement Of Cost Of Converting Production", "", "CSTP719" });
                    dt.Rows.Add(new object[] { "", "Generacion de Reportes (Por Seleccion)", "", "CSTP755" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS DE PLANTA", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de Producción por Turno y Centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Activa orden de fabricacion INACTIVA", "", "SFCP101C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Listado de Materiales BOM Formula", "", "BOMP200C" });
                    dt.Rows.Add(new object[] { "", "Reporte de cierre semanal de ODF", urlDeAcceso("CSTP312C"), "CSTP312C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Consumo Real Vs. Standard", "", "SFCP317C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Shinkrage en Molinos", urlDeAcceso("SFCP320C"), "SFCP320C" });
                    dt.Rows.Add(new object[] { "", "Reporte Horas x centro de trabajo y orden de fabricación", "", "SFCP303C" });
                    dt.Rows.Add(new object[] { "", "Listado de ordenes de fabricacion valoradas", urlDeAcceso("CSTP305C"), "CSTP305C" });
                    dt.Rows.Add(new object[] { "", "Resumen de desviacion de costo de orden/Fabricac.", urlDeAcceso("CSTP310C"), "CSTP310C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Individuales por Almacén", urlDeAcceso("CSTP308C"), "CSTP308C" });
                    // dt.Rows.Add(new object[] { "", "Individuales de PT", "", "CSTP308C" });
                    dt.Rows.Add(new object[] { "", "Ordenes de fabricación sin registro de broke (DV)", "", "CSTP257C" });

                    dt.Rows.Add(new object[] { "", "OTRAS OPCIONES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Movimiento de Inventario por centro de costo", urlDeAcceso("INVP318C"), "INVP314C" });

                    dt.Rows.Add(new object[] { "", "**CONCILIACION DE INV/CONTABLES VS CONTABILIDAD**", "", "D2" });

                    dt.Rows.Add(new object[] { "", "MANTENIMIENTO DE TABLA DE CUENTAS X INVENTARIO", "", "CSTP080" });
                    dt.Rows.Add(new object[] { "", "GENERA ARCHIVO DE CONTABILIDAD P/CONCILIAR", "", "CEAPR50C" });
                    dt.Rows.Add(new object[] { "", "Ordenes De Fabricación Valoradas (Costo Set)", "", "CSTP505C" });
                    dt.Rows.Add(new object[] { "", "Resumen Desviación Costo Orden/Fabricac. (Cst.Set)", "", "CSTP510C" });
                    dt.Rows.Add(new object[] { "", "Listado De Movimiento De Cuentas", urlDeAcceso("CEAPR05C"), "CEAPR05C" });
                    dt.Rows.Add(new object[] { "", "Resumen De Costo Por Producto Con % De Variación", "", "CSTP315C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Comprobantes", "", "CEAPR10C" });
                    dt.Rows.Add(new object[] { "", "Actualiza Saldo Inicial ITHP97 - Temporal", "", "INVP123C" });
                    dt.Rows.Add(new object[] { "", "Reportes De O/C", urlDeAcceso("PURP300C"), "PURP300C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones Bienes Economicos", urlDeAcceso("INVP318C"), "INVP318C" });
                    dt.Rows.Add(new object[] { "", "Transacciones valoradas archivo historico (YTH)", "", "INVP319C" });
                    dt.Rows.Add(new object[] { "", "Revalorización de Costo Promedio Mensual", urlDeAcceso("CSTP517"), "CSTP517" });
                    dt.Rows.Add(new object[] { "", "Generacion de Mov. Mensual Inventarios de Costos", "", "INVP205C" });


                    break;
                // UR6 LOGISTICA DE PRODUCTOS TERMINADOS
                case "6":
                    this.Title = "Menú UR6 LOGISTICA DE PRODUCTOS TERMINADOS EXTRA LX";
                    LabelMenuUR.Text = "UR6 LOGISTICA DE PRODUCTOS TERMINADOS Extra LX";


                    dt.Rows.Add(new object[] { "", "MODULO DE INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "MANTENIMIENTOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento Peso Caja Conversión", urlDeAcceso("ORDP101"), "ORDP101" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Comentarios Peso Paleta", "", "ORDP308" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento de Clientes especiales Control palet", "", "ACRP55" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Masivo Del Número De Control", "", "BILP110C" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados resumen", "", "INV598BC" });
                    dt.Rows.Add(new object[] { "", "Movimiento de transacciones productos terminados detalle", "", "INV598DC" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento y Reporte de Cheques Devueltos Temporales", "", "CDPC100" });
                    dt.Rows.Add(new object[] { "", "Relacion de documentos", "", "INV596FC" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de carga vs confirmacion de despacho", "", "ORDP583C" });
                    dt.Rows.Add(new object[] { "", "Relacion de notas de entrega (Solo Planta)", "", "INVP596C" });
                    dt.Rows.Add(new object[] { "", "Reporte de transacciones de inventario", "", "CIEP906C" });
                    dt.Rows.Add(new object[] { "", "Reporte de devoluciones", urlDeAcceso("ACRP135"), "ACRP135" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta existencia de productos", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados p/Despacho", "", "INVP391C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Term. ALM EXT. Z-GC", "", "INVP392C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Term. Consolidado", "", "INVP393C" });
                    dt.Rows.Add(new object[] { "", "Consulta inventario de Prod.Terminados con PESOS", "", "INVP300P" });
                    dt.Rows.Add(new object[] { "", "Consulta de Inventarios BPCS", "", "INVP800" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Detalle de Fallas de Transaciones", urlDeAcceso("INVP316C"), "INVP316C" });
                    dt.Rows.Add(new object[] { "", "Reporte Resumen de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });
                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte de ordenes abiertas para planificación", "", "SFCP310C" });
                    dt.Rows.Add(new object[] { "", "Reporte consolidado de Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP308C" });
                    dt.Rows.Add(new object[] { "", "Listado de Mínimo y Máximo(MODS DESP-CONSUMO)", "", "ACRP110C" });
                    dt.Rows.Add(new object[] { "", "Analisis De Movimiento", urlDeAcceso("SYSPC199"), "SYSPC199" });

                    dt.Rows.Add(new object[] { "", "MODULO GESTION DE PEDIDOS Y RMA (ORD)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento de pedidos depurados en Planta", "", "ORDP053" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta Estadistica De Pedidos y Back Log", "", "ORDP801" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Resumen de Pedidos Pendientes", urlDeAcceso("ORDP584BC"), "ORDP584BC" });
                    dt.Rows.Add(new object[] { "", "Reporte Consolidado Notas De Entrega Por Nro.Placa", "", "ORDP110C" });
                    dt.Rows.Add(new object[] { "", "Analisis de movimiento ( Inv / Fac / CxC)", urlDeAcceso("SYSPC199"), "SYSPC199" });

                    dt.Rows.Add(new object[] { "", "MODULO  DE FACTURACION Y VENTAS (BIL)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "EMISION DE DOCUMENTOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reimpresión De Notas De Entrega(Modificar no p pl)", "", "ACRP95C" });
                    dt.Rows.Add(new object[] { "", "Impresión/Reimpresión Notas De Entrega De Paletas", "", "ACRP80" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento De Clientes o Transportes Paletas", "", "PAPR01" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Selección De Documentos De Ventas", urlDeAcceso("INVP597C"), "INVP597C" });
                    dt.Rows.Add(new object[] { "", "Reimpresión de Documentos que afectan inventario", "", "ACRP90C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE COMPRAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Cuadre De Recepciones Vs. Conciliaciones", "", "CSTP632C" });
                    // dt.Rows.Add(new object[] { "", "Activa O/C Cerrada/Lineas(s)Totalmente Recibida(s)", urlDeAcceso("PURP902C"), "PURP902C" });

                    dt.Rows.Add(new object[] { "", "GEST.DE DATOS DE FABRIC.Y CONTROL DE PLANT(BOM/SFC)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Consulta De Producción Por Turno y Centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Listado De Ordenes De Fabricación Valoradas", urlDeAcceso("CSTP305C"), "CSTP305C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE CONTROL DE ACCESO DE VEHICULOS A PLANTA", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Sistema Control Acceso Vehículos a Planta", "", "CAVP900C" });


                    break;
                // UR7 COMPRAS
                case "7":
                    this.Title = "Menú UR7 COMPRAS EXTRA LX";
                    LabelMenuUR.Text = "UR7 COMPRAS Extra LX";


                    dt.Rows.Add(new object[] { "", "Procesos de Recepción", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Imprime Comprobante de Recepción", urlDeAcceso("PURP312C"), "PURP312C" });

                    dt.Rows.Add(new object[] { "", "Listado de Archivos", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Lista alfabética de proveedores", "", "PURP305D" });
                    dt.Rows.Add(new object[] { "", "Bienes economicos de Paveca", "", "ORDP518C" });

                    dt.Rows.Add(new object[] { "", "Reportes", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reporte de O/C", urlDeAcceso("PURP300C"), "PURP300C" });
                    dt.Rows.Add(new object[] { "", "Status de Contratos", urlDeAcceso("PURP313C"), "PURP313C" });

                    dt.Rows.Add(new object[] { "", "Otros", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Cierre Manual de la Orden de Compra", urlDeAcceso("PURP901C"), "PURP901C" });
                    dt.Rows.Add(new object[] { "", "Activa O/C Cerrada/Lines(s)Totalmente Recibida(s)", urlDeAcceso("PURP902C"), "PURP902C" });
                    dt.Rows.Add(new object[] { "", "Sistema Control Acceso Vehículo a Planta", "", "CAVP900C" });
                    dt.Rows.Add(new object[] { "", "Consulta De Requisiciones", urlDeAcceso("PURP326"), "PURP326" });
                    dt.Rows.Add(new object[] { "", "Reporte De Inventario Por Centro De Costo", urlDeAcceso("INVP318C"), "INVP314C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });
                    dt.Rows.Add(new object[] { "", "Consulta de Produccion por Turno y Centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Listado de Movimiento de Cuentas", urlDeAcceso("CEAPR05C"), "CEAPR05C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Control Requisiciones de Compra", urlDeAcceso("PURP324C"), "PURP324" });
                    dt.Rows.Add(new object[] { "", "Reporte de Seguimiento de Compras", urlDeAcceso("PURP328C"), "PURP328" });
                    dt.Rows.Add(new object[] { "", "Desbloqueo de Ordenes en Uso", "", "PURP503" });


                    break;
                // UR8 CONTABILIDAD
                case "8":
                    this.Title = "Menú UR8 CONTABILIDAD EXTRA LX";
                    LabelMenuUR.Text = "UR8 CONTABILIDAD Extra LX";


                    dt.Rows.Add(new object[] { "", "MANTENIMIENTOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento de tabla cuentas de balance", "", "CEAPR40" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento de tabla titulos de balance", "", "CEAPR25" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento de tabla subtitulos de balance", "", "CEAPR30" });
                    dt.Rows.Add(new object[] { "", "Detalle Contabilidad con Antiguedad de Saldos", "", "CEAPR40C" });

                    dt.Rows.Add(new object[] { "", "PROCESO DE FLETES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Comprobantes de Movimientos Bancarios", "", "BANP100C" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Masivo de Numeros de Control", "", "BILP110C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reporte Ordenes De Compra", urlDeAcceso("PURP300C"), "PURP300C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Transacciones", "", "CIEP905C" });
                    dt.Rows.Add(new object[] { "", "Reporte Ventas Por Sucursal y Clase Articulo", "", "SILP303C" });
                    dt.Rows.Add(new object[] { "", "Reporte Variación en Compras", "", "CSTP365C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Antiguedad de Saldos de ACP", "", "ACPP340C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Comprobantes", "", "CEAPR10C" });
                    dt.Rows.Add(new object[] { "", "Reporte Comparativo ACP vs. Impuesto", "", "ACPPR05C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Cuentas que Afectan a Proveedores", "", "CEAPR20C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });
                    dt.Rows.Add(new object[] { "", "Reporte Cuentas de Anticipos", "", "CEAPR05AC" });
                    dt.Rows.Add(new object[] { "", "Reporte de O/C en Transitos", "", "CEAPR30C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Pasivos Transitorios", "", "CEAPR35C" });
                    dt.Rows.Add(new object[] { "", "Cuadre Recepciones vs Conciliaciones en Und/Valor", urlDeAcceso("CSTP630C"), "CSTP630C" });

                    dt.Rows.Add(new object[] { "", "CONTROL CHEQUES EMITIDOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Menu Principal Cheques Emitidos", "", "CEAP001C" });
                    dt.Rows.Add(new object[] { "", "Reporte de O/C Recibidas Seg.Tipo Prov y Fecha", "", "ACPP400C" });

                    dt.Rows.Add(new object[] { "", "CONTROL ACCESO DE VEHICULOS A PLANTA (SCAV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Sistema Control Acceso Vehículo a Planta", "", "CAVP900C" });

                    dt.Rows.Add(new object[] { "", "OPCIONES ADICIONALES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Control Correlativo de Comprobantes x Usuarios", "", "CEAPR25C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Facturas Vencidas X Proveedor", "", "ACPP375C" });

                    dt.Rows.Add(new object[] { "", "CLD Libro Mayor configurable", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento de datos de transacciones", "", "ACPPR50" });

                    dt.Rows.Add(new object[] { "", "LIBROS CONTABLES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Declaración de Compras y Bienes DGI PAPISA", urlDeAcceso("AACPP350PA"), "AACPP350PA" });
                    dt.Rows.Add(new object[] { "", "Reporte de Facturas Consumidor Final PAPISA", urlDeAcceso("ABILP306PA"), "ABILP306PA" });


                    break;
                // UR9 PRODUCCION
                case "9":
                    this.Title = "Menú UR9 PRODUCCION EXTRA LX";
                    LabelMenuUR.Text = "UR9 PRODUCCION Extra LX";


                    dt.Rows.Add(new object[] { "", "MODULO DE INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte/Consulta Existencia Material por Almacén", "", "INVP305C" });
                    dt.Rows.Add(new object[] { "", "Existencias de productos", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados p/Despacho", "", "INVP391C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Term.Almacenes Externos Zona GC", "", "INVP392C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados Consolidado", "", "INVP393C" });
                    dt.Rows.Add(new object[] { "", "Inventario de Productos Terminados con peso", "", "INVP300P" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Resumen de Movimiento de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });
                    dt.Rows.Add(new object[] { "", "Detalle de Movimiento de Inventario", urlDeAcceso("INVP318C"), "INVP312C" });
                    dt.Rows.Add(new object[] { "", "Detalle de Fallas de Transaciones", urlDeAcceso("INVP316C"), "INVP316C" });
                    dt.Rows.Add(new object[] { "", "Reporte Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Inventario de Material de Empaque e Insumos", "", "INVP211C" });
                    dt.Rows.Add(new object[] { "", "Reporte Consolidado de Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP308C" });
                    dt.Rows.Add(new object[] { "", "Movimiento de Inventari por Centro de Costos", urlDeAcceso("INVP318C"), "INVP314C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE COMPRAS(PUR)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Lista Alfabética de Proveedores", "", "PURP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes de Compra", urlDeAcceso("PURP300C"), "PURP300C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE SFC Y BOM", "", "D1" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de Producción por Turno y Centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Consulta de Broke en Linea y Peso Real", "", "SFCP337" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Listado de ordenes de fabricacion valoradas", urlDeAcceso("CSTP305C"), "CSTP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes de Fabricación", "", "SFCP300C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes Abiertas para Planificación", "", "SFCP310C" });
                    dt.Rows.Add(new object[] { "", "Resumen de desviacion de costo de orden/Fabricac", urlDeAcceso("CSTP310C"), "CSTP310C" });
                    dt.Rows.Add(new object[] { "", "Reporte Horas x centro de trabajo y ODF", "", "SFCP303C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Shinkrage en Molinos", urlDeAcceso("SFCP320C"), "SFCP320C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE VENTAS Y PEDIDOS(BIL/ORD)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Consulta Estadística de pedidos y Back Log", "", "ORDP801" });
                    dt.Rows.Add(new object[] { "", "Resumen de Pedidos Pendientes", urlDeAcceso("ORDP584BC"), "ORDP584BC" });

                    dt.Rows.Add(new object[] { "", "OTRAS OPCIONES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Listado de Movimiento de Cuentas", urlDeAcceso("CEAPR05C"), "CEAPR05C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });
                    dt.Rows.Add(new object[] { "", "Registro De Transaciones Por Orden De Fabricación", "", "SFCP102C" });


                    break;
                // UR10 CREDITO Y COBRANZAS
                case "10":
                    this.Title = "Menú UR10 CREDITO Y COBRANZAS EXTRA LX";
                    LabelMenuUR.Text = "UR10 CREDITO Y COBRANZAS Extra LX";


                    dt.Rows.Add(new object[] { "", "MANTENIMIENTOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento de Tlf, fax e E-mail en clientes", "", "ACRPRCM" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Generación de N/C por Convenio", "", "SYSPC110D" });
                    dt.Rows.Add(new object[] { "", "Reporte y Gen/Reg. NC y ND PP. Procedente o no 2 y 4 %", "", "SYSPC125" });
                    dt.Rows.Add(new object[] { "", "Generación de Documentos por Saldos Mínimos", urlDeAcceso("ACRP500"), "ACRP500" });
                    dt.Rows.Add(new object[] { "", "Correccion de saldos/secuencia/prefijo/tipo en RAR", "", "ACRP007" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Consulta Por Documentos Cuentas x Cobrar", urlDeAcceso("ACRP399"), "ACRP399" });
                    dt.Rows.Add(new object[] { "", "Consulta Análisis de vencimiento de Ctas X Cob", urlDeAcceso("ACRP393"), "ACRP393" });
                    dt.Rows.Add(new object[] { "", "Consulta de Inventarios BPCS", "", "INVP800" });
                    dt.Rows.Add(new object[] { "", "Proyeccion de Cobranza X Semana", "", "ACRP195C" });
                    dt.Rows.Add(new object[] { "", "Movimientos Cuentas x Cobrar", urlDeAcceso("ACRP600"), "ACRP600" });


                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reporte de clientes (seleccion x lim. credito)", "", "ACRP593C" });
                    dt.Rows.Add(new object[] { "", "Estados de cuentas", urlDeAcceso("ACRP598"), "ACRP598" });
                    dt.Rows.Add(new object[] { "", "Facturas con saldos y han tenidos movimientos", "", "SYSPC210" });
                    dt.Rows.Add(new object[] { "", "Reporte de Maestra de Clientes", urlDeAcceso("ACRP593C"), "ACRP593C" });
                    dt.Rows.Add(new object[] { "", "Analisis de Antigüedad", urlDeAcceso("ACRP597"), "ACRP597" });
                    dt.Rows.Add(new object[] { "", "Analisis resumen de antiguedad por deposito individual", "", "ACR597A" });
                    dt.Rows.Add(new object[] { "", "Analisis detallado de antiguedad por deposito individual", "", "ACR597B" });
                    dt.Rows.Add(new object[] { "", "Analisis detallado antiguedad por deposito, vendedor, cliente", "", "ACR597D" });
                    dt.Rows.Add(new object[] { "", "Analisis resumem de antiguedad general", "", "ACR597E" });
                    dt.Rows.Add(new object[] { "", "Analisis detallado de antiguedad general", "", "ACR597F" });
                    dt.Rows.Add(new object[] { "", "Balance de CxC por Ant. de saldo Vendedor-Cliente", "", "ACR597G" });
                    dt.Rows.Add(new object[] { "", "Balance de CxC por tipo de cliente", "", "ACR597J" });
                    dt.Rows.Add(new object[] { "", "Balance de CxC ( Facturas sim movimientos )", "", "ACR597I" });
                    dt.Rows.Add(new object[] { "", "Reporte chequeo Puntos Envío Clientes Activos", "", "SYSP905C" });

                    dt.Rows.Add(new object[] { "", "Otros reportes de Cuentas por Cobrar", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Analisis del limite de credito", "", "ACRP400" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ventas y Cuentas x Cobrar", "", "ACRP200C" });
                    dt.Rows.Add(new object[] { "", "Selecciòn de Facturas por cliente", "", "BILP598C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Cobranza", urlDeAcceso("ACRP609"), "ACRP609" });
                    dt.Rows.Add(new object[] { "", "Reporte NC y ND PP Procedente o No", "", "SYSPC155" });
                    dt.Rows.Add(new object[] { "", "Resumen de Ventas", "", "INVP330C" });
                    dt.Rows.Add(new object[] { "", "Promociones por Cliente-Producto (propios)", "", "BILP597A" });
                    dt.Rows.Add(new object[] { "", "Promociones por Cliente_producto(terceros)", "", "BILP597A" });
                    dt.Rows.Add(new object[] { "", "Promociones por Producto-cliente(propios)", "", "BILP597B" });
                    dt.Rows.Add(new object[] { "", "Promociones por Producto-cliente(terceros)", "", "BILP597B" });
                    dt.Rows.Add(new object[] { "", "Seleccion de facturas por cliente para pago de bonificacion", "", "BILP598C" });
                    dt.Rows.Add(new object[] { "", "Seleccion de facturas por cliente para pago de bonificacion", "", "BILP598C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Clientes (selección x lim. Crédito)", "", "ACRP593C" });

                    dt.Rows.Add(new object[] { "", "REIMPRESION DE DOCUMENTOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Re-Impresion de Documentos que afectan inventarios", "", "ACRP90C" });
                    dt.Rows.Add(new object[] { "", "Re-Impresion de Documentos Financieros (no afectan Inventarios) - PAPISA", urlDeAcceso("AACRP65PA"), "AACRP65PA" });
                    

                    dt.Rows.Add(new object[] { "", "Re-Impresion de Notas de Entregas", "", "ACRP95C" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento de Consecutivos Numero de control", "", "ACRP402" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento masivo del numero de control", "", "BILP110C" });

                    dt.Rows.Add(new object[] { "", "MODULO INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de Inventario Producto Terminado por Despacho", "", "INVP391" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Analisis de movimiento ( Inv / Fac / CxC)", urlDeAcceso("SYSPC199"), "SYSPC199" });

                    dt.Rows.Add(new object[] { "", "MODULO GESTION DE PEDIDOS (ORD) Y FACTURACION (BIL)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Resumen de Pedidos Pendientes", urlDeAcceso("ORDP584BC"), "ORDP584BC" });
                    dt.Rows.Add(new object[] { "", "Consulta estadística de pedidos", "", "ORDP801" });

                    dt.Rows.Add(new object[] { "", "Opciones adicionales", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Cambio Punto Envio Clientes a Facturar", "", "ORDP65" });
                    dt.Rows.Add(new object[] { "", "Listado de Movimiento de Cuentas", urlDeAcceso("CEAPR05C"), "CEAPR05C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Puntos de Envio por Clte/Vend/Alm", urlDeAcceso("ACRP593C"), "ORDP100C" });
                    dt.Rows.Add(new object[] { "", "Lista documentos con Errores de Secuencia en RAR", "", "SYSP503" });
                    dt.Rows.Add(new object[] { "", "Consulta de Inventario de Pedidos", "", "INVP800" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ventas Por Sucursal y Clase Articulos", "", "SILP303C" });
                    dt.Rows.Add(new object[] { "", "Somete Proceso Automatico P.Pago ND-CD", "", "SYSPC501" });
                    dt.Rows.Add(new object[] { "", "Reporte de Cia, Cta, Aux, y tipos.", "", "CEAPR60C" });


                    break;
                // UR12 PRESUPUESTO
                case "12":
                    this.Title = "Menú UR12 PRESUPUESTO EXTRA LX";
                    LabelMenuUR.Text = "UR12 PRESUPUESTO Extra LX";

                    dt.Rows.Add(new object[] { "", "MANTENIMIENTOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Tabla de equivalencias cod.presupuesto vs BPCS", "", "PVPR102" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Consulta existencia de productos por almacen", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Produccion por turno y centro", urlDeAcceso("SFCP314C"), "SFCP314C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reporte De Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });


                    break;
                // UR13 LOGISTICA
                case "13":
                    this.Title = "Menú UR13 LOGISTICA EXTRA LX";
                    LabelMenuUR.Text = "UR13 LOGISTICA Extra LX";


                    dt.Rows.Add(new object[] { "", "MODULO DE INVENTARIO (INV)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Restaurar Orden de Fabricación en uso", urlDeAcceso("SFCP108"), "SFCP108" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Existencias de productos", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados p/Despacho", "", "INVP391C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados Almacenes externos zona GC", "", "INVP392C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario de Prod.Terminados Consolidado", "", "INVP393C" });
                    dt.Rows.Add(new object[] { "", "Consulta inventario de Prod.Terminados con PESOS", "", "INVP300P" });
                    dt.Rows.Add(new object[] { "", "Consulta de transacciones (Inv/Mov/Ord)", "", "INVP800" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte de Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte consolidado de control de lotes", urlDeAcceso("SFCP305C"), "SFCP308C" });
                    dt.Rows.Add(new object[] { "", "Reporte/Consulta Existencia Material por Almacén", "", "INVP305C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Movimiento de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });
                    dt.Rows.Add(new object[] { "", "Detalle de Movimiento de Inventario", urlDeAcceso("INVP318C"), "INVP312C" });
                    dt.Rows.Add(new object[] { "", "Detalle de Fallas de Transaciones", urlDeAcceso("INVP316C"), "INVP316C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Inventario (Rango=Fechas y Productos)", urlDeAcceso("INVP310H"), "INVP310CP" });
                    dt.Rows.Add(new object[] { "", "Cambio placa transporte y reimpresión N/Entrega", "", "INVP102" });
                    dt.Rows.Add(new object[] { "", "Cambio de placa por lote/producto", "", "INVP103C" });
                    dt.Rows.Add(new object[] { "", "Reporte Mov. De inventario resumido y consolidado", urlDeAcceso("INVP318C"), "INVPC325" });
                    dt.Rows.Add(new object[] { "", "Detalle de movimiento de Inventario por centro de costo", urlDeAcceso("INVP318C"), "INVP314C" });
                    dt.Rows.Add(new object[] { "", "Análisis de existencia de INV. MP por antiguedad", "", "INVP335C" });
                    dt.Rows.Add(new object[] { "", "Inventario De Mat.EM e Insumos", "", "INVP211C" });
                    dt.Rows.Add(new object[] { "", "Analisis de movimiento ( Inv / Fac / CxC)", urlDeAcceso("SYSPC199"), "SYSPC199" });
                    dt.Rows.Add(new object[] { "", "Impresion de N/Entrega Pendientes Por Generar", "", "INVP117" });

                    dt.Rows.Add(new object[] { "", "MODULO DE GESTION DE PEDIDOS (ORD)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Consulta de estadísticas de pedidos y backlog", "", "ORDP801" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Back-log productos semielaborados por cliente", "", "ORDP509C" });
                    dt.Rows.Add(new object[] { "", "Actualiza BBL para factura de exportación", "", "BILP102C" });
                    dt.Rows.Add(new object[] { "", "Estatus pedidos pendientes de clientes SE y PC", "", "ORDP905C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE FACTURACION (BIL)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "PROCESAMIENTO DE FACTURAS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reimpresión de Notas de Engrega", "", "ACRP95C" });
                    dt.Rows.Add(new object[] { "", "Reimpresión de documentos que afectan inventario", "", "ACRP90C" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Consecutivo Nro De Control", "", "ACRP402" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Masivo Del Número De Control", "", "BILP110C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE SFC Y BOM", "", "D1" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Listado de materiales BOM-FORMULA", "", "BOMP200C" });
                    dt.Rows.Add(new object[] { "", "Resumen de requerimientos de materiales", "", "MRPP301C" });
                    dt.Rows.Add(new object[] { "", "Listado de minimo y maximo (MODS: DESP-CONSUMO)", "", "ACRP110C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes de Fabricación", "", "SFCP300C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Ordenes Abiertas Para Planificación", "", "SFCP310C" });
                    dt.Rows.Add(new object[] { "", "Consulta de produccion por turno y centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Reporte Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Consumo Real vs Standard", "", "SFCP317C" });
                    dt.Rows.Add(new object[] { "", "Reporte De Shinkrage En Molinos", urlDeAcceso("SFCP320C"), "SFCP320C" });
                    dt.Rows.Add(new object[] { "", "Reporte Horas Por Centro De Trabajo y O/Fabric.", "", "SFCP303C" });

                    dt.Rows.Add(new object[] { "", "MODULO DE COMPRAS (PUR)", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Lista alfabética de proveedores", "", "PURP305C" });

                    dt.Rows.Add(new object[] { "", "PROCESOS", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Imprime Notas de Recepción de Materia Prima", "", "PURP316C" });
                    dt.Rows.Add(new object[] { "", "Imprime comprobantes Recepción de materiales", urlDeAcceso("PURP312C"), "PURP312C" });
                    // dt.Rows.Add(new object[] { "", "Cierre manual de la orden de compra", urlDeAcceso("PURP901C"), "PURP901C" });
                    // dt.Rows.Add(new object[] { "", "Activa O/C Cerrada/Lines(s)Totalmente Recibida(s)", urlDeAcceso("PURP902C"), "PURP902C" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte de Recepciones por Producto", "", "PURP318C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Recepciones por Proveedor", "", "PURP319C" });
                    dt.Rows.Add(new object[] { "", "Reporte detallado notas de entrega por proveedor", "", "PURP320C" });
                    dt.Rows.Add(new object[] { "", "Reporte O/Compra por Tipo Moneda", "", "PURP302C" });
                    dt.Rows.Add(new object[] { "", "Reporte Ordenes de compra", urlDeAcceso("PURP300C"), "PURP300C" });
                    dt.Rows.Add(new object[] { "", "Imprime Informe de contrato", "", "PURP310C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Status de contrato", urlDeAcceso("PURP313C"), "PURP313C" });
                    dt.Rows.Add(new object[] { "", "Informe de recepciones", urlDeAcceso("PURP314C"), "PURP314C" });

                    dt.Rows.Add(new object[] { "", "CONTROL ACCESO VEHICULO A PLANTA", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Sistema Control Acceso Vehículo a planta", "", "CAVP900C" });
                    dt.Rows.Add(new object[] { "", "Lista alfabética de proveedores", "", "PURP305C" });

                    dt.Rows.Add(new object[] { "", "OPCIONES ADICIONALES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Movimiento de Inventario por centro de costos", urlDeAcceso("INVP318C"), "INVP314C" });
                    dt.Rows.Add(new object[] { "", "Listado de Movimiento de Cuentas", urlDeAcceso("CEAPR05C"), "CEAPR05C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones (MVTO)para Costos", "", "CIEP905C" });


                    break;
                // UR14 CONTROL DE CALIDAD
                case "14":
                    this.Title = "Menú UR14 CONTROL DE CALIDAD EXTRA LX";
                    LabelMenuUR.Text = "UR14 CONTROL DE CALIDAD Extra LX";


                    dt.Rows.Add(new object[] { "", "MANTENIMIENTOS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento Peso Caja Conversion", urlDeAcceso("ORDP101"), "ORDP101" });
                    dt.Rows.Add(new object[] { "", "Mantenimiento Comentario Peso Paleta", "", "ORDP308" });

                    dt.Rows.Add(new object[] { "", "CONSULTAS", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Consulta de estadísticas de pedidos y backlog", "", "ORDP801" });
                    dt.Rows.Add(new object[] { "", "Existencias de productos", urlDeAcceso("INVP390C"), "INVP390C" });
                    dt.Rows.Add(new object[] { "", "Consulta de transacciones (Inv/Mov/Ord)", "", "INVP800" });
                    dt.Rows.Add(new object[] { "", "Consulta de producción por turno y por centro", urlDeAcceso("SFCP314C"), "SFCP314C" });
                    dt.Rows.Add(new object[] { "", "Consulta Inventario Prod.Terminado con PESOS", "", "INVP300P" });

                    dt.Rows.Add(new object[] { "", "REPORTES", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reportes de Inventario y lista de materiales", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Listado de Materiales BOM-Formula", "", "BOMP200C" });
                    dt.Rows.Add(new object[] { "", "Reporte/Consulta Existencia Material por Almacén", "", "INVP305C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Movimiento de Inventario", urlDeAcceso("INVP310H"), "INVP310H" });
                    dt.Rows.Add(new object[] { "", "Detalle de Movimiento de Inventario", urlDeAcceso("INVP318C"), "INVP312C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Control de Lotes", urlDeAcceso("SFCP305C"), "SFCP305C" });
                    dt.Rows.Add(new object[] { "", "Detalle de Fallas de Transaciones", urlDeAcceso("INVP316C"), "INVP316C" });
                    dt.Rows.Add(new object[] { "", "Resumen de Inventario (Rango=Fechas y Productos)", urlDeAcceso("INVP310H"), "INVP310CP" });
                    dt.Rows.Add(new object[] { "", "Reporte Mov. De inventario resumido y consolidado", urlDeAcceso("INVP318C"), "INVPC325" });
                    dt.Rows.Add(new object[] { "", "Reporte consolidado de control de lotes", urlDeAcceso("SFCP305C"), "SFCP308C" });
                    dt.Rows.Add(new object[] { "", "Detalle de movimiento de Inventario con centro", urlDeAcceso("INVP318C"), "INVP314C" });
                    dt.Rows.Add(new object[] { "", "Reporte de producción total anual", "", "INVP308C" });
                    dt.Rows.Add(new object[] { "", "Despacho de inspeccion detallado por lotes", "", "PURP270C" });

                    dt.Rows.Add(new object[] { "", "Reportes de Compras", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Lista alfabética de proveedores", "", "PURP305C" });
                    dt.Rows.Add(new object[] { "", "Imprime Informe de contrato", "", "PURP310C" });
                    dt.Rows.Add(new object[] { "", "Imprime comprobantes Recepción de materiales", urlDeAcceso("PURP312C"), "PURP312C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Status de contrato", urlDeAcceso("PURP313C"), "PURP313C" });
                    dt.Rows.Add(new object[] { "", "Informe de recepciones", urlDeAcceso("PURP314C"), "PURP314C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Recepciones por Producto", "", "PURP318C" });
                    dt.Rows.Add(new object[] { "", "Reporte de Recepciones por Proveedor", "", "PURP319C" });

                    dt.Rows.Add(new object[] { "", "Reportes de Ordenes de fabricación", "", "D2" });

                    dt.Rows.Add(new object[] { "", "Reporte de Ordenes de Fabricación", "", "SFCP300C" });
                    dt.Rows.Add(new object[] { "", "Reporte de consumo real vs. Standard", "", "SFCP317C" });
                    dt.Rows.Add(new object[] { "", "Resumen de deviación de costo de orden de fabricación", urlDeAcceso("CSTP310C"), "CSTP310C" });
                    dt.Rows.Add(new object[] { "", "Consulta de Broke en linea y Peso real", "", "SFCP337" });

                    dt.Rows.Add(new object[] { "", "Opciones Adicionales", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Reporte de Transacciones", urlDeAcceso("INVP318C"), "INVP318C" });

                    dt.Rows.Add(new object[] { "", "Otros Mantenimientos", "", "D1" });

                    dt.Rows.Add(new object[] { "", "Mantenimiento Unidades por capa (Cajas Std Unif)", "", "INVP105" });


                    break;
                // peticiones no válidas
                default:
                    Response.Redirect("Default.aspx", true);
                    break;
            }

            // asignación de datos
            GridViewMenu.DataSource = dt;
            GridViewMenu.DataBind();

        }

        protected void GridViewMenu_DataBound(object sender, EventArgs e)
        {

            int a = 1;

            for (int i = 0; i < GridViewMenu.Rows.Count; i++)
            {

                // Numeración secuencial de las líneas
                //if ("D1" != GridViewMenu.Rows[i].Cells[2].Text.ToString() && "D2" != GridViewMenu.Rows[i].Cells[2].Text.ToString())
                //{
                //    GridViewMenu.Rows[i].Cells[0].Text = a.ToString();
                //    a++;
                //}

                // Decoración y color
                //// encabezado Almacen
                if ("D1" == GridViewMenu.Rows[i].Cells[2].Text.ToString())
                {
                    GridViewMenu.Rows[i].Style.Add("border-top", "#FFFFFF solid 1px");
                    GridViewMenu.Rows[i].Style.Add("background-color", "#526376");
                    GridViewMenu.Rows[i].Font.Bold = true;
                    GridViewMenu.Rows[i].ForeColor = Color.White;
                    GridViewMenu.Rows[i].Cells[2].Text = "";
                }
                //// encabezado de Transaccion
                if ("D2" == GridViewMenu.Rows[i].Cells[2].Text.ToString())
                {
                    GridViewMenu.Rows[i].Style.Add("border-top", "#FFFFFF solid 1px");
                    GridViewMenu.Rows[i].Style.Add("background-color", "#8FADCE");
                    GridViewMenu.Rows[i].Font.Bold = true;
                    GridViewMenu.Rows[i].ForeColor = Color.White;
                    GridViewMenu.Rows[i].Cells[2].Text = "";
                }

            }

        }
    }
}