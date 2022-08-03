using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IBM.Data.DB2.iSeries;
namespace PavecaLdataConeccetionAS400
{
    public class DatabaseAS400 : IDisposable
    {
        private bool _disposed = false;
        private iDB2Connection _ConnectioniDB2400;

        ~DatabaseAS400()
        {
            Clear(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (ConnectioniDB400.State == ConnectionState.Open | ConnectioniDB400.State == ConnectionState.Broken)
                    {
                        ConnectioniDB400.Close();
                        ConnectioniDB400 = null;
                    }
                }
            }
            _disposed = true;
        }

        public iDB2Connection ConnectioniDB400
        {
            get { return _ConnectioniDB2400; }
            set { _ConnectioniDB2400 = value; }
        }

        public int ExecuteNonQueryAS400(iDB2Connection connectioniDB400, string storedProcedure)
        {
            ConnectioniDB400 = connectioniDB400;

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            int m_returnValue = commandAS400.ExecuteNonQuery();

            return m_returnValue;
        }

        public object ExecuteScalarAS400(iDB2Connection connectioniDB400, string storedProcedure)
        {
            ConnectioniDB400 = connectioniDB400;

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            object m_returnValue = commandAS400.ExecuteScalar();

            return m_returnValue;
        }

        public iDB2DataReader ExecuteReaderAS400(iDB2Connection connectioniDB400, string storedProcedure)
        {
            ConnectioniDB400 = connectioniDB400;

            iDB2DataReader dr = null;
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;


            dr = commandAS400.ExecuteReader();

            return dr;
        }


        public int ExecuteNonQueryAS400(iDB2Connection connectioniDB400, string storedProcedure, params object[] parameterValues)
        {
            ConnectioniDB400 = connectioniDB400;

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);
            int m_returnValue = commandAS400.ExecuteNonQuery();

            return m_returnValue;
        }

        public object ExecuteScalarAS400(iDB2Connection connectioniDB400, string storedProcedure, params object[] parameterValues)
        {
            ConnectioniDB400 = connectioniDB400;

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);
            object m_returnValue = commandAS400.ExecuteScalar();

            return m_returnValue;
        }

        public iDB2DataReader ExecuteReaderAS400(iDB2Connection connectioniDB400, string storedProcedure, params object[] parameterValues)
        {
            ConnectioniDB400 = connectioniDB400;

            iDB2DataReader dr = null;
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);

            dr = commandAS400.ExecuteReader();

            return dr;
        }

        public DataSet ExecuteDataSetAS400(iDB2Connection connectioniDB400, string storedProcedure, params object[] parameterValues)
        {
            ConnectioniDB400 = connectioniDB400;
            DataSet ds = new DataSet();

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = connectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);


            iDB2DataAdapter da = new iDB2DataAdapter();
            da.SelectCommand = commandAS400;

            da.Fill(ds);

            return ds;
        }

        public int ExecuteNonQueryAS400(iDB2Transaction transactionAS400, string storedProcedure, params object[] parameterValues)
        {

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;
            commandAS400.Transaction = transactionAS400;

            SetParameterValuesAS400(commandAS400, parameterValues);
            int m_returnValue = commandAS400.ExecuteNonQuery();

            return m_returnValue;
        }

        public object ExecuteScalarAS400(iDB2Transaction transactionAS400, string storedProcedure, params object[] parameterValues)
        {
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;
            commandAS400.Transaction = transactionAS400;

            SetParameterValuesAS400(commandAS400, parameterValues);
            object m_returnValue = commandAS400.ExecuteScalar();

            return m_returnValue;
        }

        public iDB2DataReader ExecuteReaderAS400(iDB2Transaction transactionAS400, string storedProcedure, params object[] parameterValues)
        {
            iDB2DataReader dr = null;
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;
            commandAS400.Transaction = transactionAS400;

            SetParameterValuesAS400(commandAS400, parameterValues);

            dr = commandAS400.ExecuteReader();

            return dr;
        }

        public DataSet ExecuteDataSetAS400(iDB2Transaction transactionAS400, string storedProcedure, params object[] parameterValues)
        {
            DataSet ds = new DataSet();

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;
            commandAS400.Transaction = transactionAS400;

            SetParameterValuesAS400(commandAS400, parameterValues);

            iDB2DataAdapter da = new iDB2DataAdapter();
            da.SelectCommand = commandAS400;

            da.Fill(ds);

            return ds;
        }

        public int ExecuteNonQueryAS400(string nomQuery)
        {
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.Text;
            commandAS400.CommandText = nomQuery;

            int m_returnValue = commandAS400.ExecuteNonQuery();

            return m_returnValue;
        }

        public object ExecuteScalarAS400(string storedProcedure)
        {

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            object m_returnValue = commandAS400.ExecuteScalar();

            return m_returnValue;
        }

        public iDB2DataReader ExecuteReaderAS400(string query)
        {

            iDB2DataReader dr = null;
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.Text;
            commandAS400.CommandText = query;

            commandAS400.CommandTimeout = 0;

            dr = commandAS400.ExecuteReader();

            return dr;
        }

        public DataSet ExecuteDataSetAS400(string storedProcedure)
        {

            DataSet ds = new DataSet();

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            iDB2DataAdapter da = new iDB2DataAdapter();
            da.SelectCommand = commandAS400;

            da.Fill(ds);

            return ds;
        }

        public int ExecuteNonQueryAS400(string storedProcedure, params object[] parameterValues)
        {


            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);
            int m_returnValue = commandAS400.ExecuteNonQuery();

            return m_returnValue;
        }

        public object ExecuteScalarAS400(string storedProcedure, params object[] parameterValues)
        {
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);
            object m_returnValue = commandAS400.ExecuteScalar();

            return m_returnValue;
        }

        public iDB2DataReader ExecuteReaderAS400(string storedProcedure, params object[] parameterValues)
        {
            iDB2DataReader dr = null;
            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);

            dr = commandAS400.ExecuteReader();

            return dr;
        }


        public DataTable ExecuteDataTableAS400(iDB2Transaction transaction, string storedProcedure, params object[] parameterValues)
        {
            DataTable dt = new DataTable();

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;
            commandAS400.Transaction = transaction;

            SetParameterValuesAS400(commandAS400, parameterValues);

            iDB2DataAdapter da = new iDB2DataAdapter();
            da.SelectCommand = commandAS400;

            da.Fill(dt);

            return dt;
        }

        public DataSet ExecuteDataSetAS400(string storedProcedure, params object[] parameterValues)
        {
            DataSet ds = new DataSet();

            iDB2Command commandAS400 = new iDB2Command();
            commandAS400.Connection = ConnectioniDB400;
            commandAS400.CommandType = CommandType.StoredProcedure;
            commandAS400.CommandText = storedProcedure;

            SetParameterValuesAS400(commandAS400, parameterValues);


            iDB2DataAdapter da = new iDB2DataAdapter();
            da.SelectCommand = commandAS400;

            da.Fill(ds);

            return ds;
        }

        private void SetParameterValuesAS400(iDB2Command commandAS400, object[] values)
        {
            DiscoverParametersAS400(commandAS400);

            for (int i = 0; i < values.Length; i++)
            {
                IDataParameter parameter = commandAS400.Parameters[i];
                SetParameterValueAS400(commandAS400, parameter.ParameterName, values[i]);
            }
        }

        private void SetParameterValueAS400(iDB2Command commandAS400, string parameterName, object value)
        {
            commandAS400.Parameters[parameterName].Value = value ?? DBNull.Value;
        }

        private iDB2Command CreateDB2CommandAS400(iDB2Command SqlCommand)
        {
            iDB2Command command = new iDB2Command();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SqlCommand.CommandText;
            if (SqlCommand.Transaction != null)
            {
                command.Transaction = SqlCommand.Transaction;
            }

            return command;
        }

        protected void DeriveParametersAS400(iDB2Command discoveryCommandAS400)
        {
            iDB2CommandBuilder.DeriveParameters(discoveryCommandAS400);
        }

        private void DiscoverParametersAS400(iDB2Command commandAS400)
        {
            using (iDB2Command discoveryCommand = CreateDB2CommandAS400(commandAS400))
            {
                discoveryCommand.Connection = ConnectioniDB400;
                DeriveParametersAS400(discoveryCommand);

                foreach (IDataParameter parameter in discoveryCommand.Parameters)
                {
                    if ((parameter.Direction == ParameterDirection.Input) | (parameter.Direction == ParameterDirection.InputOutput))
                    {
                        IDataParameter cloneParameter = (IDataParameter)((ICloneable)parameter).Clone();
                        commandAS400.Parameters.Add(cloneParameter);
                    }
                }
            }

        }

    }
}
