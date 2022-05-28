using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class GestionEncabezadoCompra : ICompraEncabezado
    {
        public long Add(ClsEncabezadoCompra Info)
        {
            

                try
                {
                    using (var db = new PruebaCarvajalEntities())
                    {
                    DateTime fecha = DateTime.Now;
                    var Enca = new EncabezadoCompra
                    {

                        Fecha = fecha,
                        IdUsuario = Info.IdUsuario,
                        SubTotal = Info.SubTotal,
                        Impuestos = Info.Impuestos,
                        Total=Info.Total

                    };
                        var res = db.EncabezadoCompra.Add(Enca);

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

        public List<ClsEncabezadoCompra> Get(string ID)
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.EncabezadoCompra
                                    select new ClsEncabezadoCompra()
                                    {
                                        IdRecord = a.IdRecord,
                                        Fecha = a.Fecha,
                                        IdUsuario = a.IdUsuario,
                                        SubTotal = a.SubTotal,
                                        Impuestos = a.Impuestos,
                                        Total = a.Total

                                    });
                    return consulta.ToList<ClsEncabezadoCompra>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<ClsEncabezadoCompra> GetAll()
        {
            try
            {

                using (var db = new PruebaCarvajalEntities())
                {
                    var consulta = (from a in db.EncabezadoCompra
                                    select new ClsEncabezadoCompra()
                                    {
                                        IdRecord = a.IdRecord,
                                        Fecha = a.Fecha,
                                        IdUsuario = a.IdUsuario,
                                        SubTotal = a.SubTotal,
                                        Impuestos = a.Impuestos,
                                        Total = a.Total

                                    });
                    return consulta.ToList<ClsEncabezadoCompra>();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}