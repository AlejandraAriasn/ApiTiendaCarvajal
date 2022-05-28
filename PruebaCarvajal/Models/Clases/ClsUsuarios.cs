using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsUsuarios
    {
        public long IdRecord { get; set; }   
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string ClaveAccesoCifrada { get; set; }
        public Nullable<System.DateTime> FechaClave { get; set; }
        public Nullable<int> NDiasClave { get; set; }
        public string Salt { get; set; }
        public int Estado { get; set; }
        public Nullable<bool> TipoUsuario { get; set; }
    }
}