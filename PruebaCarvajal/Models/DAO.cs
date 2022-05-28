
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace API
{
    public class DAO : IDisposable
    {
        SqlConnection _conexion;       

        public DAO()
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
                        if (_conexion != null && _conexion.State == ConnectionState.Open)
                        {
                            _conexion.Close();
                            _conexion.Dispose();
                            _conexion = null;
                        }

                        
                    }
                    catch { }
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DAO()
        {
            Dispose(false);
        }
        #endregion

        private void     renewConexion()
        {
             _conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
         }

        public SqlConnection conexion
        {
            get
            {
                return _conexion;
            }
        }

        public SqlCommand ObtenerComandoSql(string sentenciaSQL, ArrayList parametros, SqlConnection conexion, SqlTransaction transaccion)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = sentenciaSQL;
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.Text;
                comando.CommandTimeout = 0;
                foreach (SqlParameter p in parametros)
                {
                    comando.Parameters.Add(p);
                }

                return comando;
            }
            catch
            {
                throw;
            }
        }

        public SqlCommand ObtenerComandoSql(string sentenciaSQL, ArrayList parametros, SqlConnection conexion)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = sentenciaSQL;
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandTimeout = 0;
                foreach (SqlParameter p in parametros)
                {
                    comando.Parameters.Add(p);
                }

                return comando;
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
                if (objetoRetorno == null)
                {
                    if (cadenaConexion == null)
                    {
                        return ExecuteSP2Table(nombreSP, parametros);
                    }
                    else
                    {
                        return ((DataSet)ExecuteSP2Table(nombreSP, parametros, cadenaConexion)).Tables[0];
                    }
                }
                else if (objetoRetorno.ToLower() == "xml")
                {
                    XmlDocument xmlDocumento = new XmlDocument();
                    xmlDocumento.LoadXml(ExecuteSP2String(nombreSP, parametros).ToString());
                    return xmlDocumento;
                }
                else if (objetoRetorno.ToLower() == "datatable")
                {
                    DataSet ds = (DataSet)ExecuteSP2Table(nombreSP, parametros);
                    if (ds == null)
                    {
                        return null;
                    }

                    return ds.Tables[0];
                }
                else if (objetoRetorno.ToLower() == "dataset")
                {
                    return (DataSet)ExecuteSP2Table(nombreSP, parametros);
                }
                else if (objetoRetorno.ToLower() == "string")
                {
                    return ExecuteSP2String(nombreSP, parametros).ToString();
                }
                else
                {
                    DataSet ds = (DataSet)ExecuteSP2Table(nombreSP, parametros);
                    if (ds == null)
                    {
                        return null;
                    }

                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private object ExecuteSP2Table(string nombreSP, Dictionary<string, object> parametros)
        {
            try
            {
                if (_conexion == null || _conexion.State == ConnectionState.Closed) renewConexion();

                DataSet ds = new DataSet();
                _conexion.Open();
                SqlCommand comando = new SqlCommand(nombreSP, _conexion);
                foreach (KeyValuePair<string, object> Aux in parametros) comando.Parameters.Add(new SqlParameter(Aux.Key, Aux.Value));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(ds);
                _conexion.Close();
                _conexion.Dispose();
                if (ds.Tables.Count > 0) return ds;
                else return null;
            }
            catch (Exception ex)
            {
                if (_conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                    _conexion.Dispose();
                }
                throw new Exception(ex.Message, ex);
            }
        }

        private object ExecuteSP2Table(string nombreSP, Dictionary<string, object> parametros, string cadenaConexion)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand comando = new SqlCommand(nombreSP, con);
                foreach (KeyValuePair<string, object> Aux in parametros) comando.Parameters.Add(new SqlParameter(Aux.Key, Aux.Value));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(ds);
                if (ds.Tables.Count > 0) return ds;
                else return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
        }

        private object ExecuteSP2String(string nombreSP, Dictionary<string, object> parametros)
        {
            try
            {
                if (_conexion == null || _conexion.State == ConnectionState.Closed) renewConexion();

                SqlDataReader reader = null;
                _conexion.Open();
                SqlCommand comando = new SqlCommand(nombreSP, _conexion);
                foreach (KeyValuePair<string, object> Aux in parametros) comando.Parameters.Add(new SqlParameter(Aux.Key, Aux.Value));
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 0;
                reader = comando.ExecuteReader();
                string dat = string.Empty;
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        dat += reader.GetString(0);
                    }
                }
                return dat;
            }
            catch (Exception ex)
            {
                if (_conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                    _conexion.Dispose();
                }
                throw new Exception(ex.Message, ex);
            }
        }

        public DataTable ExecuteQuery(string p_query)
        {
            try
            {
                if (_conexion == null || _conexion.State == ConnectionState.Closed) renewConexion();

                DataSet ds = new DataSet();
                SqlCommand comando = new SqlCommand(p_query, _conexion);
                comando.CommandTimeout = 0;
                _conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(ds);
                _conexion.Close();
                _conexion.Dispose();
                if (ds.Tables.Count == 0)
                {
                    return null;
                }
                else
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                if (_conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                    _conexion.Dispose();
                }
                throw new Exception(ex.Message, ex);
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
        public int ExecuteNonQuery(string p_query)
        {
            if (_conexion == null || _conexion.State == ConnectionState.Closed) renewConexion();

            _conexion.Open();
            SqlTransaction transaccion = _conexion.BeginTransaction();

            try
            {
                DataSet ds = new DataSet();
                SqlCommand comando = new SqlCommand(p_query, _conexion, transaccion);
                comando.CommandTimeout = 0;
                int retorno = comando.ExecuteNonQuery();
                transaccion.Commit();
                _conexion.Close();
                _conexion.Dispose();
                return retorno;
            }
            catch (Exception ex)
            {
                if (_conexion.State == ConnectionState.Open)
                {
                    transaccion.Rollback();
                    _conexion.Close();
                    _conexion.Dispose();
                }
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<TElement> ExecuteQuery<TElement>(string query)
        {
            List<TElement> items = new List<TElement>();

            DataTable dt = ExecuteQuery(query);
            foreach (var rw in dt.Rows)
            {
                TElement item = (TElement)Activator.CreateInstance(typeof(TElement), rw);
                items.Add(item);
            }
            return items;
        }

        public static IEnumerable<TElement> MapeoDt2Object<TElement>(DataTable dt)
        {
            List<TElement> items = new List<TElement>();

            foreach (var rw in dt.Rows)
            {
                TElement item = (TElement)Activator.CreateInstance(typeof(TElement), rw);
                items.Add(item);
            }
            return items;
        }

        public IEnumerable<TElement> ExecuteFuncion<TElement>(string nombreSP, Dictionary<string, object> parametros)
        {
            List<TElement> items = new List<TElement>();

            parametros = (parametros == null) ? new Dictionary<string, object>() : parametros;

            DataTable dt = (DataTable)ExecuteSP(nombreSP, parametros, "datatable");
            foreach (var rw in dt.Rows)
            {
                TElement item = (TElement)Activator.CreateInstance(typeof(TElement), rw);
                items.Add(item);
            }
            return items;
        }

        public void ExecuteFuncion(string nombreSP, Dictionary<string, object> parametros)
        {
            parametros = (parametros == null) ? new Dictionary<string, object>() : parametros;
            ExecuteSP(nombreSP, parametros);
        }

        public static XmlDocument toXmlISO(object obj)
        {
            var stringwriter = new StringWriterISO();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = stringwriter.ToString();
            return xmlDoc;
        }

        public static XmlDocument toXmlUtf8(object obj)
        {
            var stringwriter = new StringWriterUtf8();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = stringwriter.ToString();
            return xmlDoc;
        }

        public static XmlDocument toXmlUtf16(object obj)
        {
            var stringwriter = new StringWriterUtf16();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = stringwriter.ToString();
            return xmlDoc;
        }

        public TElement toObjeto<TElement>(XmlDocument xmlDoc)
        {
            var stringReader = new System.IO.StringReader(xmlDoc.InnerXml);
            var serializer = new XmlSerializer(typeof(TElement));
            return (TElement)serializer.Deserialize(stringReader);
        }
    }

    public class StringWriterUtf8 : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

    public class StringWriterUtf16 : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }

    public class StringWriterISO : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.GetEncoding("ISO-8859-1"); }
        }
    }



}
