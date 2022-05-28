using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCarvajal._01.Interfaces
{
    public interface IAccionesBasicas <T>
    {
        List<T> Get(string ID);//Consulta Basica por ID
        List<T> GetAll(); //Consulta toda la información
        long Add( T Info); //Crea un nuevo Registro
    }
}
