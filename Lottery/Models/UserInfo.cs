using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Lottery.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Draws = new HashSet<Draw>();
            Tickets = new HashSet<Ticket>();
        }

        public string Id { get; set; }
        public int? AddressId { get; set; }
        public decimal? Balance { get; set; }
        public int? PhotoId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<Draw> Draws { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
