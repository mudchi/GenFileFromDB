using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMMON.Model
{
    public class SCHEDULED_CONTROL
    {
        public bool SELECT { get; set; }
        public decimal NO { get; set; }
        public string RECNO { get; set; }
        public string PROGRAMNAME { get; set; }
        public string PROGRAMDESC { get; set; }
        public string STATUS { get; set; }
        public string CONTINUEERR { get; set; }
        public string WAITPROGRAM { get; set; }
        public string TIMELIMIT { get; set; }
        public string PATHEXE { get; set; }
        public string PROGRAMTYPE { get; set; }
        public string PROGRAMGROUP { get; set; }
        public string PATHGETFILE { get; set; }
        public string FILENAME { get; set; }
        public string LOGSTATUS { get; set; }
        public DateTime? SETDATE { get; set; }
    }
}
