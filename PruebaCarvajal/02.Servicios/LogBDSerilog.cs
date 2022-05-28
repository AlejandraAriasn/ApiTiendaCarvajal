using PruebaCarvajal._01.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Serilog.Sinks.MSSqlServer;
using Serilog.Events;
using System.Data;

namespace PruebaCarvajal._02.Servicios
{
    public class LogBDSerilog : ILogSerilog
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ToString();
        private const string _schemaName = "dbo";
        private const string _tableName = "LogEvents";
        public string RegistrarError(string error, string trace)
        {
            try
            {

                SqlConnection sqlcon = new SqlConnection();
                sqlcon.ConnectionString = _connectionString;
                sqlcon.Open();


                Log.Logger = new LoggerConfiguration().WriteTo
                    .MSSqlServer(
                        connectionString: _connectionString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = _tableName,
                            SchemaName = _schemaName,
                            AutoCreateSqlTable = true
                        },
                        restrictedToMinimumLevel: LogEventLevel.Debug,
                        formatProvider: null,
                        columnOptions: GetColumnOptions(),
                        logEventFormatter: null)
                    .CreateLogger();


                Log.Debug("Getting started");
                string identificador = System.DateTime.Now.ToString("yyyyMMddmmhhssfff");
                Log.Error("{Message}{Exception}{Identificador}", error, trace, identificador);

                Log.CloseAndFlush();


                sqlcon.Close();
                return identificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public  ColumnOptions GetColumnOptions()
        {
            ColumnOptions columnOptions = new ColumnOptions();

            // Remove all the StandardColumn
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);


            // Override the default Primary Column of Serilog by your custom column name
            columnOptions.Id.ColumnName = "IdRecord";

            // Add all the custom coumns
            columnOptions.AdditionalDataColumns = new List<DataColumn>
         {
               new DataColumn { DataType = typeof(string), ColumnName = "Modulo" },
                 new DataColumn { DataType = typeof(string), ColumnName = "Metodo" },
                 new DataColumn { DataType = typeof(string), ColumnName = "Error" },
         };
            return columnOptions;
        }


    }
}