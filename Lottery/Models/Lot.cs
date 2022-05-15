using System;
using System.Collections.Generic;

namespace Lottery.Models
{
    public partial class Lot
    {
        public Lot()
        {
            Draws = new HashSet<Draw>();
            Tickets = new HashSet<Ticket>();
        }

        public int LotId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? TicketCount { get; set; }
        public decimal? TicketPrice { get; set; }
        public int? PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual ICollection<Draw> Draws { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
