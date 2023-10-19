using System;
using System.Collections.Generic;

namespace TickitingSystems.DBEntities
{
    public partial class Serverity
    {
        public Serverity()
        {
            IssueDetails = new HashSet<IssueDetail>();
        }

        public int ServerityId { get; set; }
        public string ServerityName { get; set; }
        public int ResponseTime { get; set; }

        public virtual ICollection<IssueDetail> IssueDetails { get; set; }
    }
}
