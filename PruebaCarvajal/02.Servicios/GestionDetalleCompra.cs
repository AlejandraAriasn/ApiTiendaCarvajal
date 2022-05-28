using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class GestionDetalleCompra : ICompraDetalle
    {
        public long Add(ClsDetalleCompra Info)
        {
            long ok = 0;
            
                try
                {
                 
                    using (var db = new PruebaCarvajalEntities())
                    {
                        DateTime fecha = DateTime.Now;

                        var det = new DetalleCompra
                        {
                           
                            IdRecordEncabezado = Info.IdRecordEncabezado ,
                            IdProducto = Info.IdProducto,
                            Cantidad = Info.Cantidad,
                            PrecioCompraUnidad = Info.PrecioCompraUnidad,
                            Descuento = Info.Descuento

                        };
                        var res = db.DetalleCompra.Add(det);

                        db.SaveChanges();
                        if (res.IdRecord != 0)
                        {
                            ok= res.IdRecord;
                        }
                        else
                        {
                            ok= 0;
                        }



                    }
                }
                catch (OverflowException)
                {
                    ok= 0;
                }
                catch (Exception ex)
                {

                    ok = 0;
                }

            
           return ok;

        }

        //public long Add(ClsDetalleCompra Info)
        //{
        //    long ok = 0;
        //    foreach (var item in (IEnumerable<ClsDetalleCompra>)Info)
        //    {

        //        try
        //        {

        //            using (var db = new PruebaCarvajalEntities())
        //            {
        //                DateTime fecha = DateTime.Now;

        //                var det = new DetalleCompra
        //                {

        //                    IdRecordEncabezado = item.IdRecordEncabezado,
        //                    IdProducto = item.IdProducto,
        //                    Cantidad = item.Cantidad,
        //                    PrecioCompraUnidad = item.PrecioCompraUnidad,
        //                    Descuento = item.Descuento

        //                };
        //                var res = db.DetalleCompra.Add(det);

        //                db.SaveChanges();
        //                if (res.IdRecord != 0)
        //                {
        //                    ok = res.IdRecord;
        //                }
        //                else
        //                {
        //                    ok = 0;
        //                }



        //            }
        //        }
        //        catch (OverflowException)
        //        {
        //            ok = 0;
        //        }
        //        catch (Exception ex)
        //        {

        //            ok = 0;
        //        }

        //    }
        //    return ok;

        //}
        public List<ClsDetalleCompra> Get(string ID)
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.DetalleCompra
                                    where a.IdRecordEncabezado== Convert.ToUInt32(ID)
                                    select new ClsDetalleCompra()
                                    {
                                        IdRecord = a.IdRecord,
                                        IdRecordEncabezado = a.IdRecordEncabezado,
                                        IdProducto = a.IdProducto,
                                        Cantidad = a.Cantidad,
                                        PrecioCompraUnidad = a.PrecioCompraUnidad,
                                        Descuento = a.Descuento

                                    });
                    return consulta.ToList<ClsDetalleCompra>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        //public List<ClsDetalleCompra> Get(string ID)
        //{
        //    try
        //    {

        //        using (var db = new PruebaCarvajalEntities())
        //        {
        //            //var res = from s in Splitting
        //            //          join c in Customer on s.CustomerId equals c.Id
        //            //          where c.Id == customrId
        //            //             && c.CompanyId == companyId
        //            //          select s;
        //            var consulta = (from a in db.EncabezadoCompra
        //                            join b in db.DetalleCompra on a.IdRecord equals b.IdRecordEncabezado
        //                            join c in db.Productos on b.IdProducto equals c.IdRecord
        //                            where a.IdRecord == Convert.ToInt32(ID)
        //                            select new ClsDetalleCompra()
        //                            {
        //                                IdRecord = a.IdRecord,
        //                                Fecha = a.Fecha,
        //                                IdUsuario = a.IdUsuario,
        //                                SubTotal = a.SubTotal,
        //                                Impuestos = a.Impuestos,
        //                                Total = a.Total

        //                            });
        //            return consulta.ToList<ClsEncabezadoCompra>();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}

        public List<ClsDetalleCompra> GetAll()
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.DetalleCompra
                                    select new ClsDetalleCompra()
                                    {
                                        IdRecord = a.IdRecord,
                                        IdRecordEncabezado = a.IdRecordEncabezado,
                                        IdProducto = a.IdProducto,
                                        Cantidad = a.Cantidad,
                                        PrecioCompraUnidad = a.PrecioCompraUnidad,
                                        Descuento = a.Descuento

                                    });
                    return consulta.ToList<ClsDetalleCompra>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}