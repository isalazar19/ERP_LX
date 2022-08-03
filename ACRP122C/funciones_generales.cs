using System;
using IBM.Data.DB2.iSeries;

class funciones_generales
{
    //protected string resultado;
    public string ls_resultado;
    public int sqlcode, li_resultado;
    public String connectString;

    public string convierte_fecha_400(string valor, int param)
    {
        switch (param)
        {
            case 1:
                ls_resultado = valor.Substring(6, 2) + "/" + valor.Substring(4, 2) + "/" + valor.Substring(0, 4);
                break;

            case 2:
                ls_resultado = valor.Substring(6, 4) + valor.Substring(3, 2) + valor.Substring(0, 2);
                break;
        }
        return ls_resultado;
    }

    public string cadena_con(string ls_pais)
    {
        switch (ls_pais)
        {
            case "COLOMBIA":   /*COLOMBIA*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringCO"].ConnectionString;
                break;

            case "GUATEMALA":  /*GUATEMALA*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringGU"].ConnectionString;
                break;

            case "PANAMA":   /*PANAMA*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
                break;

            case "PERU":  /*PERU*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPE"].ConnectionString;
                break;

            case "TRINIDAD & TOBAGO":   /*TRINIDAD & TOBAGO*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringTT"].ConnectionString;
                break;

            case "VENEZUELA":  /*VENEZUELA*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringVE"].ConnectionString;
                break;

            case "PROTOTIPO": /*prototitpo*/
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringDES"].ConnectionString;
                break;

        }
        return connectString;
    }

    //public string cadena_con(int pais)
    //{
    //    //String connectString = "DataSource=APPN.VENEZUEL;DefaultCollection=V82BPCSF";
    //    //iDB2Connection con = new iDB2Connection(connectString);
    //    switch (pais)
    //    {
    //        case 1: /*COLOMBIA*/
    //            connectString = "DataSource=APPN.COLOMBIA;DefaultCollection=C82BPCSF";
    //            break;

    //        case 2: /*GUATEMALA*/
    //            connectString = "DataSource=APPN.GUATEMAL;DefaultCollection=G82BPCSF";
    //            break;

    //        case 3: /*PANAMA*/
    //            //connectString = "DataSource=APPN.COLOMBIA;DefaultCollection=C82BPCSF";
    //            break;

    //        case 4: /*PERU*/
    //            connectString = "DataSource=APPN.COLOMBIA;DefaultCollection=P82BPCSF";
    //            break;

    //        case 5: /*TRINIDAD*/
    //            connectString = "DataSource=PERU;DefaultCollection=T82BPCSF";
    //            break;

    //        case 6: /*VENEZUELA*/
    //            connectString = "DataSource=APPN.VENEZUEL;DefaultCollection=V82BPCSF;LibraryList=V82BPCSF,PANLXUSRF";
    //            break;
    //    }
    //    iDB2Connection con = new iDB2Connection(connectString);

    //    return con.ConnectionString;
    //}

    public int conectar_bd(int pais)
    {
        switch (pais)
        {
            case 1: /*COLOMBIA*/
                connectString = "DataSource=APPN.COLOMBIA;DefaultCollection=C82BPCSF";
                break;

            case 2: /*GUATEMALA*/
                connectString = "DataSource=APPN.GUATEMAL;DefaultCollection=G82BPCSF";
                break;

            case 3: /*PANAMA*/
                //connectString = "DataSource=APPN.COLOMBIA;DefaultCollection=C82BPCSF";
                break;

            case 4: /*PERU*/
                connectString = "DataSource=APPN.COLOMBIA;DefaultCollection=P82BPCSF";
                break;

            case 5: /*TRINIDAD*/
                connectString = "DataSource=APPN.PERU;DefaultCollection=T82BPCSF";
                break;

            case 6: /*VENEZUELA*/
                connectString = "DataSource=APPN.VENEZUEL;DefaultCollection=V82BPCSF";
                break;
        
        }
        iDB2Connection con = new iDB2Connection(connectString);
      //con.Open();

        return this.sqlcode;
    }

    public int convierte_string_int(string valor)
    {
        li_resultado = Convert.ToInt32(valor);
        return li_resultado;
    }

    public string userfile(string ls_pais)
    {
        switch (ls_pais)
        {
            case "COLOMBIA":   /*COLOMBIA*/
                ls_resultado = "C82BPCSUSF";
                break;

            case "GUATEMALA":  /*GUATEMALA*/
                ls_resultado = "G82BPCSUSF";
                break;

            case "PANAMA":   /*PANAMA*/
                ls_resultado = "PANLXUSRF";
                break;

            case "PERU":  /*PERU*/
                ls_resultado = "P82BPCSUSF";
                break;

            case "TRINIDAD & TOBAGO":   /*TRINIDAD & TOBAGO*/
                ls_resultado = "T82BPCSUSF";
                break;

            case "VENEZUELA":  /*VENEZUELA*/
                ls_resultado = "V82BPCSUSF";
                break;

            case "PROTOTIPO": /*PROTOTIPO*/
                ls_resultado = "V82BPCSUSF";
                break;

        }

        return ls_resultado;
    }

    public int CalculateDays(DateTime oldDate, DateTime newDate)
    {
      // Diferencia de fechas
      TimeSpan ts = newDate - oldDate;

      // Diferencia de días
      return ts.Days;
    }

    public string remover_blancos(string cadena)
    {
        cadena = cadena.Replace(" ","");
        return cadena;
    }

}