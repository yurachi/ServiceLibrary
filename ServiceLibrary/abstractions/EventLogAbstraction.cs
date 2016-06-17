using System.Diagnostics;

namespace GERS_HRO_Service
{
    public class EventLogAbstraction : EventLog
    {
        public new virtual void WriteEntry(string message, EventLogEntryType type)
        {
            //TODO: error-handling (especially permissions)
            base.WriteEntry(message,type);
        }
    }
}
