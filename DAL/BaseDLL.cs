using COMMON;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TFRegLibrary.Utility;

namespace DAL
{
    public class BaseDLL : IDAL
    {
        public string strConStringInf = AppConfig.ORACLE_INF;
        public string strConStringDWH = AppConfig.ORACLE_DWH;
        public string strConStringTN = AppConfig.ORACLE_TN;
        public string strOwnerInf = AppConfig.InfOwner;
        public string strOwnerDwh = AppConfig.DwhOwner;
        public string strOwnerTn = AppConfig.TNOwner;
        public string strProgram = "";

        public void setProgram(string strProgram)
        {
            this.strProgram = strProgram;
        }
        public virtual DataTable getById(string strKey)
        {
            throw new NotImplementedException();
        }
        public virtual DataTable getById(string strKey1, string strKey2)
        {
            throw new NotImplementedException();
        }
        public virtual bool Insert<T>(T obj) where T : class, new()
        {
            throw new NotImplementedException();
        }
        public virtual string getStringById(string strKey1, string strKey2)
        {
            throw new NotImplementedException();
        }
        public virtual bool Update<T>(T obj) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(DateTime dteKey)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(string strKey)
        {
            throw new NotImplementedException();
        }

        public virtual string getStringById(string strKey1)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(string strKey1, string strValue1)
        {
            throw new NotImplementedException();
        }
    }
}
