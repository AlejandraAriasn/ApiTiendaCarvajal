using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class GestionInventario : IGestionInventario
    {
        public bool Actualizar(long IdProducto, int CantidadDisponible)
        {
            try
            {
               
                bool Result = false;
                //Connected Scenario
                using (PruebaCarvajalEntities db = new PruebaCarvajalEntities())
                {


                   var resul= db.spActualizarInventario(CantidadDisponible, IdProducto);             
                    

                   
                    if (Convert.ToInt32( resul)!=0)
                    {
                        Result = true;
                    }


                }
                return Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public long Add(ClsInventario Info)
        {
            //Inserta o actualiza el inventario

            try
            {
                using (var db = new PruebaCarvajalEntities())
                {
                    var exis = from a in db.Inventario
                               where a.IdProducto == Info.IdProducto
                               select a;
                    if(exis.Count()==0)
                    {
                        var prod = new Inventario
                        {

                            IdProducto = Info.IdProducto,
                            Descuento = Info.Descuento,
                            CantidadDisponible = Info.CantidadDisponible,
                            Estado = 1

                        };
                        var res = db.Inventario.Add(prod);

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
                      bool res=  Actualizar(exis.FirstOrDefault().IdProducto, Info.CantidadDisponible);
                        if (res)
                        {
                            return exis.FirstOrDefault().IdRecord;
                        }
                        else {
                            return 0;
                        }
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

        public List<ClsInventario> Get(string ID)
        {
            throw new NotImplementedException();
        }

        public List<ClsInventario> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ClsListaProductos> ListarProductos()
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.Inventario
                                    join b in db.Productos on a.IdProducto equals b.IdRecord
                                    select new ClsListaProductos()
                                    {
                                        IdInventario = a.IdRecord,
                                        IdRecordProducto = a.IdProducto,
                                        NombreProducto = b.NombreProducto,
                                        Descripcion =b.Descripcion,
                                        CantidadDisponible =a.CantidadDisponible,
                                        Precio = b.Precio,
                                        Descuento=a.Descuento,
                                        Imagen=b.Imagen

                                    });
                    return consulta.ToList<ClsListaProductos>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<ClsListaProductos> ListarProductosxNombre(string Busqueda)
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.Inventario
                                    join b in db.Productos on a.IdProducto equals b.IdRecord
                                    where b.NombreProducto.Contains(Busqueda)
                                    select new ClsListaProductos()
                                    {
                                        IdInventario = a.IdRecord,
                                        IdRecordProducto = a.IdProducto,
                                        NombreProducto = b.NombreProducto,
                                        Descripcion = b.Descripcion,
                                        CantidadDisponible = a.CantidadDisponible,
                                        Precio = b.Precio,
                                        Descuento = a.Descuento,
                                        Imagen = b.Imagen

                                    });
                    return consulta.ToList<ClsListaProductos>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

       
    }
}