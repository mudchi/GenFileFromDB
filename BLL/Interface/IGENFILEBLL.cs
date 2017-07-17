using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Interface
{
    public interface IGENFILEBLL
    {
        void start(string strRunby, DateTime dteRunDate, string strProgram, string strPathShared, string strFileName);
    }
}
