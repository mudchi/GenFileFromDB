using COMMON;
using COMMON.Model;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TFRegLibrary.Utility;
using Microsoft.VisualBasic;
using TFLibrary.DBManager;

namespace DAL.OracleDAL
{
    public class BottLogHeaderDAL : BaseDLL
    {


        public override bool Insert<T>(T obj)
        {
            BOTT_LOG_HEADER objIns = obj as BOTT_LOG_HEADER;
            string strSql = @" INSERT INTO BOTT_LOG_HEADER 
(STRPROGRAMNAME,DTERUNDATE,STRSERVERNAME,STRRUNBY,DTESTART 
, STRSTATUS, STRRETURN, STRPATH,DTERERUNDATE ) VALUES(";
            strSql += "  '" + objIns.STRPROGRAMNAME.Trim() + "'";//STRPROGRAMNAME
            strSql += " ,TO_DATE( '" + objIns.DTERUNDATE.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS') ";
            //DTERUNDATE
            strSql += " ,'" + objIns.STRSERVERNAME.Trim() + "'";
            //STRSERVERNAME
            strSql += " ,'" + Strings.Left(objIns.STRRUNBY, 30).Trim() + "' ";
            //STRRUNBY
            strSql += " ,TO_DATE( '" + objIns.DTESTART.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS') ";
            //DTESTART
            strSql += " ,'" + Strings.Left(objIns.STRSTATUS, 1).Trim() + "' ";
            //STRSTATUS
            strSql += " ,'" + Strings.Left(objIns.STRRETURN, 1).Trim() + "' ";
            //STRRETURN
            strSql += " ,'" + Strings.Left(objIns.STRPATH, 200).Trim() + "' ";
            //STRPATH
            strSql += " ,TO_DATE( '" + objIns.DTERERUNDATE.ToString("dd/MM/yyyy") + "', 'dd/MM/yyyy') ";
            //DTERERUNDATE
            strSql += " )";
            LogFileUtility.Sql(strProgram, strSql);
            return OracleManager.ExecuteNonQuery(strConStringInf, strSql) >= 0;
        }


        public override bool Update<T>(T obj)
        {
            BOTT_LOG_HEADER objIns = obj as BOTT_LOG_HEADER;

            string strSql = " UPDATE BOTT_LOG_HEADER  SET ";
            strSql += " STRSERVERNAME = '" + Strings.Left(objIns.STRSERVERNAME, 30) + "'";
            //STRSERVERNAME
            strSql += " ,STRRUNBY = '" + Strings.Left(objIns.STRRUNBY, 30) + "' ";
            //STRRUNBY
            strSql += " ,DTESTOP = TO_DATE( '" + objIns.DTESTOP.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS')";
            //DTESTOP
            if (Strings.Left(objIns.STRSTATUS, 1).Trim() != "")
            {
                strSql += " ,STRSTATUS = '" + Strings.Left(objIns.STRSTATUS, 1).Trim() + "' ";
                //STRSTATUS
            }
            strSql += " ,STRRETURN = '" + Strings.Left(objIns.STRRETURN, 1).Trim() + "' ";
            //STRRETURN
            strSql += " WHERE STRPROGRAMNAME='" + objIns.STRPROGRAMNAME + "'";
            strSql += " AND DTERUNDATE= TO_DATE( '" + objIns.DTERUNDATE.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS')";
            LogFileUtility.Sql(strProgram, strSql);

            return OracleManager.ExecuteNonQuery(strConStringInf, strSql) >= 0;
        }

        public override DataTable getById(string key1, string key2)
        {
            string strSql = string.Format(RSSQL.SQL_GET_SCHEDULED, AppConfig.InfOwner, key1.Trim().Replace("'", "''"), key2.Trim().Replace("'", "''"));
            LogFileUtility.Sql(strProgram, strSql);
            return OracleManager.ExecuteQuery(strConStringInf, strSql);
        }

        public override string getStringById(string key1, string key2)
        {
            string strSql = string.Format(RSSQL.SQL_GET_LOGSTATUS, AppConfig.InfOwner, key1.Trim().Replace("'", "''"), key2.Trim().Replace("'", "''"));
            LogFileUtility.Sql(strProgram, strSql);
            return (OracleManager.ExecuteScalar(strConStringInf, strSql) ?? "").ToString();
        }


    }
}
