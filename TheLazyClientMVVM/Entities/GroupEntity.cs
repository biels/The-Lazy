using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    class GroupEntity
    {
        public int id { get; set; }
        public string group_code { get; set; }
        public AcademicLevelEntity academic_level { get; set; }
        public EducationCenterEntity education_center { get; set; }
    }
}
