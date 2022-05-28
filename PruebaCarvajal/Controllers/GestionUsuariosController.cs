

using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PruebaCarvajal._01.Interfaces;
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
    [RoutePrefix("api/GestionUsuarios")]
    public class GestionUsuariosController : ApiController
    {
        //Generamos la coleccion de servicios
        IServiceCollection serviceCollection = new ServiceCollection();

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public IHttpActionResult ConsultarUsuarios()
        {

            try
            {
                serviceCollection.AddSingleton<IUsuarios, GestionUsuarios>();
                Injector.GenerarProveedor(serviceCollection);
                IUsuarios implementar = Injector.GetService<IUsuarios>();

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
        [Route("ObtenerUsuariosXnombreusuario")]
        public IHttpActionResult ConsultarUsuariosXNombreUsuario(string nombreusuario)
        {

            try
            {                
                serviceCollection.AddSingleton<IUsuarios, GestionUsuarios>();
                Injector.GenerarProveedor(serviceCollection);
                IUsuarios implementar = Injector.GetService<IUsuarios>();
                return Ok(implementar.Get(nombreusuario));
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
        [Route("RegistrarUsuario")]
        public IHttpActionResult RegistrarUsuario(ClsUsuarios infoUsu)
        {
            try
            {
                serviceCollection.AddSingleton<IUsuarios, GestionUsuarios>();
                Injector.GenerarProveedor(serviceCollection);
                IUsuarios implementar = Injector.GetService<IUsuarios>();
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

      
    }
}