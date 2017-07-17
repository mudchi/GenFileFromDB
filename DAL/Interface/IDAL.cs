using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IDAL
    {
        bool Insert<T>(T obj) where T : class, new();
        bool Update<T>(T obj) where T : class, new();
        bool Update(string strKey1, string strValue1);
        void Delete(DateTime dteKey);
        bool Delete(string strKey);
        DataTable getById(string strKey);
        DataTable getById(string strKey1, string strKey2);
        string getStringById(string strKey1);
        string getStringById(string strKey1, string strKey2);
        void setProgram(string strProgram);
    }
}
