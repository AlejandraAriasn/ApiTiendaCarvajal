using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class clsClaveUsuario
    {
        public string NombreUsuario { get; set; }
        public string ClaveAccesoCifrada { get; set; }
        public string Salt { get; set; }
        public Int64 NumeroDocumento  { get; set; }
        public string FechaClave { get; set; }
        public int NDiasClave { get; set; }
    }
}