using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Data;
using PruebaCarvajal.Models;
using PruebaCarvajal.Models.Clases;

namespace PruebaCarvajal._03.Utilidades
{
    public class cifrarContraseñas
    {

        public static string llaveMaestra;
        public static byte[] llaveMaestraBytes;

        public static void LlenarMaestra()
        {
            using (var db = new PruebaCarvajalEntities())
            {
                llaveMaestra = (from a in db.Configuracion where a.Parametro == "LLaveMaestra" select a.Valor).FirstOrDefault();
            }
                       
        }
      
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Función utilitaria. Genera un Salt de un tamaño determinado, ingresado por parámetro.
        /// </summary>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static byte[] GenerarSalt(int lenght)
        {
            // Make the RNG.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            // Make a buffer to hold X random bytes.
            byte[] bytes = new byte[lenght];
            // Make the random numbers.

            // Get X random bytes.
            rand.GetBytes(bytes);
            // Display the string.
            return bytes;
        }

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Valida si un usuario con su respectiva contraseña se encuentra almacenado
        /// en la tabla usuarios de la BD. Devuelve lo siguiente:
        /// Si la cedula del usuario es igual a la claveAcceso, devuelve 2.
        /// Si el user y passw son correctos, devuelve 1.
        /// Si el user y passw son incorrectos, devuelve 0.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passw"></param>
        /// <returns></returns>
        public int validarUsuario_claveAcceso(string user, string passw)
        {
            try
            {
                LlenarMaestra();
                // clsConexion = new ClsConexion();
                llaveMaestraBytes = StringToByteArray(llaveMaestra);
                using (HMACSHA256 hmac = new HMACSHA256(llaveMaestraBytes))
                {
                    string consulta = "select NombreUsuario, ClaveAcceso, Salt, Cedula from [Acces].[Usuario] where NombreUsuario like '" + user + "'";
                    DataTable dt = new API.APIQuery().executeQuery(consulta);
                    if (dt.Rows.Count > 0)
                    {
                        string claveacceso = Convert.ToString(dt.Rows[0].ItemArray[1]);
                        string saltHex = Convert.ToString(dt.Rows[0].ItemArray[2]);
                        string cedula = Convert.ToString(dt.Rows[0].ItemArray[3]);
                        string cedulaCifrada = cifrarClaveAcceso_conSalt(cedula, saltHex);
                        if (claveacceso == cedulaCifrada)
                        {
                            return 2;
                        }
                        string passwCifrado = cifrarClaveAcceso_conSalt(passw, saltHex);
                        if (claveacceso == passwCifrado)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        //public object[] validarclaveAccesoweb(string idusuario, string passw, string mac, string nombrePC, bool bloquearUsuario = true, bool desbloquearUsuario = false, string versionApp = null)
        //{
        //    try
        //    {
        //        List<Usuarios> users;
        //        using (PruebaCarvajalEntities db = new PruebaCarvajalEntities())
        //        {

        //            users = db.Usuarios.Where(x => x.NombreUsuario == idusuario).ToList<Usuarios>();
        //        }
        //        Usuarios user = users[0];
        //        if (user.Estado==0)
        //        {
        //            return new object[] { -101, "-1" };//usuario inactivo
        //        }
        //        else
        //        {

                 
        //            llaveMaestraBytes = StringToByteArray(llaveMaestra);
        //            using (HMACSHA256 hmac = new HMACSHA256(llaveMaestraBytes))
        //            {
        //                /*Dictionary<string, object> parametros = new Dictionary<string, object>();
        //                parametros.Add("opcion", "consultarUsuario");
        //                parametros.Add("idusuario", user.idUsuario);
        //                */
        //                //DataTable dt = new API.APIQuery().ExecuteSP("[dbo].[GestionarclaveUsuario]", parametros);

        //                clsClaveUsuario ClaveUsuario = new clsClaveUsuario();
        //                using (PruebaCarvajalEntities db = new PruebaCarvajalEntities())
        //                {
        //                    ClaveUsuario = db.Database
        //                        .SqlQuery<clsClaveUsuario>($@"[dbo].[GestionarclaveUsuario] @opcion='consultarUsuario',@idusuario={user.idUsuario}
        //                             ").FirstOrDefault();

        //                }


        //                if (ClaveUsuario != null)
        //                {
        //                    //string claveacceso = Convert.ToString(dt.Rows[0].ItemArray[1]);
        //                    //string saltHex = Convert.ToString(dt.Rows[0].ItemArray[2]);
        //                    //string cedula = Convert.ToString(dt.Rows[0].ItemArray[3]);
        //                    //string cedulaCifrada = cifrarClaveAcceso_conSalt(cedula, saltHex);
        //                    //DateTime fechaClave = Convert.ToDateTime(dt.Rows[0].ItemArray[4]);
        //                    //int NdiasClave = Convert.ToInt32(dt.Rows[0].ItemArray[5]);

        //                    string claveacceso = Convert.ToString(ClaveUsuario.claveAccesoCifrada);
        //                    string cedulaCifrada = cifrarClaveAcceso_conSalt(ClaveUsuario.Cedula.ToString(), ClaveUsuario.Salt);
        //                    DateTime fechaClave = Convert.ToDateTime(ClaveUsuario.FechaClave);
        //                    int NdiasClave = Convert.ToInt32(ClaveUsuario.NDiasClave);

        //                    var tspan = (fechaClave - DateTime.Now).TotalDays;
        //                    int dias = Convert.ToInt32(tspan);
        //                    bool cambiarClave = false;

        //                    if (dias >= NdiasClave ? cambiarClave = true : cambiarClave = false) ;
        //                    string passwCifrado = cifrarClaveAcceso_conSalt(passw, ClaveUsuario.Salt);

        //                    if (claveacceso == cedulaCifrada)
        //                    {
        //                        return new object[] { -4, "-4" };
        //                    }

        //                    else if (claveacceso == passwCifrado && cambiarClave == true)
        //                    {
        //                        return new object[] { -5, "-5" };
        //                    }
        //                    else if (claveacceso != passwCifrado)
        //                    {
        //                        return new object[] { -1, "-1" };
        //                    }
        //                    //}

        //                    //else
        //                    //{
        //                    //    return new object[] { -102, "-1" };//usuario ya logueado
        //                    //}

        //                }
        //                return regLoginUsuario(mac, nombrePC, bloquearUsuario, user.idUsuario, versionApp);
        //            }
        //            //return new object[] { -102, "-1" };
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}



        public string validarclaveAcceso(string user, string passw)
        {
            try
            {
                // clsConexion = new ClsConexion();
                LlenarMaestra();
                llaveMaestraBytes = StringToByteArray(llaveMaestra);
                using (HMACSHA256 hmac = new HMACSHA256(llaveMaestraBytes))
                {

                    string consulta = "select NombreUsuario, ClaveAcceso, Salt, Cedula from [Acces].[Usuario] where NombreUsuario like '" + user + "'";
                    DataTable dt = new API.APIQuery().executeQuery(consulta);
                    if (dt.Rows.Count > 0)
                    {
                        string claveacceso = Convert.ToString(dt.Rows[0].ItemArray[1]);
                        string saltHex = Convert.ToString(dt.Rows[0].ItemArray[2]);
                        string cedula = Convert.ToString(dt.Rows[0].ItemArray[3]);
                        string cedulaCifrada = cifrarClaveAcceso_conSalt(cedula, saltHex);
                        if (claveacceso == cedulaCifrada)
                        {
                            return claveacceso;
                        }
                        string passwCifrado = cifrarClaveAcceso_conSalt(passw, saltHex);
                        if (claveacceso == passwCifrado)
                        {
                            return claveacceso;
                        }
                        else
                        {
                            return " ";
                        }
                    }
                    return " ";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Cifra una contraseña nueva ingresada por parámetro, generando un nuevo Salt.
        /// Devuelve lo siguiente:
        /// string[0] es la claveAcceso cifrada con "Newpassw + NuevoSalt", y usando
        /// la llaveMaestra interna a esta clase.
        /// string[1] es el NuevoSalt generado internamente para esta claveAcceso.
        /// </summary>
        /// <param name="Newpassw"></param>
        /// <returns></returns>
        public static string[] cifrarClaveAcceso(string Newpassw)
        {
            try
            {
                string[] retorno = new string[2];
                retorno[1] = BitConverter.ToString(GenerarSalt(8)).Replace("-", "");
                retorno[0] = cifrarClaveAcceso_conSalt(Newpassw, retorno[1]);
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Función utilitaria. Recibe un password y un salt, y los cifra.
        /// </summary>
        /// <param name="passw"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string cifrarClaveAcceso_conSalt(string passw, string salt)
        {
            LlenarMaestra();
            llaveMaestraBytes = StringToByteArray(llaveMaestra);
            using (HMACSHA256 hmac = new HMACSHA256(llaveMaestraBytes))
            {
                byte[] password = System.Text.Encoding.UTF8.GetBytes(passw);
                string passwordHex = BitConverter.ToString(password).Replace("-", "");

                byte[] entrada = StringToByteArray(passwordHex + salt);
                byte[] salida = hmac.ComputeHash(entrada);
                return BitConverter.ToString(salida).Replace("-", "");
            }
        }
    }
}