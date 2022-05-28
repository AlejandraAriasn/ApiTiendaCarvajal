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

    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        //Generamos la coleccion de servicios
        IServiceCollection serviceCollection = new ServiceCollection();
        [HttpPost]
        [Route("IniciarSesion")]
        public IHttpActionResult IniciarSesion(JObject datos)
        {
            var datosconsulta = new ClsLogin()
            {
                usuario = datos["userName"].ToString(),
                contraseña = datos["password"].ToString()
                

            };

            try
            {
                serviceCollection.AddSingleton<ILogin, Login>();
                Injector.GenerarProveedor(serviceCollection);
                ILogin implementar = Injector.GetService<ILogin>();
                int result = implementar.IniciarSesion(datosconsulta.usuario, datosconsulta.contraseña);
                if (result == 0)
                {
                    return Unauthorized();
                }
                else
                {
                    var token = TokenGenerator.GenerateTokenJwt(datosconsulta.usuario);
                    return Ok(token);
                }
           
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
