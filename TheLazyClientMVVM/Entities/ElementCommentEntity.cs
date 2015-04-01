using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class ElementCommentEntity
    {
        public int id { get; set; }
        public int element_id { get; set; } //Va dins un element
        public UserEntity user { get; set; } //Semi buit*
        public string text { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }

        public bool isFromLocalUser()
        {
            return user.id == Com.main.localUser.id;
        }
        public bool hasBeenEdited()
        {
            return create_time != update_time;
        }
    }
}
