using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.IO;

namespace API
{
    public class APIQuery : IDisposable
    {
        SqlConnection conexion;     
        DAO vl_dao = new DAO();

        public APIQuery()
        {
            renewConexion();
        }

        #region dipose
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        if (conexion != null && conexion.State == ConnectionState.Open)
                        {
                            conexion.Close();
                            conexion.Dispose();
                            conexion = null;
                        }
                       
                        vl_dao.Dispose();
                    }
                    catch { }
                }

                _disposed = true;
            }
        }

        public virtual int ExecuteNonQuery(string _spName, Dictionary<string, object> _parameters)
        {
            int filasAfectadas = 0;
            string stringConnection = string.Empty;

            if (string.IsNullOrEmpty(_spName) || string.IsNullOrWhiteSpace(_spName))
                throw new ArgumentException("El parámetro [_nombreSP] no puede ser nullo, vacio o contener caracteres en blanco");

            stringConnection = conexion.ConnectionString;
            if (string.IsNullOrEmpty(stringConnection) || string.IsNullOrWhiteSpace(stringConnection))
                throw new System.Configuration.ConfigurationErrorsException("No se encuentra la cadena de conexión [DBConnection] especificada");

            if (_parameters != null && _parameters.Count > 0)
            {
                foreach (KeyValuePair<string, object> item in _parameters)
                {
                    if (string.IsNullOrEmpty(item.Key) || string.IsNullOrWhiteSpace(item.Key))
                        throw new ArgumentException("Los nombres de parámetros de SP no pueden ser nullos, vacios o contener caracteres en blanco");
                }
            }

            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection();

                try
                {
                    if (_parameters != null && _parameters.Count > 0)
                    {
                        foreach (KeyValuePair<string, object> item in _parameters)
                        {
                            SqlParameter parametro = new SqlParameter();

                            if (item.Value != null && item.Value.GetType().BaseType.Name == "List`1")
                                parametro.SqlDbType = SqlDbType.Structured;
                            parametro.ParameterName = item.Key;
                            parametro.Value = item.Value;
                            //parametro.SqlDbType = SqlDbType.Structured;

                            command.Parameters.Add(parametro);
                        }
                    }

                    connection.ConnectionString = conexion.ConnectionString;
                    connection.Open();

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = _spName;
                    command.Connection = connection;

                    filasAfectadas = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }

                return filasAfectadas;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~APIQuery()
        {
            Dispose(false);
        }
        #endregion

        private void renewConexion()
        {
            conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }

    

     
        public DataTable ExecuteSP(string NombreSP, Dictionary<string, object> Parametros)
        {
            try
            {
                return (DataTable)vl_dao.ExecuteSP(NombreSP, Parametros, "datatable", null);
            }
            catch
            {
                throw;
            }
        }

        public object ExecuteSP(string nombreSP, Dictionary<string, object> parametros, string objetoRetorno = "dataset", string cadenaConexion = null)
        {
            try
            {
                return vl_dao.ExecuteSP(nombreSP, parametros, objetoRetorno, cadenaConexion);
            }
            catch
            {
                throw;
            }
        }

        public DataTable executeQuery(string p_query)
        {
            try
            {
                return vl_dao.ExecuteQuery(p_query);
            }
            catch
            {
                throw;
            }
        }

        public int executeNonQuery(string p_query)
        {
            try
            {
                return vl_dao.ExecuteNonQuery(p_query);
            }
            catch
            {
                throw;
            }
        }

        

    }

    
}
