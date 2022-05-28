using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class GestionProductos : IProductos
    {
        public long Add(ClsProductos Info)
        {
   
            if (!string.IsNullOrEmpty(Info.CodProducto))
            {


                try
                {
                    using (var db = new PruebaCarvajalEntities())
                    {
                        //Validar Existencia
                        var Exis = from a in db.Productos
                                    where a.CodProducto == Info.CodProducto
                                    select a;
                        if (Exis.Count() == 0)
                        {

                            var prod = new Productos
                            {

                                CodProducto = Info.CodProducto,
                                NombreProducto = Info.NombreProducto,
                                Descripcion = Info.Descripcion,
                                Precio = Info.Precio,
                                Imagen = Info.Imagen,
                                Estado = true

                            };
                            var res = db.Productos.Add(prod);

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
                        else
                        {
                            bool up=Update( Info);
                            if (up)
                            {
                                return Exis.FirstOrDefault().IdRecord;
                            }
                            else { return 0; }
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

        public bool Delete(string ID)
        {
            try
            {
                Productos prod;
                bool Result = false;
                //Connected Scenario
                using (PruebaCarvajalEntities db = new PruebaCarvajalEntities())
                {
                    long IdRecord = Convert.ToInt32(ID);
              
                    var resul = db.spActualizarProducto(IdRecord);
                   
                    Result = true;
                  
                }
                return Result;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public List<ClsProductos> Get(string CodProducto)
        {
             try
                {
                    
                    using (var db = new PruebaCarvajalEntities())
                    {
                        var consulta = (from a in db.Productos where a.CodProducto ==CodProducto select new ClsProductos() { 
                            IdRecord=a.IdRecord,
                            NombreProducto=a.NombreProducto,
                            Descripcion=a.Descripcion,
                            Precio=a.Precio,
                            Imagen=a.Imagen,
                            Estado=a.Estado,
                            CodProducto=a.CodProducto

                        });
                        return consulta.ToList<ClsProductos>();
                    }
                }
                catch (Exception)
                {

                    return null;
                }
           
        }

        public List<ClsProductos> GetAll()
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.Productos
                                    where a.Estado==true
                                    select new ClsProductos()
                                    {
                                        IdRecord = a.IdRecord,
                                        NombreProducto = a.NombreProducto,
                                        Descripcion = a.Descripcion,
                                        Precio = a.Precio,
                                        Imagen = a.Imagen,
                                        Estado = a.Estado,
                                        CodProducto = a.CodProducto

                                    });
                    return consulta.ToList<ClsProductos>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Update(ClsProductos Info)
        {
            try
            {
                Productos prod;
                bool Result = false;
                //Connected Scenario
                using (PruebaCarvajalEntities db = new PruebaCarvajalEntities())
                {


                    prod = db.Productos.Where(d => d.CodProducto == Info.CodProducto).First();
                    if (Info.NombreProducto != null)
                    {
                        prod.NombreProducto = Info.NombreProducto;
                    }
                    if (Info.Descripcion != null)
                    {
                        prod.Descripcion = Info.Descripcion;
                    }
                    if (Info.Imagen != null)
                    {
                        prod.Imagen = Info.Imagen;
                    }

                    db.SaveChanges();
                    if (prod.IdRecord != 0)
                    {
                        Result = true;
                    }


                }
                return Result;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}