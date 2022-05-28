using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCarvajal._01.Interfaces
{
    interface IGestionInventario:IAccionesBasicas<ClsInventario>
    {
        List<ClsListaProductos> ListarProductos(); //Consulta toda la información
        List<ClsListaProductos> ListarProductosxNombre(string Busqueda); //Consulta toda la información

        bool Actualizar(long IdProducto,int CantidadDisponible);

        
    }
}
