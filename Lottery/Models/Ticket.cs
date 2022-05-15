using System;
using System.Collections.Generic;

namespace Lottery.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Draws = new HashSet<Draw>();
        }

        public int TicketId { get; set; }
        public int? LotId { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }

        public virtual Lot Lot { get; set; }
        public virtual UserInfo User { get; set; }
        public virtual ICollection<Draw> Draws { get; set; }
    }
}
