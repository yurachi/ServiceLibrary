using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;

namespace ServiceLibrary.abstractions
{
    public class SqlCommandAbstraction : IDisposable
    {
        private SqlConnection _theConnection;

        public string ConnectionString { get; set; }

        public SqlCommandAbstraction ()
        {
            //TODO: take the connection string from app.config of the calling application
            //ConnectionString = "Data Source=LONSGDBP1229.uk.db.com; Initial Catalog=CONSULTING;Trusted_Connection=TRUE;";
        }

        public void Dispose()
        {
            if (_theConnection == null) return;
            _theConnection.Dispose();
            _theConnection = null;
        }

        public SqlCommand prepareSqlCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var thisCommand = new SqlCommand(commandText);
            _theConnection = new SqlConnection(ConnectionString);
            _theConnection.Open();
            thisCommand.CommandType = commandType;
            thisCommand.Connection = _theConnection;
            foreach(var p in parameters)
                thisCommand.Parameters.Add(p);
            return thisCommand;
        }
    }
}