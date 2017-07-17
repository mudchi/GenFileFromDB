using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TFRegLibrary.Utility;

namespace DAL.OracleDAL
{
    public class BaseGenFileDAL : BaseDLL, IGenFileDAL
    {

        public virtual DataTable getDataTable(string strKey, string strDate, ref string strHeader, ref string strFooter)
        {
            TFRegEntity.Table.BOTM_SYS_VALUE entBotmSysValue = new TFRegEntity.Table.BOTM_SYS_VALUE(strConStringInf, strOwnerInf);
            DataTable dtGetSql = entBotmSysValue.Select(string.Format("  strsyskey = '{0}' ", strKey.Replace("'", "''")));
            DataTable dtStructor = new DataTable();

            if (dtGetSql != null && dtGetSql.Rows.Count > 0)
            {
                string strSql = (dtGetSql.Rows[0]["STRSYSVAL"] ?? "").ToString().Replace("{DB}", strOwnerInf);
                strSql = string.Format(strSql, strDate);
                LogFileUtility.Sql(strProgram, strSql);
                dtStructor = TFLibrary.DBManager.OracleManager.ExecuteQuery(strConStringInf, strSql);

                strHeader = (dtGetSql.Rows[0]["STRSYSDFT"] ?? "").ToString();
                strFooter = (dtGetSql.Rows[0]["STRINPUSR"] ?? "").ToString();
            }

            return dtStructor = dtStructor == null ? new DataTable() : dtStructor;
        }
    }
}
