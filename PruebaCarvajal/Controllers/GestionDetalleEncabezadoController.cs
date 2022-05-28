using Microsoft.Extensions.DependencyInjection;
using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal._02.Servicios;
using PruebaCarvajal._03.Utilidades;
using PruebaCarvajal.Models.Clases;
using PruebaCarvajal.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PruebaCarvajal.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/GestionCompra")]


    public class GestionDetalleEncabezadoController : ApiController
    {
        //Generamos la coleccion de servicios
        IServiceCollection serviceCollection = new ServiceCollection();
        

        [HttpPost]
        [Route("RegistrarEncabezado")]
        public IHttpActionResult RegistrarEncabezado(ClsEncabezadoCompra Encabezado)
        {
            try
            {
                serviceCollection.AddSingleton<ICompraEncabezado, GestionEncabezadoCompra>();
                Injector.GenerarProveedor(serviceCollection);
                ICompraEncabezado enca = Injector.GetService<ICompraEncabezado>();


                return Ok(enca.Add(Encabezado));
            }
            catch (Exception ex)
            {
                serviceCollection.AddSingleton<ISerilog, RegistroSerilog>();
                Injector.GenerarProveedor(serviceCollection);
                ISerilog Log = Injector.GetService<ISerilog>();

                ClsRegLog Result = Log.RegistrarError(ex);
                return new System.Web.Http.Results.ResponseMessageResult(
                  Request.CreateErrorResponse(
                     HttpStatusCode.InternalServerError,
                      new HttpError($"{Result.ErrorCode}//{Result.Message}")));

                throw;
            }


        }



    }
}
