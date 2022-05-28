using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsProductos
    {
      
            public long IdRecord { get; set; }
            public string CodProducto { get; set; }
            public string NombreProducto { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string Imagen { get; set; }
            public Nullable<bool> Estado { get; set; }

       
    }
}