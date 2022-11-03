using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Basic { 
    public class XMessage
    {
        public string TypeMsg { get; set; }
        public string Msg { get; set; }
        public XMessage() { }
        public XMessage(string typeMsg, string msg)
        {
            this.TypeMsg = typeMsg;
            this.Msg = msg;
        }
    }
}