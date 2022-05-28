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
    [RoutePrefix("api/GestioInventario")]
    public class GestionInventarioController : ApiController
    {
        //Generamos la coleccion de servicios
        IServiceCollection serviceCollection = new ServiceCollection();
        [HttpGet]
        [Route("ListarProductosDisponibles")]
        public IHttpActionResult ListarProductosDisponibles()
        {

            try
            {
                serviceCollection.AddSingleton<IGestionInventario, GestionInventario>();
                Injector.GenerarProveedor(serviceCollection);
                IGestionInventario implementar = Injector.GetService<IGestionInventario>();

                return Ok(implementar.ListarProductos());
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
        [Route("ListarProductosXNombre")]
        public IHttpActionResult ConsultarProductosXNombre(string Busqueda)
        {

            try
            {
                serviceCollection.AddSingleton<IGestionInventario, GestionInventario>();
                Injector.GenerarProveedor(serviceCollection);
                IGestionInventario implementar = Injector.GetService<IGestionInventario>();

                return Ok(implementar.ListarProductosxNombre(Busqueda));
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


        [HttpPost]
        [Route("RegistrarInventario")]
        public IHttpActionResult RegistrarInventario(ClsInventario infoUsu)
        {


            try
            {
                serviceCollection.AddSingleton<IGestionInventario, GestionInventario>();
                Injector.GenerarProveedor(serviceCollection);
                IGestionInventario implementar = Injector.GetService<IGestionInventario>();

                return Ok(implementar.Add(infoUsu));
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


        [HttpPut]
        [Route("ActualizaCantidadExistente")]
        public IHttpActionResult ActualizaCantidadExistente(JObject infoup)
        {


            try
            {
                serviceCollection.AddSingleton<IGestionInventario, GestionInventario>();
                Injector.GenerarProveedor(serviceCollection);
                IGestionInventario implementar = Injector.GetService<IGestionInventario>();

                return Ok(implementar.Actualizar(Convert.ToInt64(infoup["IdProducto"].ToString()),Convert.ToInt32( infoup["CantidadExistente"].ToString())));
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
