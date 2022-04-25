using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIST.Entity.BusinessEntity
{
    public class Categoria
    {
        [BEColumn("id_categoria")]
        public int pnIdCateg { get; set; }

        [BEColumn("nombre_categoria")]
        public int pnNomCateg { get; set; }

        public int estado { get; set; }
    }
}
