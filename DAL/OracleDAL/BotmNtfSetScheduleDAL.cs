using COMMON;
using COMMON.Model;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TFLibrary.DBManager;
using TFRegLibrary.Utility;

namespace DAL.OracleDAL
{
    public class BotmNtfSetScheduleDAL : BaseDLL
    {
        public override DataTable getById(string strKey1, string strKey2)
        {
            string strSql = string.Format(RSSQL.SQL_GET_SCHEDULED, strOwnerInf, strKey1, strKey2);
            LogFileUtility.Sql(strProgram, strSql);
            return OracleManager.ExecuteQuery(strConStringInf, strSql);
        }

        /// <summary>
        /// get check holiday 
        /// </summary>
        /// <param name="strKey1">Date ex.20170510</param>
        /// <returns>1 is holiday , 0 is work day</returns>
        public override string getStringById(string strKey1)
        {
            string strSql = string.Format(RSSQL.SQL_GET_FN_HOLIDAY, strOwnerTn, strKey1);
            LogFileUtility.Sql(strProgram, strSql);
            return OracleManager.ExecuteScalar(strConStringTN, strSql).ToString();
        }



        public override bool Update(string strKey1, string strValue1)
        {
            string strSql = string.Format("UPDATE {0}.BOTM_NTF_SET_SCHEDULE SET SETDATE=TO_DATE('{1}','yyyyMMdd') WHERE RECNO='{2}'", strOwnerInf, strValue1, strKey1);
            LogFileUtility.Sql(strProgram, strSql);
            return OracleManager.ExecuteNonQuery(strConStringInf, strSql) > 0;
        }



    }
}
