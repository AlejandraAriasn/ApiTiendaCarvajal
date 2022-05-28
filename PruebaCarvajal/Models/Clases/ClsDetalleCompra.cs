using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsDetalleCompra
    {
        public long IdRecord { get; set; }
        public long IdRecordEncabezado { get; set; }
        public long IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompraUnidad { get; set; }
        public Nullable<decimal> Descuento { get; set; }

    }
}