using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class UserEntity
    {
        public int id { get; set; }
        public string username { get; set; }
        public string real_name { get; set; }
        public string email { get; set; }
        public int permission_level { get; set; }   
        public string status { get; set; }
        public GroupEntity group { get; set; }
        public int balance { get; set; }        

    }
}
