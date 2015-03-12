using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyInterfaces.Containers
{
    class UserInfo
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string real_name { get; set; }
        public PermissionLevel permission_level { get; set; }
        public enum PermissionLevel { Blocked, NormalUser, TrustedUser, Moderator, SuperModerator, Admin }
    }
}
