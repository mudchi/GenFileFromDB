// <copyright file="formMain.cs" company="Apar">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>CHIRAYUT WONGDANG</author>
// <date>25/05/2017 09:44:00 </date>
// <summary></summary>
using BLL.Interface;
using COMMON;
using COMMON.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TFRegLibrary.Utility;

namespace GenOutDMS
{
    public partial class formMain : Form
    {
        IApplicationContext applicationContext = ContextRegistry.GetContext();
        ISCHEDULEBLL<SCHEDULED_CONTROL> _ScheduleBLL = null;
        int intTimeRerun;
        string strProgram = AppConfig.PROGRAMNAME_DEFAULT;
        public void setInstace()
        {
            intTimeRerun = AppConfig.TIME_RERUN;
        }
        public formMain()
        {
            InitializeComponent();
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            KHead.Purpose = AppConfig.Description;
            KHead.Title = AppConfig.ProjectName;
            this.Text = KHead.Title;
            LogFileUtility.Info(this.strProgram, "Load program");
            this.txtCountDown.Text = string.Format(AppConfig.TimeOutText, intTimeRerun);
        }
        public formMain(string strDateRun, string strProgram, string strTypeRun, string strGroupProgram, string strPathShared, string strFileName)
        {
            try
            {
                InitializeComponent();
                LogFileUtility.Info(this.strProgram, string.Format("Start Program: {1} Runby: {0} TypeRun: {2}  ", strDateRun, strProgram, strTypeRun));
                DateTime dteDateRun = dtpTransactionDate.Value = DateTime.ParseExact(strDateRun, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                lblRunby.Text = strTypeRun;
                lblProgramGroup.Text = strGroupProgram;

                if (strTypeRun == AppConfig.TYPE_WEBCALL)
                {
                    // disable timer for run manual webcall
                    TimeAuto.Enabled = false;
                    TimeAuto.Stop();
                    //call program by param
                    ((IGENFILEBLL)applicationContext[strProgram]).start(strTypeRun, dtpTransactionDate.Value, strProgram, strPathShared, strFileName);

                    Environment.Exit(0);
                }
                else
                {
                    setInstace();

                    KHead.Purpose = strGroupProgram;
                    KHead.Title = strProgram;

                    LogFileUtility.Info(this.strProgram, "*******************************************************************************");
                    LogFileUtility.Info(this.strProgram, "Program Start On " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    LogFileUtility.Info(this.strProgram, "Default culture " + Thread.CurrentThread.CurrentCulture);
                    LogFileUtility.Info(this.strProgram, "Program group " + strGroupProgram);
                    LogFileUtility.Info(this.strProgram, "*******************************************************************************");
                    _ScheduleBLL = (ISCHEDULEBLL<SCHEDULED_CONTROL>)applicationContext["ScheduleBLL"];
                    _ScheduleBLL.setProgram(this.strProgram);
                    LogFileUtility.Info(this.strProgram, "Load Scheduled");
                    LogFileUtility.Info(this.strProgram, "--------------------------------------------------------------------------");
                    LogFileUtility.Info(this.strProgram, "* * * SystemDate : " + DateTime.Now.ToString("dd/MM/yyyy"));
                    LogFileUtility.Info(this.strProgram, "* * * Transaction Date : " + dteDateRun.ToString("dd/MM/yyyy"));
                    LogFileUtility.Info(this.strProgram, "--------------------------------------------------------------------------");
                    for (int i = 0; i < AppConfig.PREVIOUS_DAY_AUTO; i++) dteDateRun = _ScheduleBLL.getPreviosWorkDay(dteDateRun);
                    LogFileUtility.Info(this.strProgram, "* * * Change transaction Date : " + dteDateRun.ToString("dd/MM/yyyy"));
                    LogFileUtility.Info(this.strProgram, "--------------------------------------------------------------------------");


                    _ScheduleBLL.insertLogHeader(strRunby: strTypeRun, strSysDate: dteDateRun, strProgramName: strProgram);

                    AppConfig.LIST_SCHEDULED = _ScheduleBLL.getListById(strProgram, strGroupProgram);
                    LogFileUtility.Info(this.strProgram, string.Format("Get {0} list : {1}", AppConfig.LIST_SCHEDULED.Count(), string.Join(",", AppConfig.LIST_SCHEDULED.Select(s => s.PROGRAMNAME))));
                    var lstUpdate = AppConfig.LIST_SCHEDULED.Where(w => w.SETDATE != null && w.SETDATE != dteDateRun).ToList();
                    foreach (var item in lstUpdate)
                    {
                        LogFileUtility.Info(this.strProgram, string.Format("Setting program {0} is not run becuase setdate is {1}", item.PROGRAMNAME, item.SETDATE.Value.ToString("dd/MM/yyyy")));
                        item.SELECT = false;
                    }
                    if (AppConfig.LIST_SCHEDULED.Count() == 0)
                    {
                        LogFileUtility.Info(this.strProgram, "list not found for program " + strProgram + " ,  System will exit the program.");
                        _ScheduleBLL.insertLogDetail(strErrNo: "", strErrDesc: "", strRemark: "NONFOUND PROGRAM: " + strProgram + " GROUP: " + strGroupProgram, strModules: "LOADSCHEDULED", strStatus: "E");
                        Environment.Exit(1);
                    }

                    List<SCHEDULED_CONTROL> lstUpdateNextMonth = AppConfig.LIST_SCHEDULED.Where(w => w.SETDATE != null && w.SELECT).ToList();

                    if (lstUpdateNextMonth.Any()) _ScheduleBLL.setNextDayMonthly(lstUpdateNextMonth);

                    _ScheduleBLL.insertLogDetail(strErrNo: "", strErrDesc: "", strRemark: "LOAD PROGRAM : " + AppConfig.LIST_SCHEDULED.Count().ToString(), strModules: "LOADSCHEDULED", strStatus: "C");
                    TimeAuto.Enabled = true;
                    LogFileUtility.Info(this.strProgram, "--------------------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                LogFileUtility.Fatal(strProgram, ex);
                Environment.Exit(1);
            }

        }
        private void TimeAuto_Tick(object sender, EventArgs e)
        {
            try
            {
                intTimeRerun -= 1;
                txtCountDown.Text = string.Format(AppConfig.TimeOutText, intTimeRerun);
                if (intTimeRerun != 0) return;
                LogFileUtility.Info(this.strProgram, "***********************Start Timer tick***********************");
                foreach (var item in AppConfig.LIST_SCHEDULED)
                {
                    if (!item.SELECT) continue;
                    string strLogstatus = "";

                    LogFileUtility.Info(this.strProgram, string.Format("Begin program {0} status : {1}", item.PROGRAMNAME, item.STATUS));

                    if (item.STATUS.ToUpper() == "RUN")
                    {
                        strLogstatus = _ScheduleBLL.getLogStatus(item.PROGRAMNAME, DateTime.Now.ToString("yyyyMMdd"));
                        LogFileUtility.Info(this.strProgram, "\t\t- Get log status program " + item.PROGRAMNAME + " is status " + strLogstatus);
                        if (!string.IsNullOrEmpty(strLogstatus))
                        {
                            item.LOGSTATUS = strLogstatus;
                            if (item.LOGSTATUS == "C" || item.LOGSTATUS == "W")
                            {
                                LogFileUtility.Info(this.strProgram, "\t\t- Program run complete remove from task.");
                                item.STATUS = "COMPLETE";
                                item.SELECT = false;
                            }
                            if (item.LOGSTATUS == "E")
                            {
                                item.STATUS = "ERROR";
                            }
                        }

                    }
                    else if (item.STATUS.ToUpper() == "ERROR")
                    {
                        if (item.CONTINUEERR.Trim() == "N")
                        {
                            //ข้าม และ ไม่รันอีก
                            LogFileUtility.Info(this.strProgram, "\t\t- CONTINUEERR is N . Setting do not continue and remove from task");
                            item.SELECT = false;
                        }
                        else
                        {   // Y = error Continue Run

                            if (_ScheduleBLL.checkOverTimeLimit(item)) continue;

                            item.STATUS = "RUN";
                            item.LOGSTATUS = "";

                            LogFileUtility.Info(this.strProgram, string.Format("\t\t- Start Continue Run program  : {0} Path : {1} Filename : {2}", item.PROGRAMNAME, item.PATHGETFILE, item.FILENAME));
                            callProgram(item.PROGRAMNAME, item.PATHGETFILE, item.FILENAME);
                        }

                    }
                    else if (item.STATUS.ToUpper() == "NOT RUN")
                    {

                        if (_ScheduleBLL.checkWaitProgram(item)) continue;
                        if (_ScheduleBLL.checkOverTimeLimit(item)) continue;

                        item.STATUS = "RUN";
                        item.LOGSTATUS = "";

                        LogFileUtility.Info(this.strProgram, string.Format("\t\t- Start Run program : {0} Path : {1} Filename : {2}", item.PROGRAMNAME, item.PATHGETFILE, item.FILENAME));
                        callProgram(item.PROGRAMNAME, item.PATHGETFILE, item.FILENAME);

                    }

                }

                if (!AppConfig.LIST_SCHEDULED.Any(w => w.SELECT))
                {
                    LogFileUtility.Info(this.strProgram, "Exit Run all complete");
                    _ScheduleBLL.insertLogDetail(strErrNo: "", strErrDesc: "", strRemark: "", strModules: "EXIT RUN ALL COMPLETE", strStatus: "C");
                    _ScheduleBLL.updateLogHeader("C", "0");
                    LogFileUtility.Info(this.strProgram, "*******************************************************************************");
                    System.Environment.Exit(0);
                }

                setInstace();


            }
            catch (Exception ex)
            {
                _ScheduleBLL.insertLogDetail(strErrNo: "", strErrDesc: ex.ToString(), strRemark: "", strModules: "EXIT RUN ALL COMPLETE", strStatus: "E");
                LogFileUtility.Fatal(this.strProgram, ex);
            }
            LogFileUtility.Info(this.strProgram, "***********************End Timer tick***********************");
            LogFileUtility.Info(this.strProgram, "--------------------------------------------------------------------------");
        }
        private void callProgram(string strProgram, string strPath, string strFileName)
        {
            try
            {
                Thread thread = new Thread(() => ((IGENFILEBLL)applicationContext[strProgram]).start(lblRunby.Text, dtpTransactionDate.Value, strProgram, strPath, strFileName));
                thread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
                thread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                thread.Start();
            }
            catch (Exception ex)
            {
                LogFileUtility.Fatal(this.strProgram, ex);
            }
        }
    }
}
