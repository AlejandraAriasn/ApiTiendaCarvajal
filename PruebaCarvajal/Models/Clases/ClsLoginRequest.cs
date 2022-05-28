using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Models.Clases
{
    public class ClsLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string mac { get; set; }
        public string nombrePC { get; set; }
        public bool bloquearUsuario { get; set; }
        public bool desbloquearUsuario { get; set; }
        public string versionApp { get; set; }
        public int UserID { get; set; }
    }
}