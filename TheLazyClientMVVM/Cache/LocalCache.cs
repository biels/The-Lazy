using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Cache
{
    public class LocalCache
    {
        public UserCache user_cache { get; set; }
        public ElementCache element_cache { get; set; }
        public AcademicLevelCache academic_level_cache { get; set; }
        public EducationCenterCache education_center_cache { get; set; }
        public SubjectCache subject_cache { get; set; }

        public LocalCache()
        {
            user_cache = new UserCache();
            element_cache = new ElementCache();
            academic_level_cache = new AcademicLevelCache();
            education_center_cache = new EducationCenterCache();
            subject_cache = new SubjectCache();
        }
    }
}
