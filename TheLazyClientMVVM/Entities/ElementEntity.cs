using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class ElementEntity
    {
        //INFO PRINCIPAL
        public int id { get; set; }
        public UserEntity user { get; set; } //Semi buit*
        public SubjectEntity subject { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        //CONSULTES EXTERNES
        public int favourite_amount { get; set; }
        public int average_rating { get; set; }

        //INFO LOCAL
        public LocalElementDataEntity local_data { get; set; }

        //EXTRES
        public bool isFromLocalUser()
        {
            return id == Com.main.localUser.id;
        }
        public bool isUnlockedForLocalUser()
        {
            return false;
        }
    }
}
