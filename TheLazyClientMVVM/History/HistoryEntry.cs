using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    class HistoryEntry
    {
        public int id { get; set; }
        public string reason {get; set;} //null / Unlock Connexió lvl 5
        public UserEntity user { get; set; } //Semi buit* [id]
        public int amount { get; set; } // > / < 0
        public ElementEntity element { get; set; } //Semi buit* [id, nom] / null
        public DateTime create_time { get; set; }
    }
}
