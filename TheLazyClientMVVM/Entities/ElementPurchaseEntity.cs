using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
   public class ElementPurchaseEntity
    {
        public int id { get; set; }
        public UserEntity user { get; set; } //Semi buit*
        public int price { get; set; }
        public int element_id { get; set; }
        public DateTime create_time { get; set; }
    }
}
