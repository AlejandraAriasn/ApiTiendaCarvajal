using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
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
    [RoutePrefix("api/GestionDetalleCompra")]
    public class GestionDetalleCompraController : ApiController
    {

        IServiceCollection serviceCollection = new ServiceCollection();
        [HttpPost]
        [Route("RegistrarDetalle")]
        public IHttpActionResult RegistrarDetalle(ClsDetalleCompra detcompra)
        {


            try
            {
                serviceCollection.AddSingleton<ICompraDetalle, GestionDetalleCompra>();
                Injector.GenerarProveedor(serviceCollection);
                ICompraDetalle implementar = Injector.GetService<ICompraDetalle>();


                return Ok(implementar.Add(detcompra));
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
