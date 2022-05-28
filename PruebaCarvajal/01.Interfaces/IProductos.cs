using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCarvajal._01.Interfaces
{
    public interface IProductos: IAccionesBasicas<ClsProductos>, IAccionesEdicion<ClsProductos>
    {

    }
}
