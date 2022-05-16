using System;
using System.Collections.Generic;

namespace Lottery.Models
{
    public partial class Address
    {
        public Address()
        {
            UserInfos = new HashSet<UserInfo>();
        }

        public int AddressId { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
