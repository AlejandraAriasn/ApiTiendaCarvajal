using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsInventario
    {
        public long IdRecord { get; set; }
        public long IdProducto { get; set; }
        public Nullable<decimal> Descuento { get; set; }
        public int Estado { get; set; }
        public int CantidadDisponible { get; set; }
    
    }
}