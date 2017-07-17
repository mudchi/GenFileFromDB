using COMMON.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Interface
{
    public interface ISCHEDULEBLL<T> : IBLL where T : class, new()
    {
        bool checkOverTimeLimit(SCHEDULED_CONTROL item);
        bool checkWaitProgram(SCHEDULED_CONTROL item);
        string getLogStatus(string strProgram, string strDate);
        List<T> getListById(string strKey1, string strKey2);
        DateTime getPreviosWorkDay(DateTime dteCheckDate);
        bool setNextDayMonthly(List<SCHEDULED_CONTROL> lstSchedule);

    }
}
