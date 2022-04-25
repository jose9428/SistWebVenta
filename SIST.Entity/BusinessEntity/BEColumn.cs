using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIST.Entity.BusinessEntity
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BEColumn : Attribute
    {
        string _name;
        public BEColumn(string name)
        {
            this._name = name;
        }
        public string Name
        {
            get { return this._name; }
        }
    }
}
