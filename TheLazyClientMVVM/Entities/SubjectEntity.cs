using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class SubjectEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortcode { get; set; }
        public string description { get; set; }
        public AcademicLevelEntity academic_level { get; set; }
        public int color { get; set; }

        public override string ToString()
        {
            return String.Format("[{1}] {0}", name, shortcode);
        }
    }
}
