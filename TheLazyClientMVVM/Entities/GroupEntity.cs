using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class GroupEntity
    {
        public int id { get; set; }
        public string group_code { get; set; }
        public AcademicLevelEntity academic_level { get; set; }
        public EducationCenterEntity education_center { get; set; }
        public override string ToString()
        {
            return String.Format("{1} {2}, {0} ({3})", education_center.name, academic_level.name, group_code, education_center.location);
        }
    }
}
