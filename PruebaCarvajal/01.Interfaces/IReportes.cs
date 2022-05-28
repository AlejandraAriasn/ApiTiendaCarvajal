using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCarvajal._01.Interfaces
{
    interface IReportes<T>
    {
        List<T> ConsultarTopVentasMas();
        List<T> ConsultarTopVentasMenos();
    }
}
