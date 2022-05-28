using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsListaProductos
    {
        public long IdInventario { get; set; }
        public long IdRecordProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDisponible { get; set; }
        public decimal Precio { get; set; }
        public Nullable<decimal> Descuento { get; set; }

        public string Imagen { get; set; }
    }
}