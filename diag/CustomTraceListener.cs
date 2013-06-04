using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CbhLib.utility;

namespace CbhLib.diag
{
    public class CustomTraceListener : System.Diagnostics.TraceListener
    {
        private ThreadQueue<string> fIncomingQ;

        public CustomTraceListener(ref ThreadQueue<string> q)
        {
            fIncomingQ = q;
        }

        public override void Write(string message)
        {
            fIncomingQ.EnQ(message);
        }

        public override void WriteLine(string message)
        {
            fIncomingQ.EnQ(message + "\n");
        }
    }
}
