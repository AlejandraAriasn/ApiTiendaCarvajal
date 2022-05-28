using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal._03.Utilidades;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PruebaCarvajal.Servicios
{
    public class GestionUsuarios : IUsuarios
    {
       

        public long Add(ClsUsuarios Info)
        {
           
            if (!string.IsNullOrEmpty( Info.NumeroDocumento) )           
            {

               
                try
                {
                    using (var db = new PruebaCarvajalEntities())
                    {
                        string[] clavecifrada = cifrarContraseñas.cifrarClaveAcceso(Info.ClaveAccesoCifrada);
                        var us = new Usuarios
                        {
                           
                            
                            NumeroDocumento = Info.NumeroDocumento,
                            NombreCompleto = Info.NombreCompleto,
                            NombreUsuario = Info.NombreUsuario,
                            CorreoElectronico = Info.CorreoElectronico,
                            ClaveAccesoCifrada = clavecifrada[0],
                            FechaClave = Info.FechaClave,
                            NDiasClave = 90,
                            Salt = clavecifrada[1],
                            Estado = Info.Estado
                        };
                        var res = db.Usuarios.Add(us);
                       
                        db.SaveChanges();
                        if (res.IdRecord != 0) 
                        {
                            return res.IdRecord;
                        }
                        else
                        {
                            return 0;
                        }           

                        
                        
                    }
                }
                catch (OverflowException)
                {
                    return 0;
                }
                catch (Exception ex)
                {

                    return 0;
                }


            }
            else
            {
                return 0;
            }
          
        }

       

        public List<ClsUsuarios> Get(string nombreusuario)
        {
            try
            {
              
                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.Usuarios
                                    where a.NombreUsuario == nombreusuario
                                    select new ClsUsuarios()
                                    {
                                        IdRecord=a.IdRecord,
                                        NumeroDocumento = a.NumeroDocumento,
                                        NombreCompleto = a.NombreCompleto,
                                        NombreUsuario = a.NombreUsuario,
                                        CorreoElectronico = a.CorreoElectronico,
                                        ClaveAccesoCifrada = a.ClaveAccesoCifrada,
                                        FechaClave = a.FechaClave,
                                        Salt = a.Salt,
                                        Estado=a.Estado,
                                        TipoUsuario=a.TipoUsuario

                                    });
                    return consulta.ToList<ClsUsuarios>();
                }
            }
            catch (Exception)
            {

                return null;
            }
          
        }

        public List<ClsUsuarios> GetAll()
        {
            try
            {
                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.Usuarios                                  
                                    select new ClsUsuarios()
                                    {
                                        NumeroDocumento = a.NumeroDocumento,
                                        NombreCompleto = a.NombreCompleto,
                                        NombreUsuario = a.NombreUsuario,
                                        CorreoElectronico = a.CorreoElectronico,
                                        ClaveAccesoCifrada = a.ClaveAccesoCifrada,
                                        FechaClave = a.FechaClave,
                                        Salt = a.Salt,
                                        Estado = a.Estado,
                                        TipoUsuario = a.TipoUsuario

                                    });
                    return consulta.ToList<ClsUsuarios>();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
           
        }

       
       

       
        
    }
}