using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDBAccess
    {
        IEnumerable<object> GetData(string code, KeyValuePair<string, object> parameters);
    }
}
