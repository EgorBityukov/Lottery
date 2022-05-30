using System;
using System.Collections.Generic;

namespace Lottery.Models
{
    public partial class Draw
    {
        public int DrawId { get; set; }
        public string WinnerId { get; set; }
        public DateTime? Date { get; set; }
        public int? LotId { get; set; }
        public int? TicketId { get; set; }
        public string Status { get; set; }

        public virtual Lot Lot { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual UserInfo Winner { get; set; }
    }
}
