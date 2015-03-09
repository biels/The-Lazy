using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyInterfaces.Containers
{
    class PostInfo
    {
        public int post_id { get; set; }
        public int parent_post_id { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        //To-Fill Fields
        public List<PostInfo> child_posts { get; set; }
    }
}
