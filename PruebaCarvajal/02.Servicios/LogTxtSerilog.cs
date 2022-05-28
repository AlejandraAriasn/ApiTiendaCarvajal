using PruebaCarvajal._01.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class LogTxtSerilog : ILogSerilog
    {
        public string RegistrarError(string error, string trace)
        {
            string CarpetaDestino = ConfigurationManager.AppSettings["RutaSerilog"].ToString();
            if (!Directory.Exists(CarpetaDestino))
            {
                Directory.CreateDirectory(CarpetaDestino);
            }

            string identificador = System.DateTime.Now.ToString("yyyyMMddmmhhssfff");
            string Ruta = $"{CarpetaDestino}/LOG.TXT";
         

            File.WriteAllText(Ruta, $"Message={error} trace={trace} identificador={identificador}");
            return identificador;
        }
    }
}