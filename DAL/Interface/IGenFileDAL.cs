using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IGenFileDAL : IDAL
    {
        DataTable getDataTable(string strKey, string strDate, ref string strHeader, ref string strFooter);
    }
}
