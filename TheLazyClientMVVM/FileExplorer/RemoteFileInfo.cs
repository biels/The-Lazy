using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.FileExplorer
{
    public class RemoteFileInfo
    {
        public string filename { get; set; }
        public long size { get; set; } 
        public DateTime modified_time { get; set; }
    }
}
