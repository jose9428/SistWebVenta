using SIST.Entity.BusinessEntity;
using SIST.Entity.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIST.Entity.BusinessLogic
{
    public class CategoriaBL
    {
        public List<Categoria> fListarTodosBL()
        {
            return new CategoriaDA().fListarTodosDA();
        }
    }
}
