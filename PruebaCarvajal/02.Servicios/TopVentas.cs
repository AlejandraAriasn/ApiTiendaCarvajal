using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaCarvajal._02.Servicios
{
    public class TopVentas : IReportes<ClsReporteTop>
    {
        public List<ClsReporteTop> ConsultarTopVentasMas()
        {
            List<ClsReporteTop> respuesta = new List<ClsReporteTop>();
            try
            {
                
                DataTable dt = new DataTable();
                var parametros = new Dictionary<string, object>();
                parametros.Add("opcion", "MasVendidos");
              
                dt = new API.APIQuery().ExecuteSP("spReporteVentas", parametros);
                
             
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        respuesta.Add(new ClsReporteTop()
                        {
                            CantidadVendido =Convert.ToInt32( dr["CantidadVendido"].ToString()),
                            NombreProducto = dr["NombreProducto"].ToString(),
                            CodProducto = Convert.ToString(dr["CodProducto"])
                           
                        });
                    }


                }
               
            }
            catch (Exception)
            {

                return null;
            }
            return respuesta;
        }

        public List<ClsReporteTop> ConsultarTopVentasMenos()
        {
            List<ClsReporteTop> respuesta = new List<ClsReporteTop>();
            try
            {

                DataTable dt = new DataTable();
                var parametros = new Dictionary<string, object>();
                parametros.Add("opcion", "MenosVendidos");

                dt = new API.APIQuery().ExecuteSP("spReporteVentas", parametros);


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        respuesta.Add(new ClsReporteTop()
                        {
                            CantidadVendido = Convert.ToInt32(dr["CantidadVendido"].ToString()),
                            NombreProducto = dr["NombreProducto"].ToString(),
                            CodProducto = Convert.ToString(dr["CodProducto"])

                        });
                    }


                }

            }
            catch (Exception)
            {

                return null;
            }
            return respuesta;
        }
    }
}