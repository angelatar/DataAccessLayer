using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DBAccess temp = new DBAccess("AdventureWorks", @"D:\Projects\ACA\HW\DataAccessLayer\DAL\Queries.txt");
            temp.GetData("Get_Emp_Man", new KeyValuePair<string, object>("@BusinessEntityID", 5));
            temp.GetData("Get_Person_Inform", new KeyValuePair<string, object>(null, null));
            temp.GetData("Get_Product_Inform", new KeyValuePair<string, object>(null, null));
        }
    }
}
