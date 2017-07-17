using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMMON.Model
{
    public class BOTT_LOG_HEADER
    {
        public string STRPROGRAMNAME { get; set; }
        public DateTime DTERUNDATE { get; set; }
        public string STRSERVERNAME { get; set; }
        public string STRRUNBY { get; set; }
        public DateTime DTESTART { get; set; }
        public string STRSTATUS { get; set; }
        public string STRRETURN { get; set; }
        public string STRPATH { get; set; }
        public DateTime DTERERUNDATE { get; set; }
        public DateTime DTESTOP { get; set; }

    }
}
