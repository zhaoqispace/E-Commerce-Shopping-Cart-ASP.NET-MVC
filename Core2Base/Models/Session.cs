using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Base.Models
{
    public class Session
    {
        public string SessionID { set; get; }
        public string UserID { set; get; }
        public string LoginTime { set; get; }
        public string LogoffTime { set; get; }
    }
}
