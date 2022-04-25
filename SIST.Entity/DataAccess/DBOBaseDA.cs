using SIST.Entity.BusinessEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIST.Entity.DataAccess
{
    public class DBOBaseDA
    {
        private SqlConnection connection;
        protected String[] arrayLibraries;
        protected DataTable dataTable;
        SqlCommand cmd;

        public SqlConnection NewConnection()
        {
            try
            {
                string strCan = "Data Source= (local) ;Initial Catalog=bdVenta ;Integrated Security=True;";

                SqlConnection cn = new SqlConnection(strCan);

                return cn;

            }
            catch (Exception ex)
            {
                throw new Exception("Método Nueva Conexión: " + ex.Message, ex);
            }
            finally { }
        }

        protected void pAddParameter(SqlCommand command, String parameterName, Object value, DbType dbTipo)
        {
            pAddParameter(command, parameterName, value, dbTipo, ParameterDirection.Input);
        }

        protected void pAddParameter(SqlCommand command, String parameterName, Object value, SqlDbType dbTipo)
        {
            pAddParameter(command, parameterName, value, dbTipo, ParameterDirection.Input);
        }

        protected void pAddParameter(SqlCommand command, String parameterName, Object value, DbType dbTipo, ParameterDirection parameterDirection)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.DbType = dbTipo;
            parameter.Direction = parameterDirection;
            command.Parameters.Add(parameter);
        }

        protected void pAddParameter(SqlCommand command, String parameterName, Object value, SqlDbType dbTipo, ParameterDirection parameterDirection)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.SqlDbType = dbTipo;
            parameter.Direction = parameterDirection;
            command.Parameters.Add(parameter);
        }


        protected string fEjecutar(SqlCommand cmd)
        {
            string Resultado = "";
            try
            {
                cmd.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Resultado = "OK";
                }
                else
                {
                    Resultado = "La información se registró anteriormente.";
                }
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                Resultado = ex.Message;
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
            return Resultado;
        }

        protected SqlDataReader fLeer(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
                throw new Exception("Método Leer: " + ex.Message, ex);
            }
        }

        protected DataTable fSeleccionar(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                DataTable dt = new DataTable();
                DbDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Seleccionar: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

        protected DataSet fDSEjecutar(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                DataSet dt = new DataSet();
                DbDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Seleccionar: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

        protected Object fObtenerValor(IDbCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Obtener Valor: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }
        protected int fEjecutarQuery(IDbCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Obtener Valor: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

        public static Object ConvertirDataReaderALista<T>(SqlDataReader myDataReader)
        {
            List<T> entidades = new List<T>();
            PropertyInfo[] propiedades = typeof(T).GetProperties();
            try
            {
                ArrayList columnasConsultadas = new ArrayList();
                for (int i = 0; i < myDataReader.FieldCount; i++) { columnasConsultadas.Add(myDataReader.GetName(i)); }
                while (myDataReader.Read())
                {
                    T entidad = Activator.CreateInstance<T>();
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        Attribute item = Attribute.GetCustomAttribute(propiedad, typeof(BEColumn));
                        if (item is BEColumn)
                        {
                            try
                            {
                                BEColumn column = (BEColumn)item;
                                if (columnasConsultadas.IndexOf(column.Name) > -1)
                                {
                                    propiedad.SetValue(entidad, myDataReader.GetValue(myDataReader.GetOrdinal(column.Name)), null);
                                }
                            }
                            catch (Exception ex) { /*throw new Exception(ex.Message);*/ }
                        }
                    }
                    entidades.Add(entidad);
                }
            }
            finally
            {
                myDataReader.Close();
            }
            return entidades;
        }
    }
}
