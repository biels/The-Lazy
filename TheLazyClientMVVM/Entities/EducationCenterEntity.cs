using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class EducationCenterEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public override string ToString()
        {
            return String.Format("{0}, [{1}]", name, location);
        }
    }
}
