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
    [RoutePrefix("api/ReporteTopVentas")]
    public class TopVentasController : ApiController
    {
        IServiceCollection serviceCollection = new ServiceCollection();


        [HttpGet]
        [Route("GenerarReporteTopMas")]

        public IHttpActionResult GenerarReporteTopMas()
        {

            try
            {
                serviceCollection.AddSingleton<IReportes<ClsReporteTop>, TopVentas>();
                Injector.GenerarProveedor(serviceCollection);
                IReportes<ClsReporteTop> implementar = Injector.GetService<IReportes<ClsReporteTop>>();

                return Ok(implementar.ConsultarTopVentasMas());
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

        [HttpGet]
        [Route("GenerarReporteTopMenos")]

        public IHttpActionResult GenerarReporteTopMenos()
        {

            try
            {
                serviceCollection.AddSingleton<IReportes<ClsReporteTop>, TopVentas>();
                Injector.GenerarProveedor(serviceCollection);
                IReportes<ClsReporteTop> implementar = Injector.GetService<IReportes<ClsReporteTop>>();

                return Ok(implementar.ConsultarTopVentasMenos());
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
