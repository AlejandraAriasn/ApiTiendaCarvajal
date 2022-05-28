using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal._03.Utilidades;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class Login : ILogin
    {
        
        public int IniciarSesion(string us, string Pass)
        {
            //ConsultarExistencia Usuario
            int valido = 0;
            try
            {
                
                var consulta = new Usuarios();
                using (var db = new PruebaCarvajalEntities())
                {
                    consulta = (from b in db.Usuarios where b.NombreUsuario == us select b).FirstOrDefault();
                }
                if (consulta != null)
                {
                    if (consulta.IdRecord > 0)
                    {
                        string passwCifrado = cifrarContraseñas.cifrarClaveAcceso_conSalt(Pass, consulta.Salt);
                        if (passwCifrado == consulta.ClaveAccesoCifrada)
                        {
                            valido = 1;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }         

            
            return valido;
        }
    }
}