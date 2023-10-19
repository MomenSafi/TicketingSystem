using System;
using System.Collections.Generic;

namespace TickitingSystems.DBEntities
{
    public partial class IssueStatus
    {
        public IssueStatus()
        {
            IssueDetails = new HashSet<IssueDetail>();
            IssueDetailsLogs = new HashSet<IssueDetailsLog>();
        }

        public int IssueStatesId { get; set; }
        public string IssueStatesName { get; set; }

        public virtual ICollection<IssueDetail> IssueDetails { get; set; }
        public virtual ICollection<IssueDetailsLog> IssueDetailsLogs { get; set; }
    }
}
