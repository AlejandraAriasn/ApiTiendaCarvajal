using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCarvajal._01.Interfaces
{
    public interface IAccionesEdicion<T>
    {
        bool Update(T Info);
        bool Delete(string ID); 
    }

}
