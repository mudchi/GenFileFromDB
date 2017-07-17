using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Interface
{
    public interface IBLL
    {
        void setProgram(string strProgram);
        bool insertLogHeader(string strRunby, DateTime strSysDate, string strProgramName);
        bool updateLogHeader(string strStatusHead, string strReturn);
        bool insertLogDetail(string strErrNo, string strErrDesc, string strRemark, string strModules, string strStatus);
    }
}
