using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMMON.Model
{
    public class BOTT_LOG_DETAIL
    {
        public string STRPROGRAMNAME { get; set; }
        public DateTime? DTEHEADRUNDATE { get; set; }
        public DateTime? DTEDETAILRUNDATE { get; set; }
        public string STRMODULES { get; set; }
        public string STRERRORNO { get; set; }
        public string STRERRORDESC { get; set; }
        public string STRREMARK { get; set; }
        public string STRMODULESTS { get; set; }
    }
}
