using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class ElementEntity
    {
        public int id { get; set; }
        public UserEntity user { get; set; } //Semi buit*
        public SubjectEntity subject { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
    }
}
