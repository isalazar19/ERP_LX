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

    public string cadena_con(int pais)
    {
        //String connectString = "DataSource=APPN.VENEZUEL;DefaultCollection=V82BPCSF";
        //iDB2Connection con = new iDB2Connection(connectString);
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
                connectString = "DataSource=PERU;DefaultCollection=T82BPCSF";
                break;

            case 6: /*VENEZUELA*/
                connectString = "DataSource=APPN.VENEZUEL;DefaultCollection=V82BPCSF;LibraryList=V82BPCSF,PANLXUSRF";
                break;
        }
        iDB2Connection con = new iDB2Connection(connectString);

        return con.ConnectionString;
    }

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
}