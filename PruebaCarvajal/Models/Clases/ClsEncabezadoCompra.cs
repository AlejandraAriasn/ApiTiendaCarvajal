using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsEncabezadoCompra
    {
        public long IdRecord { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public long IdUsuario { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> Impuestos { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}