using System;
using System.Collections.Generic;

namespace Lottery.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Lots = new HashSet<Lot>();
            UserInfos = new HashSet<UserInfo>();
        }

        public int PhotoId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
