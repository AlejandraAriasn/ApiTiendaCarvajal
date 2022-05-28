using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PruebaCarvajal._01.Interfaces;
using PruebaCarvajal._02.Servicios;
using PruebaCarvajal._03.Utilidades;
using PruebaCarvajal.Models;
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
    [RoutePrefix("api/GestionProductos")]
    public class GestionProductosController : ApiController
    {
        //Generamos la coleccion de servicios
        IServiceCollection serviceCollection = new ServiceCollection();

      
            [HttpGet]
        [Route("ObtenerProductos")]
        public IHttpActionResult ConsultarProductos()
        {

            try
            {
                serviceCollection.AddSingleton<IProductos, GestionProductos>();
                Injector.GenerarProveedor(serviceCollection);
                IProductos implementar = Injector.GetService<IProductos>();

                return Ok(implementar.GetAll());
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
        [Route("ObtenerProductosXCodigo")]
        public IHttpActionResult ConsultarProductosXCodigo(string CodProducto)
        {

            try
            {
                serviceCollection.AddSingleton<IProductos, GestionProductos>();
                Injector.GenerarProveedor(serviceCollection);
                IProductos implementar = Injector.GetService<IProductos>();
                return Ok(implementar.Get(CodProducto));
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
        [Route("RegistrarProducto")]
        public IHttpActionResult RegistrarProducto(ClsProductos infoUsu)
        {

          
            try
            {
                serviceCollection.AddSingleton<IProductos, GestionProductos>();
                Injector.GenerarProveedor(serviceCollection);
                IProductos implementar = Injector.GetService<IProductos>();
                
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

        [HttpDelete]
        [Route("EliminarProducto")]
        public IHttpActionResult EliminarProducto(string ID)
        {
            try
            {
                serviceCollection.AddSingleton<IProductos, GestionProductos>();
                Injector.GenerarProveedor(serviceCollection);
                IProductos implementar = Injector.GetService<IProductos>();
                return Ok(implementar.Delete(ID));
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
