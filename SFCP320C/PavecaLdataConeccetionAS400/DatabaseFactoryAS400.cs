using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using IBM.Data.DB2.iSeries;

namespace PavecaLdataConeccetionAS400
{
    public static class DatabaseFactoryAS400
    {
        public static iDB2Connection CreateConnectionAS400(string keyConnection)
        {
            string connectionAS400 = ConfigurationManager.AppSettings[keyConnection];
            iDB2Connection sqlConnectionAS400 = new iDB2Connection(connectionAS400);
            sqlConnectionAS400.Open();

            return sqlConnectionAS400;

            
        }

        public static iDB2Connection CloneConnectionAS400(iDB2Connection ConnectionAS400)
        {
            string connectionAS400 = ConnectionAS400.ConnectionString;
            iDB2Connection iDBConnectionAS400 = new iDB2Connection(connectionAS400);
            iDBConnectionAS400.Open();

            return iDBConnectionAS400;
        }

        public static DatabaseAS400 CreateDatabaseAS400(string keyConnection)
        {
            string connectionAS400 = ConfigurationManager.ConnectionStrings[keyConnection].ConnectionString;
            DatabaseAS400 databaseAS400 = new DatabaseAS400();
            iDB2Connection iDBConnectionAS400 = new iDB2Connection(connectionAS400);
            iDBConnectionAS400.Open();
            databaseAS400.ConnectioniDB400 = iDBConnectionAS400;

            return databaseAS400;
        }

        public static DatabaseAS400 CloneDatabaseAS400(DatabaseAS400 dataBase)
        {

            string connectionAS400 = dataBase.ConnectioniDB400.ConnectionString;
            DatabaseAS400 databaseAS400 = new DatabaseAS400();
            iDB2Connection iDBConnectionAS400 = new iDB2Connection(connectionAS400);
            iDBConnectionAS400.Open();
            databaseAS400.ConnectioniDB400 = iDBConnectionAS400;

            return databaseAS400;
        }
    }
}
