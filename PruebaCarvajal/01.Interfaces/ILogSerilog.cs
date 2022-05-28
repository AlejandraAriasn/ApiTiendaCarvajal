using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCarvajal._01.Interfaces
{
    public  interface ILogSerilog
    {
        string RegistrarError(string error, string trace);
        
    }
}
