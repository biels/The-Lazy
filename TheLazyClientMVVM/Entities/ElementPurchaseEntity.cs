using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    class ElementPurchaseEntity
    {
        public int id { get; set; }
        public UserEntity user { get; set; } //Semi buit*
        public ElementEntity element { get; set; } //Semi buit*
        public int price { get; set; }
        public DateTime update_time { get; set; }
    }
}
