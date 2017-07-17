// <copyright file="ScheduleBLL.cs" company="Apar">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>CHIRAYUT WONGDANG</author>
// <date>25/05/2017 09:44:00 </date>
// <summary></summary>

using BLL.Interface;
using COMMON;
using COMMON.Model;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TFRegLibrary.Utility;

namespace BLL.Core
{
    public class ScheduleBLL : BaseBLL, ISCHEDULEBLL<SCHEDULED_CONTROL>
    {
        private IDAL _ScheduleDAL = null;

        public ScheduleBLL() : base()
        {
            _ScheduleDAL = (IDAL)_applicationContext["BotmNtfSetScheduleDAL"];
        }

        public override void setProgram(string strProgram)
        {
            this.strProgramLog = strProgram;
            _ScheduleDAL.setProgram(strProgram);
            _BottLogHeaderDAL.setProgram(strProgram);
            _BottLogDetailDAL.setProgram(strProgram);
        }

        public List<SCHEDULED_CONTROL> getListById(string strKey1, string strKey2)
        {
            List<SCHEDULED_CONTROL> lst = new List<SCHEDULED_CONTROL>();
            DataTable dt = _ScheduleDAL.getById(strKey1, strKey2);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lst.Add(new SCHEDULED_CONTROL()
                {
                    SELECT = true,
                    NO = i + 1,
                    RECNO = (dt.Rows[i]["RECNO"] ?? "").ToString(),
                    PROGRAMNAME = (dt.Rows[i]["PROGRAMNAME"] ?? "").ToString(),
                    PROGRAMDESC = (dt.Rows[i]["PROGRAMDESC"] ?? "").ToString(),
                    STATUS = "NOT RUN",
                    CONTINUEERR = (dt.Rows[i]["CONTINUEERR"] ?? "").ToString(),
                    WAITPROGRAM = (dt.Rows[i]["WAITPROGRAM"] ?? "").ToString(),
                    TIMELIMIT = (dt.Rows[i]["TIMELIMIT"] ?? "").ToString(),
                    PATHEXE = (dt.Rows[i]["PATHEXE"] ?? "").ToString(),
                    PROGRAMTYPE = (dt.Rows[i]["PROGRAMTYPE"] ?? "").ToString(),
                    PROGRAMGROUP = (dt.Rows[i]["PROGRAMGROUP"] ?? "").ToString(),
                    PATHGETFILE = (dt.Rows[i]["PATHGETFILE"] ?? "").ToString(),
                    FILENAME = (dt.Rows[i]["FILENAME"] ?? "").ToString(),
                    SETDATE = dt.Rows[i]["SETDATE"] == DBNull.Value ? null : (DateTime?)Convert.ChangeType(dt.Rows[i]["SETDATE"], typeof(DateTime)),
                    LOGSTATUS = ""
                });

            }
            return lst;
        }

        public string getLogStatus(string strProgram, string strDate)
        {
            return _BottLogHeaderDAL.getStringById(strDate, strProgram);
        }

        public bool checkOverTimeLimit(SCHEDULED_CONTROL item)
        {
            LogFileUtility.Info(strProgramLog, string.Format("Check limit time {0} Time now : {1}", (string.IsNullOrEmpty(item.TIMELIMIT) ? "-" : item.TIMELIMIT), DateTime.Now.ToString("HH:mm:ss")));
            bool isOver = true;
            if (!string.IsNullOrEmpty(item.TIMELIMIT.Trim()))
            {
                if (Convert.ToDecimal(item.TIMELIMIT.Trim().Replace(":", "")) > Convert.ToDecimal(DateTime.Now.ToString("HHmmss")))
                {
                    LogFileUtility.Info(strProgramLog, "\t- Pass");
                    isOver = false;
                }
                else
                {
                    LogFileUtility.Info(strProgramLog, "\t- Do not run because it's over time limit : " + item.TIMELIMIT.Trim());
                    item.SELECT = false;
                    isOver = true;
                }
            }
            else
            {
                if (item.STATUS.ToUpper() == "ERROR")
                {
                    LogFileUtility.Info(strProgramLog, "\t- Status ERROR Do not run because time limit is empty ");
                    item.SELECT = false;
                    isOver = true;
                }
                else
                {
                    LogFileUtility.Info(strProgramLog, "\t- Pass");
                    isOver = false;
                }
            }

            return isOver;
        }

        public DateTime getPreviosWorkDay(DateTime dteCheckDate)
        {
            do
            {
                dteCheckDate = dteCheckDate.AddDays(-1);
                LogFileUtility.Info(strProgramLog, "\t- Change run date to " + dteCheckDate.ToString("dd/MM/yyyy") + " Day : " + dteCheckDate.DayOfWeek.ToString());
            } while (dteCheckDate.DayOfWeek == DayOfWeek.Sunday || dteCheckDate.DayOfWeek == DayOfWeek.Saturday || checkHoliday(dteCheckDate));

            return dteCheckDate;
        }

        public bool checkHoliday(DateTime dteCheckDate)
        {
            bool isHoliday = true;
            LogFileUtility.Info(strProgramLog, "\t\t- Start check working date : " + dteCheckDate.ToString("yyyyMMdd"));
            string strGetData = _ScheduleDAL.getStringById(dteCheckDate.ToString("yyyyMMdd"));
            LogFileUtility.Info(strProgramLog, "\t\t\t- recieved value : " + strGetData);
            if (strGetData == "1")
            {
                isHoliday = true;
                LogFileUtility.Info(strProgramLog, "\t\t\t- Check working date : FAIL : Today is holiday ");
            }
            else if (strGetData == "0")
            {
                isHoliday = false;
                LogFileUtility.Info(strProgramLog, "\t\t\t- Check working date : PASS ");
            }
            return isHoliday;
        }

        public bool checkWaitProgram(SCHEDULED_CONTROL item)
        {
            bool isWait = true;
            string[] arrWaitProgram = item.WAITPROGRAM.Split(',').Where(s => !string.IsNullOrEmpty(s.Trim())).ToArray();
            if (arrWaitProgram.Length > 0)
            {
                LogFileUtility.Info(strProgramLog, "Wait program : " + item.WAITPROGRAM);
                for (int r = 0; r <= arrWaitProgram.Length - 1; r++)
                {
                    if (string.IsNullOrEmpty(arrWaitProgram[r].Trim()))
                        break;
                    int intRow = Convert.ToInt32(arrWaitProgram[r].Trim());

                    if (AppConfig.LIST_SCHEDULED[intRow - 1].STATUS.ToUpper() == "COMPLETE")
                    {
                        isWait = false;
                        LogFileUtility.Info(strProgramLog, "\t\t- Program " + AppConfig.LIST_SCHEDULED[intRow - 1].PROGRAMNAME + " status complete.");
                    }
                    else if (AppConfig.LIST_SCHEDULED[intRow - 1].STATUS.ToUpper() == "ERROR")
                    {
                        if (!AppConfig.LIST_SCHEDULED[intRow - 1].SELECT)
                        {
                            LogFileUtility.Info(strProgramLog, "\t\t- Wait Program " + AppConfig.LIST_SCHEDULED[intRow - 1].PROGRAMNAME + " but It is error and not run.So remove program " + item.PROGRAMNAME + " from task.");
                            item.SELECT = false;
                        }

                        isWait = true;
                        break;  // End of loop
                    }
                    else
                    {
                        if (!AppConfig.LIST_SCHEDULED[intRow - 1].SELECT)
                        {
                            LogFileUtility.Info(strProgramLog, "\t\t- Wait Program " + AppConfig.LIST_SCHEDULED[intRow - 1].PROGRAMNAME + " but It is not run.So remove program " + item.PROGRAMNAME + " from task.");
                            item.SELECT = false;
                        }
                        isWait = true;
                        break; // End of loop
                    }
                }
            }
            else
            {
                isWait = false;
            }
            return isWait;
        }

        public bool setNextDayMonthly(List<SCHEDULED_CONTROL> lstSchedule)
        {
            bool isComplete = false;
            DateTime dteRunDate = lstSchedule.Select(s => s.SETDATE.Value).FirstOrDefault();
            DateTime dteNextDate = new DateTime(dteRunDate.Year, dteRunDate.Month + 1, int.Parse(AppConfig.MONTHLY_RUN_DATE));

            LogFileUtility.Info(strProgramLog, "Set next run date monthly");

            while ((dteNextDate - dteRunDate).Days < 20)
            {
                dteNextDate = new DateTime(dteNextDate.Year, dteNextDate.Month + 1, int.Parse(AppConfig.MONTHLY_RUN_DATE));
            }

            for (int i = 0; i < AppConfig.PREVIOUS_DAY_AUTO + AppConfig.PREVIOUS_DAY_AUTO_SCH_MAIN; i++) dteNextDate = getPreviosWorkDay(dteNextDate);

            foreach (var item in lstSchedule)
            {
                LogFileUtility.Info(strProgramLog, "\t- Update " + item.RECNO + " next run to date : " + dteNextDate.ToString("yyyyMMdd"));
                _ScheduleDAL.Update(item.RECNO, dteNextDate.ToString("yyyyMMdd"));
            }
            return isComplete;
        }
    }
}
