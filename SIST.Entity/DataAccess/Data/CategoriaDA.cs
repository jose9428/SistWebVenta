using SIST.Entity.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIST.Entity.DataAccess.Data
{
    public class CategoriaDA:DBOBaseDA
    {
        private SqlCommand cmdSQL = new SqlCommand();
        public List<Categoria> fListarTodosDA()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                cmdSQL.Connection = NewConnection();
                cmdSQL.CommandText = "select * from categoria";

                SqlDataReader drSQL = fLeer(cmdSQL);
                lista = (List<Categoria>)ConvertirDataReaderALista<Categoria>(drSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return lista;
        }
    }
}
