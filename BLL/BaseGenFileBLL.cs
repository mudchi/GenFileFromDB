// <copyright file="BaseGenFileBLL.cs" company="Apar">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>CHIRAYUT WONGDANG</author>
// <date>30/05/2017 17:12:00 </date>
// <summary>Base class loadfile doses not edit this internal business except found bug if you want to add more work you can override in your class.</summary>


using BLL.Interface;
using COMMON;
using DAL.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TFRegLibrary.Utility;

namespace BLL
{
    public class BaseGenFileBLL : BaseBLL, IGENFILEBLL
    {

        /// <summary>
        /// Setting default format date for set filename ex.YYYYMMDD
        /// </summary>
        protected string strFormatDateFile = AppConfig.DefaultFormatDateFile;
        /// <summary>
        /// Setting PrefixSysKey for get sql Schema ex.SQL_IM_BT_DMSFTX
        /// </summary>
        protected string strPrefixSysKey = "";
        /// <summary>
        /// Setting default format date for set header date ex.YYYY-MM-DD
        /// </summary>
        protected string strFormatDateHeader = AppConfig.DefaultFormatDateHeader;
        /// <summary>
        /// Setting default format date for set header date ex.where FT_DATE=YYYYMMDD
        /// </summary>
        protected string strFormatDateWhere = AppConfig.DefaultFormatDateWhere;

        protected IGenFileDAL _GenFileDAL = null;

        protected bool IsAuto = false;
        protected List<int> lstLineErrFill = new List<int>();
        protected int intStep = 1;
        protected int intTotalrows = 0;
        protected int intTotal = 0;
        public override void setProgram(string strProgram)
        {
            strProgramLog = strProgram;
            _BottLogHeaderDAL.setProgram(strProgram);
            _BottLogDetailDAL.setProgram(strProgram);
            _GenFileDAL = (IGenFileDAL)_applicationContext["BaseGenFileDAL"];
            _GenFileDAL.setProgram(strProgram);
        }
        public virtual void start(string strRunby, DateTime dteRunDate, string strProgram, string strPathShared, string strFileName)
        {
            string strPathFileSave = "";
            string strHeader = "";
            string strFooter = "";
            try
            {
                strPrefixSysKey = AppConfig.PREFIX_SYSKEY_SQL + strProgram.Substring(3);

                setProgram(strProgram);

                LogFileUtility.Info(strProgramLog, "*******************************************************************************");
                LogFileUtility.Info(strProgramLog, "Program Start On " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                LogFileUtility.Info(strProgramLog, "Default culture " + Thread.CurrentThread.CurrentCulture);
                LogFileUtility.Info(strProgramLog, "*******************************************************************************");

                LogFileUtility.Info(strProgramLog, string.Format("{0}.Insert LogHeader Runby : {1} ,Programname : {2} ,RunDate : {3},PathFile : {4}, FileName : {5}", (intStep++), strRunby.ToUpper(), strProgram, dteRunDate.ToString("dd/MM/yyyy"), strPathShared, strFileName));
                insertLogHeader(strRunby, dteRunDate, strProgram);

                LogFileUtility.Info(strProgramLog, string.Format("{0}.Change last string of path file to \\", (intStep++)));
                if (!strPathShared.EndsWith("\\")) strPathShared += "\\";


                LogFileUtility.Info(strProgramLog, string.Format("{0}.Change file name {1} add date {2}", (intStep++), strFileName, DateTime.Now.ToString(strFormatDateFile)));
                strFileName = string.Format(strFileName, DateTime.Now.ToString(strFormatDateFile));
                LogFileUtility.Info(strProgramLog, string.Format("\t- Set file name {0}", strFileName));
                strPathFileSave = Path.Combine(strPathShared, strFileName);
                LogFileUtility.Info(strProgramLog, string.Format("\t- Set path file {0}", strPathFileSave));


                LogFileUtility.Info(strProgramLog, string.Format("{0}.Check exists directory path {1} ", (intStep++), strPathShared));
                if (!Directory.Exists(strPathShared))
                {
                    LogFileUtility.Info(strProgramLog, "\t- Directory not found");
                    insertLogDetail("", "Directory not found path " + strPathShared, "", "", "E");
                    updateLogHeader("E", "1");
                    return;
                }
                LogFileUtility.Info(strProgramLog, string.Format("\t- Directory exists"));


                LogFileUtility.Info(strProgramLog, string.Format("{0}.Check exists text file {1} ", (intStep++), strPathFileSave));

                if (File.Exists(strPathFileSave))
                {
                    LogFileUtility.Info(strProgramLog, string.Format("\t- Delete file {0} ", strPathFileSave));
                    File.Delete(strPathFileSave);
                }


                LogFileUtility.Info(strProgramLog, string.Format("{0}.GetData from syskey {1} ", (intStep++), strPrefixSysKey));
                DataTable dtData = _GenFileDAL.getDataTable(strPrefixSysKey, dteRunDate.ToString(strFormatDateWhere), ref strHeader, ref strFooter);
                intTotal = dtData.Rows.Count;
                insertLogDetail("", string.Format("Get data from syskey {0} : {1} records", strPrefixSysKey, intTotal), "", "", "C");
                LogFileUtility.Info(strProgramLog, string.Format("\t- Total {0} Rows ", intTotal));

                LogFileUtility.Info(strProgramLog, string.Format("{0}.Write to file total {1} rows ", (intStep++), intTotal));
                writeFileFromData(dtData, dteRunDate, strPathFileSave, strHeader, strFooter);
                LogFileUtility.Info(strProgramLog, string.Format("\t- Success {0}/{1} ", intTotalrows, intTotal));


                if (!lstLineErrFill.Any())
                {
                    insertLogDetail("", string.Format("Total Rows : {0}/{1} records", intTotalrows, intTotal), "", "", "C");
                    updateLogHeader("C", "0");
                }
                else
                {
                    if (lstLineErrFill.Any()) insertLogDetail("", " Error fill data Line : " + string.Join(",", lstLineErrFill) + string.Format(" ( Total Rows Insert : {0}/{1} records. )", intTotalrows, intTotal), "", "", "W");
                    updateLogHeader("W", "0");
                }



            }
            catch (Exception ex)
            {
                LogFileUtility.Fatal(strProgramLog, ex);
                if (lstLineErrFill.Any()) insertLogDetail("", " Error fill data Line : " + string.Join(",", lstLineErrFill) + string.Format(" ( Total Rows Insert : {0}/{1} records. )", intTotalrows, intTotal), "", "", "E");
                insertLogDetail("", ex.ToString(), "", "", "E");
                updateLogHeader("E", "1");
            }
        }
        protected virtual void writeFileFromData(DataTable dtData, DateTime dteRunDate, string strPathFileSave, string strHeader, string strFooter)
        {

            using (var objStrWriter = new StreamWriter(strPathFileSave, true))
            {
                strHeader = string.Format(strHeader, dteRunDate.ToString(strFormatDateHeader));
                objStrWriter.WriteLine(strHeader);
                for (int i = 0; i < intTotal; i++)
                {
                    string strDataInRows = "";
                    try
                    {
                        for (int j = 0; j < dtData.Columns.Count; j++)
                        {
                            if (j > 0) strDataInRows += "|";
                            strDataInRows += (dtData.Rows[i][j] ?? "").ToString();
                        }

                        objStrWriter.WriteLine(strDataInRows);
                        intTotalrows++;
                    }
                    catch (Exception ex)
                    {
                        LogFileUtility.Fatal(ex);
                        lstLineErrFill.Add(i + 1);
                    }

                }
                objStrWriter.WriteLine(string.Format(strFooter, intTotalrows));
            }
        }
    }
}
