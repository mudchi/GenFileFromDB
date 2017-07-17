// <copyright file="BaseBLL.cs" company="Apar">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>CHIRAYUT WONGDANG</author>
// <date>19/05/2017 09:44:00 </date>
// <summary>Base class business doses not edit this internal business except found bug if you want to add more work you can override in your class.</summary>

using BLL.Interface;
using COMMON.Model;
using DAL.Interface;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    public abstract class BaseBLL : Interface.IBLL
    {

        protected IApplicationContext _applicationContext = null;
        public string strProgramLog = "";
        protected IDAL _BottLogHeaderDAL = null;
        protected IDAL _BottLogDetailDAL = null;
        protected BOTT_LOG_HEADER objLogHeader = new BOTT_LOG_HEADER();
        protected BOTT_LOG_DETAIL objLogDetail = new BOTT_LOG_DETAIL();
        public BaseBLL()
        {
            _applicationContext = ContextRegistry.GetContext();
            _BottLogHeaderDAL = (IDAL)_applicationContext["BottLogHeaderDAL"];
            _BottLogDetailDAL = (IDAL)_applicationContext["BottLogDetailDAL"];
        }

        public virtual bool insertLogHeader(string strRunby, DateTime strSysDate, string strProgramName)
        {
            objLogDetail.STRPROGRAMNAME = objLogHeader.STRPROGRAMNAME = strProgramName;
            objLogDetail.DTEHEADRUNDATE = objLogHeader.DTERUNDATE = DateTime.Now;
            objLogHeader.STRSERVERNAME = Environment.MachineName;
            objLogHeader.STRRUNBY = strRunby;
            objLogHeader.DTESTART = DateTime.Now;
            objLogHeader.DTERERUNDATE = strSysDate;
            objLogHeader.STRPATH = Application.StartupPath;
            objLogHeader.STRSTATUS = "R";
            return _BottLogHeaderDAL.Insert<BOTT_LOG_HEADER>(objLogHeader);
        }
        public virtual bool updateLogHeader(string strStatusHead, string strReturn)
        {
            objLogHeader.STRSTATUS = strStatusHead;
            objLogHeader.STRRETURN = strReturn;
            objLogHeader.DTESTOP = DateTime.Now;
            return _BottLogHeaderDAL.Update<BOTT_LOG_HEADER>(objLogHeader);
        }
        public virtual bool insertLogDetail(string strErrNo, string strErrDesc, string strRemark, string strModules, string strStatus)
        {
            objLogDetail.STRERRORNO = strErrNo;
            objLogDetail.STRERRORDESC = strErrDesc;
            objLogDetail.STRREMARK = strRemark;
            objLogDetail.STRMODULESTS = strStatus;
            objLogDetail.DTEDETAILRUNDATE = DateTime.Now;
            objLogDetail.STRMODULES = strModules;
            return _BottLogDetailDAL.Insert<BOTT_LOG_DETAIL>(objLogDetail);
        }
        public virtual void setProgram(string strProgram)
        {
            throw new NotImplementedException();
        }

    }
}
