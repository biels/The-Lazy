using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class UserEntity : Entity
    {
        public int id { get; set; }
        public string username { get; set; }
        public string real_name { get; set; }
        public GenderEnum gender { get; set; }
        public string email { get; set; }
        public int permission_level { get; set; }   
        public string status { get; set; }
        public string group_code { get; set; }
        public AcademicLevelEntity academic_level { get; set; }
        public EducationCenterEntity education_center { get; set; }
        public int balance { get; set; }

        public enum GenderEnum { M, F }
    }
}
