using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM
{
    public class ElementFilter
    {               
        public string search_text { get; set; } //Falta
        public SubjectEntity subject { get; set; }
        public List<ElementEntity> getFilteredElements()
        {

            string sql = "";
            if (subject != null) { String.Format("WHERE subject_id={0}", subject.id); }
            
            return DbClient.DbElementClient.getFilteredElementList(sql);
        }
        public List<AcademicLevelEntity> _AcademicLevels { get; set; }
        public List<SubjectEntity> _Subjects { get; set; }
        public void init()
        {
            _AcademicLevels = DbClient.DbAcademicLevelClient.getAcademicLevelList();
        }
        public void updateSubjectList(AcademicLevelEntity academic_level)
        {
            if (academic_level == null) { return; }
            _Subjects = DbClient.DbSubjectEditorClient.getSubjectList(academic_level.id);
        }
        
    }
}
