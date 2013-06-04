using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CbhLib.diag
{
    public class WinEventLog
    {
        private string EVENTLOG_SOURCE = "Genesis";
        private string EVENTLOG_LOG = "Application";

        public WinEventLog(string source)
        {
            EVENTLOG_SOURCE = source;
        }
        public WinEventLog(string log, string source)
        {
            EVENTLOG_LOG = log;
            EVENTLOG_SOURCE = source;
        }

        private void AddEntry(string sSource, string sLog, string sEvent, EventLogEntryType eventLogType, int eventId)
        {
            try
            {
                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);
                EventLog.WriteEntry(sSource, sEvent, eventLogType, eventId);
            }
            catch (Exception E)
            {
                Trace.WriteLine(GetCompleteException(E));
            }
        }
        private void AddEventLogEntry(string sEvent, EventLogEntryType eventLogType, int eventId)
        {
            string sSource = EVENTLOG_SOURCE;
            string sLog = EVENTLOG_LOG;
            try
            {
                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);
                EventLog.WriteEntry(sSource, sEvent, eventLogType, eventId);
            }
            catch (Exception E)
            {
                Trace.WriteLine(GetCompleteException(E));
            }
        }

        public void AddEventLogException(System.Exception E)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Exception: " + GetCompleteException(E));
            AddEventLogEntry(sb.ToString(), EventLogEntryType.Error, 0);
        }

        public string GetCompleteException(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            while (e != null)
            {
                sb.AppendLine(e.Message);
                e = e.InnerException;
            }
            return sb.ToString();
        }
    }
}
