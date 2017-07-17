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
    public class BottLogDetailDAL : BaseDLL
    {

        public override bool Insert<T>(T objIns)
        {
            BOTT_LOG_DETAIL obj = objIns as BOTT_LOG_DETAIL;
            string strSql = "";
            strSql += " INSERT INTO BOTT_LOG_DETAIL ";
            strSql += " (STRPROGRAMNAME,DTEHEADRUNDATE,DTEDETAILRUNDATE,STRMODULES,";
            strSql += " STRERRORNO,STRERRORDESC,STRREMARK,STRMODULESTS )";
            strSql += " VALUES(";
            strSql += "  '" + Strings.Left(obj.STRPROGRAMNAME,30).Trim().Replace("'", "''") + "'";
            //STRPROGRAMNAME
            strSql += " , TO_DATE( '" + obj.DTEHEADRUNDATE.Value.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS') ";
            //DTEHEADRUNDATE
            strSql += " , TO_DATE( '" + obj.DTEDETAILRUNDATE.Value.ToString("dd/MM/yyyy HH:mm:ss") + "', 'dd/MM/yyyy HH24:MI:SS') ";
            //DTEDETAILRUNDATE
            strSql += " ,'" + Strings.Left(obj.STRMODULES, 30).Trim().Replace("'", "''") + "' ";
            //STRMODULES
            strSql += " ,'" + Strings.Left(obj.STRERRORNO, 30).Trim().Replace("'", "''") + "' ";
            //STRERRORNO
            strSql += " ,'" + Strings.Left(obj.STRERRORDESC, 200).Trim().Replace("'", "''") + "' ";
            //STRERRORDESC
            strSql += " ,'" + Strings.Left(obj.STRREMARK, 30).Trim().Replace("'", "''") + "' ";
            //STRREMARK
            strSql += " ,'" + Strings.Left(obj.STRMODULESTS, 1).Trim().Replace("'", "''") + "' ";
            //STRMODULESTS
            strSql += " )";
            LogFileUtility.Sql(strProgram, strSql);
            return OracleManager.ExecuteNonQuery(strConStringInf, strSql) >= 0;
        }


      


    }
}
