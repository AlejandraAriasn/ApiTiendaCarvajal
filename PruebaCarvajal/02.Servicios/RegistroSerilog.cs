using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal._02.Servicios;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Servicios
{
    public class RegistroSerilog : ISerilog
    {
        /// <summary>
        /// Escribe en la tabla dbo.LogEvents o en la ruta del web config que contenga el tag RutaSerilog en un archivo log.txt
        /// Nutget necesarios Serilog, Segilog.Settings.File, Segilog.Sinks.File, Serilog.Sinks.MSSqlServer
        /// </summary>

        ILogSerilog logSqlServer = new LogBDSerilog();
        ILogSerilog logTXT = new LogTxtSerilog();


        ClsRegLog ISerilog.RegistrarError(Exception ex)
        {
            ClsRegLog log = new ClsRegLog();
            string result;
            try
            {
                result = logSqlServer.RegistrarError(ex.Message, ex.StackTrace);
                log.ErrorCode = result;
                log.Message = $"{ex.Message}**{ex.StackTrace}";
            }
            catch (Exception ex2)
            {
                result = logTXT.RegistrarError(ex.Message, ex.StackTrace);
                log.ErrorCode = result;
                log.Message = $"{ex.Message}**{ex.StackTrace}";
            }
            return log;
        }

        
    }
}