using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IBM.Data.DB2.iSeries;
using PavecaLdataConeccetionAS400;
using PavecaCommonlDataLibreryAS400;


namespace SFCP320C
{
    public class Datos
    {
        string connectString = "";
        public Datos(Conexiones particion)
        {
            switch (particion)
            {
                case Conexiones.COLOMBIA:
                    connectString = "connectStringCO";
                    break;
                case Conexiones.GUATEMALA:
                    connectString = "connectStringGU";
                    break;
                case Conexiones.PANAMA:
                    connectString = "connectStringPAN";
                    break;
                case Conexiones.PERU:
                    connectString = "connectStringPE";
                    break;
                case Conexiones.TRINIDAD:
                    connectString = "connectStringTT";
                    break;
                case Conexiones.VENEZUELA:
                    connectString = "connectStringVE";
                    break;
                case Conexiones.PROTOTIPO:
                    connectString = "connectStringDES";
                    break;
                default:
                    break;
            }      
                        

            
        }

        public Datos(int axc)
        {

        }

        public enum Conexiones { COLOMBIA,GUATEMALA,PANAMA,PERU,TRINIDAD,VENEZUELA,PROTOTIPO }

        public List<TableZMFL01Class> LlenarFacility()
        {
            using (DatabaseAS400 _db = DatabaseFactoryAS400.CreateDatabaseAS400(connectString))
            {
                List<TableZMFL01Class> _result = new List<TableZMFL01Class>();
                iDB2DataReader _dr;

                try
                {
                    _dr = _db.ExecuteReaderAS400(@"SELECT MFFACL, MFFACL || ' - ' || MFDESC AS MFDESC FROM ZMFL01 ORDER BY MFFACL");

                    while (_dr.Read())
                    {
                        TableZMFL01Class m_TableZMFL01Class = new TableZMFL01Class();
                        m_TableZMFL01Class = EntityHelperAS400.Mapper<TableZMFL01Class>(_dr, m_TableZMFL01Class);
                        _result.Add(m_TableZMFL01Class);

                    }

                    return _result;
                }
                catch (ExceptionCustomList)
                {
                    throw;
                }

            }

        }

        public List<TableLWKL01Class> LlenarCtroWrk()
        {
            using (DatabaseAS400 _db = DatabaseFactoryAS400.CreateDatabaseAS400(connectString))
            {
                List<TableLWKL01Class> _result = new List<TableLWKL01Class>();
                iDB2DataReader _dr;

                try
                {
                    _dr = _db.ExecuteReaderAS400(@"SELECT WWRKC, WWRKC || ' - ' || WDESC AS WDESC, WFAC FROM LWKL01 ORDER BY WWRKC");

                    while (_dr.Read())
                    {
                        TableLWKL01Class m_TableLWKL01Class = new TableLWKL01Class();
                        m_TableLWKL01Class = EntityHelperAS400.Mapper<TableLWKL01Class>(_dr, m_TableLWKL01Class);
                        _result.Add(m_TableLWKL01Class);
                    }

                    return _result;
                }
                catch (ExceptionCustomList)
                {
                    throw;
                }

            }

        }

        public List<Shrinkage> LlenarGV(params object[] values)
        {
            int FechaDesde = int.Parse(values[0].ToString());
            int FechaHasta = int.Parse(values[1].ToString());
            int CtroWrkDesde = int.Parse(values[2].ToString());
            int CtroWrkHasta = int.Parse(values[3].ToString());
            string EstadoOrden = values[4].ToString();
            string Particion = values[5].ToString();
            using (DatabaseAS400 _db = DatabaseFactoryAS400.CreateDatabaseAS400(connectString))
            {
                List<Shrinkage> _result = new List<Shrinkage>();
                iDB2DataReader _dr;
                try
                {
                    _dr = _db.ExecuteReaderAS400(@"
                            SELECT S.SID as ESTADO, a.TREF AS NumOrd, THWRKC AS IdCtroWrk,D.WDESC as NbCtroWrk,S.SPROD as IdProd,I.IDESC as NbProd,
                                   SUM(CASE WHEN TTYPE = 'I' AND TWHS = 'MO' AND (TPROD LIKE ('MP%') OR TPROD LIKE ('FS%')) THEN TQTY ELSE 0 END)*-1 AS CONSUMIDO,  
                                   SUM(CASE WHEN TTYPE = 'R' AND TWHS = 'SE' AND TPROD LIKE ('S%')  THEN TQTY ELSE 0 END) AS PRODUCIDO,
                                   SUM(CASE WHEN TTYPE = 'DV' AND TWHS = 'MO' AND TPROD LIKE ('MP%')  THEN TQTY ELSE 0 END) AS RECHAZADO
                            FROM ITHL15 a left outer join FSOL02 S on a.TREF=S.SORD inner join IIML01 I on S.SPROD=I.IPROD inner join LWKL01 d on a.THWRKC=d.WWRKC 
                            WHERE (TWHS IN ('SE','MO')) AND (TTYPE IN ('I', 'R','DV'))
                             AND (TTDTE BETWEEN " + FechaDesde.ToString() + " AND " + FechaHasta.ToString() + " ) " 
                              + "AND (TPROD LIKE 'S%' OR TPROD LIKE 'MP%' OR TPROD LIKE 'FS%'  ) "
                              + "AND THWRKC BETWEEN " + CtroWrkDesde.ToString() + " AND " + CtroWrkHasta.ToString() 
                              + " AND S.SID = '" + EstadoOrden + "'" + 
                            "GROUP BY a.TREF,S.SPROD,I.IDESC ,THWRKC, D.WDESC,S.SID " +                  
                            "ORDER BY THWRKC,a.TREF");

                    while (_dr.Read())
                    {
                        Shrinkage m_ShrinkageClass = new Shrinkage();
                        m_ShrinkageClass = EntityHelperAS400.Mapper<Shrinkage>(_dr, m_ShrinkageClass);
                        _result.Add(m_ShrinkageClass);
                    }

                    return _result;
                }
                catch (ExceptionCustomList)
                {
                    throw;
                }

            }

        }
    }
}