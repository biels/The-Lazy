using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class ElementEntity : Entity
    {
        //INFO PRINCIPAL
        public int id { get; set; }
        public UserEntity user { get; set; } //Semi buit*
        public SubjectEntity subject { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }

        //CONSULTES EXTERNES
        public int favourite_amount { get; set; }
        public int average_rating { get; set; }
        public int purchase_count { get; set; }

        //INFO LOCAL
        public LocalElementDataEntity local_data { get; set; }

        //EXTRES
        public bool isFromLocalUser()
        {
            return id == Com.main.localUser.id;
        }
        public bool hasBeenModified()
        {
            return create_time != update_time;
        }
    }
}
